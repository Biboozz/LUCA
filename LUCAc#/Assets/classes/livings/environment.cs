using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {
	
	public List<Species> livings = new List<Species>();
	private int time = 0;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		time = (time + 1) % 10;
		if (time == 0) 
		{
			foreach (Species s in livings)
			{
				s.update();
			}
		}
	}

	public void remove(GameObject G)
	{
		Destroy (G);
	}

	public List<moleculePack> moleculesAvailableAt()
	{
		return (new List<moleculePack> ());
	}

	public void consume()
	{

	}
}
