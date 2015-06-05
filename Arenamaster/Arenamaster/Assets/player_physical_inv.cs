using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_physical_inv : MonoBehaviour {
	//public List<item> items = new List<item> ();
	public List<item> items;
	public int cap; // max number of items player can carry
	public bool[] slot_occupied;

	// Use this for initialization
	void Start () {
		items = new List<item>();

		slot_occupied = new bool[] {false, false, false, false,
			false, false, false, false};

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
