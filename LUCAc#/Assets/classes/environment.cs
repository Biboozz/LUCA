using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {

	private int Heigth;
	private int Width;

	public List<Species> livings = new List<Species>();
	public GameObject cellPrefab; //just for the test, will not be needed in the futur
	private int coolDown = 0;


	// Use this for initialization
	void Start () 
	{
	    
	}
	
	// Update is called once per frame
	void Update () 
	{
		coolDown = (coolDown + 1) % 10;
		if (coolDown == 0) 
		{
			foreach (Species s in livings)
			{
				s.update();
			}
		}


		if (Input.GetKeyDown (KeyCode.R)) 
		{

		}
	}

	private int get_width()
	{
		return Width;
	}

	public void remove(GameObject G)
	{
		Destroy (G);
	}


}
