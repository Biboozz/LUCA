﻿using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour 
{
	private string _pseudo = "Guest";
	public string pseudo 					{ 	get { return _pseudo; 			} 		set { _pseudo = value; 		} 	}


    void OnClick()
    {
        string button = gameObject.name;
        switch (button)
        {
			case "Button - Solo":
                Application.LoadLevel(1);
                break;

			case "Button - Rejoindre Serveur":



				// START CLIENT
				BoltLauncher.StartClient();
				BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse("127.0.0.1:2500"));
				break;

			case "Button - Creer Serveur":




				// START SERVER
				BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("127.0.0.1:2500"));
				BoltNetwork.LoadScene("Multijoueur_game");
				break;

            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
