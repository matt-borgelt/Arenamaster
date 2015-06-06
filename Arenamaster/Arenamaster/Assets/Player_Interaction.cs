using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.IO;


public class Player_Interaction : MonoBehaviour {

	public Player_health playerHealth;
	public npc_script npc;
	public item_script item;
	public player_physical_inv inventory;
	public container_script container;
	public item_script selectedItemInInventory;

	public Vector3 mouseTarget;
	public Camera cameraObject;
	public Ray sightLine;

	public Text upperLeftCornerText;
	public Text console;

	public GameObject selected; /* the current item which is selected. */
	public string parent; /* the parent type of selected. */



	Vector3 position; // mouse position when clicked to bring up menu
	public Rect windowContextMenu;
	public Rect windowPhysicalInventory;
	public Rect windowDialog;
	public Rect windowContainer;
	public Rect windowInventoryItem;

	public bool onContextMenu;
	public bool onPlayerPhysicalInventory;
	public bool onDialogLaunch;
	public bool onContainerOpen; public bool onContainerItem;
	public bool onInventoryItem;

	public RaycastHit hit;
	

	void Start () {
		onContextMenu = false;
		onPlayerPhysicalInventory = false;
		//inventorySlotSelect = false;
		selected = new GameObject();
		onDialogLaunch = false;
		onContainerOpen = false;
		onInventoryItem = false;
		onContainerItem = false;

	}

	void Update () {

		Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)){
			// the object identified by hit.transform was clicked

			string[] display = hit.transform.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);
			upperLeftCornerText.text = (display[0] + " : "  );
		}

		if (Input.GetMouseButtonDown (1)) {

			selected = GameObject.Find(hit.collider.gameObject.name);   //hit.transform.gameObject

			parent = selected.transform.parent.name.ToString();
			if(parent=="npc"){ npc = selected.GetComponent<npc_script>(); }
			if(parent=="item"){ item = selected.GetComponent<item_script>(); }
			if(parent=="container"){ container = selected.GetComponent<container_script>();}

			position = Input.mousePosition;
			onContextMenu=true;
		}
		if (Input.GetMouseButtonUp (1)) {
			onContextMenu=false;
			onPlayerPhysicalInventory=false;
			onDialogLaunch=false;
			onContainerOpen=false;
			onInventoryItem=false;
			onContainerItem=false;
			selected = null;
		}
	}

	void OnGUI(){
		if (onContextMenu){
			string[] header = selected.ToString().Split(new string[] {"("}, System.StringSplitOptions.RemoveEmptyEntries);


			if(parent=="npc"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuNPC, header[0] );
			}
			if(parent=="Player"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuPlayer, header[0] );
			}
			/*if(parent=="Terrain"){
				windowContextMenu = GUI.Window(0, new Rect(position.x,Screen.height-position.y, 200, 120), ContextMenuGeneric, header[0] );
			}*/
			if(parent=="item"){
				windowContextMenu = GUI.Window(0, new Rect(position.x, Screen.height-position.y, 200, 120), ContextMenuItem, header[0] );
			}
			if(parent=="container"){
				windowContextMenu = GUI.Window(0, new Rect(position.x, Screen.height-position.y, 200, 120), ContextMenuContainer, header[0] );
			}
		}
		if (onPlayerPhysicalInventory) {
			windowPhysicalInventory = GUI.Window(0, new Rect( (position.x)+200,Screen.height-position.y, 200, 150), PhysicalInventoryPlayer, "On your person" );
		}
		if (onInventoryItem) {
			windowInventoryItem = GUI.Window(0, new Rect(position.x, Screen.height-position.y, 200, 120), ContextMenuInInventory, "item in inv" );
		}

		if (onDialogLaunch) {
			windowDialog = GUI.Window(0, new Rect( 100,100, 500, 200), DialogNPC, npc.name );
		}


		if (onContainerOpen) {
			windowContainer = GUI.Window(0, new Rect( (position.x)+200,Screen.height-position.y, 200, 150), ContainerInventory, container.name );
		}
		if (onContainerItem) {
			windowInventoryItem = GUI.Window(0, new Rect(position.x, Screen.height-position.y, 200, 120), ContextMenuInContainer, "item in box" );
		}
	}






	void ContextMenuContainer(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=container.examine;
		}
		if (GUI.Button (new Rect (10, 40+1, 100, 20), "Look-in")) {
			onContainerOpen=true;
		}
	}
	void ContainerInventory(int windowId){
		float x = 10.0F; //starting x position of first tile
		float y = 20.0F; //starting y position of first tile
		float offset = 5.0F; //distance between tiles
		float tileSize = 25.0F; //dimension of square tile
		
		if (GUI.Button (new Rect (x, y + offset, tileSize, tileSize), ((container.slot[0]) ? container.items[0].uid : " E"))) {
			if(container.slot[0]) {onContainerItem=true;}
		} 
	}
	void ContextMenuInContainer(int windowId){
		GUI.Button (new Rect (10, 20, 100, 20), "Hi");
	}






	void ContextMenuItem(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=item.examine;
		}
		if (GUI.Button (new Rect (10, 40+1, 100, 20), "Pick-up")) {
			//inventory.items.Add(item);
			inventory.items.Add(item);

			//item.inventory_spot = inventory.items.Count-1;
			//item.onGround=false;
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
			onPlayerPhysicalInventory=true;
		}
	}

	void PhysicalInventoryPlayer(int windowId){
		float x = 10.0F; //starting x position of first tile
		float y = 20.0F; //starting y position of first tile
		float offset = 5.0F; //distance between tiles
		float tileSize = 25.0F; //dimension of square tile

		// 1st column
		if (GUI.Button (new Rect (x, y + offset, tileSize, tileSize), ((inventory.slot [0]) ? inventory.items [0].uid : " E"))) {
			onInventoryItem=true;
			//selectedItemInInventory=inventory.items[0];
		}
	}
	void ContextMenuInInventory(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text="hi";
		}
	}









	void ContextMenuNPC(int windowId){
		if (GUI.Button (new Rect (10, 20, 100, 20), "Examine")) {
			console.text=npc.examine;
		}

		if(GUI.Button (new Rect (10, 40+1, 100, 20), "Attack")){
			console.text="You attack : " + selected.gameObject.name; 
			npc.current = npc.current - 10;
		}

		if (GUI.Button (new Rect (10, 60+1, 100, 20), "Check-health")) {
			console.text=selected.gameObject.name + "'s `hp is : " + npc.current.ToString();
		}

		if (GUI.Button (new Rect (10, 80+1, 100, 20), "Talk-to")) {
			//console.text="npc dialog here...";
			onDialogLaunch=true;
		}
	}

	void DialogNPC(int windowId){

		GUI.Label (new Rect (10, 20, 500, 200), npc.dialog[npc.currentLine]);
		if (GUI.Button (new Rect (10, 60, 100, 20), ((npc.currentResp < (npc.numOfResps )) ? (npc.responses[npc.currentResp]) : "( ... ) ") )) {
			npc.currentResp++;
			if(npc.currentLine< (npc.numOfLines-1) ){
				npc.currentLine++;
			}
			if(npc.currentLine >= npc.numOfLines){
				onDialogLaunch=false;
			}
		}

	}




}
