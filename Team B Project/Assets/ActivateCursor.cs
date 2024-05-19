using UnityEngine;

public class ActivateCursor : MonoBehaviour
{
    void Start()
    {
        // Ensure the cursor is visible
        Cursor.visible = true;

        // Ensure the cursor is not locked
        Cursor.lockState = CursorLockMode.None;
    }
}
