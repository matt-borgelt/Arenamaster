using UnityEngine;
using System.Collections;
using System;

public class item : MonoBehaviour {

	public string name_string;
	public string examine;
	public float weight;
	public bool onGround;
	public int inventory_spot;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		//onGround = true;
		//name_string = "";
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		inventory_spot = 0;
		//name_string = "X";
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround == false) {
			rend.enabled=false;
		}
	}
}
