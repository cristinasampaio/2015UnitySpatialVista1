using UnityEngine;
using System.Collections;


/* Character Movement
 * 
 * Characters should have clean forward movement
 * slope should affect movement
 * Characters should turn faster the longer they turn
 * Collision should slow and move back characters so they can't walk through walls.
 * 
 * Movement should also slow if you are not looking where you are going, slightly at first
 * and then more the longer you look away, or the farther */
public class MovePlayer : MonoBehaviour {
	private float speed = 20.0f;
	private float roatationSpeed = 32.0f;
	private float jumpSpeed = 8.0f;
	private float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float turnDirection = 0.0f;
	private float turnTime = 0.0f;
	
	private RaycastHit hit;

	//Make sure character controller is working
	void Start() {
		CharacterController controller = GetComponent<CharacterController>();
		controller.enabled = true;
	}

	/* Update handels all the movement for the character.
	 * Checks which direction keys (or stick) is being pushed.
	 * IF Up or Down, the character will move forward of back.
	 * IF Left or Right, the character will turn which direction they are "facing" */
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		//Check if the character is on the ground, for jumping if we ever want that.
		if (controller.isGrounded) {

			//This grabs the direction you are pushing the stick/keys
			float Horizontal = Input.GetAxis("Horizontal");
			float Vertical = Input.GetAxis("Vertical");

			//Forward movement is done with a vector.
			Vector3 Go = new Vector3(0,0,Vertical);
			moveDirection = Go;
			
			float slope = 0.0f;
			//Check size of character for the 4th parameter
			/*if(controller.Raycast(transform.position, Vector3.down, hit, 10)) {
				slope = hit.normal;
			}*/
			
			moveDirection = transform.TransformDirection(moveDirection);
			//moveDirection += Mathf.sin((double)slope);
			moveDirection *= speed;
			
			/*These if statements set up a system where
			 * if the character has been turning in a direction
			 * they turn a bit faster every second*/
			/*if (turnDirection > 0.0f && Horizontal > 0.0f) {
				turnTime += Time.deltaTime;
				if (turnTime > 2 && turnDirection < 20.0f) {
					turnDirection += turnTime;
					turnTime -= turnTime;
				}
				else {
					turnDirection = turnDirection;
				}
			}
			else if(turnDirection < 0f && Horizontal < 0f) {
				turnTime += Time.deltaTime;
				if (turnTime > 2 && turnDirection > -40.0f) {
					turnDirection -= turnTime;
					turnTime -= turnTime;
				}
				else {
					turnDirection = turnDirection;
				}
			}
			else {
				turnDirection = Horizontal;
				//turnTime = 0;
			}*/
			turnDirection = Horizontal;
			turnDirection *=  roatationSpeed;


			//The start of jumping if we ever want it.
			//if (Input.GetButton("Jump"))
			//moveDirection.y = jumpSpeed;
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		transform.Rotate (0, turnDirection * Time.deltaTime, 0);
	}
}