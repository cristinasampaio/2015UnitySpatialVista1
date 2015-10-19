using UnityEngine;
using System.Collections;

public class abstractTutorial : MonoBehaviour {
	public int state = 0;
	Vector3 storedVector;
	GameObject target;
	GameObject oldObject;
	GameObject manager;
	GameObject tutorialManager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("GameMaster");
		tutorialManager = GameObject.Find ("TutorialManager");
		storedVector = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "x")
		{
			state++;
            try
            {
                using (System.IO.StreamWriter myfile = System.IO.File.AppendText("Data/data.csv"))
                {
                    GameObject x = GameObject.Find("x");
                    myfile.Write(x.transform.localPosition.x + "," + x.transform.localPosition.y + "," + x.transform.localPosition.z +",");
                }
            }
            catch
            {
            }
            gameObject.GetComponent<tutWrite>().WritePosition();
			if (state == 1)
			{
				storedVector = new Vector3(-12.12451f, 14.06308f, -6.114337f);
				gameObject.transform.position = storedVector;
			}
			else if (state == 2)
			{
				storedVector = new Vector3(-28.24592f, 14.06308f, -7.045423f);
				gameObject.transform.position = storedVector;
			}
			else if (state == 3)
			{
				storedVector = new Vector3(-4.24359f, 14.06308f, -25.0599f);
				gameObject.transform.position = storedVector;
				target = GameObject.Find("table");
				oldObject = GameObject.Find("abstractTable");

				oldObject.GetComponent<Renderer>().enabled = false;
				target.GetComponent<Renderer>().enabled= true;

			}
			else if (state == 4)
			{
				storedVector = new Vector3(-6.06902f, 13.5867f, -20.8498f);
				gameObject.transform.position = storedVector;
			}
			else if (state == 5)
			{
				target = GameObject.Find("whippedCream");
				target.GetComponent<abstractTutorial>().setState(5);
				target.GetComponent<Renderer>().enabled = true;
				storedVector = new Vector3(-19.15138f, 17.30782f, -7.111935f);
				target.transform.position = storedVector;
				Destroy (gameObject);
			}

			else if (state == 7)
			{
				target = GameObject.Find("x");
				storedVector = new Vector3(-19.15138f, 13.62326f, -5.550145f);
				target.transform.position = storedVector;
			}

			else if (state == 8)
			{
				target = GameObject.Find("x");
				storedVector = new Vector3(-19.15138f, 13.62326f, -8.4f);
				target.transform.position = storedVector;
				manager.GetComponent<MasterScript> ().setTimer (5.0f);
			}
			else if (state == 9)
			{
				//manager.GetComponent<MasterScript> ().setTimer (5.0f);
				target = GameObject.Find("x");
				target.GetComponent<Renderer>().enabled = false;
				target = GameObject.Find("table");
				target.GetComponent<Renderer>().enabled = false;
				target = GameObject.Find("stop");
				target.GetComponent<Renderer>().enabled = true;
				target = GameObject.Find ("go");
				target.GetComponent<Renderer>().enabled = true;
				tutorialManager.GetComponent<TutorialControlScript>().Initiate();
				Destroy(gameObject);
			}

		}
	}

	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.name == "x")
		{
			if (state == 6)
			{
				target = GameObject.Find("x");
				target.transform.localScale = new Vector3(0.5f, 0.21f, 0.5f);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "boundBox")
		{
			gameObject.transform.position = storedVector;
		}
	}

	void setState (int val)
	{
		state = val;
	}
}
