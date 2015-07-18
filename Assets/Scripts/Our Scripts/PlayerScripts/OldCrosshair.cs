using UnityEngine;
using System.Collections;

public class OldCrosshair : MonoBehaviour {
	public Texture2D crosshairTex;
	public float crossScale = 2;

	void OnGUI () {
		if (crosshairTex != null)
		{
			GUI.DrawTexture(new Rect((Screen.width-crosshairTex.width*crossScale) / 2, (Screen.height - crosshairTex.height * crossScale) / 2, crosshairTex.width * crossScale, crosshairTex.height * crossScale), crosshairTex);
		}
		else
			Debug.Log ("No crosshair");
	}
}
