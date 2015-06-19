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

	private int posx;
	private int posy;

	private int[,] tab_Pos = new int[81, 2];

	private bool started = false;

	// Use this for initialization
	void Start () 
	{
		//GenerateTab ();
//		I = gameObject.GetComponentInParent<Individual>();
//		R = I.RM;
//		cellMolecules = I.cellMolecules;
//		moleculetarget = cellMolecules[0];
//		started = true;
	}

	public void Initialize()
	{
		I = gameObject.GetComponentInParent<Individual>();
		R = I.RM;
		cellMolecules = I.cellMolecules;
		moleculetarget = cellMolecules[0];
		started = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (started) 
		{
			if (I.delay >= 360) 
			{
				I.delay = 0;
				if(!I.gotDest && !I.isPlayed)
				{
					posx = (int)(transform.position.x / 20);
					posy = (int)(transform.position.y / 20);
					if(posx > 6 && posx < 94 && posy > 6 && posy < 94)
						AnalyseMolecules ();
				}
			} 
			else
				I.delay++;
		}

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
		bool b = false;	//Molécule trouvé

		//Case centrale, où est la cellule
		tab_Pos [0, 0] = posx;
		tab_Pos [0, 1] = posy;

		First_radius (posx, posy);	//Ajout des positions pour le premier rayon

		for (int i = 0; i <= 8 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]]);	//Test si présence de la molécule recherch dans le premier rayon
		}

		if (!b)	//Si la molécule n'est pas trouvé, lance le procédé sur le deuxième rayon
			Second_radius (posx, posy);

		for (int i = 9; i <= 24 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]]);
		}

		if (!b)
			Third_radius (posx, posy);
		
		for (int i = 25; i <= 48 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]]);
		}

		if (!b)
			Fourth_radius (posx, posy);
		
		for (int i = 49; i <= 68 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]]);
		}

		if (!b)
			Fifth_radius (posx, posy);
		
		for (int i = 69; i <= 80 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]]);
		}
	}

	public bool TestAnalyse(List<moleculePack> M)
	{
		foreach(moleculePack P in M)
		{
			if(P.moleculeType.name == FindMoleculeLack() && P.count > 40)	//Molécule trouvé
			{
				I.gotDest = true;
				I.target = new Vector3(posx_square * 20 + 10, posy_square * 20 + 10, 0.1f);
				return true;
			}
			else
				return false;
		}
		return false;
	}

    public void First_radius(int posx_square, int posy_square)
    {
        tab_Pos[1, 0] = posx_square - 1;
        tab_Pos[1, 1] = posy_square + 1;

        tab_Pos[2, 0] = posx_square;
        tab_Pos[2, 1] = posy_square + 1;

        tab_Pos[3, 0] = posx_square + 1;
        tab_Pos[3, 1] = posy_square + 1;

        tab_Pos[4, 0] = posx_square + 1;
        tab_Pos[4, 1] = posy_square;

        tab_Pos[5, 0] = posx_square + 1;
        tab_Pos[5, 1] = posy_square - 1;

        tab_Pos[6, 0] = posx_square;
        tab_Pos[6, 1] = posy_square - 1;

        tab_Pos[7, 0] = posx_square - 1;
        tab_Pos[7, 1] = posy_square - 1;

        tab_Pos[8, 0] = posx_square - 1;
        tab_Pos[8, 1] = posy_square;
    }

    public void Second_radius(int posx_square, int posy_square)
    {
        tab_Pos[9, 0] = posx_square - 2;
        tab_Pos[9, 1] = posy_square + 2;

        tab_Pos[10, 0] = posx_square - 1;
        tab_Pos[10, 1] = posy_square + 2;

        tab_Pos[11, 0] = posx_square;
        tab_Pos[11, 1] = posy_square + 2;

        tab_Pos[12, 0] = posx_square + 1;
        tab_Pos[12, 1] = posy_square + 2;

        tab_Pos[13, 0] = posx_square + 2;
        tab_Pos[13, 1] = posy_square + 2;

        tab_Pos[14, 0] = posx_square + 2;
        tab_Pos[14, 1] = posy_square + 1;

        tab_Pos[15, 0] = posx_square + 2;
        tab_Pos[15, 1] = posy_square;

        tab_Pos[16, 0] = posx_square + 2;
        tab_Pos[16, 1] = posy_square - 1;

        tab_Pos[17, 0] = posx_square + 2;
        tab_Pos[17, 1] = posy_square - 2;

        tab_Pos[18, 0] = posx_square + 1;
        tab_Pos[18, 1] = posy_square - 2;

        tab_Pos[19, 0] = posx_square;
        tab_Pos[19, 1] = posy_square - 2;

        tab_Pos[20, 0] = posx_square - 1;
        tab_Pos[20, 1] = posy_square - 2;

        tab_Pos[21, 0] = posx_square - 2;
        tab_Pos[21, 1] = posy_square - 2;

        tab_Pos[22, 0] = posx_square - 2;
        tab_Pos[22, 1] = posy_square - 1;

        tab_Pos[23, 0] = posx_square - 2;
        tab_Pos[23, 1] = posy_square;

        tab_Pos[24, 0] = posx_square - 2;
        tab_Pos[24, 1] = posy_square + 1;
    }

	public void Third_radius(int posx_square, int posy_square)
	{
		int acc = -3;
		for (int i = 25; i <= 31; i++) 
		{
			tab_Pos[i, 0] = posx_square + acc;
			tab_Pos[i ,1] = posy_square + 3;
			acc++;
		}
		acc = 3;
		for (int i = 32; i <= 37; i++) 
		{
			tab_Pos[i, 0] = posx_square + 3;
			tab_Pos[i ,1] = posy_square + acc;
			acc--;
		}
		acc = 3;
		for (int i = 38; i <= 43; i++) 
		{
			tab_Pos[i, 0] = posx_square + acc;
			tab_Pos[i ,1] = posy_square - 3;
			acc--;
		}
		acc = -3;
		for (int i = 44; i <= 48; i++) 
		{
			tab_Pos[i, 0] = posx_square - 3;
			tab_Pos[i ,1] = posy_square + acc;
			acc++;
		}
	}

	public void Fourth_radius(int posx_square, int posy_square)
	{
		int acc = -2;
		for (int i = 49; i <= 53; i++) 
		{
			tab_Pos[i, 0] = posx_square + acc;
			tab_Pos[i ,1] = posy_square + 4;
			acc++;
		}
		acc = 2;
		for (int i = 54; i <= 58; i++) 
		{
			tab_Pos[i, 0] = posx_square + 4;
			tab_Pos[i ,1] = posy_square + acc;
			acc--;
		}
		acc = 2;
		for (int i = 59; i <= 63; i++) 
		{
			tab_Pos[i, 0] = posx_square + acc;
			tab_Pos[i ,1] = posy_square - 4;
			acc--;
		}
		acc = -2;
		for (int i = 64; i <= 68; i++) 
		{
			tab_Pos[i, 0] = posx_square - 4;
			tab_Pos[i ,1] = posy_square + acc;
			acc++;
		}
	}

	public void Fifth_radius(int posx_square, int posy_square)
	{
		tab_Pos[69, 0] = posx_square - 1;
		tab_Pos[69, 1] = posy_square + 5;

		tab_Pos[70, 0] = posx_square;
		tab_Pos[70, 1] = posy_square + 5;

		tab_Pos[71, 0] = posx_square + 1;
		tab_Pos[71, 1] = posy_square + 5;

		tab_Pos[72, 0] = posx_square + 5;
		tab_Pos[72, 1] = posy_square + 1;

		tab_Pos[73, 0] = posx_square + 5;
		tab_Pos[73, 1] = posy_square;

		tab_Pos[74, 0] = posx_square + 5;
		tab_Pos[74, 1] = posy_square - 1;

		tab_Pos[75, 0] = posx_square + 1;
		tab_Pos[75, 1] = posy_square - 5;

		tab_Pos[76, 0] = posx_square;
		tab_Pos[76, 1] = posy_square - 5;

		tab_Pos[77, 0] = posx_square - 1;
		tab_Pos[77, 1] = posy_square - 5;

		tab_Pos[78, 0] = posx_square - 5;
		tab_Pos[78, 1] = posy_square - 1;

		tab_Pos[79, 0] = posx_square - 5;
		tab_Pos[79, 1] = posy_square;

		tab_Pos[80, 0] = posx_square - 5;
		tab_Pos[80, 1] = posy_square + 1;
	}


}
