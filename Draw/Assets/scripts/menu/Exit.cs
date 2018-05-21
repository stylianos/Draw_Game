using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
      GetComponent<Renderer>().material.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.yellow;
		
	}
	
	void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.black;
		
	}

    void OnMouseUp()
    {
        Application.Quit();

    }


}
