import UnityEngine
#Not really deprecated but it needs to be replaced.
#A lot of code in this are forcibly creating textures when we could just be using Unity's built in GUI system.
#Which also means this menu probably won't work well in VR.
#Would require extensive fixes to the login screen.
#Not that bad though, but definitely time consuming. - Jacob
class Login (MonoBehaviour): 

	public username as string
	public gender as string
	public mystyle = GUIStyle()
	
	public playNormal as Texture2D
	public playHover as Texture2D
	
	def Start():
		w = .156*Screen.width
		h = .137*Screen.height
		x = (Screen.width-w)/2
		y = (Screen.height/2)-(1.4*h)
		GetComponent[of GUITexture]().pixelInset = Rect (x, y, w, h)
		
	def OnGUI() as void:
		mystyle.fontSize = 20
		xcenter = Screen.width/2
		ycenter = Screen.height/2

		GUI.Label(Rect(xcenter-55,ycenter-70,10,10), "Enter ID", mystyle)
		GUI.Label(Rect(xcenter-55,ycenter-10,10,10), "Enter Sex", mystyle)
		
		username = GUI.TextField(Rect(xcenter-100, ycenter-50, 200, 25), username, 25)
		gender = GUI.TextField(Rect(xcenter-100, ycenter+10, 200, 20), gender, 25)

	def OnMouseEnter() as void:
		GetComponent[of GUITexture]().texture = playHover

	def OnMouseExit() as void:
		GetComponent[of GUITexture]().texture = playNormal

	def OnMouseDown() as void:
		saveCSV()
		Application.LoadLevel("testingtutorial")
					
	//This runs when first playing
	def saveCSV ():
		//Checks that the data folder exists
		if not System.IO.Directory.Exists ("/Data/"):
        	System.IO.Directory.CreateDirectory ("Data/")
		filepath = "Data/data.csv"
		del = ","
		output = "$username$del$gender$del\n"
		if not System.IO.File.Exists(filepath):
			try:
				using myfile = System.IO.StreamWriter(filepath, true):
					myfile.Write("ID,Sex\nLevel,Stage,Time\nObjectName,StartX,StartY,StartZ,EndX,EndY,EndZ,Confidence,TimePlaced\nLevel,Stage,Time\n")
			except e:
				Debug.Log("There was an error in writing this object to data.scv")
		try:
			using myfile = System.IO.StreamWriter(filepath, true):
				myfile.Write(output)
		except e:
			Debug.Log("There was an error in writing this object to data.scv")
		myfile.Close()
