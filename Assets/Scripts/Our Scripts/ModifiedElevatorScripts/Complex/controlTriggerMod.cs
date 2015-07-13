using UnityEngine;
using System.Collections;

public class controlTriggerMod : MonoBehaviour {

	private	elevControlMod		elevContrl;

	void Start(){
		elevContrl	= this.transform.parent.GetComponent<elevControlMod>();
	}

	void OnTriggerEnter( Collider other ){
		if( !elevContrl.isElevMoving ){
			elevContrl.useControls 	= true;
			elevContrl.prevBtn 		= elevContrl.curFloorLevel;
			elevContrl.SelectButtonOnTrigger( true );
		}
	}

	void OnTriggerExit( Collider other ){
		if( !elevContrl.isElevMoving ){
			elevContrl.useControls 	= false;
			elevContrl.SelectButtonOnTrigger( false );
		}
	}

}
