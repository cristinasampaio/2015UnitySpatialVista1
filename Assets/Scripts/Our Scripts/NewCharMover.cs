using UnityEngine;
using System.Collections;
using UnityEngine.VR;
//Written by Jacob Kennedy and Alex Freedman.
//Designed more for VR.
//Will explain more about the script since it's not that intuitive at a first glance.
public class NewCharMover : MonoBehaviour {
	public float speed = 20.0f;
	public float rotationSpeed = 32.0f;
	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float rotateDirection = 0;
	private float turnDirection = 0.0f;
	private float scale;
	private float rotation;
	CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		controller.enabled = true;

		//If you're wondering, this is because every scene has a different innate scale associated with it.
		//A result of using pre-made scenes from different sources which did not adher to a standard scale.
		//Could be removed in the future if all scenes were homebaked with scale standards.
		//Until then, with each new scene you'll have to mess around to get the right scale.
		if (Application.loadedLevelName == "Apartment01_2") {
			scale = .13f;
		} else if (Application.loadedLevelName == "testingtutorial") {
			scale = .4f;
		} else {
			scale = 1.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.enabled = true) {
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
					Debug.Log (Camera.main.transform.eulerAngles.y);
					transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, rotation, this.transform.rotation.eulerAngles.z);
					//Camera.main.transform.rotation = new Quaternion(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);
					if(VRDevice.isPresent)
						InputTracking.Recenter();
					else
						Camera.main.transform.localRotation = Quaternion.identity;

				}
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		transform.Rotate (0, rotateDirection * rotationSpeed * Time.deltaTime, 0);
		controller.Move (moveDirection * speed * scale * Time.deltaTime);


	
	}
}
