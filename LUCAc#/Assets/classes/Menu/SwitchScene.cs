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
				// START CLIENT
				BoltLauncher.StartClient();
				BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
				break;

			case "Button - CreerServ":
				// START SERVER
				BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
				BoltNetwork.LoadScene("Tutorial1");
				break;

            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
