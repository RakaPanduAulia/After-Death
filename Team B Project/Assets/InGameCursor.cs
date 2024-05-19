using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCursor : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }
}
