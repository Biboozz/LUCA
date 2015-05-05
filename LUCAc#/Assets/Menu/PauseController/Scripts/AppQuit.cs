using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Time.timeScale = 1f;
		BoltLauncher.Shutdown ();
		Application.LoadLevel(0);
	}
}
