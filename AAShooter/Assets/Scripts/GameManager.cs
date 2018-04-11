using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Texture crossHair;
    private AudioSource music;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        music = GetComponent<AudioSource>();
        music.Play();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(((Screen.width / 4) - 25),
                        ((Screen.height / 2) - 25), 50, 50),
                        crossHair, ScaleMode.ScaleToFit, true, 0.0f);

        GUI.DrawTexture(new Rect((((Screen.width / 4) * 3) - 25),
                        ((Screen.height / 2) - 25), 50, 50),
                        crossHair, ScaleMode.ScaleToFit, true, 0.0f);
    }

	// Update is called once per frame
	void Update ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
