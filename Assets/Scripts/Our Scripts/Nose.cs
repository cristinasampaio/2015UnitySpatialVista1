using UnityEngine;
using System.Collections;

public class Nose : MonoBehaviour {

	public Camera CameraFacing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = CameraFacing.transform.position + 
			CameraFacing.transform.rotation * new Vector3(0f,-.3f,.2f);
		transform.LookAt (CameraFacing.transform.position);
		transform.Rotate (0.0f, -180.0f, 0.0f);
	}
}
