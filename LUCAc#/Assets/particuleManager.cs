using UnityEngine;
using System.Collections;

public class particuleManager : MonoBehaviour {

	public GameObject particules;
	int i = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (i == 0) {
			GameObject p = (GameObject)Instantiate (particules, new Vector3 (Random.Range (-200f, 2200f), Random.Range (-200f, 2200f), 0f), transform.rotation);
			p.transform.SetParent (transform);
			i = 30;
		}
		i--;
	}
}
