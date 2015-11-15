using UnityEngine;
using System.Collections;

/**
 * 
 *  Authors: Bryce Danz, Sam Dixon
 *  Tutorial script developed for 2014/15 spatial cognition research project
 *  Date: 4/11/15
 *  Attach this script to the book object in the tutorial level or shit will break.
 * 
 */


//Deprecated code. Not used anymore after development of a new tutorial.
//Also kinda rude comments above.
//Left just for the sake of history.
public class MessagePlayer : MonoBehaviour {

	//tracks players progress through tutorial
	public static string playerState;
	TextMesh text;
	public GUISkin skin;
	public static string guiDisplayText;
	public static bool showGUI = true;
	GUIStyle myStyle;// = GUIStyle();
	public Texture displaytext;
	Vector3 pos;

	//highlighting shaders
	Shader highlightShader;
	Shader normalShader;
	//Shader tempShader;
	
	//vending machine object
	public static int coCounter = 0;
	public GameObject textbook;
	public GameObject board;
	public GameObject lostTable;
	public GameObject scrollTable;
	GameObject canspot;
	Ray ray;
	RaycastHit hit;

	/* init values for the looking at whiteboard test */
	Vector3 lookVector;
	float lookDistance = 4.0f;
	float objInitPos;

	//Manager for the elevator
	GameObject manager;

	/* Cooldown timer - should prevent player from rapidly clicking through dialog. */
	public static bool cooldownElapsed = true;
	static bool firstTimeInState = true;
	static float timer;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		pos = transform.position;
		manager = GameObject.Find ("GameMaster");
		manager.GetComponent<MasterScript> ().setTimer (120.0f);
		playerState = "welcome";
		guiDisplayText = "Welcome to the experiment! Press spacebar to continue.";
		showGUI = true;
		myStyle =  new GUIStyle();
		myStyle.fontSize = 20;
		myStyle.wordWrap = true;
		canspot = GameObject.Find ("canspot");

