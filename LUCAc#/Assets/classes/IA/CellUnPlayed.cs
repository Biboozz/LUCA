using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CellUnPlayed : MonoBehaviour {

	private Individual I;
	private List<moleculePack> cellMolecules = new List<moleculePack>();	//Liste des molecules de la cellules
	private moleculePack moleculetarget;
	private List<moleculePack> moleculesTerrain;

	// Use this for initialization
	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		cellMolecules = I.cellMolecules;
		//moleculesTerrain
	}
	
	// Update is called once per frame
	void Update () 
	{

		Debug.Log (cellMolecules [0].moleculeType);	//Acces nom de la molecule
		Debug.Log (cellMolecules [0].count);	// Acces montant de la molecule
	}

	public string FindMoleculeLack()
	{
		foreach (moleculePack mp in cellMolecules) 
		{
			if(mp.count < moleculetarget.count)		//Si le montant de la molécule est inférieur à la molécule cible, alors cette molécule deviens la molécule cible
			{
				moleculetarget = mp;
			}
		}


	}
}
