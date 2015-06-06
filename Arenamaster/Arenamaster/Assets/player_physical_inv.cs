using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_physical_inv : MonoBehaviour {

	public List<item_script> items;
	public int numOfSlots; 
	public bool[] slot;

	// Use this for initialization
	void Start () {

		numOfSlots = 16;

		items = new List<item_script>();


		slot = new bool[] {false, false, false, false,
									false, false, false, false,
									false, false, false, false,
									false, false, false, false,
		};

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
