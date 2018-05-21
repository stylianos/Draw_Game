using UnityEngine;
using System.Collections;

    using System.Collections.Generic;       //Allows us to use Lists. 

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    Object[] myMusic; // declare this as Object array
    int current_Track;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        myMusic = Resources.LoadAll("music", typeof(AudioClip));
        current_Track = 0;
        GetComponent<AudioSource>().clip = myMusic[current_Track] as AudioClip;
        GetComponent<AudioSource>().Play();



    }


    //Update is called every frame.
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            current_Track += 1;
            if (current_Track >= myMusic.Length - 1)
            {
                current_Track = 0;
            }
            GetComponent<AudioSource>().clip = myMusic[current_Track] as AudioClip;
            GetComponent<AudioSource>().Play();
        }
    }
}
