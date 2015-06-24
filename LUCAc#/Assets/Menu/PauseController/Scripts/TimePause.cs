using UnityEngine;
using System.Collections;

public class TimePause : MonoBehaviour, IPausable {
	public float pauseDelay = 0.3f;

	private float timeScale;

	public void OnUnPause() {
		Time.timeScale = timeScale;
	}
	
	public void OnPause() {
		if (!(Application.loadedLevel == 2)) 
		{
			timeScale = Time.timeScale;
			Invoke ("StopTime", pauseDelay);
		}
	}

	void StopTime() {
		Time.timeScale = 0;
	}
}
