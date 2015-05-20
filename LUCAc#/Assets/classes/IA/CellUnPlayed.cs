using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CellUnPlayed : MonoBehaviour {

	private Individual I;
	private resourcesManager R;
	private List<moleculePack> cellMolecules;	//Liste des molecules de la cellules
	private moleculePack moleculetarget;
	private List<List<moleculePack>> moleculesAroundCell;

	private int posx_square;
	private int posy_square;

	private int time;

	// Use this for initialization
	void Start () 
	{
		time = 0;
		I = gameObject.GetComponentInParent<Individual>();
		R = I.RM;
		cellMolecules = I.cellMolecules;
		moleculetarget = cellMolecules[0];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (time >= 420) 
		{
			time = 0;
			if(!I.gotDest)
				AnalyseMolecules ();
		} 
		else
			time++;
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
		bool b = true;

		for (int i = 0; i < 3 && b; i++) //Ligne n°1
		{
			if((posx - 1 + i < 100 && posx - 1 + i > 0) && (posy + 5 < 100))
			{
				posx_square = posx - 1 + i;
				posy_square = posy + 5;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);	//*
			}
		}
		for (int i = 0; i < 5 && b; i++) //Ligne n°2
		{
			if((posx - 2 + i < 100 && posx - 2 + i > 0) && (posy + 4 < 100))
			{
				posx_square = posx - 2 + i;
				posy_square = posy + 4;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);	//*
			}
		}
		for (int i = 0; i < 7 && b; i++) //Ligne n°3
		{
			if((posx - 3 + i < 100 && posx - 3 + i > 0) && (posy + 3 < 100))
			{
				posx_square = posx - 3 + i;
				posy_square = posy + 3;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);	//*
			}
		}
		for (int i = 0; i < 9 && b; i++) //Ligne n°4
		{
			if((posx - 4 + i < 100 && posx - 4 + i > 0) && (posy + 2 < 100))
			{
				posx_square = posx - 4 + i;
				posy_square = posy + 2;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);	//*
			}
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°5
		{
			if((posx - 5 + i < 100 && posx - 5 + i > 0) && (posy + 1 < 100))
			{
				posx_square = posx - 5 + i;
				posy_square = posy + 1;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);	//*
			}
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°6
		{
			if(posx - 5 + i < 100 && posx - 5 + i > 0)
			{
				posx_square = posx - 5 + i;
				posy_square = posy;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
		for (int i = 0; i < 11 && b; i++) //Ligne n°7
		{
			if((posx - 5 + i < 100 && posx - 5 + i > 0) && (posy - 1 > 0))
			{
				posx_square = posx - 5 + i;
				posy_square = posy - 1;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
		for (int i = 0; i < 9 && b; i++) //Ligne n°8
		{
			if((posx - 4 + i < 100 && posx - 4 + i > 0) && (posy - 2 > 0))
			{
				posx_square = posx - 4 + i;
				posy_square = posy - 2;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
		for (int i = 0; i < 7 && b; i++) //Ligne n°9
		{
			if((posx - 3 + i < 100 && posx - 3 + i > 0) && (posy - 3 > 0))
			{
				posx_square = posx - 3 + i;
				posy_square = posy - 3;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
		for (int i = 0; i < 5 && b; i++) //Ligne n°10
		{
			if((posx -2 + i < 100 && posx - 2 + i > 0) && (posy - 4 > 0))
			{
				posx_square = posx - 2 + i;
				posy_square = posy - 4;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
		for (int i = 0; i < 3 && b; i++) //Ligne n°11
		{
			if((posx - 1 + i < 100 && posx - 1 + i > 0) && (posy - 5 > 0))
			{
				posx_square = posx - 1 + i;
				posy_square = posy - 5;
				b = !TestAnalyse(R.moleculeRepartition[posx_square, posy_square]);
			}
		}
	}

	public bool TestAnalyse(List<moleculePack> M)
	{
		foreach(moleculePack P in M)
		{
			if(P.moleculeType.name == FindMoleculeLack() && P.count > 0)	//Molécule trouvé
			{
				I.gotDest = true;
				I.target = new Vector3(posx_square * 20 + 10, posy_square * 20 + 10);
				return true;
			}
			else
				return false;
		}
		return false;
	}

}
