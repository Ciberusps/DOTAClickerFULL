using UnityEngine;
using System.Collections;

public class СustomCursor : MonoBehaviour
{
    public Texture2D customCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Use this for initialization
    void Start ()
    {
        Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
    }
}
