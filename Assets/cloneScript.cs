using UnityEngine;
using System.Collections;

public class cloneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3 (-100f, -100f, -100f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void moveClone(Vector3 pos)
	{
		this.transform.position = pos;
	}
}
