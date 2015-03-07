using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {

    protected int Width;
    protected int Height;
	public GameObject cell;
	public cell cellBehavior;
	public List<Species> livings = new List<Species>();
	private int time = 0;

	// Use this for initialization
	void Start () 
	{
		//test

		List<Individual> Individuals = new List<Individual>();
		Individual I = new Individual ((GameObject)Instantiate (cell), this, 1000, 50, false);
		cell.transform.Translate (10, 3, 0);
		Individuals.Add (I);
		I = new Individual ((GameObject)Instantiate (cell), this, 1000, 50, false);
		cell.transform.Translate (10, 3, 0);
		Individuals.Add (I);

		I = new Individual ((GameObject)Instantiate (cell), this, 1000, 50, false);
		cell.transform.Translate (10, 3, 0);
		Individuals.Add (I);
		Species nyanCat = new Species(Individuals,cell,new List<perkData> (),this,false,10000);
		foreach(Individual Y in nyanCat.Individuals)
		{
			Y.species = nyanCat;
		}
		cell.transform.position = new Vector3 (10, 1, 10);
		livings.Add (nyanCat);
		//end test
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
