using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
//Written by Jacob Kennedy and Alex Freedman.
//Designed more for VR.
//Will explain more about the script since it's not that intuitive at a first glance.
public class NewCharMover : MonoBehaviour {
	//First, this is the stuff for looking around with the mouse, taken from the MouseLook.cs script
	//It might be worth just activating the script rather then moving it here, but we shall see.
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

	//All of this stuff below is for the general movement of the character.
	public float speed = 20.0f;
	public float rotationSpeed = 32.0f;
	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float rotateDirection = 0;
	private float turnDirection = 0.0f;
	private float scale;
	private float rotation;
	CharacterController controller;

	int currentState = 0;

	//This stuff is more for general highlighting and pickup
	float pickupDistance = 10.0f;
	float dop = 1.5f;
	bool pickedUp = false;
	bool isHighlight = false;
	GameObject obj = null;
	Shader highlightShader;
	List<Shader> shaderList = new List<Shader>();

	bool confidenceGUI = false;
	UIDisp ui;

	private Color normalColor;

	//For deciding what buttons involving objects do.
	private bool controllerConnected = false;
	private bool OculusConnected = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		controller.enabled = true;
		//Shader stuff
		highlightShader = Shader.Find ("Self-Illumin/Diffuse");

		//Probably should be improved
		controllerConnected = GameObject.Find ("GameMaster").GetComponent<MasterScript> ().getGamePad ();
		OculusConnected = GameObject.Find ("GameMaster").GetComponent<MasterScript> ().getVR ();
		receiveState (GameObject.Find("GameMaster").GetComponent<MasterScript>().getState());

		ui = GameObject.Find ("GameMaster").GetComponent<UIDisp> ();

		//If you're wondering, this is because every scene has a different innate scale associated with it.
		//A result of using pre-made scenes from different sources which did not adher to a standard scale.
		//Could be removed in the future if all scenes were homebaked with scale standards.
		//Until then, with each new scene you'll have to mess around to get the right scale.
		//Also, the office is used as the standard of what is good scale (hahahahahaha)
		if (Application.loadedLevelName == "Apartment_Scene") {
			scale = .13f;
		} else if (Application.loadedLevelName == "testingtutorial") {
			scale = .4f;
		} else {
			scale = 1.0f;
		}

		pickupDistance *= scale;

