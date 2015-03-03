using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class Species
{
	private List<Individual> _Individuals = new List<Individual>();
	private int _IndividualsNumber = 0;
	public List<perkData> unlockedPerks = new List<perkData> ();
	public bool isPlayed = false;
	public string name;
	public GameObject cell;
	public environment place;
	private int _survivedTime = 0;

	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}

	public Species(environment place, int baseLifeTime) {
		this.place = place;
		this.baselifeTime = baseLifeTime;
	}

	public int IndividualsNumber
	{
		get
		{
			return _IndividualsNumber;
		}
	}

	public List<Individual> Individuals
	{
		get
		{
			return _Individuals;
		}
		set
		{
			_Individuals = value;
			_IndividualsNumber = value.Count;
		}
	}

	public void update()
	{
		_survivedTime = _survivedTime + 1;
	}

	public Species(List<Individual> Individuals, GameObject cell, List<perkData> unlockedPerks, environment place, bool isPlayed)
	{
		_Individuals = Individuals;
		this.cell = cell;
		this.unlockedPerks = unlockedPerks;
		this.place = place;
		this.isPlayed = isPlayed;
	}
}
