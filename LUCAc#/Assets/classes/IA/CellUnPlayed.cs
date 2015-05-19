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
	private List<List<moleculePack>> moleculesAroundCell;

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

	public void AnalyseMolecules()
	{
		int posx = (int)(transform.position.x / 20);
		int posy = (int)(transform.position.y / 20);
		int posx_square;
		int posy_square;
		bool b = true;

		for (int i = 0; i < 3 && b; i++) //Ligne n°1
		{
			posx_square = posx - 1 + i;
			posy_square = posy + 5;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 5 && b; i++) //Ligne n°2
		{
			posx_square = posx - 2 + i;
			posy_square = posy + 4;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 7 && b; i++) //Ligne n°3
		{
			posx_square = posx - 3 + i;
			posy_square = posy + 3;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 9 && b; i++) //Ligne n°4
		{
			posx_square = posx - 4 + i;
			posy_square = posy + 2;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°5
		{
			posx_square = posx - 5 + i;
			posy_square = posy + 1;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°6
		{
			posx_square = posx - 5 + i;
			posy_square = posy;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°7
		{
			posx_square = posx - 5 + i;
			posy_square = posy - 1;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 9 && b; i++) //Ligne n°8
		{
			posx_square = posx - 4 + i;
			posy_square = posy - 2;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 7 && b; i++) //Ligne n°9
		{
			posx_square = posx - 3 + i;
			posy_square = posy - 3;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 5 && b; i++) //Ligne n°10
		{
			posx_square = posx - 2 + i;
			posy_square = posy - 4;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}
		for (int i = 0; i < 3 && b; i++) //Ligne n°11
		{
			posx_square = posx - 1 + i;
			posy_square = posy - 5;
			b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
		}

	}

	public bool TestAnalyse(List<moleculePack> M)
	{
		foreach(moleculePack P in M)
		{
			if(P.moleculeType.name == FindMoleculeLack() && P.count > 0)	//Molécule trouvé
			{

				return true;
			}
			else
				return false;
		}
	}

}
