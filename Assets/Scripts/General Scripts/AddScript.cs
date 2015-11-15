using UnityEngine;
using System.Collections;

class AddScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("target")) {
			if (obj.GetComponent<WriteCoordinates> () == null) {
				obj.AddComponent<WriteCoordinates> ();
				obj.GetComponent<WriteCoordinates> ().checkColliders();
			}
		}
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("nontarget")) {
			if (obj.GetComponent<WriteCoordinates>() == null) {
				obj.AddComponent<WriteCoordinates>();
				obj.GetComponent<WriteCoordinates>().checkColliders();
			}
		}
	}
}
