import UnityEngine

class elevator (MonoBehaviour): 
	#elevatorManager as elevatorManager = GetComponent[of elevatorManager]()
	public manager as GameObject
	def Start ():
		manager = GameObject.Find("GameMaster") 
	 
	def Update ():
	#if player collides with elevator inside call eventHandler(enterElevator)
	#if buttonpressedUP: eventHandler(upButtonPressed)
	#if bottonPressedDown: eventHandler (downButtonPressed)
	#if playercolides outerelevator collider from inside eventHandler(exitedElevator)
		player = GameObject.Find("Player")
		if player != null and Vector3.Distance(player.transform.position, transform.localPosition) < 5 and Input.GetKeyDown('e'):         	
	        	Debug.Log("e key was pressed")
	        	Debug.Log(Vector3.Distance(player.transform.position, transform.localPosition))
	        	#Debug.Log(manager.GetComponent[of MasterScript]().getFloor())
	        	goUp()
        	
	#def Awake() as void:
	#	DontDestroyOnLoad(transform.gameObject)
	def goUp():
		Debug.Log("go up")
		manager.GetComponent[of MasterScript]().getNextFloor()	
	#delay = elevatorManager.getDelay
	#wait delay*.5
	#load scene elevatorManager.getNextFloor
	#wait delay*.5
	#open door
	#if playable
	#wait until player leaves then close
	#else wait a set amount of time then call go up again
	#closeDoors
	#goUp()
	def onTriggerEnter(other as Collider) as void:
		Debug.Log("enter")
	def onTriggerExit():
		Debug.Log("Exit")
	def animationManager(animation):
		Debug.Log("anim")
	#play animation
	#sound manager be its own thing
	def eventHandler(eventE):
		Debug.Log("anim")
	#caseStatement for different events
	#upButtonPressed: if can go up: goUp()
	#downButtonPressed: if can go down: goDown()
	#enteredElevator: close doors
	#exitedElevator: closedoors