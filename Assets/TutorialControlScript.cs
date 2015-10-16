using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialControlScript : MonoBehaviour {
	List<int> layer1 = new List<int>();
	List<int> layer2 = new List<int>();
	bool placedObj = true;
	int id = 0;

	public delegate void move();

	public static event move moveEvent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (placedObj = false)
			placeObj ();
	}

	public int registerObj(int layer)
	{
		id++;
		switch (layer) {
		case 1:
			layer1.Add(id);
			break;
		case 2:
			layer2.Add(id);
			break;
		}
		return 0;
	}

	public void Initiate()
	{
		placedObj = false;
	}

	void placeObj()
	{
		//nothing
	}
}
