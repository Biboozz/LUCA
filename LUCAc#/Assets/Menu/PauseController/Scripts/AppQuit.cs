using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Debug.Log ("Load Menu");
		Time.timeScale = 1f;
		Application.LoadLevel(0);
	}
}
