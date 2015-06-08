import UnityEngine

public class MasterScript (MonoBehaviour): 
	Scene = ""
	flag = 0
	public isSet = 0
	public SpawnInElevator = 0
	private isTutorial = true
	public DelayTimer = 0.0	//DelayTimer is the amount of time spent after the doors close until next load
	public canExit = true
	public timer as double
	public canOpen = false
	public state = 2	
	testFloors as List = ["Office_Scene", 
						"Apartment01_2"]
	#Distinction: testFloors is the scenes used for testing.
	#Travelfloors are the scenes that the elevator travels through to reach a testing scene.
	TravelFloors = [	
				'elescene1',
				'elescene2',
				'elescene3',
				##'SceneA'
				]
	floorsRemaining = len(TravelFloors)
	tFloors = len(TravelFloors)
	midpos = {"Test" : "DSFDS"}
	
	public levelTimer = 0.0
	private globalTime = 0.0
	private lastFloor = null
	//public visited = true
	test = 0
	
	poskeep = keeper()
	
	def Awake ():
		GameObject.DontDestroyOnLoad(self)
		if (GameObject.FindObjectsOfType(GetType()).Length > 1):
			Destroy(self)		
		timer = 120
		Screen.showCursor = false;
			
	def Update():
		//timer -= Time.deltaTime
		
		levelTimer += Time.deltaTime
		
		if canOpen == false and (levelTimer >= timer):
			canOpen = true
		
		if (DelayTimer >= 0.0 and isSet == 1):
			//Debug.Log(DelayTimer + " start" + Time.time)
			DelayTimer -= Time.deltaTime
			//Debug.Log(DelayTimer+ " " + Time.time)
			
		elif (DelayTimer <= 0.0 and isSet == 1):
			Debug.Log("going to next floor")
			getNextFloor()
	
	//Goes to the next floor and changes state appropriately, if state == 1, state = 2 and  vice versa
	//We only remove a testing floor if scene = 2, implying we have collected data for that floor.	
	def getNextFloor():
		if floorsRemaining <= 0:
			if len(testFloors) == 0:
				end()
			canExit = true
			Debug.Log("going to next testing floor " +  state)
			if state == 1:
				state = 2
				Application.LoadLevel(testFloors[0] as string)
				//Now Writes info about floor you were on.
				lastLevel = testFloors[0] as string
				writeInfo(lastLevel,levelTimer)
				testFloors.RemoveAt(0)
			else:
				if isTutorial:
					isTutorial = false
				Application.LoadLevel(testFloors[0] as string)
				writeInfo(lastLevel, levelTimer)
				state = 1
			##has to be +1 because of the floorremaining logic at the bottom	
			floorsRemaining = len(TravelFloors)+1
			hallFrame = GameObject.Find("ElevHallFrame")	
			hallFrame.collider.isTrigger = false;
			globalTime += levelTimer
			levelTimer = 0
			Debug.Log(timer)
			timer = 120.0
			Debug.Log(timer)
			
		else:
			Debug.Log("going to next floor " + state)
			
			canExit = false			
			Application.LoadLevel(TravelFloors[tFloors-floorsRemaining] as string)
			hallFrame = GameObject.Find("ElevHallFrame")
			hallFrame.collider.isTrigger = false;
			globalTime += levelTimer
			levelTimer = 0
			DelayTimer = 10.0
			
		floorsRemaining--	
		isSet = 0
		canOpen = false
	
	//Called by the Elevator to go to the next floor. This sets some appropriate variables
	//then starts the timer
	def goNextFloor():
		DelayTimer =  5.0
		isSet = 1
		SpawnInElevator = 1
	def playElevatorMusic():
		Debug.Log("~~playing~~")
		audio.Play()
	def stopElevatorMusic():
		Debug.Log("~~stoping~~")
		audio.Stop()
	
	//Returns the user state; might be useful in the future		
	def getState():
		return state
	//Returns the spawn point, used by the elevator to figure out if the player is still travelling inside
	def getSpawnPoint():
		return SpawnInElevator
	//??
	def getPos(name as string):
		return poskeep.ReturnPos(name)

	def getRot(name as string):
		return keeper.ReturnRot(name)
	
	//Call this on multiroom floors
	def dontSpawnInElev():
		SpawnInElevator = 0;
	
	def lockElevDoor():
		canOpen = false
		
	def getTimer():
		return levelTimer
		
	def setTimer(t as double):
		Debug.Log(t)
		timer = t
		
	def getCanOpen():
		return canOpen
	
	/*//Takes a boolean value 
	def setVisited(setVal):
		visited = setVal*/
					
	
	//Called when the game is about to end. Depreciated code.
	//Will need to call 'end' before we load each new testing floor.
	def end():
		/* This will eventually be code to save item positions that the player forgot about.
		items = GameObject.FindGameObjectsWithTag("pickup")
		for item in items:
			try:
				item.GetComponent[of WriteCoordinates]().saveCSV()
			except e:
				pass*/
		filepath = "Data/data.csv"
		try:
			using myfile = System.IO.StreamWriter(filepath, true):
				myfile.WriteLine("TotalTime,$globalTime")
		except e:
			Debug.Log("There was an error in writing this object to data.scv")
			
		Application.Quit()
			
	def writeInfo(name, time):
		filepath = "Data/data.csv"
		try:
			using myfile = System.IO.StreamWriter(filepath, true):
				myfile.WriteLine(name,state,time,"\n")
		except e:
			Debug.Log("There was an error in writing this object to data.scv")
