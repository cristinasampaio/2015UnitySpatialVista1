using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.VR;

public class MasterScript : MonoBehaviour {
	/*If you're wondering why this is in plugins
	 It's because this script needs to run first above all others. That's mainly why.
	 Also as a note this is another important script, but thankfully was more recently written.
	 -Jacob*/
	public bool test = true;
	//All public variables
	public bool isSet = false;
	public bool spawnInElevator = false;
	public bool isTutorial = true;
	public bool canExit = true;
	public bool canOpen = false;
	public double timer;
	public double delayTimer = 0.0;
	public double levelTimer = 0.0;
	public double doorTimeDelay = 120.0;
	public int state = 2;

	//The private variables
	private double globalTime = 0.0;
	private string lastFloor = null;
	private bool fade = false;

	//testFloors are the scenes in which peoples spatial memory will be tested.
	List<string> testFloors = new List<string>();
	private int elevFloorsRem;
	private int elevFloorsMax;
	//Travel floors are the floors between test floors, they are there to distract the player and little (or nothing) else.
	List<string> travelFloors = new List<string>();
	private int testFloorsRem;

	//For the last 2 functions in this script.
	private bool controllerConected = false;
	private bool OculusConnected = false;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 30;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		GameObject.Find("Player").SendMessage("receiveState", state);
		string[] names = Input.GetJoystickNames ();
		if (names.Length > 0) {
			controllerConected = true;
		}
		if (VRDevice.isPresent) {
			OculusConnected = true;
		}

		//First add the testFloors
		testFloors.Add ("Office_Scene");
		testFloors.Add ("Apartment_Scene");
		//testFloors.Add ("");
		testFloorsRem = testFloors.Count;

		//Now add the travelFloors
		travelFloors.Add ("elescene1");
		travelFloors.Add ("elescene2");
		travelFloors.Add ("elescene3");
		//travelFloors.Add ("");
		elevFloorsRem = travelFloors.Count;
		elevFloorsMax = travelFloors.Count;
	}
	
	// Update is called once per frame
	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		if (GameObject.FindObjectsOfType (GetType ()).Length > 1)
			Destroy (this.gameObject);
	}

	void Update () {
		levelTimer += Time.deltaTime;

		if (!canOpen && levelTimer >= doorTimeDelay) {
			canOpen = true;
			//NOTE: REALLY BAD AND HACKY WORKAROUND
			this.SendMessage("unpauseStory");
		}

		if (delayTimer > 0.0 && isSet) {
			delayTimer -= Time.deltaTime;
		} 

		if (delayTimer <= 1.0 && isSet && !fade)
		{
			GameObject.Find("Fader").GetComponent<FadeInOut>().FadeOut();
			fade = true;
		}

		else if (delayTimer <= 0.0 && isSet) {
			getNextFloor();
		}
	}
	//Logic to decide if we're going to the next test or the next elevator floor.
	public void getNextFloor() {
		if (elevFloorsRem <= 0) {
			if(testFloorsRem == 0)
				end();
			else
			{
				canExit = true;
				loadNextTest();
			}
			elevFloorsRem = elevFloorsMax;
		}
		else 
		{
			canExit = false;
			loadNextElevator();
		}
		isSet = false;
		canOpen = false;
		fade = false;
	}

	public void goNextFloor() {
		delayTimer = 5.0;
		isSet = true;
		spawnInElevator = true;
	}
	public void playElevatorMusic() {
		GetComponent<AudioSource>().Play();
	}
	public void stopElevatorMusic() {
		GetComponent<AudioSource> ().Stop ();
	}

	//Returns the current user state
	public int getState() {
		return state;
	}
	//Returns the spawn point, used by the elevator to figure out if the player is still travelling inside
	public bool getSpawnPoint() {
		return spawnInElevator;
	}

	/*//Character Position and Rotation Maybe???
	def getPos (string name) {
		return poskeep.ReturnPos (name);
	}
	def getRot (string name) {
		return poskeep.ReturnRot (name);
	}*/

	public void dontSpawnInElev() {
		spawnInElevator = false;
	}

	public void lockElevFoor() {
		canOpen = false;
	}

	public double getTimer() {
		return levelTimer;
	}

	public void setTimer(double t) {
		//timer = t;
	}

	public bool getCanOpen() {
		return canOpen;
	}

	void writeInfo(string name, double time) {
		string filepath = "Data/data.csv";
		try {
			using( StreamWriter myfile = new StreamWriter(filepath) ){
				myfile.WriteLine(name,state,time,"\n");
			}
		}
		catch(Exception e) {
			Debug.Log("There was an error in writing this object to data.scv" + e.ToString());
		}
	}

	void end() {
		string filepath = "Data/data.csv";
		try {
			using( StreamWriter myfile = new StreamWriter(filepath, true) ){
				myfile.WriteLine("TotalTime,$globalTime");
			}
		}
		catch(Exception e) {
			Debug.Log("There was an error in writing this object to data.scv" + e.ToString());
		}
			
		Application.Quit ();
	}
	//Test floors meaning places where players pick up and drop objects.
	public void loadNextTest()
	{
		if(state == 1) {
			state = 2;
			lastFloor = testFloors[0] as string;
			Application.LoadLevel(lastFloor);
			//Writes data about floor here.
			writeInfo(lastFloor, levelTimer);
			//Since we are moving into the final stage, we don't need the scene anymore.
			testFloors.RemoveAt(0);
			testFloorsRem--;
		}
		else {
			if (isTutorial)
			{
				GameObject.Find("Fader").GetComponent<FadeInOut>().FadeIn();
				isTutorial = false;
			}
			state = 1;
			Application.LoadLevel(testFloors[0] as string);
			writeInfo(lastFloor, levelTimer);
		}

		GameObject hallFrame = GameObject.Find("ElevHallFrame");
		hallFrame.GetComponent<Collider>().isTrigger = false;
		globalTime += levelTimer;
		levelTimer = 0.0;
		doorTimeDelay = 120.0;
	}
	//Elevator floors meaning the kinda-jokey floors where you just hang out. In an elevator.
	public void loadNextElevator()
	{
		//Debug.Log("Going to next floor " + state);

		Application.LoadLevel(travelFloors[elevFloorsMax-elevFloorsRem] as string);
		elevFloorsRem--;
		
		GameObject.Find("Fader").GetComponent<FadeInOut>().FadeIn();
		
		GameObject hallFrame = GameObject.Find ("ElevHallFrame");
		hallFrame.GetComponent<Collider>().isTrigger = false;
		
		globalTime += levelTimer;
		levelTimer = 0.0;
		delayTimer = 10.0;
		doorTimeDelay = 10.0;
	}

	public bool getVR() {
		return OculusConnected;
	}

	public bool getGamePad() {
		return controllerConected;
	}

	public void OnLevelWasLoaded(int level)
	{
		GameObject.Find("Player").SendMessage("receiveState", state);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}