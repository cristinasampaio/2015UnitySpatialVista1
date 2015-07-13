using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class elevControlMod : MonoBehaviour {

	public  	Material 			buttonOnMat, 
											buttonOffMat, 
											buttonSelectorMat;
	public  	Material			ledMat;
	public  	Transform		ledPanel;
	public  	float				ledMatSwitchDelay 		= .5f;
	private	Material[]		ledMatsArray;

    private 	List<Transform>		buttonLightList;
	private 	GameObject	hallFrame;
	private	List<Texture>			texturesList;
	private	AnimationClip		openAnim,
													closeAnim;

	public  	Transform			btnLightGroup;
	public  	string					hallFrameTag 			= "elev01hallFrame";

	public  	int						curFloorLevel				= 0;
	public  	float					timeBtwnFloors			= 3;
	public  	bool						doorOpen;
	public  	bool						waitForFixedUpdate	= true; //FIXES JITTER WHEN PLAYER IS ON MOVING ELEVATOR
	[HideInInspector]
	public  	bool						useControls,
												useCallBtn,	
												isElevMoving;
	[HideInInspector]
	public  	int						prevBtn,
												newFloor;
	private	Transform			elevator;

	/// <summary>
	/// returns the floor level to move the elevator to. 
	/// A Value of 13 will return the current floor. 
	/// Anything greater than 13 will return one floor lower. (14 is actually 13)
	/// </summary>
	/// <returns>The floor for the elevator to go to.</returns>
	/// <param name="floor">Floor.</param>
	private int floorCheck( int floor ){
		if( floor == 13 )
			return curFloorLevel;
		if( floor > 12 )
			floor -= 1;
		return floor;
	}

	void Start () {
		//TRANSFORM REFERENCE
		elevator = this.transform;

		//BUTTON LIGHTS OBJECT LIST, POPULATE LIGHTS OBJECT LIST, SORT LIGHTS OBJECT LIST
		buttonLightList = new List<Transform>();
		foreach ( Transform btnLight in btnLightGroup ){ 
			buttonLightList.Add( btnLight );
		}
		buttonLightList = buttonLightList.OrderBy( Transform=>Transform.name ).ToList();


		//HALL FRAMES OBJECT LIST, POPULATE FRAMES OBJECT LIST, SORT FRAMES OBJECT LIST
		hallFrame = GameObject.FindGameObjectWithTag("elev01hallFrame");

		//LED PANEL TEXTURES LIST, POPULATE TEXTURES LIST, SORT THE LIST
		texturesList = new List<Texture>();
		foreach ( Texture tex in Resources.LoadAll( "LEDPanelTextures" ) ){ 
			texturesList.Add( tex );
		}
		texturesList = texturesList.OrderBy( Texture=>Texture.name ).ToList();

		//SET ANIMATION CLIPS
		openAnim = transform.animation.GetClip( "OpenDoors" );
		closeAnim = transform.animation.GetClip( "CloseDoors" );	

		//ASSIGN LED MATERIALS TO ELEVATOR AND HALL FRAMES, THEN SET LED FLOOR DISPLAY & ELEVATOR TO CURRENT FLOOR
		ledMatsArray = new Material[2];
		ledMatsArray[0] = ledPanel.renderer.material;
		ledMatsArray[1] = ledMat;
		ledPanel.renderer.materials = ledMatsArray;
		hallFrame.GetComponent<elevHallFrameControllerMod>().HallLedPanel.renderer.materials = ledMatsArray;
		LEDPanel( curFloorLevel );
		elevator.position = hallFrame.transform.position;

		//SET DOOR OPEN/CLOSE
		if ( doorOpen ) {
			elevator.animation.clip = openAnim;
			elevator.animation[openAnim.name].time =openAnim.length;
			elevator.animation.Play();
			hallFrame.GetComponent<elevHallFrameController>().animation.clip = openAnim;
			hallFrame.GetComponent<elevHallFrameController>().animation[openAnim.name].time = openAnim.length;
			hallFrame.GetComponent<elevHallFrameController>().animation.Play();
			}
		}

	/// <summary>
	/// Buttons the select.
	/// </summary>
	/// <param name="buttonNum">Button number.</param>
	public void ButtonSelect( int buttonNum ){
		if( buttonNum == 13 && prevBtn == 12 ){
			buttonNum = 14;
			newFloor = 14;
		}
		if( buttonNum == 13 && prevBtn == 14 ){
			buttonNum = 12;
			newFloor = 12;
		}
		if(buttonNum > 21 ){
			buttonNum = 0;
			newFloor	=0;
		}
		if(buttonNum < 0 ){
			buttonNum = 21;
			newFloor	=21;
		}
		if( buttonNum > 12 )
			buttonNum -= 1;
		if (newFloor == 13)
			newFloor = buttonNum;

		var selectedBtn = buttonLightList[ buttonNum ];
		var oldMat 		= selectedBtn.renderer.material;

		buttonLightList[ prevBtn  ].renderer.material = oldMat;
		selectedBtn.renderer.material = buttonSelectorMat;
		prevBtn =  buttonNum ;
	}

	/// <summary>
	/// Finds the button to to highlight when player enters the trigger for the elevator control panel.
	/// </summary>
	/// <param name="turnOn">If set to <c>true</c> turn on.</param>
	public void SelectButtonOnTrigger( bool turnOn ){
		if( curFloorLevel <= 12 )
			newFloor = curFloorLevel ;
		else
			newFloor = curFloorLevel + 1;

		if( turnOn )
			buttonLightList[ curFloorLevel ].renderer.material = buttonSelectorMat;
		else
			buttonLightList[ prevBtn ].renderer.material = buttonOffMat;
	}

	/// <summary>
	/// Presses highlighted button on the elevator panel.
	/// </summary>
	/// <param name="buttonNum">Button number.</param>
	public void PressButton( int buttonNum ){
		switch ( buttonNum ) {
		case 20:
			ButtonHelpLight( true );
			break;
		case 21:
			ButtonOpenLight( true );
			//OPEN THE DOOR, IF IT IS CLOSED
			if( !doorOpen )
				OpenDoor( curFloorLevel );
			break;
			
		default:
			break;
		}
	}

	/// <summary>
	/// Turn Button light ON/OFF for Floor Buttons.
	/// NOTE: USE FLOOR NUMBERS, 0 = Basement!
	/// </summary>
	/// <param name="buttonNum">Button number (THE FLOOR NUMBER).</param>
	/// <param name="turnOn">If set to <c>true</c> turn on.</param>
	void ButtonFloorLight(  int buttonNum, bool turnOn  ){
		//SAFETY CHECK
	
		//CHANGE BUTTON OBJECT MATERIAL
		if( turnOn )
			buttonLightList[buttonNum].renderer.material = buttonOnMat;
		else
			buttonLightList[buttonNum].renderer.material = buttonOffMat;

	}

	/// <summary>
	/// Help Button light ON/OFF.
	/// </summary>
	void ButtonHelpLight( bool turnOn ){
		if( turnOn )
			buttonLightList[19].renderer.material = buttonOnMat;
		else
			buttonLightList[19].renderer.material = buttonOffMat;;
	}


	/// <summary>
	/// Open Button light ON/OFF.
	/// </summary>
	void ButtonOpenLight( bool turnOn ){
		if( turnOn )
			buttonLightList[20].renderer.material = buttonOnMat;
		else
			buttonLightList[20].renderer.material = buttonOffMat;
	}


	/// <summary>
	/// Switch LED display to this number
	/// NOTE: USE FLOOR NUMBER
	/// </summary>
	/// <param name="newFlooNum">New Floor number.</param>
	void LEDPanel( int newFloorNum ){
		//SAFETY CHECK!
		StartCoroutine( LEDPanelSwitch(  newFloorNum ) );
	}
	/// <summary>
	/// Switch LED display from current floor to new floor incrementally
	/// </summary>
	/// <param name="newFloorNum">New floor number.</param>
	/// <param name="floorTime">Time between floors.</param>
	void LEDPanel( int newFloorNum, float floorTime ){
		
		StartCoroutine( LEDPanelSwitch(  newFloorNum, floorTime ) );
	}

	/// <summary>
	/// Switch LED display to this number.
	/// </summary>
	/// <param name="newFloorNum">Floor number.</param>
	IEnumerator LEDPanelSwitch( int newFloorNum ){
		//SWITCH TO BLANK LED TEXTRES
		ledMat.SetTexture( "_MainTex",		texturesList[ texturesList.Count-2 ] );
		ledMat.SetTexture( "_Illum",			texturesList[ texturesList.Count-1 ] );
		yield return new WaitForSeconds( ledMatSwitchDelay );

		//CONVERT FLOOR NUMBER TO LIST INDEXES
		int illume 	= ( newFloorNum * 2 ) +1;
		int diff		= illume - 1;
		
		//CHANGE LED MATERIAL TEXTURES
		ledMat.SetTexture( "_MainTex", texturesList[diff] );
		ledMat.SetTexture( "_Illum", texturesList[illume] );

		//TURN BUTTON LIGHT OFF
		ButtonFloorLight( newFloorNum, false );

		curFloorLevel = newFloorNum;
	}

	/// <summary>
	/// Switch LED display from current floor to new floor incrementally 
	/// </summary>
	/// <param name="newfloorNum">New Floor number.</param>
	/// <param name="floorTime">Time between Floors.</param>
	IEnumerator LEDPanelSwitch( int newFloorNum, float floorTime ){
		//UP OR DOWN
		int floorIncrement = ( newFloorNum < curFloorLevel ) ? -1 : 1;

		while ( curFloorLevel != newFloorNum ) {
			//SWITCH TO BLANK LED TEXTRES
			ledMat.SetTexture( "_MainTex",		texturesList[ texturesList.Count-2 ] );
			ledMat.SetTexture( "_Illum",			texturesList[ texturesList.Count-1 ] );
			yield return new WaitForSeconds( ledMatSwitchDelay );

			//CONVERT FLOOR NUMBER TO LIST INDEXES
			int illume 	= ( (curFloorLevel + floorIncrement ) * 2 ) + floorIncrement;
			if( floorIncrement < 0 )
				illume = illume + 2;
			int diff = illume - 1;

			//CHANGE LED MATERIAL TEXTURES
			ledMat.SetTexture( "_MainTex", texturesList[diff] );
			ledMat.SetTexture( "_Illum", texturesList[illume] );
			yield return new WaitForSeconds( floorTime - ledMatSwitchDelay );

			curFloorLevel += floorIncrement;
		}
		//TURN BUTTON LIGHT OFF
		ButtonFloorLight( newFloorNum, false );
	}

	/// <summary>
	/// Opens the elevator door only.
	/// </summary>
	void OpenDoor(){
		transform.animation.clip = openAnim;
		transform.animation.Play();
		doorOpen = true;
	}
	/// <summary>
	/// Opens the elevator & hall door.
	/// </summary>
	/// <param name="floor">Floor.</param>
	void OpenDoor( int floor ){
		transform.animation.clip = openAnim;
		transform.animation.Play();
		hallFrame.GetComponent<elevHallFrameControllerMod>().OpenDoor();
		doorOpen = true;
	}

	/// <summary>
	/// Closes the elevator door only.
	/// </summary>
	void CloseDoor(){
		transform.animation.clip = closeAnim;
		transform.animation.Play();
		doorOpen = false;
	}
	/// <summary>
	/// Closes the elevator & hall door.
	/// </summary>
	/// <param name="floor">Floor.</param>
	void CloseDoor( int floor ){
		transform.animation.clip = closeAnim;
		transform.animation.Play();
		hallFrame.GetComponent<elevHallFrameControllerMod>().CloseDoor();
		doorOpen = false;
	}

	/// <summary>
	/// Moves the elevator to the specified floor.
	/// </summary>
	/// <param name="moveToFloor">Move to floor.</param>
	public void MoveElevator( int moveToFloor , bool callElevator){
		return;
	}

	/// <summary>
	/// Moves the elevator.
	/// </summary>
	/// <param name="startPos">Start position.</param>
	/// <param name="endPos">End position.</param>
	
	void Update () {
		//USE THE HALL FRAME CALL BUTTON > TRIGGERED FROM callBtnTrigger.cs
		if( useCallBtn ){
			if(Input.GetKeyDown(KeyCode.E))
			{
				hallFrame.GetComponent<elevHallFrameControllerMod>().OpenDoor ();
			}

		}

		//USE THE ELEVATOR CONTROL PANEL > TRIGGERED FROM  controlTrigger.cs
		if( useControls ){

			//HIGHLIGHT SELECTION UP/DOWN
			if( Input.GetKeyDown( KeyCode.R ) ){
				newFloor ++;
				ButtonSelect( newFloor );
			}
			if( Input.GetKeyDown( KeyCode.F ) ){
				newFloor --;
				ButtonSelect( newFloor );
			}

			//SELECT THE HIGHLIGHTED BUTTON
			if(Input.GetKeyDown(KeyCode.E)){
				hallFrame.GetComponent<elevHallFrameControllerMod>().CloseDoor();
				CloseDoor();
				//DO OUR STUFF HERE
			}	
		}
	}
}

