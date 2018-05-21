using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{

    GameObject UI_Logic;
    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
        UI_Logic = GameObject.FindGameObjectWithTag("UI");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.cyan;

    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.black;

    }

    void OnMouseUp()
    {

        UI_Logic.GetComponent<ButtonsLogic>().Go_To_Credits();
    }

}