		/* highlight vending machine */
		highlightShader = Shader.Find("Self-Illumin");
		normalShader = Shader.Find("Diffuse");
		//tempShader = Shader.Find("Diffuse");
		lookVector = new Vector3(Screen.width/2, Screen.height/2, 0);
		board = GameObject.Find("Teachers_Board");
		textbook = GameObject.Find("Book");
		objInitPos = gameObject.transform.position.x;
		lostTable = GameObject.Find ("lost_table");
		scrollTable = GameObject.Find ("Table_freeze_target");
		StartCoroutine ("cooldownCountdown");
	}

	/* handles the state advancing blocking timer */
	IEnumerator cooldownCountdown() {
		cooldownElapsed = false;

		for (timer = 1.5f; timer >= 0; timer -= Time.deltaTime) {
			yield return 0;
		}

		Debug.Log ("Timer done.");
		cooldownElapsed = true;
	}

	void OnGUI() {
		/*GUI.skin.box.wordWrap = true;
		GUI.skin.box.stretchWidth = true;
		GUI.skin.box.stretchHeight = true;
		GUI.skin.box.fontSize = 25;*/

		if (showGUI) {
			GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height-175, 500, 150), displaytext);
			//GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 100, 500, 200),guiDisplayText , myStyle);
			GUI.Box(new Rect(Screen.width / 2 - 225, Screen.height-165, 450, 140), guiDisplayText, myStyle);
		}
	}
	
	void OnCollisionEnter(Collision col){

		/* handle the case where the player should put the can on the lost and found table */
		Debug.Log (col.gameObject.name);
		if ((col.gameObject == canspot) && (playerState == "pickup_task_drop")) {
			showGUI = true;
			canspot.GetComponent<MeshRenderer>().enabled = false;
			playerState = "task_end";
			StartCoroutine ("cooldownCountdown");
			guiDisplayText = "Thank you! Feel free to play with picking up and dropping objects in the room. When you're ready to begin, please leave through the elevator.";
		}

		if ((col.gameObject.name == "Floor") && (playerState == "pickup_task_drop")) {
			Debug.Log (col.gameObject.name);
			showGUI = true;
			StartCoroutine ("cooldownCountdown");
			guiDisplayText = "Oops! You missed the table. Please try again. Press space to continue.";
		}

		/* handle the case where the player should mousewheel the can to the table while immobile */
		if ((col.gameObject.name == "Table_freeze_target") && (playerState == "pickup_task_mousewheel")) {
			playerState = "pickup_task_drop";
			StartCoroutine ("cooldownCountdown");
		}

	}

	// Update is called once per frame
	void Update () {

		/* detect keypress of c to hide or show user text unless it has been 3 seconds since they were shown instruction */
		if(Input.GetKeyDown(KeyCode.Space)) {
			if (cooldownElapsed == true) {
				showGUI = !showGUI;
				Debug.Log ("Cooldown elapsed.");
			}

			else {
				Debug.Log ("Cooldown not elapsed.");
				Debug.Log (cooldownElapsed);
			}
		}
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			transform.position = pos;
		}

		switch (playerState) {
		case "welcome":
			
			if(Input.GetKeyDown(KeyCode.Space) && cooldownElapsed){
				playerState = "inform_help";
				showGUI = true;
				StartCoroutine ("cooldownCountdown");
				}
		break;

		case "inform_help":
			guiDisplayText = "You can press the spacebar at any time to review your current objective.";

			if(Input.GetKeyDown(KeyCode.Space) && cooldownElapsed){
				playerState = "inform_look";
				showGUI = true;
				StartCoroutine ("cooldownCountdown");
			}
		break;
					
		case "inform_look":
			guiDisplayText = "Press either W, A, S, D or the arrow keys to move; Use the mouse to look around. Try this, and press the spacebar to continue.";
			if(Input.GetKeyDown(KeyCode.Space) && cooldownElapsed){
				playerState = "look_vending";
				showGUI = true;
				StartCoroutine ("cooldownCountdown");
			}
		break;
		
		/* tutorial objective 1: force player to use WASD and looking controls */
		case "look_vending":
			guiDisplayText = "Your first objective is to examine the glowing whiteboard. Press the spacebar to continue.";
			board.gameObject.GetComponent<Renderer>().material.shader = highlightShader;

			/* detect whiteboard */
			ray = Camera.main.ScreenPointToRay(lookVector);
			if (Physics.Raycast(ray, out hit, lookDistance)) {

				/* if it's the proper whiteboard advance state */
				if (hit.collider.name == "Teachers_Board") {
					board.gameObject.GetComponent<Renderer>().material.shader = normalShader;
					showGUI = true;
					playerState = "pickup_task_find";
					StartCoroutine ("cooldownCountdown");
				}
			}
		break;
	
		case "pickup_task_find":
			board.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			guiDisplayText = "Nice job! One of the students has left their " + gameObject.name+ " near their computer. Please find it. Press the spacebar to continue.";

			/* detect textbook */
			ray = Camera.main.ScreenPointToRay(lookVector);
			if (Physics.Raycast(ray, out hit, lookDistance)) {
				
				/* if it's the proper whiteboard advance state */
				if (hit.collider.gameObject == gameObject) {
					board.gameObject.GetComponent<Renderer>().material.shader = normalShader;
					showGUI = true;
					playerState = "pickup_task_retrieve";
					StartCoroutine ("cooldownCountdown");
				}
			}
		break;
		
		case "pickup_task_retrieve":
			guiDisplayText = "Please pick up the " +gameObject.name + " by moving the red dot in the center of the screen over the "+gameObject.name + " and left clicking on it. Glowing objects can be picked up. Press spacebar to continue.";

			if(gameObject.transform.position.x != objInitPos) {
				showGUI = true;
				playerState = "pickup_task_mousewheel";
				firstTimeInState = true;
				StopAllCoroutines();
				StartCoroutine ("cooldownCountdown");
			}
		break;

		case "pickup_task_mousewheel":
			guiDisplayText = "First, let's try using the mouse-wheel to place objects. Scroll the mouse-wheel to move objects closer or farther away. Without moving, please place the can on the glowing table behind you. Press spacebar to continue.";
			scrollTable.gameObject.GetComponent<Renderer>().material.shader = highlightShader;	

		break;
		case "pickup_task_drop":
			scrollTable.gameObject.GetComponent<Renderer>().material.shader = normalShader;	
			if (firstTimeInState) {
				showGUI = true;
				guiDisplayText = "Nice work! There is a lost-and-found table near the door. Please go return the " + gameObject.name+ " to the lost and found table by positioning the "+gameObject.name+" over the purple circle and left clicking to drop it. Press spacebar to continue.";
			}

			//lostTable.gameObject.renderer.material.shader = highlightShader;
			canspot.GetComponent<MeshRenderer>().enabled = true;
			/* detect collision between book and table in function */
			if (firstTimeInState) {
				firstTimeInState = false;
			}
		break;

		case "task_end":
			guiDisplayText = "Thank you! Feel free to play with picking up and dropping objects in the room. When you're ready to begin, please leave through the elevator. Press spacebar to continue.";
			lostTable.gameObject.GetComponent<Renderer>().material.shader = normalShader;	
			playerState = "final";
			manager.GetComponent<MasterScript> ().setTimer (5.0f);
		break;	

		}
	}
}
