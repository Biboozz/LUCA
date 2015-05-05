using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Time.timeScale = 1f;
		if (Application.loadedLevel == 3 && BoltNetwork.isServer) 
		{
			BoltLauncher.Shutdown ();
		}	
		Application.LoadLevel(0);
	}
}
