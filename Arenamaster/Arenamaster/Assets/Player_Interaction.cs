using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour {

	public Player_health playerHealth;

	public Vector3 mouseTarget;
	public Camera cameraObject;
	public Ray sightLine;

	public Text text;
	public Text console;
	public GameObject selected;
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
		//RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			// the object identified by hit.transform was clicked
			text.text = ( hit.transform.ToString());
		}

		if (Input.GetMouseButtonDown (1)) {

			selected = GameObject.Find(hit.collider.gameObject.name);   //hit.transform.gameObject
			position = Input.mousePosition;
			menu=true;
		}
		if (Input.GetMouseButtonUp (1)) {
			menu=false;
			selected = null;
		}
	}

	void OnGUI(){
		GUI.skin = mySkin;
		if (menu){
			string[] header = selected.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);


			if(selected.gameObject.name=="Tim tom"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}

			else if(selected.gameObject.name=="Player"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}
			if(selected.gameObject.name=="Terrain"){
				window = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuGeneric, header[0] );
			}
		}
	}

	void ContextMenuGeneric(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			if(selected.gameObject.name == "Terrain"){
				console.text="That is the ground.";
			}
		}
	}

	void ContextMenuNPC(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			if(selected.gameObject.name == "Player"){
				console.text="That is you.";
			}
			if(selected.transform.gameObject.name=="Tim tom"){
				console.text="Your opponent, the enemy.";
			}
		}

		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			if(selected.gameObject.name=="Tim tom"){
				console.text="You attack the NPC.";
			}
			if(selected.gameObject.name=="Player"){
				console.text="You hit yourself.";
				playerHealth.current = playerHealth.current - 10;
			}
		}
		GUI.DragWindow(new Rect(0, 0, 10000, 10000));

	}
}
