using UnityEngine;
using System.Collections;
using System;

public class item_script : MonoBehaviour {

	public string bid; //base id from xml
	public string uid; // unique id of instanced item (determined at run-time or item creation)

	public string itemname;
	public string type;
	public string examine;
	//public float weight;

	public bool onGround;

	//public Renderer rend;


	public bool isWearable;


	void Start () {

	}
	

	void Update () {
		//if (onGround == false) {
			//rend.enabled=false;
		//}
	}
}
