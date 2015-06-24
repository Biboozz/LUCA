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
	public actionData workCost = new actionData(0, new List<moleculePack>(), new List<moleculePack>());
	public actionData workProducts = new actionData(0, new List<moleculePack>(), new List<moleculePack>());
	public bool isPlayed = false;
	public string name;
	public GameObject cell;
	public environment place;
	private int _survivedTime = 0;
	public int individualLifeTime;
	public Color color;
	private int toxic_absorb_amount = 10;// Nombres de toxines abosrbees en une seule fois.
	private int _absorb_amount = 100; // Nombre de mollecules absorbees par la cellule en une fois
	private int _absorb_cooldown = 300; //temps entre 2 absorptions (ms)
	public List<molecule> absorb = new List<molecule>();

	public bool canPhagocyt = false;
	public List<string> immunities = new List<string> ();

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

	public void forceUnlockSkill(skill S) //sans verif, pas d'ajout d'espèce ou de cout (cheat)
	{
		upgrade (S);
		foreach (Individual I in Individuals) 
		{
			upgrade(S, I);
		}
	}

	public void naturalUnlock(skill S)// avec toutes les verifs et avec ajout d'espèces
	{
		List<Individual> L = unlockers (S);
		place.addSpecies (this, L).upgrade(S);
		if ((float)L.Count >= 0.60f * (float)Individuals.Count) 
		{
			foreach (Individual I in L) 
			{
				I.ATP -= S.devCosts.ATP;
				foreach (moleculePack mpS in S.devCosts.cellMolecules) 
				{
					I.cellMolecules.Find (mp => mp.moleculeType.ID == mpS.moleculeType.ID).count -= mpS.count;
				}
				upgrade(S, I);
			}
		}
	}

	private List<Individual> unlockers(skill S)
	{
		List<Individual> R = new List<Individual> ();
		foreach (Individual I in Individuals) 
		{
			if (unlockIndividual(I, S))
			{
				R.Add(I);
			}
		}
		return R;
	}

	public static bool unlockIndividual(Individual I, skill S)
	{
		if (I.ATP >= S.devCosts.ATP) 
		{
			foreach (moleculePack mpS in S.devCosts.cellMolecules) 
			{
				moleculePack mpI = I.cellMolecules.Find (mp => mp.moleculeType.ID == mpS.moleculeType.ID);
				if (mpI == null || mpI.count < mpS.count) 
				{
					return false;
				} 
			}
			return true;
		} 
		else 
		{
			return false;
		}
	}

	private void upgrade(skill ski)
	{
		unlockedPerks.Add (ski);
		workCost += ski.workCosts;
		workProducts += ski.workProducts;
		individualLifeTime = (int)((float)individualLifeTime * ski.agingDeltaModifier);
		if (ski.canPhagocyt) 
		{
			canPhagocyt = true;
		}
		if (ski.immunityType != "none") 
		{
			immunities.Add(ski.immunityType + " " + ski.immunityStrength.ToString());
		}
	}

	private void upgrade(skill S, Individual I)
	{
		I.speed *= S.speedModifier;
		if (S.speedModifier > 1f) 
		{
			I.duration -= linear(4f, S.speedModifier - 1);
		} 
		else if (S.speedModifier < 1f) 
		{
			I.duration += linear(4f, S.speedModifier - 1);
		}
		//I.duration = 18;
		if (S.addFlagel) 
		{
			I.transform.FindChild ("flagella").gameObject.GetComponent<CellFlagellaAnimation> ().shown = true;
		}
		if (S.removeFlagel) 
		{
			I.transform.FindChild ("flagella").gameObject.GetComponent<CellFlagellaAnimation> ().shown = false;
		}
		if (S.addLashes) 
		{
			I.transform.FindChild ("cilia").gameObject.GetComponent<cellCiliaAnimation> ().shown = true;
		}
		if (S.removeLashes) 
		{
			I.transform.FindChild ("cilia").gameObject.GetComponent<cellCiliaAnimation> ().shown = false;
		}
		I.transform.localScale = mult (I.transform.localScale, new Vector3 (S.sizeXModifier, S.sizeYModifier, 1f));
		I.transform.FindChild ("core").localScale *= S.coreSizeModifier;
		I.transform.FindChild ("membrane").localScale *= S.membraneSizeModifier;
	}

	private float linear(float pente, float x)
	{
		return pente * x;
	}

	private Vector3 mult( Vector3 v1, Vector3 v2)
	{
		return new Vector3 (v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

}
