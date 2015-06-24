using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Time.timeScale = 1f;
		if (Application.loadedLevel == 2 && BoltNetwork.isServer) 
		{
			BoltLauncher.Shutdown ();
			BoltNetwork.LoadScene("Menu");
		}	
		Application.LoadLevel(0);
	}
}
