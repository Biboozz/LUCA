using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	private int i;
	public GameObject Remous;

	// Use this for initialization
	void Start () 
	{
		i = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (i % 10 == 0) 
		{
			Instantiate(Remous, new Vector3(Random.Range(50,1950),10,Random.Range(50,1950)),Remous.transform.rotation);
			i = 0;
		}
		i++;
	}
}
