using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_physical_inv : MonoBehaviour {
	//public List<item> items = new List<item> ();
	public List<item> items;
	public int cap; // max number of items player can carry

	// Use this for initialization
	void Start () {
		items = new List<item>();
		cap = 3;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
