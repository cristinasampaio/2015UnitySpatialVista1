import UnityEngine

class officemaster (MonoBehaviour): 
	
	public Cam as Camera
	
	private lastroom = ""
	storyState as int
	toldstory = false
	#storytext as string
	/*office = false
	reception = false
	meeting = false*/
	finalreturn = false
	display = false
	inElevator = false
	public displaytext as Texture
	//public mystyle = GUIStyle()
	//public boxStyle = GUIStyle()
	gamemaster as GameObject
	public a0 as AudioClip 
	public a1 as AudioClip 
	public a2 as AudioClip 
	public a3 as AudioClip 
	public a4 as AudioClip 
	public a5 as AudioClip 
	public a6 as AudioClip 
	public a7 as AudioClip 
	public audioPlayed = 0
	public audioLog =[]
	public color as Color
	textureColor as Color
	public endPhase1 = 0;
	
	private wallTxt as GameObject;
	private Txt as TextMesh;
	
	storytext = ["You are an intern in a company called Logale & Jabran and today is your first morning at your job.",
	"Your phone is ringing",
	"Welcome! Thank you so much for coming in on such short notice. Please take a tour of the office before all employees arrive: there are four separate rooms for you to explore. Please look around everywhere and examine all small objects.",
	"Our former intern was dismissed yesterday, and he usually cleaned up the desks and tables at the end of the day, so please don't mind if the office is not completely in order. You will be covering some of his former tasks so please make sure to look EVERYWHERE: behind, above, and under desks, counters, couches, chairs, etc. Pay very close attention to where every single object is.",	
	  "Now that you are finished with the office tour, I think you should also visit the office above us, as we collaborate with them in a number of projects. Please go to the elevator to head up there.",
	  "We need you back in the office immediately. Please hurry back.",
	  "Wow, what a crazy morning. Our last intern came into the office while you were gone. He didn’t handle his dismissal very well, and now I’m at the police station reporting the incident. Please, before everyone gets in, go around to all of the rooms and put everything back in place. There will be a very important meeting this morning.",
	  "Please set up everything exactly the same as it was. Your boss is very particular about the way the office is organized."]
	def Awake ():
		
		storyState = 0		
		audio as AudioSource
		audio = gameObject.GetComponent[of AudioSource]()
		audio.clip = a0
		if(Application.loadedLevelName == "Apartment01_2"):
			Destroy(self)
		GameObject.DontDestroyOnLoad(self)
		if (GameObject.FindObjectsOfType(GetType()).Length > 1):
			#Debug.Log("destroy")
			#Destroy(audio)
			DestroyImmediate(self)	
			return	
		TellStory()
		audioLog = [a0,a1,a2,a3,a4,a5,a6,a7]
		if(audio.isPlaying == false):
			updateStory()
		gamemaster = GameObject.Find("GameMaster")
		
		//txtBG.GetComponent(MeshFilter).Mesh = Cubemap;
		wallTxt = GameObject("TextField")
		wallTxt.AddComponent(TextMesh) 
		//wallTxt.AddComponent(MeshRenderer); 
		//meshRender = wallTxt.GetComponent(MeshRenderer); 
		//material = meshRender.material; 
		//meshRender.material = Resources.Load("Arial", Material); 
		//myFont = Resources.Load("Arial",Font);
		bounds = wallTxt.GetComponent[of Renderer]().bounds;
		
		Txt = wallTxt.GetComponent(TextMesh)
		Txt.anchor = TextAnchor.MiddleCenter
		Txt.fontSize = 18
		Txt.color = Color.black
		
		wallTxt.transform.position = Cam.transform.position + Cam.transform.rotation * Vector3(0.0f, -10.0f, 40.0f)
		wallTxt.transform.LookAt(Cam.transform.position)
		wallTxt.transform.Rotate(0.0f, 180.0f, 0.0f)
					
	def Update():
		if Input.GetKeyDown("space"):
			if display == true:
				display = false
				toldstory = true
				storyState += 1
				audioPlayed = 0
				updateStory()
		if (gamemaster.GetComponent[of MasterScript]().getTimer() >= 120 and endPhase1 == 0):
			display = true
			toldstory = true
			endPhase1 = 1
		
		Txt.text = storytext[storyState].ToString() + "\n" + "<color=#ffffffff>Press SpaceBar to Continue</color>";
		
	def updateStory():
		if(storyState < 4):
			toldstory = false
		elif(storyState == 4 and endPhase1 == 1):
			toldstory = false
		elif(GameObject.Find("GameMaster").GetComponent[of MasterScript]().getState() == 2 and storyState < 8):
			toldstory = false
		GameObject.Find("Player").GetComponent[of CharacterController]().enabled = true
		if(toldstory is false):
			
			if(GetComponent[of AudioSource]().isPlaying == true and audioPlayed == 0):
					GetComponent[of AudioSource]().Stop()
			if (storyState == 1 or storyState == 2):
				phoneScript = GameObject.Find("sidePhone")
				phoneScript.GetComponent[of Phone]().ring()
			
			#audio.clip = audioLog[storyState]
			#audioPlayed = 1
			#audio.Play()
			
				#storytext = "You are an intern in a company called Logale & Jabran and today is your first morning at your job. Press the E key to continue."
			
				#storytext = "Please set up everything exactly the same as it was. Your boss is very particular about the way the office is organized."
			TellStory()
			
		
	def OnLevelWasLoaded():
		updateStory()
		
		/*
		if(finalreturn == false and office == true and reception == true and meeting == true):
			if(Application.loadedLevelName == "entrance_desk"):
				finalreturn = true*/
		#if checkRooms() == true and Application.loadedLevelName == "entrance_desk":
		#	elevator = GameObject.Find("ElevatorPREFAB").GetComponent[of SimpleElevator]().elevator.openDoor()
			
			
	
		
		
	def Getlastroom ():
		return lastroom
		
	def Setlastroom ():
		lastroom = Application.loadedLevelName
		
	def IncrementState():
		storyState += 1
		
	def setToldStory():
		toldstory = false	
		
	def TellStory():
		display = true
		GameObject.Find("Player").GetComponent[of CharacterController]().enabled = false
		