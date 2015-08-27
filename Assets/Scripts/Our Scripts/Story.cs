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
		Bounds bounds = TxtWall.GetComponent<Renderer>().bounds;
		Txt.anchor = TextAnchor.MiddleCenter;
		Txt.alignment = TextAlignment.Center;
		Txt.fontSize = 18;
		Txt.color = Color.black;
		TxtWall.transform.position = Cam.transform.position + Cam.transform.rotation * new Vector3 (0.0f, -10.0f, 40.0f);
		TxtWall.transform.LookAt (Cam.transform.position);
		TxtWall.transform.Rotate (0.0f, 180.0f, 0.0f);

		//Start the story
		TellStory ();
		DisplayStory (text[0], audio[0]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
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
	}

	void TellStory() {
		display = true;
		GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
	}

	void LetPlay() {
		display = false;
		GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
		Txt.GetComponent<Renderer> ().enabled = false;
	}

	void DisplayStory(string str, AudioClip ac) {
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
	}
}
