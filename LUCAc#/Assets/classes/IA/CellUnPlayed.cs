using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CellUnPlayed : MonoBehaviour {

	private Individual I;
	private resourcesManager R;
	private List<moleculePack> cellMolecules;	//Liste des molecules de la cellules
	private List<List<moleculePack>> moleculesAroundCell;

	private moleculePack target1;
	private moleculePack target2;
	private moleculePack target3;

	private int posx;
	private int posy;

	private int[,] tab_Pos = new int[81, 2];	//Tableau pour les 81 carré a gérer avec position en x puis celle en y

	private bool started = false;

	public void Start()
	{
		I = gameObject.GetComponentInParent<Individual>();	//Définition de la cellule
		R = I.RM;	//Création du resourcesManager
		cellMolecules = I.cellMolecules;	//Récupération de la liste de moleculePack
		target1 = new moleculePack();
		target2 = new moleculePack();
		target3 = new moleculePack();
		target1.count = 100000;
		target2.count = 100000;
		target3.count = 100000;
		started = true;
		posx = (int)(transform.position.x / 20);	//Calcul position x par rapport au carré
		posy = (int)(transform.position.y / 20);	//Calcul position y par rapport au carré
	}

	public void Initialize()
	{
		I = gameObject.GetComponentInParent<Individual>();	//Définition de la cellule
		R = I.RM;	//Création du resourcesManager
		cellMolecules = I.cellMolecules;	//Récupération de la liste de moleculePack
		target1 = new moleculePack();
		target2 = new moleculePack();
		target3 = new moleculePack();
		target1.count = 100000;
		target2.count = 100000;
		target3.count = 100000;
		started = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (started) 
		{
			if (I.delay >= 360) //Action effectuer toutes les 6 secondes
			{
				I.delay = 0;

				if(!I.gotDest && !I.isPlayed)	//Si la cellule n'a pas de destination et qu'elle n'est pas joué
				{
					posx = (int)(transform.position.x / 20);	//Calcul position x par rapport au carré
					posy = (int)(transform.position.y / 20);	//Calcul position y par rapport au carré

					if(posx > 6 && posx < 94 && posy > 6 && posy < 94)	//Si elle est dans le périmètre et que le calcul du rayon ne sort pas du terrain
					{
						AnalyseMolecules();
					}
				}
			} 
			else
				I.delay++;
		}

	}

	public List<moleculePack> FindMoleculeLack()
	{
		foreach (moleculePack mp in cellMolecules) 
		{
			int count = mp.count;
			if(target1.count > count)
			{
				target3 = target2;
				target2 = target1;
				target1 = mp;
			}
			else if(target2.count > count && target1 != mp)
			{
				target3 = target2;
				target2 = mp;
			}
			else if(target3.count > count && target1 != mp && target2 != mp)
			{
				target3 = mp;
			}
		}
		return new List<moleculePack>{target1, target2, target3};
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
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]], i);	//Test si présence de la molécule recherché est dans le premier rayon
		}

		if (!b)	//Si la molécule n'est pas trouvé, lance le procédé sur le deuxième rayon
			Second_radius (posx, posy);

		for (int i = 9; i <= 24 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]], i);
		}

		if (!b)
			Third_radius (posx, posy);
		
		for (int i = 25; i <= 48 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]], i);
		}

		if (!b)
			Fourth_radius (posx, posy);
		
		for (int i = 49; i <= 68 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]], i);
		}

		if (!b)
			Fifth_radius (posx, posy);
		
		for (int i = 69; i <= 80 && !b; i++) 
		{
			b = TestAnalyse(R.moleculeRepartition[tab_Pos[i, 0], tab_Pos[i, 1]], i);
		}
	}

	public bool TestAnalyse(List<moleculePack> M, int i)
	{
		foreach(moleculePack P in M)
		{
			List<moleculePack> targets = FindMoleculeLack();
			if((P.moleculeType.name == targets[0].moleculeType.name && targets[0].count < 300) || (P.moleculeType.name == targets[1].moleculeType.name && targets[1].count < 300) || (P.moleculeType.name == targets[2].moleculeType.name && targets[2].count < 300))	//Molécule trouvé
			{
				I.target = new Vector3(tab_Pos[i, 0] * 20 + 10, tab_Pos[i, 1] * 20 + 10, 0.1f);
				I.gotDest = true;
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
