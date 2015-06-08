using UnityEngine;
using System.Collections;

public class elevHallFrameControllerMod : MonoBehaviour {


	public		int						floor;
	public		Transform			callButtonLight;		
	public		Transform			HallLedPanel;
	public		string					elevTag			= "elev01";

	private	SimpleElevator			elevator;
	private	AnimationClip	openAnim,
												closeAnim;

	void Start(){
		//GRAB ELEVATOR REF
		elevator = GameObject.Find("ElevatorPREFAB").transform.GetComponent<SimpleElevator>();
		//SET ANIMATION CLIPS
		openAnim = transform.animation.GetClip( "OpenDoors" );
		closeAnim = transform.animation.GetClip( "CloseDoors" );	
	}

	/// <summary>
	/// Opens the Hall Frame Doors.
	/// </summary>
	public void OpenDoor(){
		transform.animation.clip = openAnim;
		transform.animation.Play();
	}

	/// <summary>
	/// Closes the Hall Frame Door.
	/// </summary>
	public void CloseDoor(){
		transform.animation.clip = closeAnim;
		transform.animation.Play();
	}

	/// <summary>
	/// Turn Call Button light ON/OFF .
	public void CallButtonLight( bool turnOn ){
		//CHANGE BUTTON OBJECT MATERIAL
		if( turnOn )	
			callButtonLight.renderer.material = elevator.buttonOnMat;
		else
			callButtonLight.renderer.material  = elevator.buttonOffMat;
	}

	public void CallElevator(){
		CallButtonLight( true );
	}
}
