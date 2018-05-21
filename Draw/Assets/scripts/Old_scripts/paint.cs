using UnityEngine;
using System.Collections;

public class paint : MonoBehaviour {
    void Update() {
        if (!Input.GetMouseButton(0))
            return;
        
        RaycastHit hit;
        if (!Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit))
            return;
        
        Renderer renderer = hit.collider.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
            return;
        
        Texture2D tex = renderer.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
		
		tex.SetPixel((int)pixelUV.x-1, (int)pixelUV.y-1, Color.black);
		tex.SetPixel((int)pixelUV.x-1, (int)pixelUV.y, Color.black);
		tex.SetPixel((int)pixelUV.x-1, (int)pixelUV.y+1, Color.black);
		
        tex.Apply();
    }
	
	
}