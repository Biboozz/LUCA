using UnityEngine;
using System.Collections;

public class onClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		Individual I = gameObject.GetComponent<Individual>();
		if (I.isPlayed) 
		{
			I.isSelectioned = !I.isSelectioned;
		}
	}
}
