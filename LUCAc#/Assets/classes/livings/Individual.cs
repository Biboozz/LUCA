using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Linq;

public class Individual : MonoBehaviour
{
	private int _lifeTime;
	public bool alive = true;
	public Species species;
	private int _survivedTime = 0;
	public bool isPlayed = false;
	public List<moleculePack> cellMolecules = new List<moleculePack>();
	public int ATP;
	private int coolDown = 0;
	private bool initialized = false;
	private environment place;

	#region accessors
	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}

	public int lifetime
	{
		get
		{
			return _lifeTime;
		}
	}
	#endregion

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
		if (coolDown >= 10 && initialized) 
		{
			coolDown = 0;
			_survivedTime = _survivedTime + 1;
			alive = (_survivedTime < _lifeTime);
			action ();
		} 
		else 
		{
			coolDown++;
		}
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
					//not finished yet
				}
			}
		}
	}

	public void Initialize(Vector3 position, int lifeTime, Species species, environment place, bool isPlayed, List<moleculePack> molecules, int ATP)
	{
		transform.position = position;
		_lifeTime = lifetime;
		this.species = species;
		transform.parent = place.transform;
		this.place = place;
		this.isPlayed = isPlayed;
		cellMolecules = molecules;
		this.ATP = ATP;
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
