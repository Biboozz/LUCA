using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class cell : MonoBehaviour {

	public environment place;
	public Species species;
	public Individual individual;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{ 
		transform.Translate(0.05f,0f,0f);
		transform.Rotate (0, 0, UnityEngine.Random.Range(-2,3));
	}

	public void action () 
	{
		if ((individual != null) && (species != null) && (place != null)) 
		{
			foreach (perkData p in species.unlockedPerks) 
			{
				p.action(this);
			}
		}
	}
}
