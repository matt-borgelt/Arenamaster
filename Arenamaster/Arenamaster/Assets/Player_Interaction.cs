using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour {

	public Player_health playerHealth;
	public npc_health npcHealth;
	public item item;
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
	public bool inventorySlotSelect;
	//public GUISkin mySkin;

	public RaycastHit hit;

	//public class selected{
	//	public GameObject selected;

	//};

	void Start () {
		menu = false;
		OnPlayerPhysicalInventory = false;
		inventorySlotSelect = false;
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
			if(parent=="item"){ item = selected.GetComponent<item>(); }

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
			item.inventory_spot = inventory.items.Count-1;
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
		float x = 10.0F; //starting x position of first tile
		float y = 20.0F; //starting y position of first tile
		float offset = 5.0F; //distance between tiles
		float tileSize = 25.0F; //dimension of square tile

		// 1st column
		GUI.Button (new Rect (x, y + offset, tileSize, tileSize), (inventory.items.Find ((item obj) => (obj.inventory_spot == 0))) ? (inventory.items.Find ((item obj) => (obj.inventory_spot == 0))).name : " ");
		GUI.Button (new Rect (x, y+(1*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==1))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==1))).name : " "  );
		GUI.Button (new Rect (x, y+(2*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==2))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==2))).name : " "  );
		GUI.Button (new Rect (x, y+(3*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==3))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==3))).name : " "  );

		//2nd column
		GUI.Button (new Rect (x+(1*tileSize+offset), y+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==4))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==4))).name : " "  );
		GUI.Button (new Rect (x+(1*tileSize+offset), y+(1*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==5))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==5))).name : " "  );
		GUI.Button (new Rect (x+(1*tileSize+offset), y+(2*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==6))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==6))).name : " "  );
		GUI.Button (new Rect (x+(1*tileSize+offset), y+(3*tileSize)+offset, tileSize, tileSize), (inventory.items.Find((item obj) => (obj.inventory_spot==7))) ? (inventory.items.Find((item obj) => (obj.inventory_spot==7))).name : " "  );
	

	}

	void ContextMenuNPC(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=npcHealth.examine;
		}

		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			console.text="You attack : " + selected.gameObject.name; 
			npcHealth.current = npcHealth.current - 10;
		}

		if (GUI.Button (new Rect (10, 60+1, 100, 20), "Check-health")) {
			console.text=selected.gameObject.name + "'s `hp is : " + npcHealth.current.ToString();
		}

		if (GUI.Button (new Rect (10, 80+1, 100, 20), "Talk-to")) {
			console.text="npc dialog here...";
		}
	}
}
