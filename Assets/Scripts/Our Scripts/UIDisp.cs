﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VR;

public class UIDisp : MonoBehaviour {
	Component[] cgroups;
	CanvasGroup tpanel;
	CanvasGroup cpanel;
	Text tbox;
	bool tdisp = false;
	bool cdisp = false;
	bool isOn = false;

	// Use this for initialization
	void Awake () {
		cgroups = this.GetComponentsInChildren (typeof(CanvasGroup));
		foreach (CanvasGroup group in cgroups) {
			if (group.name == "TextPanel")
				tpanel = group;
			else if (group.name == "ConfPanel")
				cpanel = group;
			group.alpha = 0;
		}

		tbox = tpanel.GetComponentInChildren<Text> ();

		if (!VRDevice.isPresent) {
			RectTransform temp = tpanel.GetComponent<RectTransform>();

		}
	
	}

	public void sendTextToUi(string text)
	{
		//shell function
		//will receive text from anywhere else and pass it to the text panel
		if (tdisp == false)
			enableText ();
		tbox.text = text;
	}

	public void disableUI()
	{
		//shell function
		//will disable all UI elements
		if (isOn = true) {
			foreach (CanvasGroup group in cgroups) {
				group.alpha = 0;
			}
			isOn = false;
			tdisp = false;
			cdisp = false;
		}
	}

	public void enableText()
	{
		tpanel.alpha = 1;
		tdisp = true;
	}

	public void showConf()
	{
		//shell function
		//shows the confidence interval panel
		cpanel.alpha = 1;
		isOn = true;
	}
	public void showConf(string name)
	{
		//shell function
		//shows the confidence interval panel with potentially name of the object
	}
}