import UnityEngine

/*
Alexander Quigley Freedman + the 2014 Spacial Memory Unity Team
I like to write some useful info up here if I can.
This code in total should give all of the things the player can do, besides moving.
Included is holding objects, moving them around and placing them.
Terms:
	held object - means the object currently selected by index
	dop - Depth of Displacement which is how far from the player the held object is.
*/

#Basically what Alex said. Very important file, try not to mess with this too much.
#...except it does need to be rewritten because of Boo. So I'll try to add some further comments
#in hopes of making the behavior(s) needed clear. -Jacob
class PlayerBehavior (MonoBehaviour): 
	
	numPlaced = 0#We should probably scrap my Placed idea above for something better.
	p = 0
	obj as GameObject
	highlightShader as Shader
	normalShader as List
	tempShader as List
	showConfidenceGui = false
	public boxstyle = GUIStyle()
	public textstyle = GUIStyle()
	cameraOffset = .5
	public currentState = 1
	index = 0#Position in list of inventory items.
	total = 0
	mystyle = GUIStyle()
	public displaytext as Texture
	
	#Depth of placement, so you can fine tune right where objects drop.
	dop = 1.5
	default_dop = 1.5
	
	#inventory
	pickupDistance = 50.0;
	hasObject = false
	currObject as GameObject
	
	//For deciding what buttons involving objects do.
	private controllerConnected = false;
	private OculusConnected = false;
	
	def Start():
		dop = (dop * gameObject.transform.localScale.y)
		default_dop = dop
		pickupDistance = pickupDistance * gameObject.transform.localScale.y
		highlightShader = Shader.Find("Self-Illumin/Diffuse")
		normalShader = []
		tempShader = []
		
		//Probably should be improved
		currentState = GameObject.Find("GameMaster").GetComponent[of MasterScript]().getState()
		controllerConnected = GameObject.Find("GameMaster").GetComponent[of MasterScript]().getGamePad()
		OculusConnected = GameObject.Find("GameMaster").GetComponent[of MasterScript]().getVR()
		
		mystyle.fontSize = 20
		mystyle.wordWrap = true
		mystyle.alignment = TextAnchor.UpperCenter
	
	def Update ():
		#if Input.GetKeyDown('`'):
		#	Debug.Log(currObject.gameObject.GetComponent[of Rigidbody]().velocity)

		if currentState == 2:
			hit1 as RaycastHit
			ray2 as Ray =  Camera.main.ScreenPointToRay(Vector3(Screen.width/2, Screen.height/2, 0))
		
			#Code for highlighting objects that can be picked up.
			#This may seem scary, but it's really just a bunch of checks.
			#Basically we need to check to see if we're holding the object or not in addition to
			#if we're just looking at it, and decide from there how we change the shader.
			#This allows us to decide when and how it should be glowing.
			if (Physics.Raycast(ray2, hit1, pickupDistance)):
				if hit1.collider.gameObject.tag == 'target' or hit1.collider.gameObject.tag == 'nontarget':
					if(obj != hit1.collider.gameObject):
						tempShader = normalShader
						normalShader = []
						for mat in hit1.collider.gameObject.GetComponent[of Renderer]().materials:
							try:
								normalShader.Add(mat.shader)
							except:
								pass
						if(hasObject == false):
							for mat in hit1.collider.gameObject.GetComponent[of Renderer]().materials:
								try:
									mat.shader = highlightShader
								except:
									pass
					if(obj != null and obj != hit1.collider.gameObject):
						i = 0
						for mat in obj.GetComponent[of Renderer]().materials:
							try:
								mat.shader = tempShader[i]
							except:
								pass
							i += 1
					obj = hit1.collider.gameObject
				elif(obj != null):
					i = 0
					for mat in obj.GetComponent[of Renderer]().materials:
						try:
							mat.shader = normalShader[i]
						except:
							pass
						i += 1
					obj = null		
			elif(obj != null):
				i = 0
				for mat in obj.GetComponent[of Renderer]().materials:
					try:
						mat.shader = normalShader[i]
					except:
						pass
					i += 1
				obj = null
		
			#Yeah. This needs to be rewritten because we're changing our confidencegui a bit.
			#No clue though. Sorry. -Jacob
			if showConfidenceGui:
				if(Input.GetKeyDown(KeyCode.Backspace) or Input.GetButtonDown("Fire2")):
						showConfidenceGui = false
				else:
					for i in range(1,8):#Bad way to do this, rewrite later.
						if(Input.GetKeyDown("" + i)):
							obj = currObject as GameObject
							time = GameObject.Find("GameMaster").GetComponent[of MasterScript]().getTimer()
							obj.GetComponent[of WriteCoordinatesOLD]().saveCSV(i,time)
							showConfidenceGui = false
							numPlaced += 1 #This should show us when we've 
							currObject.gameObject.tag = 'Untagged'
							currObject = null
			else:
				if Input.GetMouseButtonDown(0) or Input.GetButtonDown("Fire1"):
					if not hasObject:
						hit as RaycastHit
						ray as Ray = Camera.main.ScreenPointToRay(Vector3(Screen.width/2, Screen.height/2, 0))
						if (Physics.Raycast(ray, hit, pickupDistance)):
							if hit.collider.gameObject.tag == 'target' or hit1.collider.gameObject.tag == 'nontarget':
								hasObject = true
								currObject = hit.transform.gameObject
								for col in currObject.GetComponents[of Collider]():
									col.enabled = false
								currObject.GetComponent[of Rigidbody]().useGravity = false
								currObject.GetComponent[of Rigidbody]().constraints = RigidbodyConstraints.FreezeAll
								currObject.GetComponent[of Rigidbody]().constraints &= ~RigidbodyConstraints.FreezePositionY
								currObject.transform.localEulerAngles = currObject.GetComponent[of WriteCoordinatesOLD]().startRot
					else:
						hasObject = false
						for col in currObject.GetComponents[of Collider]():
							col.enabled = true
						currObject.GetComponent[of Rigidbody]().useGravity = true
						currObject.GetComponent[of Rigidbody]().velocity = Vector3(0,-.01,0)
						currObject.gameObject.GetComponent[of WriteCoordinatesOLD]().positionUpdate()
						if (currObject.gameObject.tag == 'target'):
							showConfidenceGui = true
						dop = default_dop
	
							
						//GameObject.Find("GameMaster").GetComponent[of MasterScript]().UpdatePos(currObject.name,currObject.transform.localPosition)
				if hasObject:
					currObject.transform.position = Camera.main.ScreenToWorldPoint(Vector3(Screen.width/2, Screen.height/2, (Camera.main.nearClipPlane+dop)))

				if controllerConnected and OculusConnected:
					if (Input.GetAxis("Mouse ScrollWheel") or Input.GetAxis("RightStickVertical")):
						sign = Input.GetAxis("Mouse ScrollWheel") or -Input.GetAxis("RightStickVertical")
						//Debug.Log(sign);
						if (sign > 0.05 and dop < 3.0 * gameObject.transform.localScale.y):
							dop += .03
						elif (sign < -0.05 and dop > 0.5 * gameObject.transform.localScale.y):
							dop -= .03
				elif controllerConnected and not OculusConnected:
					if Input.GetAxis("Mouse ScrollWheel") or (Input.GetButtonDown("RightBumper") or Input.GetButtonDown("LeftBumper")):
						Debug.Log("THIS HERE");
						sign = Input.GetAxis("Mouse ScrollWheel")
						//Special code for using the bumpers.
						if Input.GetButtonDown("RightBumper"):
							sign = 1.0
						elif Input.GetButtonDown("LeftBumper"):
							sign = -1.0
						if (sign > 0.05 and dop < 3.0 * gameObject.transform.localScale.y):
							dop += .03
						elif (sign < -0.05 and dop > 0.5 * gameObject.transform.localScale.y):
							dop -= .03
				else:
					if Input.GetAxis("Mouse ScrollWheel"):
						sign = Input.GetAxis("Mouse ScrollWheel")
						//Debug.Log(sign);
						if (sign > 0.05 and dop < 3.0 * gameObject.transform.localScale.y):
							dop += .03
						elif (sign < -0.05 and dop > 0.5 * gameObject.transform.localScale.y):
							dop -= .03
				
							
	#Will just Exit the game for now, should be changed later.
	#Maybe Reset to scene 1? Perhaps say YOU WIN(even if you don't)
	public def reset():
		Debug.Log("EXIT")
		Application.Quit()
			
	#Last step before exiting (or restarting) the game, sets data.csv up for the next runthrough
	public def finishCSVsave():
		filepath = "Data/data.csv"
		output = Time.time
		try:
			using myfile = System.IO.StreamWriter(filepath, true):
				myfile.WriteLine(output)
		except e:
			Debug.Log("There was an error in finishing writing to data.scv\nYou should press return in the document to set the writer to a newline.")
		
	#Will be removed once the UI is fixed. Instead will be a simple call to the UI itself.	
	def OnConfidenceGUI ():
		dialogWidth = 400;
		dialogHeight = 200;

		GUI.DrawTexture(Rect((Screen.width - dialogWidth-50) / 2, (Screen.height - dialogHeight-30) / 2, dialogWidth+50, dialogHeight+50), displaytext)
		GUI.Label(Rect((Screen.width - dialogWidth) / 2, (Screen.height - dialogHeight) / 2, dialogWidth, dialogHeight),"Press [1 - 7] to indicate how confident you are", mystyle)
		GUI.Label(Rect((Screen.width - dialogWidth) / 2, (Screen.height - dialogHeight+150) / 2, dialogWidth, dialogHeight),"IF THE OBJECT ISN'T WERE YOU INTENDED TO PLACE IT PRESS [BACKSPACE] TO TRY AGAIN", mystyle)
		/*
		//TODO : make the dialog pretty!
		textstyle.fontSize = 20
		//textstyle.wordWrap = true
		GUILayout.BeginArea(Rect((Screen.width - dialogWidth) / 2, (Screen.height - dialogHeight) / 2, dialogWidth, dialogHeight),boxstyle)
		GUILayout.BeginVertical("Box")
		GUILayout.FlexibleSpace()

		GUILayout.BeginHorizontal()
		GUILayout.FlexibleSpace()
		GUILayout.Label("Press [1 - 7]",textstyle)
		GUILayout.FlexibleSpace()
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		GUILayout.FlexibleSpace()
		GUILayout.Label("to indicate how confident you are", textstyle)
		GUILayout.FlexibleSpace()
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		GUILayout.FlexibleSpace()
		GUILayout.Label("OR")
		GUILayout.FlexibleSpace()
		GUILayout.EndHorizontal()

		GUILayout.BeginHorizontal()
		GUILayout.FlexibleSpace()
		GUILayout.Label("If this isn't where you want the object", textstyle)
		GUILayout.FlexibleSpace()
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		GUILayout.FlexibleSpace()
		GUILayout.Label("press [BACKSPACE] to Try Again", textstyle)
		GUILayout.FlexibleSpace()
		GUILayout.EndHorizontal()

		GUILayout.FlexibleSpace()
		GUILayout.EndVertical()
		GUILayout.EndArea()*/
	
	def OnGUI():
		//Debug.Log(showConfidenceGui)
		if(showConfidenceGui):
			OnConfidenceGUI()