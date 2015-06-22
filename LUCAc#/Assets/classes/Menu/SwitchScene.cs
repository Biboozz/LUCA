using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour 
{
	private string _pseudo;
	public string pseudo 					{ 	get { return _pseudo; 			} 		set { _pseudo = value; 		} 	}

    void OnClick()
    {
		string button = gameObject.name;
        switch (button)
        {
			case "Button - Solo":
                Application.LoadLevel(2);
                break;

			case "Button - Rejoindre Serveur":
				// START CLIENT
				BoltLauncher.StartClient();
				BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(/*"25.54.209.3:139"*/ "127.0.0.1:27000"));
				break;

			case "Button - Creer Serveur":
				// START SERVER
				BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse(/*"25.54.209.3:139"*/ "127.0.0.1:27000"));
				BoltNetwork.LoadScene("Multijoueur_game");
				break;

            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
