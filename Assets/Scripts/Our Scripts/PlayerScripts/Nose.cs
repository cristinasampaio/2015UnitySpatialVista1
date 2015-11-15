using UnityEngine;
using System.Collections;

public class Nose : MonoBehaviour {

	public Camera CameraFacing;

	private Vector3 StartAt;

	// Use this for initialization
	void Start () {
		StartAt = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = CameraFacing.transform.position + 
			CameraFacing.transform.rotation * new Vector3(0f,-.09f,.12f);
		//transform.Rotate (0.0f, 180.0f, 0.0f);
		transform.LookAt (2*CameraFacing.transform.position-StartAt);
	}
}
