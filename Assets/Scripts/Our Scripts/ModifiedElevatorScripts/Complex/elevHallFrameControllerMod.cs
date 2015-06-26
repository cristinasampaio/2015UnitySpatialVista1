using UnityEngine;
using System.Collections;

public class SimpleHallFrame : MonoBehaviour {


	public		int						floor;
	public		Transform			callButtonLight;		
	public		Transform			HallLedPanel;
	public		string					elevTag			= "elev01";

	private	elevControlMod			elevator;
	private	AnimationClip	openAnim,
												closeAnim;

	void Start(){
		//GRAB ELEVATOR REF
		elevator = GameObject.Find("ElevatorPREFAB").transform.GetComponent<elevControlMod>();
		//SET ANIMATION CLIPS
		openAnim = transform.GetComponent<Animation>().GetClip( "OpenDoors" );
		closeAnim = transform.GetComponent<Animation>().GetClip( "CloseDoors" );	
	}

	/// <summary>
	/// Opens the Hall Frame Doors.
	/// </summary>
	public void OpenDoor(){
		transform.GetComponent<Animation>().clip = openAnim;
		transform.GetComponent<Animation>().Play();
	}

	/// <summary>
	/// Closes the Hall Frame Door.
	/// </summary>
	public void CloseDoor(){
		transform.GetComponent<Animation>().clip = closeAnim;
		transform.GetComponent<Animation>().Play();
	}

	/// <summary>
	/// Turn Call Button light ON/OFF .
	public void CallButtonLight( bool turnOn ){
		//CHANGE BUTTON OBJECT MATERIAL
		if( turnOn )	
			callButtonLight.GetComponent<Renderer>().material = elevator.buttonOnMat;
		else
			callButtonLight.GetComponent<Renderer>().material  = elevator.buttonOffMat;
	}

	public void CallElevator(){
		CallButtonLight( true );
		elevator.MoveElevator( floor, true );
	}
}
