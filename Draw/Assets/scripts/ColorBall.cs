using UnityEngine;
using System.Collections;

public class ColorBall : MonoBehaviour {
	
	float force = 250f;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(new Vector3(force , 0f ,0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
