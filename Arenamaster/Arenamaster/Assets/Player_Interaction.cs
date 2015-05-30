using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour {


	public Vector3 mouseTarget;
	public Camera cameraObject;
	public Ray sightLine;

	public Text text;
	public bool menu;
	Vector3 position;

	void Start () {

	}

	void Update () {

		Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			// the object identified by hit.transform was clicked
			text.text = ( hit.transform.ToString());
		}

		if (Input.GetMouseButtonDown (1)) {
			position = Input.mousePosition;
			menu=true;
		}
		if (Input.GetMouseButtonUp (1)) {
			menu=false;
		}
	}

	void OnGUI(){

		if (menu){
			string[] header = text.text.ToString().Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries);
			GUI.Box(new Rect(position.x,Screen.height-position.y, 120, 120), header[0]);

		}
	}
}
