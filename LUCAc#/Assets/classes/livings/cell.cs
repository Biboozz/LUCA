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
	public List<moleculePack> cellMolecules = new List<moleculePack>();
	public int ATP;

	// Use this for initialization
	void Start () 
	{
		transform.Rotate (0, 0, UnityEngine.Random.Range(0,360));
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		transform.Translate(0.05f,0f,0f);
		transform.Rotate (0, 0, UnityEngine.Random.Range(-2,3));
		toCorrectPosition();
	}

	public void action () 
	{
		foreach (perkData p in species.unlockedPerks) 
		{
			if (p.active && p.cost.ATP <= this.ATP)
			{
				bool valid = !(p.cost.cellMolecules.Count > this.cellMolecules.Count || p.cost.environmentMolecules.Count > place.moleculesAvailableAt().Count);
				List<moleculePack> validMP = new List<moleculePack>();
				if (valid) 
				{

				}
			}
		}
	}

	void toCorrectPosition()
	{
		Vector3 pos = transform.position;
		if (pos.x < 1) 
		{
			pos.x = 1;
		}
		if (pos.x > 2000) 
		{
			pos.x = 2000;
		}
		if (pos.z < 1) 
		{
			pos.z = 1;
		}
		if (pos.z > 2000) 
		{
			pos.z = 2000;
		}
		transform.position = pos;
	}
}
