using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class container_script : MonoBehaviour {
	//public item item;
	public List<item_script> items;
	public string examine;



	public bool[] slot;

	void Start () {
		items = new List<item_script>();


		slot = new bool[] {false, false, false, false,
			false, false, false, false,
			false, false, false, false,
			false, false, false, false,
		};

		//items.Add (pop.CreateGameObject ("redbrick"));

	}
	

	void Update () {
	
	}
}
