using UnityEngine;
using System.Collections;

public class player_stats : MonoBehaviour {

	public float stat_volatile; 	// player's emotional change rate, range: -10.0F -- 10.0F
	public float stat_volatile_floor;
	public float stat_volatile_ceiling;


	public float stat_confident; 	// player's confidence rating, range: -100.0F -- 100.0F
	public float stat_confident_floor;
	public float stat_confident_ceiling;

	public float stat_mood;
	
	public bool stat_temp_exalted;
	public bool stat_temp_addicted;

	public bool stat_depraved; 
	public bool stat_chivalric;
	public bool stat_order; 
	

	void Start () {
		stat_volatile_floor = -10.0F;
		stat_volatile_ceiling = 10.0F;
		stat_volatile = 1.0F;

		stat_confident_floor = -100.0F;
		stat_confident_ceiling = 100.0F;
		stat_confident = 1.0F;


	}
	

	void Update () {
		if ((stat_volatile < stat_volatile_floor) || (stat_volatile > stat_volatile_ceiling)) {
			//your character has borderline personality disorder
		}

		if ((stat_confident < stat_confident_floor) || (stat_confident > stat_confident_ceiling)) {
			// your character has crippling confidence issue
		}

		// if (delta_confidence > 25) {exalted = true}
		// if (exalted) addiction=false;

		stat_mood = stat_mood + (stat_confident - stat_volatile);

	}
}
