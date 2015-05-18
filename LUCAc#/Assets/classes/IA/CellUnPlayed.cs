using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CellUnPlayed : MonoBehaviour {

	private Individual I;
	private resourcesManager R;
	private List<moleculePack> cellMolecules = new List<moleculePack>();	//Liste des molecules de la cellules
	private moleculePack moleculetarget;
	private List<moleculePack>[,] moleculesTerrain;
	private List<List<moleculePack>[,]> moleculesAroundCell;

	// Use this for initialization
	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		cellMolecules = I.cellMolecules;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moleculesTerrain = R.moleculeRepartition;
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

		return moleculetarget.moleculeType.name;
	}

	public void closeMolecule()
	{
		int posx = (int)(transform.position.x / 20);
		int posy = (int)(transform.position.y / 20);

		/*for (int i = 0; i < 3; i++) //Ligne n°1
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 1 + i, posy + 5]);
		}
		for (int i = 0; i < 5; i++) //Ligne n°2
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 2 + i, posy + 4]);
		}
		for (int i = 0; i < 7; i++) //Ligne n°3
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 3 + i, posy + 3]);
		}
		for (int i = 0; i < 9; i++) //Ligne n°4
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 4 + i, posy + 2]);
		}
		for (int i = 0; i < 11; i++) //Ligne n°5
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 5 + i, posy + 1]);
		}
		for (int i = 0; i < 11; i++) //Ligne n°6
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 5 + i, posy]);
		}
		for (int i = 0; i < 11; i++) //Ligne n°7
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 5 + i, posy - 1]);
		}
		for (int i = 0; i < 9; i++) //Ligne n°8
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 4 + i, posy - 2]);
		}
		for (int i = 0; i < 7; i++) //Ligne n°9
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 3 + i, posy - 3]);
		}
		for (int i = 0; i < 5; i++) //Ligne n°10
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 2 + i, posy - 4]);
		}
		for (int i = 0; i < 3; i++) //Ligne n°11
		{
			moleculesAroundCell.Add(R.moleculeRepartition[posx - 1 + i, posy - 5]);
		}

		foreach (List<moleculePack>[,] L in moleculesAroundCell) 
		{
			foreach(moleculePack M in L)
			{
				if(M.moleculeType.name == FindMoleculeLack())	//Molécule trouvé
				{

				}
			}
		}*/

	}
}
