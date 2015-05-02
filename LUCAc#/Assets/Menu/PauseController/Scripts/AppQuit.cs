using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Time.timeScale = 1f;
		Application.LoadLevel(0);
	}
}
