using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour 
{
	string registered_game = "network_luca_game!&@&"; //nom de notre jeu sur les serveurs unity (volontairement complexe)
	bool is_refresing = false;
	float refresh_request_lenght = 3.0f; //temps supplémentaire : donner une impression de recherche 
	HostData[] host_data; //tableau contenant les serveurs créés
	
	/*** création serveur ***/
	
	public void StartServer()
	{
		Network.InitializeServer(16, 25000, true);
		MasterServer.RegisterHost(registered_game, "LUCA I", "Test implamentation of server code");
	}
	
	/*** recherche serveur ***/
	
	public IEnumerator refresh_host_list()
	{
		Debug.Log ("Recherche...");
		MasterServer.RequestHostList (registered_game);
		float time_started = Time.time;
		float time_end = time_started + refresh_request_lenght;
		
		while (Time.time < time_end) 
		{
			host_data = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame ();
		}


		
		if (host_data == null || host_data.Length == 0) 
		{
			Debug.Log ("Aucun serveur trouvé.");
		} 
		else if (host_data.Length == 1) 
		{
			Debug.Log (host_data.Length + " a été trouvé.");
		} 
		else 
		{
			Debug.Log (host_data.Length + " ont été trouvés.");
		}
		
	}
	
	/*** initialisation du serveur ***/
	
	public void OnserverInitialized()
	{
		Debug.Log ("Le serveur est initialisé.");
	}
	
	/*** enregistrement **/
	
	public void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded) 
		{
			Debug.Log ("Enregistrement réussi.");
		}
	}

    void OnClick()
    {


        string button = gameObject.name;
        switch (button)
        {
			case "Button - Solo":
                Application.LoadLevel(1);
                break;

			case "Button - RejoindreServ":
				StartCoroutine("refresh_host_list");
				break;

			case "Button - CreerServ":
				StartServer();
				OnserverInitialized();
				break;

            case "Button - Quitter":
                Application.Quit();
                break;
        }
    }
}
