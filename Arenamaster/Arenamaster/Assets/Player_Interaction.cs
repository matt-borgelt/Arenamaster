using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour {

	public Player_health playerHealth;
	public npc_health npcHealth;
	public testitemscript item;
	public player_physical_inv inventory;

	public Vector3 mouseTarget;
	public Camera cameraObject;
	public Ray sightLine;

	public Text text;
	public Text console;

	public GameObject selected;
	public string parent;

	public bool menu;
	Vector3 position;
	public Rect windowContextMenu;
	public Rect windowPhysicalInventory;
	public bool OnPlayerPhysicalInventory;
	//public GUISkin mySkin;

	public RaycastHit hit;

	//public class selected{
	//	public GameObject selected;

	//};

	void Start () {
		menu = false;
		OnPlayerPhysicalInventory = false;
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

			parent = selected.transform.parent.name.ToString();
			if(parent=="npc"){ npcHealth = selected.GetComponent<npc_health>(); }
			if(parent=="item"){ item = selected.GetComponent<testitemscript>(); }

			position = Input.mousePosition;
			menu=true;
		}
		if (Input.GetMouseButtonUp (1)) {
			menu=false;
			OnPlayerPhysicalInventory=false;
			selected = null;
		}
	}

	void OnGUI(){
		//GUI.skin = mySkin;
		if (menu){
			string[] header = selected.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);


			if(parent=="npc"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}
			if(parent=="Player"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuPlayer, header[0] );
			}
			if(parent=="Terrain"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuGeneric, header[0] );
			}
			if(parent=="item"){
				windowContextMenu = GUI.Window(0, new Rect(position.x, Screen.height-position.y, 200, 120), ContextMenuItem, header[0] );
			}
		}
		if (OnPlayerPhysicalInventory) {
			windowPhysicalInventory = GUI.Window(0, new Rect( (position.x)+200,Screen.height-position.y, 200, 150), PhysicalInventoryPlayer, "On your person" );

		}
	}

	void ContextMenuItem(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=item.examine;
		}
		if (GUI.Button (new Rect (10, 40+1, 100, 20), "Pick-up")) {
			inventory.items.Add(item);
			item.onGround=false;
		}
	}

	void ContextMenuGeneric(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			if(selected.gameObject.name == "Terrain"){console.text="Dirt.";}
		}
	}

	void ContextMenuPlayer(int windowId){
		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			console.text="You hit yourself.";
			playerHealth.current = playerHealth.current - 10;
		}
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
				console.text = "That is you.";
		}
		if (GUI.Button (new Rect (10, 60+1, 100, 20), "Check-health")) {
			console.text=playerHealth.current.ToString();
		}
		if (GUI.Button (new Rect (10, 80 + 1, 100, 20), "Physical inv")) {
			OnPlayerPhysicalInventory=true;
		}
	}

	void PhysicalInventoryPlayer(int windowId){
		/*GUI.Button (new Rect (10, 20, 20, 20), "");  GUI.Button (new Rect (30+1, 20, 20, 20), "");
		GUI.Button (new Rect (10, 40+1, 20, 20), "");						GUI.Button (new Rect (30+1, 40+1, 20, 20), "");
		GUI.Button (new Rect (10, 60+1, 20, 20), "");						GUI.Button (new Rect (30+1, 60+1, 20, 20), "");
		GUI.Button (new Rect (10, 80+1, 20, 20), "");						GUI.Button (new Rect (30+1, 80+1, 20, 20), "");
		GUI.Button (new Rect (10, 100+1, 20, 20), "");						GUI.Button (new Rect (30+1, 100+1, 20, 20), "");*/
		foreach(testitemscript item in inventory.items){
			GUI.Button(new Rect(10, 20, 20, 20), item.name);
		}

	}

	void ContextMenuNPC(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=npcHealth.examine;
		}

		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			console.text="You attack : " + selected.gameObject.name;; 
			npcHealth.current = npcHealth.current - 10;
		}

		if (GUI.Button (new Rect (10, 60+1, 100, 20), "Check-health")) {
			console.text=selected.gameObject.name + "'s hp is : " + npcHealth.current.ToString();
		}
	}
}
