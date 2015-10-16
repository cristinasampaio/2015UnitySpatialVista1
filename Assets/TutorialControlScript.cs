using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialControlScript : MonoBehaviour {
	public bool placedObj = true;
	int id = 0;

	public delegate void move();
	move newDelegate;

	public static event move moveEvent;

	public List<move> layer1 = new List<move>();
	public List<move> layer2 = new List<move>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (placedObj == false) {
			placeObj ();
			placedObj = true;
		}
	}

	public void registerObj(int layer, move delFunc)
	{
		id++;
		switch (layer) {
		case 1:
			layer1.Add(delFunc);
			break;
		case 2:
			layer2.Add(delFunc);
			break;
		}
	}

	public void Initiate()
	{
		placedObj = false;
	}

	void placeObj()
	{
		if (layer1.Count > 0) {
			newDelegate = layer1 [0];
			newDelegate ();
			layer1.RemoveAt(0);
		} else if (layer2.Count > 0) {
			newDelegate = layer2 [0];
			newDelegate ();
			layer2.RemoveAt(0);
		}
		else
		{
			//tutorial is ogre
		}
	}
}
