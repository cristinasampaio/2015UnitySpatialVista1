#pragma strict
var playNormal : Texture2D;
var playHover : Texture2D;
var login : GameObject;
function Start () {
	/*var rect = guiTexture.pixelInset;
 	rect.width = .156*Screen.width;//rect.width * Screen.width / rect.width;
 	rect.height = .137*Screen.height;//rect.height +20;//* Screen.height / rect.width;
 	rect.x = rect.width / 2.0;
 	rect.y = rect.height / 2.0;
 	guiTexture.pixelInset = rect;
 	Debug.Log(Screen.width+":"+rect.width+" "+Screen.height+":"+rect.height);
 	px = */
 	
 	var w = .156*Screen.width;
 	var h = .137*Screen.height;
 	var x = (Screen.width-w)/2;
 	var y = (Screen.height)/2;
	GetComponent.<GUITexture>().pixelInset = Rect (x, y, w, h);
	Debug.Log(GetComponent.<GUITexture>().pixelInset);
	Debug.Log(Screen.width+":"+Screen.height);
}

function OnMouseEnter() {
	GetComponent.<GUITexture>().texture = playHover;
}
function OnMouseExit() {
	GetComponent.<GUITexture>().texture = playNormal;
}
function OnMouseDown() {
	Instantiate(login, Vector3(0, 0, 0),Quaternion.identity);
	Destroy(GameObject.Find("ExitButton"));
	Destroy(gameObject);
}