﻿#pragma strict
var exitNormal : Texture2D;  
var exitHover : Texture2D;
function Start () {
	var w = .156*Screen.width;
 	var h = .137*Screen.height;
 	var x = (Screen.width-w)/2;
 	var y = (Screen.height/2)-h;
	GetComponent.<GUITexture>().pixelInset = Rect (x, y, w, h);
	Debug.Log(GetComponent.<GUITexture>().pixelInset);
	Debug.Log(Screen.width+":"+Screen.height);
}

function OnMouseEnter() {
	GetComponent.<GUITexture>().texture = exitHover;
}
function OnMouseExit() {
	GetComponent.<GUITexture>().texture = exitNormal;
}
function OnMouseDown() {
	Application.Quit();
}