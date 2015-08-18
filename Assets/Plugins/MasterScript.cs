using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MasterScript : MonoBehaviour {

	string scene = "";
	bool flag = false;
	int test = 0;

	//All public variables
	public bool isSet = false;
	public bool spawnInElevator = false;
	public bool isTutorial = true;
	public double delayTimer = 0.0;
	public bool canExit = true;
	public double timer;
	public bool canOpen = false;
	public int state = 2;
	public double levelTimer = 0.0;
	public double doorTimeDelay = 120.0;

	//The private variables
	private double globalTime = 0.0;
	private string lastFloor = null;

	//testFloors are the scenes in which peoples spatial memory will be tested.
	List<string> testFloors = new List<string>();
	int floorsRemaining;
	//Travel floors are the floors between test floors, they are there to distract the player and little (or nothing) else.
	List<string> travelFloors = new List<string>();
	int tFloors;


	// Use this for initialization
	void Start () {
		//First add the testFloors
		testFloors.Add ("Office_Scene");
		testFloors.Add ("Apartment01_2");
		//testFloors.Add ("");
		floorsRemaining = testFloors.Count;

		//Now add the travelFloors
		travelFloors.Add ("elescene1");
		travelFloors.Add ("elescene2");
		travelFloors.Add ("elescene3");
		//travelFloors.Add ("");
		tFloors = travelFloors.Count;
	}
	
	// Update is called once per frame
	void Update () {
		levelTimer += Time.deltaTime;

		if (!canOpen && levelTimer >= doorTimeDelay) {
			canOpen = true;
		}

		if (delayTimer > 0.0 && isSet) {
			delayTimer -= Time.deltaTime;
		} else if (delayTimer <= 0.0 && isSet) {
			getNextFloor();
		}
	}

	public void getNextFloor() {
		if (tFloors <= 0) {
			if(floorsRemaining == 0)
				end();
			tFloors = travelFloors.Count;
			canExit = true;
			if(state == 1) {
				state = 2;
				lastFloor = testFloors[0] as string;
				Application.LoadLevel(lastFloor);
				//Writes data about floor here.
				writeInfo(lastFloor, levelTimer);
				//Since we are moving into the final stage, we don't need the scene anymore.
				testFloors.RemoveAt(0);
				floorsRemaining--;
			}
			else {
				if (isTutorial)
					GameObject.Find("Fader").GetComponent<FadeInOut>().FadeIn();
					isTutorial = false;
				Application.LoadLevel(testFloors[0] as string);
				writeInfo(lastFloor, levelTimer);
				state = 1;
			}
			GameObject hallFrame = GameObject.Find("ElevHallFrame");
			hallFrame.GetComponent<Collider>().isTrigger = false;
			globalTime += levelTimer;
			levelTimer = 0.0;
		} else {
			//Debug.Log("Going to next floor " + state);
			
			GameObject.Find("Fader").GetComponent<FadeInOut>().FadeOut();
			
			canExit = false;
			Application.LoadLevel(travelFloors[tFloors-floorsRemaining] as string);
			tFloors --;
			
			GameObject.Find("Fader").GetComponent<FadeInOut>().FadeIn();
			
			GameObject hallFrame = GameObject.Find ("ElevHallFrame");
			hallFrame.GetComponent<Collider>().isTrigger = false;
			
			globalTime += levelTimer;
			levelTimer = 0.0;
			delayTimer = 10.0;
		}
		isSet = false;
		canOpen = false;
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
		GetComponent<AudioSource>().Play ();
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
}