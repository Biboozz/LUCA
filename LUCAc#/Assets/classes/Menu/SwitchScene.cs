using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour 
{
    void OnClick()
    {
        string button = gameObject.name;
        switch (button)
        {
			case "Button - Solo":
                Application.LoadLevel(1);
                break;

			case "Button - RejoindreServ":
				break;

			case "Button - CreerServ":
				break;

            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
