using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour {
	
	public Player_health playerHealth;
	//public player_stats stats;
	public Slider slider;
	public Text console;

	public float jump_speed; 
	public float gravity;
	private Vector3 moveDirection;

	public bool walking, running;

	public float walkingSpeed;
	public float runningSpeed;
	public float sprintingSpeed;

	public float modeOfMovement;

	//public float base_speed;			/* base movment speed */
	public float adjusted_speed;      /* base movment speed with all other variables factored in */
	public float penalty;



	public float cap;
	public float energyCurrent; 			/* the current stamina value  */
	public float drainRate;	/* the rate at which stamina is drained while sprinting */
	public float sprintMultiplier;	/* the factor by which speed is multiplied during sprint. */
	public float recovery; 	/* stamina points recovered during rest frame. */
	public float cannotRunBelowThis; /* can't run below this value */

	void Awake(){
		walking = true;
		running = false;
		energyCurrent = cap;
	}

	void Start(){
		jump_speed = 8.0F;
		gravity = 20.0F;
		moveDirection = Vector3.zero;

		walkingSpeed = 2.0F;
		runningSpeed = 7.0F;
		
		cap = 100.0F;
		
		modeOfMovement = walkingSpeed;
		

		
		cannotRunBelowThis = 1.0F;
		
		slider.maxValue = cap;
		slider.minValue = 0;
		slider.value = energyCurrent;
	}

	
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();


		if (energyCurrent < cap) {
			energyCurrent += 1.0F;
			//slider.value += 1.0F;
		}

		if ((Input.GetKey (KeyCode.LeftShift))) {
			if (energyCurrent > cannotRunBelowThis) {
				modeOfMovement = runningSpeed;
				energyCurrent -= 1.0F;
				slider.value -= 1.0F;
			}else{
				modeOfMovement = walkingSpeed;
			
			}
		} 
		if  ((Input.GetKeyUp(KeyCode.LeftShift))){
			modeOfMovement = walkingSpeed;
	

		}

		console.text = energyCurrent.ToString();





		//slider.value = energyCurrent;



		if (controller.isGrounded) {
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= modeOfMovement;

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
