using UnityEngine;
using System.Collections;

public class callBtnTriggerMod : MonoBehaviour {

	private	elevControlMod						elevContrl;
	private	elevHallFrameControllerMod		hallFrameContrl;

	void Start(){
		hallFrameContrl	= this.transform.parent.GetComponent<elevHallFrameControllerMod>();
		elevContrl		= GameObject.FindGameObjectWithTag( hallFrameContrl.elevTag ).transform.GetComponent<elevControlMod>(); 
	}

	void OnTriggerEnter( Collider other ){
		if( !elevContrl.isElevMoving ){//&& elevContrl.curFloorLevel !=  hallFrameContrl.floor ){
			elevContrl.newFloor = hallFrameContrl.floor;
			elevContrl.useCallBtn = true;
			hallFrameContrl.callButtonLight.renderer.material = elevContrl.buttonSelectorMat;
		}
	}

	void OnTriggerExit( Collider other ){
		elevContrl.useCallBtn = false;
		if( !elevContrl.isElevMoving )
			hallFrameContrl.callButtonLight.renderer.material = elevContrl.buttonOffMat;
	}
}