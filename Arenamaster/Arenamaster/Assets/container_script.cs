using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class container_script : MonoBehaviour {
	//public item item;

	public item_populate itemDb;

	public List<item_script> items;
	public string examine;

	public List<string> keys; //list of bid's to fetch from xml


	public bool[] slot;

	void Start () {
		//itemsInContainer = new List<item_script>();
		//fetchContents = new List<string> ();

		slot = new bool[] {false, false, false, false,
			false, false, false, false,
			false, false, false, false,
			false, false, false, false,
		};

		/*for (int i=0; i< keys.Count; i++) {


			GameObject preset = new GameObject();

			if(itemDb.itemsDict.TryGetValue( keys[0], out preset)) {
				Debug.Log("didn't find item in dictionary.");
			}
			Debug.Log("preset: " + preset);
			//GameObject newitem = Instantiate(Resources.Load("item")) as GameObject;

			//newitem.name = "halloo";
			//newitem.GetComponent<item_script>().bid = preset.GetComponent<item_script>().bid;
			//item_script preset = itemDb.items[ fetchContents[0] ];
			//items.Add( newitem        );
			//item_script newItem = new item_script();
		
			//GameObject newitem = Instantiate(   ) as GameObject;



			//items.Add( newItem    );
		}*/


		//items.Add (pop.CreateGameObject ("redbrick"));

	}
	

	void Update () {
	
	}
}
