﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {
	
	public List<Species> livings = new List<Species>(); // liste espèces
	public GameObject cellPrefab; // just for the test, will not be needed in the futur
	private int coolDown = 0; // temps d'attente
	public GameObject UICellImage; // affichage infos
	public GameObject UICellDescriptionBox; // affichage infos
	private List<molecule> _molecules; // liste molécules
	public playerSpeciesDataDisplayer PSDD;
	public resourcesManager RM;
	private System.Random Rdm;
	public ConsoleInitializer CI;
	private int YouCursor = 0;
	public GameObject Cam;

	// Use this for initialization
	void Start () 
	{
		Rdm = new System.Random (25); // nombre alléatoire 0-25

		for (int j = 0; j < 6; j++) // création de 6 especes
		{
			Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));
			S.cell = cellPrefab;
			for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
			{
				S.Individuals.Add (Instantiate(cellPrefab).GetComponent<Individual>());
				CI.cellsplayed.Add(S.Individuals[S.Individuals.Count - 1]);
			}
			S.isPlayed = j == 0;
			if (S.isPlayed)
			{
				S.name = "LUCA";
				PSDD.species = S;
			}
			else
			{
				S.name = "test";
			}
			for (int i = 0; i < S.Individuals.Count; i++) 
			{
				S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000),UnityEngine.Random.Range(0,2000), 0.1f), 50000, S, this, j == 0, new List<moleculePack>(), Rdm.Next(500)); //apparition coordonnées random
				S.Individuals[i].descriptionBox = UICellDescriptionBox;
				S.Individuals[i].transform.FindChild("core").gameObject.GetComponent<SpriteRenderer>().color = S.color; //modif couleur core en fonction de l'espece
				S.Individuals[i].transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().color = S.color;
//				foreach(molecule m in molecules)
//				{
//					S.Individuals[i].cellMolecules.Add(new moleculePack(RdmMol.Next(30),m));
//				}
			}
			livings.Add (S); //ajout liste espece vivante
		}
	}

	public Species addSpecies(Species parent, List<Individual> starters)
	{
		Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));
		S.isPlayed = parent.isPlayed;
		if (S.isPlayed) 
		{
			PSDD.species = S;
			parent.isPlayed = false;
			foreach(Individual I in parent.Individuals)
			{
				I.isPlayed = false;
			}
		}
		S.name = parent.name;
		foreach (Individual I in starters) 
		{
			I.transform.FindChild("core").gameObject.GetComponent<SpriteRenderer>().color = S.color;
			I.transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().color = S.color;
			I.isPlayed = S.isPlayed;
			parent.Individuals.RemoveAt(parent.Individuals.FindIndex(r => r.Equals(I)));
			S.Individuals.Add(I);
			I.species = S;
		}
		foreach (skill ski in parent.unlockedPerks) 
		{
			S.unlockedPerks.Add(ski);
		}
		livings.Add (S);
		return S;
	}
	
	// Update is called once per frame
	void Update () 
	{
		coolDown = (coolDown + 1) % 10; // ?? 
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

	public List<molecule> molecules
	{
		get
		{
			return _molecules;
		}
		set
		{
			System.Random RdmMol = new System.Random (45);
			List<molecule> nm = new List<molecule>();
			foreach(molecule m in value)
			{
				if(nm.Find(mp => mp.ID == m.ID) == null)
				{
					nm.Add(m);
				}
			}
			foreach(Species S in livings)
			{
				foreach(Individual I in S.Individuals)
				{
					foreach(molecule m in value)
					{
						if(I.cellMolecules.Find(mp => mp.moleculeType.ID == m.ID) == null)
						{
							I.cellMolecules.Add(new moleculePack(RdmMol.Next(5,6000),m));
						}
					}
				}
			}
			RM.molecules = nm;
			_molecules = nm;
		}
	}

	public void YouButton()
	{
		Vector3 pos = Cam.transform.position;

		foreach (Species S in this.livings) 
		{
			if (S.isPlayed) 
			{
				if (YouCursor >= S.Individuals.Count) 
				{
					YouCursor = 0;
				}

				//Deselctionne les cellules
				foreach (Individual I in S.Individuals)
				{
					I.isSelectioned = ((Input.GetKey(KeyCode.LeftShift))||(Input.GetKey(KeyCode.RightShift)));
				}

				//Selectionne la cellule courante
				pos = S.Individuals [YouCursor].transform.position;
				S.Individuals[YouCursor].isSelectioned = true;



				YouCursor++;
			}
		}

		pos.z = -500;

		Cam.transform.position = pos;
	}

}
