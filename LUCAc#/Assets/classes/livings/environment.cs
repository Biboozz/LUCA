using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {
	
	public List<Species> livings = new List<Species>();
	public GameObject cellPrefab; //just for the test, will not be needed in the futur
	private int coolDown = 0;

	// Use this for initialization
	void Start () 
	{
		//test
		Species S = new Species (this);
		livings.Add (S);
		for (int i = 1; i <= 4000; i++) 
		{
			S.Individuals.Add (Instantiate(cellPrefab).GetComponent<Individual>());
		}

		for (int i = 0; i < S.Individuals.Count; i++) 
		{
			S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000), 0.1f,UnityEngine.Random.Range(0,2000)), 50000, S, this, false, new List<moleculePack>(), 100);
		}
		//end test
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
