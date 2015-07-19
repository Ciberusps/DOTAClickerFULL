using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float pixelsToUnits = 100f;
    public int targetWidth;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        Camera.main.orthographicSize = height / pixelsToUnits / 2;

       // Camera.main.orthographicSize = Screen.height / pixelsToUnits / 2;
        
        
        /*
        float xFactor = Screen.width / 1920f;
        float yFactor = Screen.height / 1080f;


        Camera.main.rect = new Rect(0, 0, 1, xFactor / yFactor);
        */
    }
}
