using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public Transform target;
	public float smooth= 100.0f;
	// Use this for initialization
	void Start () {
		//target = GameObject.Find("graphics").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.position.x,target.position.y+1,target.position.z);//target.position;//Vector3.Lerp (transform.position, target.position,			Time.deltaTime * smooth);
		//transform.position = 5.0f;
	}
}
