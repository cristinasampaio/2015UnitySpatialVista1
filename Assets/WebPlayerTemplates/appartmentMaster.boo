import UnityEngine

class appartmentMaster (MonoBehaviour): 
	public storyState as int
	toldstory = false
	display = false
	public displaytext as Texture
	mystyle = GUIStyle()
	public boxStyle = GUIStyle()
	private gamemaster as MasterScript
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
	i = 0
	quickflag = 1
	public color as Color
	
	storytext = ["You just received a call that your parents are in town for a visit. You tell your parents that you are in your new apartment and ask them to meet you there. You have a roommate, so before your parents arrive, you want to make sure the apartment looks perfect. Please go around the apartment to check if every room and the deck outside are in decent condition. Your roommate has a habit of leaving empty soda cans around, so if you find any, please bring it back to the kitchen.",
	"Your parents remember that they need to feed the meter and you offer to go for them. You tell your parents that you will also need to run some errands and that you and your roommate should be back in 30 minutes or so. Your parents make themselves at home.",
	"Please go to the elevator.",
	"You are back in your apartment and realize that your mom decided to rearrange some furniture and objects while you and your roommate were out. Your roommate is not very happy about it, and you assure her that you will help replacing everything where it was once your parents leave.",
	"Your parents say goodbye and your task is to make the apartment look exactly the same as it was before. Specifically, please set up everything exactly the same as it was. Your roommate is very particular about the way the apartment is set up."
	
	]
	  
	def Awake ():
		
		Physics.gravity = Vector3(0,-9.8,0)
		Debug.Log("GRAV: "+Physics.gravity)
		storyState = 0		
		audio as AudioSource
		audio = gameObject.GetComponent[of AudioSource]()
		audio.clip = a0
		GameObject.DontDestroyOnLoad(self)
		if (GameObject.FindObjectsOfType(GetType()).Length > 1):
			#Debug.Log("destroy")
			#Destroy(audio)
			Destroy(self)	
			return	
		TellStory()
		audioLog = [a0,a1,a2,a3,a4,a5,a6,a7]
		if(audio.isPlaying == false):
			updateStory()
		gamemaster = GameObject.Find("GameMaster").GetComponent[of MasterScript]()
	
	def Update ():
		if Input.GetKeyDown("space"):
			Debug.Log("TESTTEST")
			if display == true:
				display = false
				toldstory = true
				storyState += 1
				audioPlayed = 0
				updateStory()
		if gamemaster.getTimer() >= 120:
			updateStory()
		elif gamemaster.getState() == 2 and quickflag == 1:
			quickflag = 0
			updateStory()
			
	def updateStory():
		Debug.Log(storyState)
		i += 1
		if(storyState == 0):
			toldstory = false
		elif(storyState == 1 and gamemaster.getTimer() >= 120):
			toldstory = false
		elif(storyState == 2):
			toldstory = false
		elif(storyState == 3 and gamemaster.getState() == 2):
			toldstory = false
		elif(storyState == 4):
			Debug.Log("STORYSTATE 4")
			toldstory = false
		if(toldstory is false):
			if(GetComponent[of AudioSource]().isPlaying == true and audioPlayed == 0):
					GetComponent[of AudioSource]().Stop()
			
			
			/*audio.clip = audioLog[storyState]
			audioPlayed = 1
			audio.Play()*/
			
				#storytext = "You are an intern in a company called Logale & Jabran and today is your first morning at your job. Press the E key to continue."
			
				#storytext = "Please set up everything exactly the same as it was. Your boss is very particular about the way the office is organized."
			TellStory()
			
	def IncrementState():
		storyState += 1
		
	def setToldStory():
		toldstory = false
		
	def OnGUI():
		mystyle.fontSize = Screen.width/60
		mystyle.wordWrap = true
		
		if(display is true):
			color.a = .7
			GUI.backgroundColor = color
			GUI.Box(Rect(Screen.width*.1,Screen.height*.74,Screen.width*.8,Screen.height*.25),"",boxStyle)
			GUI.Label(Rect(Screen.width*.15,Screen.height*.75,Screen.width*.7,Screen.height*.1), storytext[storyState].ToString(), mystyle)
			GUI.Label(Rect(Screen.width*.35,Screen.height*.95,Screen.width*.7,Screen.height*.9), "<color=#ffffffff>Press SpaceBar to Continue</color>", mystyle)
			
			
	def TellStory():
		Debug.Log("TELL: " + storyState)
		display = true
		
	def OnLevelWasLoaded():
		Physics.gravity = Vector3(0,-9.8,0)
		Debug.Log(Physics.gravity)