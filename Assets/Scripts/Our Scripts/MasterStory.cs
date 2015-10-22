using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class MasterStory : MonoBehaviour {
	//WIP: To be used for story purposes
	//Probably a better method is to store the story in a file and read from file, but for now this works.

	string[] apartStory = {
		"You just received a call that your parents are in town for a visit. You tell your parents that you are in your new apartment and ask them to meet you there. You have a roommate, so before your parents arrive, you want to make sure the apartment looks perfect. Please go around the apartment to check if every room and the deck outside are in decent condition. Your roommate has a habit of leaving empty soda cans around, so if you find any, please bring it back to the kitchen.",
	     "Your parents remember that they need to feed the meter and you offer to go for them. You tell your parents that you will also need to run some errands and that you and your roommate should be back in 30 minutes or so. Your parents make themselves at home.",
	     "Please go to the elevator.",
	     "You are back in your apartment and realize that your mom decided to rearrange some furniture and objects while you and your roommate were out. Your roommate is not very happy about it, and you assure her that you will help replacing everything where it was once your parents leave.",
	     "Your parents say goodbye and your task is to make the apartment look exactly the same as it was before. Specifically, please set up everything exactly the same as it was. Your roommate is very particular about the way the apartment is set up."
	             
	};

	string[] officeStory = {
		"You are an intern in a company called Logale & Jabran and today is your first morning at your job.",
	    "Your phone is ringing",
	    "Welcome! Thank you so much for coming in on such short notice. Please take a tour of the office before all employees arrive: there are four separate rooms for you to explore. Please look around everywhere and examine all small objects.",
	    "Our former intern was dismissed yesterday, and he usually cleaned up the desks and tables at the end of the day, so please don't mind if the office is not completely in order. You will be covering some of his former tasks so please make sure to look EVERYWHERE: behind, above, and under desks, counters, couches, chairs, etc. Pay very close attention to where every single object is.",	
	    "Now that you are finished with the office tour, I think you should also visit the office above us, as we collaborate with them in a number of projects. Please go to the elevator to head up there.",
	    "We need you back in the office immediately. Please hurry back.",
	    "Wow, what a crazy morning. Our last intern came into the office while you were gone. He didn’t handle his dismissal very well, and now I’m at the police station reporting the incident. Please, before everyone gets in, go around to all of the rooms and put everything back in place. There will be a very important meeting this morning.",
	    "Please set up everything exactly the same as it was. Your boss is very particular about the way the office is organized."
	};

	enum STORYTYPE
	{
		APARTMENT,
		OFFICE,
		NONE,
	}

	STORYTYPE currentStory = STORYTYPE.NONE;
	bool visitApartment = false;
	bool visitOffice = false;
	bool storyPause = true;
	bool uiActive = false;
	int storyState = 0;
	UIDisp uiObj;

	// Use this for initialization
	void Start () {
		uiObj = GameObject.Find ("UI").GetComponent<UIDisp> ();
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown ("space") || Input.GetButtonDown("Fire1")) && storyPause == false) {
			switch (currentStory) {
			case STORYTYPE.APARTMENT:
				apartmentFunc ();
				break;
			case STORYTYPE.OFFICE:
				officeFunc ();
				break;
			}
		} 
		else if (Input.GetKeyDown ("space") || Input.GetButtonDown("Fire1"))
		{
			uiObj.disableText();
		}

	
	}

	void OnLevelWasLoaded(int level)
	{
		GameObject uitemp = GameObject.Find ("UI");
		if (uitemp != null) {
			uiObj = uitemp.GetComponent<UIDisp>();
		}

		string name = Application.loadedLevelName;
		if (name == "Apartment_Scene") {
			currentStory = STORYTYPE.APARTMENT;
			apartmentFunc ();
		} else if (name == "Office_Scene") {
			currentStory = STORYTYPE.OFFICE;
			officeFunc ();
		} else {
			storyPause = true;
		}

		//Note to self: At this point it should also do something to consider how many strings it needs to print out
		//Best option is to probably just have all the logic in here, but it'll be messy.
	}

	void apartmentFunc()
	{
		if (visitApartment = false) {
			storyState = 0;
			visitApartment = true;
		}
		if (storyState > apartStory.Length) {
			storyPause = true;
			return;
		}
		uiObj.sendTextToUi (apartStory [storyState]);

		storyPause = false;

		if (storyState == 1 || storyState == 2 || storyState == 4) {
			storyPause = true;
		}

		storyState++;

	}

	void officeFunc()
	{
		if (visitOffice = false) {
			storyState = 0;
			visitApartment = true;
		}
		if (storyState > officeStory.Length) {
			storyPause = true;
			return;
		}
		uiObj.sendTextToUi (officeStory [storyState]);

		storyPause = false;

		if (storyState == 3 || storyState == 4 || storyState == 7) {
			storyPause = true;
		}

		storyState++;

	}

	void unpauseStory(){
		switch(currentStory)
	    {
			case STORYTYPE.APARTMENT:
				apartmentFunc ();
				break;
			case STORYTYPE.OFFICE:
				officeFunc ();
				break;
		}
	}
}
