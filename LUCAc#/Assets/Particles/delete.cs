using UnityEngine;
using System.Collections;

public class delete : MonoBehaviour {

	private int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		if (i % 1200 == 0) 
		{
			Destroy(gameObject);
			i=0;
		}
	}
}
