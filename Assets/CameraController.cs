using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float xFactor = Screen.width / 1920f;
        float yFactor = Screen.height / 1080f;


        Camera.main.rect = new Rect(0, 0, 1, xFactor / yFactor);
    }
}
