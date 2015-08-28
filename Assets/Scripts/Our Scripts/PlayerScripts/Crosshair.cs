using UnityEngine;
using System.Collections;

/*Thank you to eVRydayVR and his reticle tutorial on youtube*/

public class Crosshair : MonoBehaviour {
	//New Oculus Crosshair code
	public Camera CameraFacing;
	private Vector3 originalScale;
	
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
		this.CameraFacing = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (CameraFacing.transform.position,
		                              CameraFacing.transform.rotation * Vector3.forward),
		                     out hit)) {
			distance = hit.distance;
		} else {
			distance = CameraFacing.farClipPlane * 0.95f;
		}
		transform.position = CameraFacing.transform.position +
			CameraFacing.transform.rotation * Vector3.forward * distance;
		transform.LookAt (CameraFacing.transform.position);
		transform.Rotate (0.0f, 180.0f, 0.0f);
		if (distance < 10.0f) {
			distance *= 1 + 5*Mathf.Exp (-distance);
		}
		transform.localScale = originalScale * distance;
	}
	
	//Old regular screen crosshair code.
	/*public Texture2D crosshairTex;
	public float crossScale = 2;
	void OnGUI () {
		if (crosshairTex != null)
		{
			GUI.DrawTexture(new Rect((Screen.width-crosshairTex.width*crossScale) / 2, (Screen.height - crosshairTex.height * crossScale) / 2, crosshairTex.width * crossScale, crosshairTex.height * crossScale), crosshairTex);
		}
		else
			Debug.Log ("No crosshair");
	}*/

	void OnLevelWasLoaded(int level)
	{
		this.CameraFacing = Camera.main;
	}
}