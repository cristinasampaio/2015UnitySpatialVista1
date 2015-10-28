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
	private UIDisp ui;

	//private bool xb = false;
	//private bool yb = false;
	//private bool zb = false;
	private bool isActive = false;
	private bool check = false;
	bool firstdrop = false;

	private float tol = 0.05f;

	public delegate void move();
	move thisDelegate;

	public delegate void remm(int layer);
	remm returnDelegate;

	Rigidbody thisbod;

	// Use this for initialization
	void Start () {
		ui = GameObject.Find ("UI").GetComponent<UIDisp> ();
		thisDelegate = moveToRoom;
		this.ogPos = this.transform.position;
		this.x = ogPos.x;
		this.y = ogPos.y;
		this.z = ogPos.z;
		this.transform.position = new Vector3 (16.0f, 12.0f, 10.0f);
		tutorialManager = GameObject.Find ("TutorialManager");
		tutorialManager.GetComponent<TutorialControlScript> ().registerObj(layer, moveToRoom);
		thisbod = this.GetComponent<Rigidbody> ();
		thisbod.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Update()
	{
		if (isActive && thisbod.useGravity) {
			if (thisbod.IsSleeping() && check == false)
			{
				if (checkXYZ() == true)
				{
					this.tag = "Untagged";
					thisbod.constraints = RigidbodyConstraints.FreezeAll;
					this.clone.transform.position = new Vector3(-100f, -100f, -100f);
					isActive = false;
					tutorialManager.GetComponent<TutorialControlScript>().placedObj = false;
					ui.sendTextToUi("Good job! Here comes the next object.");
					//success, lock object, load next object
				}
				else if (firstdrop == false)
				{
					ui.sendTextToUi("Invalid placement. Try using the mouse wheel or shoulder buttons to move the object closer.");
					Debug.Log ("unsuccessful placement");
					//non-success, check xb, yb and zb and output a message
				}
				else
				{
					firstdrop = false;
				}
				check = true;
			//	xb = false;
			//	yb = false;
			//	zb = false;
			}
			else if (!thisbod.IsSleeping() && check == true)
			{
				check = false;
			}
		}
	}
	
	void moveToRoom()
	{
		this.transform.position = new Vector3 (13.865f, 13f, -1.6789f);
		clone.transform.position = ogPos;
		thisbod.isKinematic = false;
		isActive = true;
		firstdrop = true;
	}

	bool checkXYZ()
	{
		if (this.transform.position.x < (ogPos.x + tol) && this.transform.position.x > (ogPos.x - tol)) {	
			if (this.transform.position.y < (ogPos.y + tol) && this.transform.position.y > (ogPos.y - tol)) {
				if (this.transform.position.z < (ogPos.z + tol) && this.transform.position.z > (ogPos.z - tol))
					return true;
			}
		}
		return false;

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "boundBox")
		{
			this.transform.position = new Vector3 (13.865f, 13f, -1.6789f);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			Debug.Log ("test");
		}
	}
}
