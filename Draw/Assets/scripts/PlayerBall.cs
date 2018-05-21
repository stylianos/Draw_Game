using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class EventWithStr : UnityEvent<string>
{

}
public class PlayerBall : MonoBehaviour {
	

    float moveSpeed = 3f;
	Color paint_color ;
	bool collided = false;
	float color_spending_rate = 0.002f;
	bool rubber = false;
	Texture2D image_No_Color;
	public Texture2D sphere;
	public Texture2D rubber_ball;
    public EventWithStr collidedWithBall;

    // Use this for initialization

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        EventManager.AddEvent("isPainting");
        collidedWithBall = new EventWithStr();
    }
    void Start () {
        // Load the Appropriate Rubber textue
		if (SceneManager.GetActiveScene().name == "Level_1" ) {
			image_No_Color = Resources.Load("level_1_rubber") as Texture2D;
		}
		if (SceneManager.GetActiveScene().name == "Level_2" ) {
			image_No_Color = Resources.Load("level_2_rubber") as Texture2D;
		}
		if (SceneManager.GetActiveScene().name == "Level_3" ) {
			image_No_Color = Resources.Load("level_3_rubber") as Texture2D;
		}
		
		sphere = Resources.Load("sphere") as Texture2D;
		GetComponent<Renderer>().material.mainTexture = sphere;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
       
		if ( Input.GetKey(KeyCode.LeftShift)){

            EventManager.TriggerEvent("isPainting");
            Texture2D tex_ball  = GetComponent<Renderer>().material.mainTexture as Texture2D;
			Color ball_color = tex_ball.GetPixel(0,0);
			
			 if(!ball_color.Equals(Color.white) && collided ){
				
				if ( rubber) {

                    Paint( true );
                }
				else{
					Paint( false );	
				}
			}
		}
	}

	void Paint ( bool erase )  {
	
		RaycastHit hit;
		
		if ( !Physics.Raycast(this.transform.position, new Vector3(0f,-1f,0f) , out hit ) ) {
			Debug.Log ("FALSE");
			return;
		}
		
		Renderer renderer = hit.collider.GetComponent<Renderer>();
		MeshCollider meshCollider = hit.collider as MeshCollider;
        if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null ||  meshCollider == null){
			return;
		}
           
        // Paint the texture
        Texture2D tex = renderer.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        //Why am I multiplying here with the width? 
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if (! erase)
        {
            for (int i = 1; i < 6; i++)
            {
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y - i, paint_color);
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, paint_color);
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y + i, paint_color);
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, paint_color);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y - i, paint_color);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y, paint_color);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y + i, paint_color);
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y - i, paint_color);
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y, paint_color);
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y + i, paint_color);

            }
        }
        else
        {
            for (int i = 1; i < 6; i++)
            {
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y - i, image_No_Color.GetPixel((int)pixelUV.x, (int)pixelUV.y - i));
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, image_No_Color.GetPixel((int)pixelUV.x, (int)pixelUV.y));
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y + i, image_No_Color.GetPixel((int)pixelUV.x, (int)pixelUV.y + i));
                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, image_No_Color.GetPixel((int)pixelUV.x, (int)pixelUV.y));
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y - i, image_No_Color.GetPixel((int)pixelUV.x - i, (int)pixelUV.y - i));
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y, image_No_Color.GetPixel((int)pixelUV.x - i, (int)pixelUV.y));
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y + i, image_No_Color.GetPixel((int)pixelUV.x - i, (int)pixelUV.y + i));
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y - i, image_No_Color.GetPixel((int)pixelUV.x + i, (int)pixelUV.y - i));
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y, image_No_Color.GetPixel((int)pixelUV.x + i, (int)pixelUV.y));
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y + i, image_No_Color.GetPixel((int)pixelUV.x + i, (int)pixelUV.y + i));

            }
        }
	
		tex.Apply();
		change_color_of_ball();
			
	}
	
	void change_color_of_ball() {
			
		Texture2D tex_ball  = gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
		Color[] colr = tex_ball.GetPixels(0);
		
        //Slowly turning the color into white
		for ( int i = 0 ; i < colr.Length ; i++ ) {
				Color temp = colr[i];
				if (temp.r < 1f ) {
					temp.r += color_spending_rate;
				}
				if (temp.g < 1f ) {
					temp.g += color_spending_rate;
				}
				if (temp.b < 1f ) {
					temp.b += color_spending_rate;
				}
				colr[i] = temp;		
				//colr[i] = Color.Lerp( colr[i] ,Color.white, Time.deltaTime*color_spending_rate);
				
		}
		
		tex_ball.SetPixels(colr ,0 );
		tex_ball.Apply();

	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "blue" || collision.gameObject.tag == "red" || collision.gameObject.tag == "yellow" || collision.gameObject.tag == "rubber") {
            
            switch ( collision.gameObject.tag )
            {
                case "blue":
                    collidedWithBall.Invoke("blue");
                    break;
                case "red":
                    collidedWithBall.Invoke("red");
                    break;
                case "yellow":
                    collidedWithBall.Invoke("yellow");
                    break;
                case "rubber":
                    collidedWithBall.Invoke("rubber");
                    break;
                default:
                    Debug.Log("Collided with something else");
                    break;
            }
            Texture2D color_ball_tex = collision.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
			Color ball_color = color_ball_tex.GetPixel(0,0);
			
			paint_color = ball_color;

			Texture2D tex  = gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
			gameObject.GetComponent<Renderer>().material.mainTexture = tex;		
			Color[] colr = tex.GetPixels(0);
			for ( int i = 0 ; i < colr.Length ; i++ ) {
				colr[i] =  ball_color;
			}
			
			tex.SetPixels(colr ,0 );
			tex.Apply();
			Destroy(collision.gameObject);
			collided = true;
			rubber = false;
		}
		
		if (collision.gameObject.tag == "rubber" ) {
			
			//gameObject.renderer.material.mainTexture = collision.gameObject.renderer.material.mainTexture;
			Texture2D color_ball_tex = collision.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
			Color[] old_color = color_ball_tex.GetPixels();
			
			Texture2D tex  = gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;
			
			gameObject.GetComponent<Renderer>().material.mainTexture = tex;		
			Color[] colr = tex.GetPixels(0);
			for ( int i = 0 ; i < colr.Length ; i++ ) {
				colr[i] =  old_color[i];
			}
			
			tex.SetPixels(colr ,0 );
			tex.Apply();
			
			
			Destroy(collision.gameObject);
			collided = true;
			rubber = true;
		}
	}
}



/*
float temp_z = this.transform.position.z;
float temp_x = this.transform.position.x;
float temp_y = this.transform.position.y;

temp_z = Mathf.Clamp(temp_z , -7.5f , 7f ) ;
//Debug.Log ("To temp_Z einai " + temp_z);
//Debug.Log ("To temp_x einai " + temp_x);
temp_x = Mathf.Clamp(temp_x , -15f , 3.8f);

Vector3 new_pos = new Vector3( temp_x , temp_y , temp_z );
this.transform.position = new_pos;

*/
