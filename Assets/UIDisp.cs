using UnityEngine;
using System.Collections;

public class UIDisp : MonoBehaviour {
	Component[] cgroups;

	// Use this for initialization
	void Start () {
		cgroups = this.GetComponentsInChildren (typeof(CanvasGroup));
		foreach (CanvasGroup group in cgroups) {
			group.alpha = 0;
		}
	
	}

	public void sendTextToUi(string text)
	{
		//shell function
		//will receive text from anywhere else and pass it to the text panel
	}

	public void disableUI()
	{
		//shell function
		//will disable all UI elements
	}

	public void showConf()
	{
		//shell function
		//shows the confidence interval panel
	}
	public void showConf(string name)
	{
		//shell function
		//shows the confidence interval panel with potentially name of the object
	}
}
