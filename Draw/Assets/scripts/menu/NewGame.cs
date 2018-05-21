using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

    GameObject UI_Logic;
	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.black;
        UI_Logic = GameObject.FindGameObjectWithTag("UI");
        Debug.Log("I found the UI" + UI_Logic.name);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
	}
	
	void OnMouseExit(){
		
	}

    private void OnMouseUp(){
        UI_Logic.GetComponent<ButtonsLogic>().New_Game();
    }

}
