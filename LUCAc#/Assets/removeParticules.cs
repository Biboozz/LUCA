using UnityEngine;
using System.Collections;

public class removeParticules : MonoBehaviour {

	int duration = 2000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		duration--;
		if (duration == 0) {
			Destroy(gameObject);
		}
	}
}
