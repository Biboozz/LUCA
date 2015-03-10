using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class Species
{
	public List<Individual> Individuals = new List<Individual>();
	private int _IndividualsNumber = 0;
	public List<perkData> unlockedPerks = new List<perkData> ();
	public bool isPlayed = false;
	public string name;
	public GameObject cell;
	public environment place;
	private int _survivedTime = 0;
	public int individualLifeTime;

	#region accessors
	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}

	public int IndividualsNumber
	{
		get
		{
			return _IndividualsNumber;
		}
	}
	#endregion



	#region constructors
	public Species(List<Individual> Individuals, GameObject cell, List<perkData> unlockedPerks, environment place, bool isPlayed, int individualLifeTime)
	{
		this.Individuals = Individuals;
		this.cell = cell;
		this.unlockedPerks = unlockedPerks;
		this.place = place;
		this.isPlayed = isPlayed;
		this.individualLifeTime = individualLifeTime;
		place.livings.Add (this);
	}

	public Species(GameObject cell, List<perkData> unlockedPerks, environment place, bool isPlayed, int individualLifeTime)
	{
		this.cell = cell;
		this.unlockedPerks = unlockedPerks;
		this.place = place;
		this.isPlayed = isPlayed;
		this.individualLifeTime = individualLifeTime;
		place.livings.Add (this);
	}

	public Species(environment place) 
	{
		this.place = place;
		place.livings.Add (this);
	}
	#endregion




	public void checkDeath()
	{
		List<int> a = new List<int> ();
		foreach (Individual I in Individuals) 
		{
			if (!I.alive)
			{
				a.Add(Individuals.IndexOf(I));
				place.remove(I.gameObject);
			}
		}
		int c = 0;
		foreach (int b in a) 
		{
			Individuals.RemoveAt(b - c);
			c++;
		}
	}

	public void update()
	{
		_survivedTime = _survivedTime + 1;
		checkDeath ();
	}
}