		//TODO: Add Font Information/Variables
	}
	
	// Update is called once per frame
	//There will be 3 different update types + The object pickup, drop, and adjustment code.
	// 1. With Mouse and Keyboard
	// 2. With Contorller
	// 3. With Oculus
	void Update () {
		if (currentState == 1) {
			Movement();
		}
		else if (currentState == 2) {
			if (!confidenceGUI) {
				Movement();
				highlightObjects();
				objPickDrop();
				pickedUpUpdate ();
			}
			else {
				//Code for the confidence selector.
				ConfidenceSelection();
			}
		}

	}

	void Movement() {
		if (controller.enabled = true) {
			if (controllerConnected && OculusConnected) {
				Oculus(); //Includes special controller layout for Oculus
				transform.Rotate (0, rotateDirection * rotationSpeed * Time.deltaTime, 0);
			}
			else if (controllerConnected && !OculusConnected) {
				Controller();//Should be just controller support, might add keyboard to work here too.
			}
			else {
				MouseandKeyboard();
			}
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * speed * scale * Time.deltaTime);
	}

	//This is the movement for Mouse and Keyboard.
	void MouseandKeyboard() {
		float Horizontal = Input.GetAxis ("Horizontal");
		float Vertical = Input.GetAxis ("Vertical");
		
		moveDirection = new Vector3(Horizontal, 0, Vertical);
		moveDirection = transform.TransformDirection (moveDirection);

		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 5;
			
			rotationY += Input.GetAxis("Mouse Y") * 5;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(0, rotationX, 0);
			this.GetComponentInChildren<Camera>().transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			this.GetComponentInChildren<Camera>().transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}

		//Fine control for picked up objects.
		if (pickedUp) {
			float sign = 0.0f;
			if ((sign = Input.GetAxis("Mouse ScrollWheel")) != 0) {
				if (sign > 0.05f && dop < 6.0f * scale) {
					dop += .02f;
				}
				else if (sign < -0.05f && dop > 3.0f * scale) {
					dop -= .02f;
				}
			}
		}
	}

	//This is for movement with controller.
	void Controller() {
		float Horizontal = 0f;
		float Vertical = 0f;
		Horizontal = Input.GetAxis ("LeftStickHorizontal");
		Vertical = Input.GetAxis ("LeftStickVertical");

		moveDirection = new Vector3(Horizontal, 0, -Vertical);
		moveDirection = transform.TransformDirection (moveDirection);

		float rotationX = 0.0f;
		rotationX = transform.localEulerAngles.y + Input.GetAxis("RightStickHorizontal") * sensitivityX;
		
		rotationY -= Input.GetAxis("RightStickVertical") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(0, rotationX, 0);
		this.GetComponentInChildren<Camera>().transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

		//Fine control for picked up objects.
		if (pickedUp) {
			if (Input.GetButton("RightBumper") && dop < 6.0f * scale) {
				dop += .02f;
			}
			else if (Input.GetButton("LeftBumper") && dop > 3.0f * scale) {
				dop -= .02f;
			}
		}
	}

	//This is for Oculus Connected, it uses simpler controlls for keyboard/gamepad.
	void Oculus() {
		if (controller.isGrounded) {
			float Horizontal = Input.GetAxis ("Horizontal");
			float Vertical = Input.GetAxis ("Vertical");
			moveDirection = new Vector3(0, 0, Vertical);
			moveDirection = transform.TransformDirection (moveDirection);
			rotateDirection = Horizontal;
			
			//float rotation = Vector3.Angle (Camera.main.transform.forward, controller.transform.forward);
			//if (rotation > 1f) {
			
			//OK, so the point of thee following code:
			/*We want the viewer to move forward in the direction of their 'body' most of the time.
				 To better mimic the human body the way this works is that if you turn your head a certain angle
				 or fully turn around then your body will face that new direction.
				 This means you can look roughly in a 90-degree cone in front of you without changing your body.

				 However, for people reading this that want to work with VR: You cannot just modify the camera
				 yourself. Unity 'owns' the camera so you can't change the local rotation like you can
				 with the in-scene camera, hence the recenter option. */
			rotation = Camera.main.transform.eulerAngles.y;
			if (Mathf.Abs((rotation - this.transform.eulerAngles.y)) > 60.0f) {
				//Debug.Log (Camera.main.transform.eulerAngles.y);
				transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, rotation, this.transform.rotation.eulerAngles.z);
				//Camera.main.transform.rotation = new Quaternion(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);
				if(VRDevice.isPresent)
					InputTracking.Recenter();
				else
					Camera.main.transform.localRotation = Quaternion.identity;
				
			}
		}

		//Fine control for picked up objects.
		float sign = 0.0f;
		if( (sign = Input.GetAxis("RightStickVertical")) != 0) {
			sign = -sign;
			if (sign > 0.05f && dop < 6.0f * scale) {
				dop += .02f;
			}
			else if (sign < -0.05f && dop > 3.0f * scale) {
				dop -= .02f;
			}
		}
	}



	void highlightObjects()
	{
		RaycastHit hit1;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		if (Physics.Raycast (ray, out hit1, pickupDistance)) {
			if ((hit1.collider.gameObject.tag == "target" || hit1.collider.gameObject.tag == "nontarget") && pickedUp == false) {
				if (obj == null) {
					obj = hit1.collider.gameObject;

					foreach (Material mat in obj.GetComponent<Renderer>().materials) {
						shaderList.Add (mat.shader);
					}

					foreach (Material mat in obj.GetComponent<Renderer>().materials) {
						mat.shader = highlightShader;
					}

					isHighlight = true;
				}
			} else if (obj != null && isHighlight == true && pickedUp == false) {
				foreach (Material mat in obj.GetComponent<Renderer>().materials) {
					mat.shader = shaderList [0];
					shaderList.RemoveAt (0);
				}
				isHighlight = false;
				obj = null;
			}
		} else if (obj != null && isHighlight == true && pickedUp == false) {
			foreach (Material mat in obj.GetComponent<Renderer>().materials) {
				mat.shader = shaderList [0];
				shaderList.RemoveAt (0);
			}
			isHighlight = false;
			obj = null;
		}

	}

	void objPickDrop()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")) {
			if (obj != null && pickedUp == false && (obj.tag == "target" || obj.tag == "nontarget")) {
				pickedUp = true;

				foreach (Collider col in obj.GetComponents<Collider>())
				{
					col.enabled = false;
				}
				foreach (Material mat in obj.GetComponent<Renderer>().materials) {
					mat.shader = shaderList [0];
					shaderList.RemoveAt (0);
				}
				Rigidbody body = obj.GetComponent<Rigidbody>();
				body.useGravity = false;
				body.constraints = RigidbodyConstraints.FreezeAll;
				body.constraints &= ~RigidbodyConstraints.FreezePositionY;
				//can't access Boo script here for rotation
			}
			else if (obj != null && pickedUp == true)
			{
				pickedUp = false;
				foreach (Collider col in obj.GetComponents<Collider>())
				{
					col.enabled = true;
				}
				Rigidbody body = obj.GetComponent<Rigidbody>();
				body.useGravity = true;
				body.velocity = new Vector3(0f, -.01f,0f);

				if (obj.gameObject.tag == "target") {
					confidenceGUI = true;
					ui.showConf();
				}

				//can't access writecoordinates here due to boo script
				obj = null;
				//Reset distance of object (dop)
				dop = 1.5f;
			}
		}
	}

	void pickedUpUpdate()
	{
		if (obj != null && pickedUp == true)
		{
			obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, (Camera.main.nearClipPlane+dop)));
		}
	}

	void receiveState(int state)
	{
		currentState = state;
	}

	/* This is the simple version right now, select a number on the keyboard to choose your confidence.
	 * A system with on screen boxes you can look at to choose your confidence (for the Oculus Rift)
	 * should be added next. This system should use the already in place GUI system, just like here. */
	void ConfidenceSelection()
	{
		if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Fire2")) {
			confidenceGUI = false;
		}
		else {
			for (int i = 1; i <= 8; i ++) {
				if(Input.GetKeyDown("" + i)) {
					float time = (float)GameObject.Find("GameMaster").GetComponent<MasterScript>().getTimer();
					obj.GetComponent<WriteCoordinates>().saveCSV(i,time);
					confidenceGUI = false;
					obj.gameObject.tag = "Untagged";
					obj = null;
				}
			}
		}
	}
}
