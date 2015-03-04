using UnityEngine;
using System.Collections;

public class cell : MonoBehaviour {

	public environment E;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		transform.Translate(0.05f,0f,0f);
		transform.Rotate (0, 0, Random.Range(-2,3));

	}
}
