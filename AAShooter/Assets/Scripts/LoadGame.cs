using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLevel(int _levelIndex)
    {
        Application.LoadLevel(_levelIndex);
    }
}
