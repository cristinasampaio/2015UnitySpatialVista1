import UnityEngine

public class WriteCoordinates (MonoBehaviour): 

	p = false
	placed = 0 #Checks whether object has been placed
	confidence = 0 #Confidence score on placement of object
	startX =  0.0
	startY = 0.0
	startZ = 0.0
	public startRot as Vector3
	endX = 0.0
	endY = 0.0
	endZ = 0.0
	newpos = {"printer" : Vector3(-17.61,0.59,40.77),"printerpaper" : Vector3(18.28,0.59,18.39),"mouse" : Vector3(19.19,6.4,27.25), "chair2" : Vector3(20.74,0.36,30),"monitor2" : Vector3(8.77,11.89,16.88),"singleflower" : Vector3(-9.77,10.96,16.58),"chair1" : Vector3(9.81,0.57,61.1),"cup" : Vector3(0.34,0.41,55.02),"keyboard1" : Vector3(5.27,0.63,48.08),"monitor1" : Vector3(-5.16,1.06,33.72),"paper263" : Vector3(35.22,0.18,24.92),"tablet1" : Vector3(-17.05,-0.88,-11.23),"trashbasketmeeting" : Vector3(18.87,0,6.39),"plant2" : Vector3(-15.7,1.95,63.53),"plant" : Vector3(-14.55,2.11,19.34),"paper229" : Vector3(-18.8,1.64,26.91),"bluebinders" : Vector3(39.32,2.05,44.99),"chair3" : Vector3(13.98,4.3,-16.47),"wallpicture" : Vector3(0.93,8.97,68.3),"chair6" : Vector3(44.9,4.7,13.3),"officechair" : Vector3(2.49,0.55,-20.87),"chair" : Vector3(-14.48,0.4,2.36),"L&Jposter" : Vector3(51.6,12.2,5.6),"bluechair1" : Vector3(19.72,2.68,1.03),"bluechair2" : Vector3(22.73,5.38,18.66),"pencilholder" : Vector3(-14.05,0.32,16.9),"trashbasket" : Vector3(-7.55,0.71,-2.99),"chair10" : Vector3(10.6,0.4,-22.2),"cup1" : Vector3(-4.21,8.36,15.07),"phone_2" : Vector3(-35.86,9.92,2.35),"trophe" : Vector3(-38.64,10.86,45.76),"plant1" : Vector3(-23.45,3.57,0.44),"bookletstack" : Vector3(16.92,-11.51,0.11),"bookletstack1" : Vector3(18.41,-23.82,-1.25),"folders" : Vector3(11.16,6.65,-1.39),"vase4" : Vector3(-14.38,0.57,-22.61),"vase3" : Vector3(-8.05,-0.07,-22.97),"whitechair" : Vector3(-11.27,4.41,23.76),"floorlight" : Vector3(-18.45,0.09,-19.09),"books" : Vector3(-29.83,5.07,6.58)}
	storedVector as Vector3
#here are the new rotation coordinates. I copied the format above for the "position"; the numbers follow the order X, Y, and Z in Unity:
	newrot = {"printer" : Vector3(270,103.86,0),"printerpaper" : Vector3(280,285,90),"mouse" : Vector3(270,270,0), "chair2" : Vector3(270,195,0),"monitor2" : Vector3(370.84,464.99,-94.89),"singleflower" : Vector3(50.177,-70.04,273.98),"chair1" : Vector3(270,250,0),"cup" : Vector3(355.60,270,-48.38),"keyboard1" : Vector3(275,90,270),"monitor1" : Vector3(371.55,179.37,87.301),"chair3" : Vector3(363.80,219.89,193.85),"wallpicture" : Vector3(0,180,103.033),"chair6" : Vector3(2.1733,180,120),"officechair" : Vector3(270,40,0),"L&Jposter" : Vector3(0,270,70.6294),"bluechair1" : Vector3(403.57,237.87,-136.37),"trashbasket" : Vector3(350.31,-180,-180),"cup1" : Vector3(270,0,0),"phone_2" : Vector3(270,54.693,0),"bookletstack" : Vector3(0,0,0),"folders" : Vector3(12.78,-74.62,185.80)}

	def Awake ():
		self.startX = transform.position.x
		self.startY = transform.position.y
		self.startZ = transform.position.z
		self.startRot = transform.localEulerAngles
		gameObject.layer = 8
		checkColliders()
		gameObject.GetComponent[of Rigidbody]().constraints = RigidbodyConstraints.FreezeAll
		gameObject.GetComponent[of Rigidbody]().constraints &= ~RigidbodyConstraints.FreezePositionY
		
		
		#if(GameObject.Find("GameMaster").GetComponent[of MasterScript]().GetPos(gameObject.name) != null):
		//try:
		//	self.transform.localPosition = GameObject.Find("GameMaster").GetComponent[of MasterScript]().GetPos(gameObject.name)
		//except e:
		//	pass
		if(GameObject.Find("GameMaster").GetComponent[of MasterScript]().getState() == 2):
			try:
				gameObject.transform.localPosition = keeper.ReturnPos(gameObject.name)
				storedVector = gameObject.transform.position
				gameObject.transform.localEulerAngles = keeper.ReturnRot(gameObject.name)
			except e:
				storedVector = gameObject.transform.position;
	
	def Update ():
		if p == true:
			if (gameObject.GetComponent[of Rigidbody]().velocity == Vector3(0,0,0)):
				keeper.SetPos(gameObject.name, Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z))
				keeper.SetRot(gameObject.name, startRot)
				storedVector = gameObject.transform.position;
				p = false
			
	/*This function takes as input the confidence of each item
	 * and puts it, along with all relevant position data
	 * Starting x,y,z and ending x,y,z into a comma seperated string
	 * Which is output to a file in the Data folder, which must be
	 * created by hand*/
	public def saveCSV(conf, time):
		self.endX = transform.position.x
		self.endY = transform.position.y
		self.endZ = transform.position.z
		self.confidence = conf
		name = self.gameObject.name
		keeper.SetPos(name, Vector3(self.endX, self.endY, self.endZ))
		keeper.SetRot(name, gameObject.transform.localEulerAngles)
		filepath = "Data/data.csv"
		del = ","
		output = "$name$del$startX$del$startY$del$startZ$del$endX$del$endY$del$endZ$del$confidence$del$time$del"
		try:
			using myfile = System.IO.StreamWriter(filepath, true):
				myfile.WriteLine(output)
		except e:
			Debug.Log("There was an error in writing this object to data.scv")
			
	def positionUpdate():
		p = true
	
	def checkColliders():
		if	gameObject.GetComponent[of Rigidbody]() == null:
			gameObject.AddComponent[of Rigidbody]()
			gameObject.GetComponent[of Rigidbody]().constraints = RigidbodyConstraints.FreezeAll
			gameObject.GetComponent[of Rigidbody]().constraints &= ~RigidbodyConstraints.FreezePositionY
		if	gameObject.GetComponent[of BoxCollider]() == null:
			gameObject.AddComponent[of BoxCollider]()

	def OnTriggerEnter(col as Collider):
		if (col.gameObject.name == "boundBox"):
			gameObject.transform.position = storedVector
			GetComponent[of Rigidbody]().velocity = Vector3.zero
			GetComponent[of Rigidbody]().angularVelocity = Vector3.zero
		
		