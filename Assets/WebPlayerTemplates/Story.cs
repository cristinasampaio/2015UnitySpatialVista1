using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Story : MonoBehaviour {

	public Camera Cam;
	public int StopForExploring;

	List<AudioClip> audio = new List<AudioClip>();
	List<string> text = new List<string>();

	//Not sure if we need both of these, but we'll see.
	private bool inElevator = false;
	private bool display = false;
	private int storyState;
	private int storyLength;
	private AudioSource audioSource;
	private GameObject TxtWall;
	private TextMesh Txt;
	private GameObject backgroundCube;
	private double endOfExploringTime;

	// Use this for initialization
	void Start () {
		storyLength = 0;
		storyState = 0;
		//Get Level name, then load text and audio from that folder in resources.
		string lvlName = Application.loadedLevelName;
		StreamReader reader = new StreamReader ("Assets/Resources/" + lvlName + "/" + (lvlName + ".txt"));
		try
		{
			do
			{
				string tmp = reader.ReadLine();
				storyLength += 1;
				text.Add(tmp);
			}
			while(reader.Peek() != -1);
		}
		finally
		{
			reader.Close();
		}

		string[] files = Directory.GetFiles ("Assets/Resources/" + lvlName + "/");
		int size = files.Length;
		for (int i = 0; i < size; i++) {
			if(!files[i].Contains(".txt") && !files[i].Contains(".meta")) {
				AudioClip ac = Resources.Load(files[i]) as AudioClip;
				audio.Add (ac);
			}
		}
		//Get Audio source for playing audio later.
		audioSource = gameObject.GetComponent<AudioSource> ();
		//Set up text object.
		TxtWall = new GameObject("TextField");
		Txt = TxtWall.AddComponent <TextMesh>() as TextMesh;
		TxtWall.AddComponent<BoxCollider> ();
		Txt.anchor = TextAnchor.MiddleCenter;
		Txt.alignment = TextAlignment.Center;
		Txt.fontSize = 18;
		Txt.color = Color.black;

		//Start the story
		TellStory ();
		DisplayStory (text[0], audio[0]);
		backgroundCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		endOfExploringTime = 0.0;//This variable denotes when it should inform you that you can be done exploring.
		if (StopForExploring == 0)
			StopForExploring = -1;//Defaults to -1 so it there is none, unless you set it.
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && storyState != StopForExploring) {
			if(display) {
				if(audioSource.isPlaying) {
					audioSource.Stop();
				}
				if (storyState == storyLength) {
					LetPlay();
				}
				else {
					storyState += 1;
					try
					{
						DisplayStory (text[storyState], audio[storyState]);
					}

					catch
					{
						return;
					}
				}
			}
		}
		if (!display) {
			Txt.GetComponent<Renderer>().enabled = false;
			audioSource.Stop();
		}
		if(storyState == StopForExploring) {
			double time = GameObject.Find("GameMaster").GetComponent<MasterScript>().getTimer();
			if (endOfExploringTime == 0.0) {
				LetPlay ();
				endOfExploringTime = time + 20;
			}
			else if (time >= endOfExploringTime) {
				Debug.Log("THIS HERE");
				TellStory ();
				DisplayStory (text[storyState], audio[storyState]);
				endOfExploringTime = 10000;//Huge value to ensure you can read and move on from end of exploration stage.
			}
		}
	}

	void TellStory() {
		display = true;
		GameObject.Find("Player").GetComponent<MovePlayer>().enabled = false;
	}

	void LetPlay() {
		display = false;
		GameObject.Find("Player").GetComponent<MovePlayer>().enabled = true;
		Txt.GetComponent<Renderer> ().enabled = false;
	}

	void DisplayStory(string str, AudioClip ac) {
		TxtWall.transform.position = Cam.transform.position + new Vector3 (20.0f, -6.0f, 0f);
		TxtWall.transform.LookAt (Cam.transform.position);
		TxtWall.transform.Rotate (0.0f, 180.0f, 0.0f);
		int size = str.Length;
		if (str.Length == 0) {
			return;
		}
		int spot = 0;
		string end = "\n Press Space to Continue";
		string tmpStr = "";
		for (int c = 0; c < size; c++) {
			tmpStr +=  str[c];
			if (spot > 50 && str[c] == ' ') {
				if(spot != size-1) {
					tmpStr += '\n';
					spot = 0;
				}
			}
			spot++;
		}
		tmpStr += end;
		Txt.text = tmpStr;
		audioSource.PlayOneShot (ac, 1);
		//Bounds bounds = TxtWall.GetComponent<Renderer>().bounds;
		//backgroundCube.transform.position = TxtWall.transform.position;

	}

	void HideStory() {
		display = false;
	}
}
