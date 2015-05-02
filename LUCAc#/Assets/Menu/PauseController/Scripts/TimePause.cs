using UnityEngine;
using System.Collections;

public class TimePause : MonoBehaviour, IPausable {
	public float pauseDelay = 0.3f;

	private float timeScale;

	public void OnUnPause() {
		Time.timeScale = timeScale;
	}
	
	public void OnPause() {
		timeScale = Time.timeScale;
		Invoke ("StopTime", pauseDelay ); 
	}

	void StopTime() {
		Time.timeScale = 0;
	}
}
