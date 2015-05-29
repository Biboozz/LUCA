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
	public List<skill> unlockedPerks = new List<skill> ();
	public actionData workCost;
	public actionData workProducts;
	public bool isPlayed = false;
	public string name;
	public GameObject cell;
	public environment place;
	private int _survivedTime = 0;
	public int individualLifeTime;
	public Color color;
	private int toxic_absorb_amount = 10;// Nombres de toxines abosrbees en une seule fois.
	private int _absorb_amount = 10000; // Nombre de mollecules absorbees par la cellule en une fois
	private int _absorb_cooldown = 300; //temps entre 2 absorptions (ms)
	public List<molecule> absorb = new List<molecule>();

	#region accessors
	public int toxic_absorb
	{
		get
		{
			return toxic_absorb_amount;
		}
	}

	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}
	public int absorb_amount
	{
		get
		{
			return _absorb_amount;
		}
	}
	public int absorb_cooldown
	{
		get
		{
			return _absorb_cooldown;
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
	public Species(List<Individual> Individuals, GameObject cell, List<skill> unlockedPerks, environment place, bool isPlayed, int individualLifeTime, Color color)
	{
		this.Individuals = Individuals;
		this.cell = cell;
		this.unlockedPerks = unlockedPerks;
		this.place = place;
		this.isPlayed = isPlayed;
		this.individualLifeTime = individualLifeTime;
		place.livings.Add (this);
		this.color = color;
	}

	public Species(GameObject cell, List<skill> unlockedPerks, environment place, bool isPlayed, int individualLifeTime, Color color)
	{
		this.cell = cell;
		this.unlockedPerks = unlockedPerks;
		this.place = place;
		this.isPlayed = isPlayed;
		this.individualLifeTime = individualLifeTime;
		place.livings.Add (this);
		this.color = color;
	}

	public Species(environment place, Color color) 
	{
		this.place = place;
		place.livings.Add (this);
		this.color = color;
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

	public void update() //augmentation temps de survie + 1
	{
		_survivedTime = _survivedTime + 1;
		checkDeath ();
	}

	public void addCell(Individual I)
	{
		I.species = this;
		I.isPlayed = isPlayed;
		I.place = place;
		I.RM = place.RM;
		Individuals.Add (I);
		place.CI.cellsplayed.Add (I);
		I.Initialize (I.transform.position, 0, this, place, isPlayed, new List<moleculePack> (), I.ATP);
		I.transform.FindChild("core").gameObject.GetComponent<SpriteRenderer>().color = color; //modif couleur core en fonction de l'espece
		I.transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().color = color;

	}
}
