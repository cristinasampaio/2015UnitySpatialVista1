using UnityEngine;
using System.Collections;

public class abstractTutorial : MonoBehaviour {
	public int state = 0;
	GameObject target;
	GameObject oldObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "x")
		{
			state++;
			if (state == 1)
			{
				gameObject.transform.position = new Vector3(-12.12451f, 14.06308f, -6.114337f);
			}
			else if (state == 2)
			{
				gameObject.transform.position = new Vector3(-28.24592f, 14.06308f, -7.045423f);
			}
			else if (state == 3)
			{
				gameObject.transform.position = new Vector3(-4.24359f, 14.06308f, -25.0599f);
				target = GameObject.Find("table");
				oldObject = GameObject.Find("abstractTable");

				oldObject.GetComponent<Renderer>().enabled = false;
				target.GetComponent<Renderer>().enabled= true;

			}
			else if (state == 4)
			{
				gameObject.transform.position = new Vector3(-6.06902f, 13.5867f, -20.8498f);
			}
			else if (state == 5)
			{
				target = GameObject.Find("whippedCream");
				target.GetComponent<abstractTutorial>().setState(5);
				target.GetComponent<Renderer>().enabled = true;
				target.transform.position = new Vector3(-19.15138f, 17.30782f, -7.111935f);
				Destroy (gameObject);
			}

			else if (state == 7)
			{
				target = GameObject.Find("x");
				target.transform.position = new Vector3(-19.15138f, 13.62326f, -5.550145f);
			}

			else if (state == 8)
			{
				target = GameObject.Find("x");
				target.transform.position = new Vector3(-19.15138f, 13.62326f, -8.4f);
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

	void setState (int val)
	{
		state = val;
	}
}
