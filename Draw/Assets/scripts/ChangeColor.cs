using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{


    private Color[] colors_to_Choose_From;
    // Use this for initialization
    void Start()
    {
        colors_to_Choose_From = new Color[] { Color.yellow, Color.blue, Color.red };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {

        Color my_Color = colors_to_Choose_From[Random.Range(0, colors_to_Choose_From.Length)];
        GetComponent<Renderer>().material.color = my_Color;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.black;

    }
}
