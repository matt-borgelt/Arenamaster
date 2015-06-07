using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class slider_script : MonoBehaviour {

	public Player_health hp;
	public Player_Movement movement;

	public Slider sliderHp;
	public Slider sliderMovement;
	
	void Start () {
		sliderHp.maxValue = hp.cap;
		sliderHp.minValue = 0;
		sliderHp.value = hp.current;

		sliderMovement.maxValue = movement.cap;
		sliderMovement.minValue = 0;
		sliderMovement.value = movement.energyCurrent;
	}

	void Update () {
		sliderHp.maxValue = hp.cap;
		sliderHp.minValue = 0;
		sliderHp.value = hp.current;
		
		sliderMovement.maxValue = movement.cap;
		sliderMovement.minValue = 0;
		sliderMovement.value = movement.energyCurrent;
	}
}
