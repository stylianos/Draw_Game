using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class ColorSphereLogic : MonoBehaviour
{


    private GameObject[] blue_Balls_Spawning_Points;
    private GameObject[] red_Balls_Spawning_Points;
    private GameObject[] yellow_Balls_Spawning_Points;
    private GameObject[] rubber_Balls_Spawning_Points;

    public GameObject blue_ball;
    public GameObject red_ball;
    public GameObject yellow_ball;
    public GameObject rubber_ball;

    private int blue_Ball_Counter;
    private int red_Ball_Counter;
    private int yellow_Ball_Counter;
    private int rubber_Ball_Counter;

    public int spawn_Number_Blue;
    public int spawn_Number_Red;
    public int spawn_Number_Yellow;
    public int spawn_Number_Rubber;

    public bool apply_Force_Red;
    public bool apply_Force_Blue;
    public bool apply_Force_Yellow;
    public bool apply_Force_Rubber;

    private float thrust;
    public float spawn_Red_Balls_Every_X_Seconds;
    public float spawn_Blue_Balls_Every_X_Seconds;
    public float spawn_Yellow_Balls_Every_X_Seconds;
    public float spawn_Rubber_Balls_Every_X_Seconds;


    float time_Counter_Red;
    float time_Counter_Blue;
    float time_Counter_Yellow;
    float time_Counter_Rubber;

    // Use this for initialization
    void Start()
    {

        red_Ball_Counter = 0;
        blue_Ball_Counter = 0;
        yellow_Ball_Counter = 0;
        rubber_Ball_Counter = 0;
        thrust = 20;


        red_Balls_Spawning_Points = GameObject.FindGameObjectsWithTag("red_ball_spawn_point");
        blue_Balls_Spawning_Points = GameObject.FindGameObjectsWithTag("blue_ball_spawn_point");
        yellow_Balls_Spawning_Points = GameObject.FindGameObjectsWithTag("yellow_ball_spawn_point");
        rubber_Balls_Spawning_Points = GameObject.FindGameObjectsWithTag("rubber_ball_spawn_point");

        SpawnBallofColor(red_ball, red_Balls_Spawning_Points, ref red_Ball_Counter, spawn_Number_Red, apply_Force_Red);
        SpawnBallofColor(blue_ball, blue_Balls_Spawning_Points, ref blue_Ball_Counter, spawn_Number_Blue, apply_Force_Blue);
        SpawnBallofColor(yellow_ball, yellow_Balls_Spawning_Points, ref yellow_Ball_Counter, spawn_Number_Yellow, apply_Force_Yellow);
        SpawnBallofColor(rubber_ball, rubber_Balls_Spawning_Points, ref rubber_Ball_Counter, spawn_Number_Rubber, apply_Force_Rubber);

        time_Counter_Red = spawn_Red_Balls_Every_X_Seconds;
        time_Counter_Blue = spawn_Blue_Balls_Every_X_Seconds;
        time_Counter_Yellow = spawn_Yellow_Balls_Every_X_Seconds;
        time_Counter_Rubber = spawn_Rubber_Balls_Every_X_Seconds;


        GameObject playerball = GameObject.FindGameObjectWithTag("Player");
        playerball.GetComponent<PlayerBall>().collidedWithBall.AddListener(CollidedWithBall);
    }

    // Update is called once per frame
    void Update()
    {

        if (time_Counter_Red - Time.timeSinceLevelLoad < 0)
        {

            SpawnBallofColor(red_ball, red_Balls_Spawning_Points, ref red_Ball_Counter, spawn_Number_Red, apply_Force_Red);
            time_Counter_Red = Time.timeSinceLevelLoad + spawn_Red_Balls_Every_X_Seconds;
        }
        if (time_Counter_Blue - Time.timeSinceLevelLoad < 0)
        {

            SpawnBallofColor(blue_ball, blue_Balls_Spawning_Points, ref blue_Ball_Counter, spawn_Number_Blue, apply_Force_Blue);
            time_Counter_Blue = Time.timeSinceLevelLoad + spawn_Red_Balls_Every_X_Seconds;
        }
        if (time_Counter_Yellow - Time.timeSinceLevelLoad < 0)
        {

            SpawnBallofColor(yellow_ball, yellow_Balls_Spawning_Points, ref yellow_Ball_Counter, spawn_Number_Yellow, apply_Force_Yellow);
            time_Counter_Yellow = Time.timeSinceLevelLoad + spawn_Red_Balls_Every_X_Seconds;
        }
        if (time_Counter_Rubber - Time.timeSinceLevelLoad < 0)
        {

            SpawnBallofColor(rubber_ball, rubber_Balls_Spawning_Points, ref rubber_Ball_Counter, spawn_Number_Rubber, apply_Force_Rubber);
            time_Counter_Rubber = Time.timeSinceLevelLoad + spawn_Red_Balls_Every_X_Seconds;
        }

    }

    void CollidedWithBall(string ball_Color)
    {
        switch (ball_Color)
        {
            case "blue":
                blue_Ball_Counter--;
                break;
            case "red":
                red_Ball_Counter--;
                break;
            case "yellow":
                yellow_Ball_Counter--;
                break;
            case "rubber":
                rubber_Ball_Counter--;
                break;
            default:
                Debug.Log("Collided with something else");
                break;
        }
    }

    void SpawnBallofColor(GameObject ball, GameObject[] spawning_Points, ref int ball_Counter, int number_to_Spawn, bool apply_Force)
    {

        int will_Spawn = number_to_Spawn - ball_Counter;
        //Debug.Log("I am spawning " + ball.name + " the current number is " + ball_Counter + " and I will spawn  " + will_Spawn);

        int k = 0;
        for (int i = 0; i < will_Spawn; i++)
        {
            if (k >= spawning_Points.Length - 1)
            {
                k = 0;
            }
            GameObject new_ball = Instantiate(ball, spawning_Points[k].transform.position, Quaternion.identity);
            if (apply_Force)
            {
                new_ball.GetComponent<Rigidbody>().AddForce(new_ball.transform.forward * thrust, ForceMode.Impulse);
            }
            k++;
            ball_Counter++;

        }

    }
}
