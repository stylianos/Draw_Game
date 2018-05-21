using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class WinLogic: MonoBehaviour {
	
	GameObject      score;
	GameObject      time_left;
    GameObject      currentCanvas;
    Texture2D       original; 
    UnityAction     isPaintingListener;
    
    public int percentage_ToWin;
    public int time_ToWinLevel;

    private string  next_Level_Name;
    private float   time_left_float;
    private float   original_difference;
    private bool    star_Timer;
    private bool    level_AlreadyLoading;

    private void Awake()
    {
        isPaintingListener = new UnityAction(calculate_percentage);
    }

    private void OnEnable()
    {
        
    }
    // Use this for initialization
    void Start () {

        EventManager.StartListening("isPainting", isPaintingListener);
        currentCanvas           = GameObject.FindGameObjectWithTag("Canvas");
		score                   = GameObject.Find("percentage");
		time_left               = GameObject.Find("time");
        time_left_float         = time_ToWinLevel;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_1":
                {
                    Debug.Log("Loaded Level 1");
                    original = Resources.Load("level_1_original") as Texture2D;
                    currentCanvas.GetComponent<Renderer>().material.mainTexture = Resources.Load("level_1_draw") as Texture2D;
                    original_difference = calculate_difference();
                    star_Timer = true;
                    break;
                }
            case "Level_2":
                {
                    Debug.Log("Loaded Level 2");
                    original = Resources.Load("level_2_original") as Texture2D;
                    currentCanvas.GetComponent<Renderer>().material.mainTexture = Resources.Load("level_2_draw") as Texture2D;
                    original_difference = calculate_difference();
                    star_Timer = true;
                    break;
                }
            case "Level_3":
                {
                    Debug.Log("Loaded Level 3");
                    original = Resources.Load("level_3_original") as Texture2D;
                    currentCanvas.GetComponent<Renderer>().material.mainTexture = Resources.Load("level_3_draw") as Texture2D;
                    original_difference = calculate_difference();
                    star_Timer = true;
                    break;
                }

            default:
                Debug.LogError("Couldn't find the right level");
                break;
        }

	}
	

	void Update () {

        if (star_Timer)
        {
            time_left_float -= Time.deltaTime;
            time_left.GetComponent<TextMesh>().text = time_left_float.ToString("F0");

            if (time_left_float < 0)
            {
            
                LostLevel();
            }
        }
       
	}
	
	
	
	float calculate_difference () {
        int winner = 0;
        Texture2D current_PaintingTexture = currentCanvas.GetComponent<Renderer>().material.mainTexture as Texture2D;
        Color[] currentCanvas_PixelArray  = current_PaintingTexture.GetPixels(0);
        Color[] original_PixelsArray      = original.GetPixels(0);

        for ( int i = 0 ; i < currentCanvas_PixelArray.Length ; i++ ) {
			if (currentCanvas_PixelArray[i].Equals(original_PixelsArray[i]) ) {
					winner++;
			}
		}
		return 65536 - winner;	
	}
	
	void calculate_percentage(){
        float new_difference = calculate_difference();
		float percentage = 1 - new_difference / original_difference;
		percentage = percentage * 100f;
		score.GetComponent<TextMesh>().text = percentage.ToString("F0") + " %"; ;

        if ( percentage > percentage_ToWin)
        {
            if ( !level_AlreadyLoading)
            {
                level_AlreadyLoading = true;
                WonLevel();
            }
       
        }

    }

    void WonLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_1":
                {
                    next_Level_Name = "Level_2";
                    LoadNextLevel();
                    break;
                }
            case "Level_2":
                {
                    next_Level_Name = "Level_3";
                    LoadNextLevel();
                    break;
                }
            case "Level_3":
                {
                    next_Level_Name = "menu";
                    LoadNextLevel();
                    break;
                }

            default:
                Debug.LogError("Couldn't find the right level");
                break;
        }
    }

    void LostLevel()
    {

        SceneManager.LoadScene("menu");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("menu"));

    }

    public void LoadNextLevel()
    {
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        //Don't let the Scene activate until you allow it to
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_Level_Name);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
