using UnityEngine;
using System.Collections;

public class tutAccuracyScript : MonoBehaviour {
	Vector3 ogPos;
	public int layer = 0;
	public float x;
	public float y;
	public float z;
	private int id;
	public GameObject clone;
	private GameObject tutorialManager;

	public delegate void move();
	move thisDelegate;

	// Use this for initialization
	void Start () {
		thisDelegate = moveToRoom;
		this.ogPos = this.transform.position;
		this.x = ogPos.x;
		this.y = ogPos.y;
		this.z = ogPos.z;
		this.transform.position = new Vector3 (16.0f, 12.0f, 10.0f);
		tutorialManager = GameObject.Find ("TutorialManager");
		id = tutorialManager.GetComponent<TutorialControlScript> ().registerObj(layer);
	}
	
	void moveToRoom()
	{
		this.transform.position = new Vector3 (13.865f, 13.377f, -1.6789f);
		clone.transform.position = ogPos;
	}
}
