using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SimpleElevator: MonoBehaviour {
	public  	Material 			buttonOnMat, 
	buttonOffMat, 
	buttonSelectorMat;
	public  	Material			ledMat;
	public  	Transform		ledPanel;
	public  	float				ledMatSwitchDelay 		= .5f;
	private	Material[]		ledMatsArray;
	
	private 	List<Transform>		buttonLightList;
	private 	GameObject	hallFrame;
	private	List<Texture>			texturesList;
	private	AnimationClip		openAnim,
	closeAnim;
	
	public  	Transform			btnLightGroup;

	public 	bool doorOpen;
	private	Transform			elevator;

	public bool inElevator = false;
	GameObject manager;
	GameObject player;
	private MasterScript sn;
	public int isSet = 0;
	public float ReopenTimer = 10.0f;
	//public float timer = -100.0f;
	public bool canExit = false;
	//elevatorsounds
	private AudioSource source;
	public AudioClip elevatorOpen;
	public AudioClip elevatorClose;
	public AudioClip elevatorTravel;

	bool a = true;
	bool b = true;

	void Start () {
		//init sound source
		source = GetComponent<AudioSource>();
		//TRANSFORM REFERENCE
		elevator = this.transform;
		hallFrame = GameObject.Find("ElevHallFrame");
		manager = GameObject.Find ("GameMaster");
		player = GameObject.Find( "Player");
		sn = manager.GetComponent<MasterScript>(); 

		//LED PANEL TEXTURES LIST, POPULATE TEXTURES LIST, SORT THE LIST
		texturesList = new List<Texture>();
		foreach ( Texture tex in Resources.LoadAll( "LEDPanelTextures" ) ){ 
			texturesList.Add( tex );
		}

		//SET ANIMATION CLIPS
		openAnim = transform.GetComponent<Animation>().GetClip( "OpenDoors" );
		closeAnim = transform.GetComponent<Animation>().GetClip( "CloseDoors" );	
		elevator.position = hallFrame.transform.position;
	
		//Decide if the player is still in the elevator or not
		if(sn.getSpawnPoint() == 1){
			Debug.Log(Application.loadedLevelName);
			player.transform.position = gameObject.transform.position;
			player.transform.localEulerAngles = transform.localEulerAngles;
			player.transform.Translate(0,1*this.transform.localScale.y,0);
			isSet = 1;
			inElevator = true;
			manager.GetComponent<MasterScript>().setTimer(120.0f);
			manager.GetComponent<MasterScript>().dontSpawnInElev();
			//OpenDoor();
		}
		else
		{
			isSet = 1;
			//manager.GetComponent<MasterScript>().setTimer(30.0f);
			//CloseDoor ();
		}
		//If the player is on a travelling floor, then they can't exit
		//They can only look outside of the elevator
		if (sn.canExit == false) {
			canExit = false;
			hallFrame.GetComponent<Collider>().isTrigger = false;
			Debug.Log("hallframe should be false");
			manager.GetComponent<MasterScript>().setTimer(10.0f);

		}
		}
	void LEDPanel( int newFloorNum ){
		StartCoroutine( LEDPanelSwitch(  newFloorNum ) );
	}

	/// <summary>
	/// Switch LED display to this number.
	/// </summary>
	/// <param name="newFloorNum">Floor number.</param>
	IEnumerator LEDPanelSwitch( int newFloorNum ){
		//SWITCH TO BLANK LED TEXTRES
		ledMat.SetTexture( "_MainTex",		texturesList[ texturesList.Count-2 ] );
		ledMat.SetTexture( "_Illum",			texturesList[ texturesList.Count-1 ] );
		yield return new WaitForSeconds( ledMatSwitchDelay );

		//CONVERT FLOOR NUMBER TO LIST INDEXES
		int illume 	= ( newFloorNum * 2 ) +1;
		int diff		= illume - 1;
		
		//CHANGE LED MATERIAL TEXTURES
		ledMat.SetTexture( "_MainTex", texturesList[diff] );
		ledMat.SetTexture( "_Illum", texturesList[illume] );
	}

	//Opens both the elevator and the hallframe door
	void OpenDoor(){
		GameObject.Find ("elevBlocker").GetComponent<BoxCollider> ().enabled = false;
		Debug.Log ("got here");
		GetComponent<AudioSource>().PlayOneShot(elevatorOpen);
		transform.GetComponent<Animation>().clip = openAnim;
		transform.GetComponent<Animation>().Play();
		doorOpen = true;
		hallFrame.GetComponent<SimpleHallFrame>().OpenDoor();
	}

	//Closes both the elevator and hallframe door
	void CloseDoor(){
		GetComponent<AudioSource>().PlayOneShot(elevatorClose);
		transform.GetComponent<Animation>().clip = closeAnim;
		transform.GetComponent<Animation>().Play();
		doorOpen = false;
		hallFrame.GetComponent<SimpleHallFrame>().CloseDoor();
	}
	//Logic to open/close the door when the player enters.
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player") 
		{
						return;
		}
		Debug.Log (isSet);
		if (isSet == 0 && (Time.timeSinceLevelLoad > 1.0)) { 
			inElevator = true;
			CloseDoor ();
			manager.GetComponent<MasterScript>().playElevatorMusic();
		} else if(isSet == 1 && inElevator)
		{	//dont need to set inElevator because spawning does that
			OpenDoor ();
		}else
		{
			Debug.Log("test");
			manager.GetComponent<MasterScript>().playElevatorMusic();
			CloseDoor ();
			inElevator = true;
			isSet =0;
			Debug.Log(isSet);
		}
	}
	//Logic to close the door when the player exits the elevator
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag != "Player") 
		{
			return;
		}

		inElevator = false;
		CloseDoor ();
		GameObject.Find ("elevBlocker").GetComponent<BoxCollider> ().enabled = true;
		//manager.GetComponent<MasterScript>().setTimer(ReopenTimer);
		manager.GetComponent<MasterScript>().stopElevatorMusic();
	}
	void Update () {
		//if in the elevator and the timer flag goes off, then we can start to load the next floor
		if (inElevator && isSet == 0) {
			Debug.Log("dasfsd");
			manager.GetComponent<MasterScript>().goNextFloor();
			isSet = 1;
		}
		//if player exits the elevator, start a timer after the timer is done set to value it will never get normally
		// to denote that its not in use.
		//if (timer > 0) {
		//	timer -= Time.deltaTime;
		//} 
		if (manager.GetComponent<MasterScript>().getCanOpen()) {
			if(inElevator == false && a == true){
				Debug.Log("Open Door");
				OpenDoor ();
				a = false;
			}else if(inElevator == true && b == true){
				CloseDoor();
				Debug.Log("close door and move on");
				isSet = 0;
				b = false;
			}
			//timer = -100.0f;
		}
	}

	bool getInElevator(){
				return inElevator;
		}
}

