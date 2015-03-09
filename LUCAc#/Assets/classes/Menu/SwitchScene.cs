using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnClick()
    {
        string button = gameObject.name;
        switch (button)
        {
            case "Button - Jouer":
                Application.LoadLevel("Game");
                break;
            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
