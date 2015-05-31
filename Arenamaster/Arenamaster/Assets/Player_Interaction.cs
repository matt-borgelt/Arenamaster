using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour {

	public Player_health playerHealth;
	public npc_health npcHealth;

	public Vector3 mouseTarget;
	public Camera cameraObject;
	public Ray sightLine;

	public Text text;
	public Text console;

	public GameObject selected;
	public string parent;

	public bool menu;
	Vector3 position;
	public Rect window;
	public GUISkin mySkin;

	public RaycastHit hit;

	void Start () {
		menu = false;
		selected = new GameObject();
	}

	void Update () {

		Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)){
			// the object identified by hit.transform was clicked

			string[] display = hit.transform.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);
			text.text = (display[0] + " : "  );
		}

		if (Input.GetMouseButtonDown (1)) {

			selected = GameObject.Find(hit.collider.gameObject.name);   //hit.transform.gameObject
			//npcHealth = selected.npcHealth;
			parent = selected.transform.parent.name.ToString();
			position = Input.mousePosition;
			menu=true;
		}
		if (Input.GetMouseButtonUp (1)) {
			menu=false;
			selected = null;
		}
	}

	void OnGUI(){
		//GUI.skin = mySkin;
		if (menu){
			string[] header = selected.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);


			if(parent=="npc"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}
			if(parent=="Player"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}
			if(selected.gameObject.name=="Terrain"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuGeneric, header[0] );
			}
		}
	}

	void ContextMenuGeneric(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			if(selected.gameObject.name == "Terrain"){console.text="Dirt.";}
		}
	}

	void ContextMenuNPC(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			if(selected.gameObject.name == "Player") {console.text="That is you.";}
			if(selected.gameObject.name == "Tim tom"){console.text="Your opponent, the enemy.";}
			if(selected.gameObject.name == "Donny")  {console.text="It's Donny, Tim-tom's friend.";}
		}

		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			if(selected.gameObject.name=="Tim tom"){console.text="You attack the NPC."; npcHealth.current = npcHealth.current - 10;}
			if(selected.gameObject.name=="Player"){
				console.text="You hit yourself.";
				playerHealth.current = playerHealth.current - 10;
			}
		}

		if (GUI.Button (new Rect (10, 60 + 1, 100, 20), "Check-health")) {
			if(selected.gameObject.name=="Tim tom"){console.text=npcHealth.current.ToString();}
		}
		GUI.DragWindow(new Rect(0, 0, 10000, 10000));

	}
}
