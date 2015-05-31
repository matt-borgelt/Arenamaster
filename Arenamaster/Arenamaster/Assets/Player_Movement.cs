using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour {
	
	public Player_health playerHealth;
	public Slider Stamina_Slider;


	public float current_speed;		/* The speed the player is moving at in this frame. */
	public float base_speed;			/* base movment speed */
	public float adjusted_speed;      /* base movment speed with all other variables factored in */
	public float penalty;

	public float jump_speed; 
	public float gravity;
	private Vector3 moveDirection;

	public float cap;
	public float current; 			/* the current stamina value  */
	public float drainRate;	/* the rate at which stamina is drained while sprinting */
	public float sprintMultiplier;	/* the factor by which speed is multiplied during sprint. */
	public float recovery; 	/* stamina points recovered during rest frame. */
	public float staminaThreshhold; /* can't run below this value */

	void Start(){
		base_speed = 5.0F; //your normal base speed
		current_speed = base_speed; //the current speed starts out as the player's normal base speed
		drainRate = 1.0F; 
		sprintMultiplier = 2.0F; 
		recovery = 1.0F; 
		staminaThreshhold = 1.0F; 

		jump_speed = 8.0F;
		gravity = 20.0F;
		moveDirection = Vector3.zero;

		cap = 120F; //how much total stamina you have
		current = cap;

		Stamina_Slider.maxValue = cap;
		Stamina_Slider.value = current;

		penalty = playerHealth.lowHealthSpeedPenalty;
		adjusted_speed = base_speed - penalty; 
		//Debug.Log (speedBase);

	}

	
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();

		penalty = playerHealth.lowHealthSpeedPenalty;
		adjusted_speed = base_speed - penalty; 
		//Debug.Log (speedBase); //prints what's in parentheses out to the console

		if ( (Input.GetKey (KeyCode.LeftShift)) && !(controller.velocity.magnitude==0)  ) {
			if (current > staminaThreshhold){
				/* While leftshift is held down and if the player still has stamina left,
				 * player speed is doubled and stamina decreases.*/
				current_speed = adjusted_speed * sprintMultiplier;  //=speedbase * staminamultiplier
				current-=drainRate;
				Stamina_Slider.value = current;
			}
			else{ /* if stamina is 0 speed is returned to normal. */
				current_speed = adjusted_speed; //=speedbase;
			}
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			current_speed = adjusted_speed;
		}
		if( !(Input.GetKey (KeyCode.LeftShift)) && (current < cap) ) {
			/* The stamina recharges while leftshift is not held down, 
			 * but it doesn't recharge beyond the player's max stamina capacity. */
			current+=recovery;
			Stamina_Slider.value = current;
		}


		if (controller.isGrounded) {
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= current_speed;

			if (Input.GetButton("Jump")){
				moveDirection.y = jump_speed;
			}
		}
		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}
}
