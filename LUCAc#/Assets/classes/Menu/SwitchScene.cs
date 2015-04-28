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
			case "Button - Solo":
                Application.LoadLevel(1);
                break;
            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
