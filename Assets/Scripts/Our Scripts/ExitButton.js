#pragma strict
var exitNormal : Texture2D;  
var exitHover : Texture2D;
function Start () {
	var w = .156*Screen.width;
 	var h = .137*Screen.height;
 	var x = (Screen.width-w)/2;
 	var y = (Screen.height/2)-h;
	guiTexture.pixelInset = Rect (x, y, w, h);
	Debug.Log(guiTexture.pixelInset);
	Debug.Log(Screen.width+":"+Screen.height);
}

function OnMouseEnter() {
	guiTexture.texture = exitHover;
}
function OnMouseExit() {
	guiTexture.texture = exitNormal;
}
function OnMouseDown() {
	Application.Quit();
}