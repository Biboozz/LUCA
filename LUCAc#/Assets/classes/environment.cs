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

	// Use this for initialization
	void Start () 
	{
		System.Random Rdm = new System.Random (25); // nombre alléatoire 0-25

		for (int j = 0; j < 6; j++) // création de 6 especes
		{
			Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));

			for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
			{
				S.Individuals.Add (Instantiate(cellPrefab).GetComponent<Individual>());
			}
			S.isPlayed = j == 0;
			if (S.isPlayed)
			{
				S.name = "LUCA";
			}
			else
			{
				S.name = "test";
			}
			for (int i = 0; i < S.Individuals.Count; i++) 
			{
				S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000), 0.1f,UnityEngine.Random.Range(0,2000)), 50000, S, this, j == 0, new List<moleculePack>(), Rdm.Next(500)); //apparition coordonnées random
				S.Individuals[i].representation = UICellImage;
				S.Individuals[i].descriptionBox = UICellDescriptionBox;
				S.Individuals[i].transform.FindChild("core").gameObject.GetComponent<MeshRenderer>().material.color = S.color; //modif couleur core en fonction de l'espece
				S.Individuals[i].transform.FindChild("Membrane").gameObject.GetComponent<MeshRenderer>().material.color = S.color;
//				foreach(molecule m in molecules)
//				{
//					S.Individuals[i].cellMolecules.Add(new moleculePack(RdmMol.Next(30),m));
//				}
			}
			livings.Add (S); //ajout liste espece vivante
		}

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
			_molecules = value;
			foreach(Species S in livings)
			{
				foreach(Individual I in S.Individuals)
				{
					foreach(molecule m in _molecules)
					{
						if(I.cellMolecules.Find(mp => mp.moleculeType.ID == m.ID) == null)
						{
							I.cellMolecules.Add(new moleculePack(RdmMol.Next(30),m));
						}
					}
				}
			}
		}
	}

}
