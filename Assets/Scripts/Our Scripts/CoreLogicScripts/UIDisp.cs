using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VR;

public class UIDisp : MonoBehaviour {
	Component[] cgroups;
	CanvasGroup tpanel;
	CanvasGroup cpanel;
	Text tbox;
	public float distz = 1.2f;
	public float distx = 0f;
	public float disty = 0f;
	bool textDisp = false;
	bool confDisp = false;
	bool isOn = false;
	bool moveToPosition = false;
	Vector3 offset;
	float valOffset = 1f;

	// Use this for initialization
	void Awake () {
		offset = new Vector3 (distx, disty, distz);
		cgroups = this.GetComponentsInChildren (typeof(CanvasGroup));
		foreach (CanvasGroup group in cgroups) {
			if (group.name == "TextPanel")
				tpanel = group;
			else if (group.name == "ConfPanel")
				cpanel = group;
			group.alpha = 0;
		}

		tbox = tpanel.GetComponentInChildren<Text> ();

	
	}

	public void Update()
	{
		//this.GetComponent<RectTransform> ().position = Camera.main.transform.position + offset;

		if (moveToPosition == true) {
			this.GetComponent<RectTransform>().position = Camera.main.transform.position + Camera.main.transform.forward*valOffset;
			transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
			moveToPosition = false;
		}
	}

	public void sendTextToUi(string text)
	{
		//shell function
		//will receive text from anywhere else and pass it to the text panel
		if (textDisp == false)
			enableText ();
		tbox.text = text;
		moveToPosition = true;
	}

	public void disableUI()
	{
		//shell function
		//will disable all UI elements
		if (isOn == true) {
			foreach (CanvasGroup group in cgroups) {
				group.alpha = 0;
			}
			isOn = false;
			textDisp = false;
			confDisp = false;
		}
	}

	public void enableText()
	{
		tpanel.alpha = 1;
		textDisp = true;
	}

	public void showConf()
	{
		//shell function
		//shows the confidence interval panel
		cpanel.alpha = 1;
		confDisp = true;
		isOn = true;
		moveToPosition = true;
	}
	public void showConf(string name)
	{
		//shell function
		//shows the confidence interval panel with potentially name of the object
	}
	public void disableText()
	{
		tpanel.alpha = 0;
		textDisp = false;
	}
	public void disableConf()
	{
		cpanel.alpha = 0;
		confDisp = false;
	}
	public void OnLevelWasLoaded(int id)
	{
		if (Application.loadedLevelName == "Office_Scene") {
			distx = 1f;
			distz = 0f;
			disty = 0f;
			valOffset = 1f;
			offset = new Vector3(distx, disty, distz);
		} else if (Application.loadedLevelName == "Apartment_Scene") {
			distx = -1f;
			disty = 0f;
			distz = 0f;
			valOffset = -1f;
			offset = new Vector3(distx, disty, distz);
		}
	}
}
