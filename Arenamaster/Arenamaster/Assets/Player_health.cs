using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_health : MonoBehaviour {

	public Slider Health_Slider;

	public float cap;		/* health cap */
	public  float current;	/* current health */
	public float playerWoundedAmount;		/* how many hp they are lacking */
	public static float damageTaken;

	public float lowHealthSpeedPenalty; /* if player is wounded, they are slower */
	public float lowHealthSpeedSlowdownAmount;

	void Start () {
		cap = 100.0F;
		current = 100.0F;
		lowHealthSpeedSlowdownAmount = 100.0F;
		playerWoundedAmount = cap - current;
		lowHealthSpeedPenalty = playerWoundedAmount / lowHealthSpeedSlowdownAmount;
		//damageTaken = -5; /* damageTaken = attackEffectiveness - defense */

		/* defense = hardiness lvl + armor rating + armor type level */
		/* attackEffectiveness = npc (or non-npc) attack lvl in whatever they're using + 
		 * weapon strength. Both these variables could be imported from another class */

		Health_Slider.maxValue = cap;
		Health_Slider.value = current;
	}

	void Update () {
		Health_Slider.maxValue = cap;
		Health_Slider.value = current;

		playerWoundedAmount = cap - current;
		lowHealthSpeedPenalty = playerWoundedAmount / lowHealthSpeedSlowdownAmount;
	}
}
