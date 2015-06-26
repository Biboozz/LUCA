using UnityEngine;
using UnityEngine.UI;
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

	public List<Individual> selectedI = new List<Individual>{};

	private int MapDimension = 3;
	private System.Random rand = new System.Random();
	public BoardMap[,] BM;
	public POINT Playercursor = new POINT();
	public POINT ButtonCursor = new POINT();

	private int YouCursor = 0;
	public GameObject Cam;

	public GameObject groupSelectioner;
	public Button[] buttons;
	private List<List<Individual>> groups;
	private int selected = -1;
	public GameObject UIGroupZone;
	public GameObject UItaskList;
	
	public int selectedButton
	{
		get
		{
			return selected;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Rdm = new System.Random (25); // nombre alléatoire 0-25

		for (int j = 0; j < 6; j++) // création de 6 especes
		{
			Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));
			S.cell = cellPrefab;
			S.individualLifeTime = 300;
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
				switch (j)
				{
				case 1:
					S.name = "Acidobacteria";
					break;
				case 2:
					S.name = "Epsilobacteria";
					break;
				case 3:
					S.name = "Clostridia";
					break;
				case 4:
					S.name = "Deinococcaceae";
					break;
				case 5:
					S.name = "Phycobacteria";
					break;
				}
			}
			for (int i = 0; i < S.Individuals.Count; i++) 
			{
				S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000),UnityEngine.Random.Range(0,2000), 0.1f), 20, S, this, j == 0, new List<moleculePack>(), Rdm.Next(500)); //apparition coordonnées random
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
		groups = new List<List<Individual>> ();
		foreach (Button B in groupSelectioner.GetComponentsInChildren<Button>()) {
			Navigation n = B.navigation;
			n.mode = Navigation.Mode.None;
			B.navigation = n;
		}
	}
	
	public Species addSpecies(Species parent, List<Individual> starters)
	{
		Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));
		S.individualLifeTime = 300;
		S.isPlayed = parent.isPlayed;
		S.cell = parent.cell;
		if (S.isPlayed) 
		{
			PSDD.species = S;
			parent.isPlayed = false;
			foreach(Individual I in parent.Individuals)
			{
				I.isPlayed = false;
			}
		}
		string[] str = parent.name.Split(new char[] {' '});
		S.name = str[0];
		if (str.Length > 1) 
		{
			int n;
			int.TryParse (str [1], out n);
			S.name += " " + (n + 1).ToString ();
		} 
		else 
		{
			S.name += " 1";
		}
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

		if (Input.GetKeyDown (KeyCode.G)) 
		{
			if (selected == -1)
			{
				List<Individual> g = new List<Individual>();
				foreach (Individual I in livings.Find(s => s.isPlayed).Individuals)
				{
					if (I.isSelectioned)
					{
						if (I.group == null)
						{
							g.Add(I);
							I.group = g;
							setTargettoGroup(g);
						}
						else
						{
							I.isSelectioned = false;
						}
					}
				}
				if (g.Count > 0)
				{
					if (groups.Count < 10)
					{
						groups.Add(g);
						groupSelectioner.transform.GetChild(10 - groups.Count).gameObject.GetComponent<Button>().interactable = true;
					}
					else
					{
						groups[9] = g;
					}
				}
			}
			else
			{
				unselectButton();
				groupSelectioner.transform.GetChild(10 - groups.Count).gameObject.GetComponent<Button>().interactable = false;
				foreach (Individual I in groups[selected])
				{
					I.group = null;
				}
				groups.RemoveAt(selected);
				selected = -1;
			}
		}
	}

	private void unselectButton()
	{
		ColorBlock cb;
		if (selected != -1) 
		{
			cb = buttons[selected].colors;
			cb.normalColor = Color.yellow;
			buttons[selected].colors = cb;
			//UIGroupZone.SetActive(false);
			//UItaskList.SetActive(false);
		}
	}

	private void selectButton()
	{
		ColorBlock cb;
		if (selected != -1) 
		{
			cb = buttons [selected].colors;
			cb.normalColor = cb.highlightedColor;
			buttons [selected].colors = cb;
			//UIGroupZone.SetActive(true);
			//UItaskList.SetActive(true);
		}
	}

	private void selectGroup()
	{
		foreach (Individual I in groups[selected]) 
		{
			I.isSelectioned = true;
		}
	}

	public void G1Click()
	{
		unselectButton();
		int b = selected;
		selected = 0;
		if (b != selected) 
		{

			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G2Click()
	{
		unselectButton ();
		int b = selected;
		selected = 1;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G3Click()
	{
		unselectButton ();
		int b = selected;
		selected = 2;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G4Click()
	{
		unselectButton ();
		int b = selected;
		selected = 3;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G5Click()
	{
		unselectButton ();
		int b = selected;
		selected = 4;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G6Click()
	{
		unselectButton ();
		int b = selected;
		selected = 5;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G7Click()
	{
		unselectButton ();
		int b = selected;
		selected = 6;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G8Click()
	{
		unselectButton ();
		int b = selected;
		selected = 7;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G9Click()
	{
		unselectButton ();
		int b = selected;
		selected = 8;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
		}
	}

	public void G10Click()
	{
		unselectButton ();
		int b = selected;
		selected = 9;
		if (b != selected) 
		{
			selectButton();
			selectGroup();
		} 
		else 
		{
			unselectButton();
			selected = -1;
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
							I.cellMolecules.Add(new moleculePack(RdmMol.Next(50,200),m));
						}
					}
				}
			}
			RM.molecules = nm;
			_molecules = nm;

			GenerateBM ();
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

	public void setTargettoGroup(List<Individual> group)	//Set formation for a list of individual
	{
		float moyx = 0f;
		float moyy = 0f;
		
		foreach (Individual I in group)
		{
			moyx = moyx + I.transform.position.x;
			moyy = moyy + I.transform.position.y;
		}
		
		int count = group.Count;
		Vector3 target = new Vector3 (moyx / count, moyy / count);
		
		if (count > 0 && count <= 9) 
		{
			switch (count) {
			case 1 : group[0].target = target;	//Solo
				break;
			case 2 : group[0].target = target;	//Duo cote à cote
				group[1].target = new Vector3(target.x + 10, target.y);
				break;
			case 3 : group[0].target = target;	//Triangle
				group[1].target = new Vector3(target.x + 10, target.y);
				group[2].target = new Vector3(target.x + 5, target.y + 10);
				break;
			case 4 : group[0].target = target;	//Carré
				group[1].target = new Vector3(target.x + 10, target.y);
				group[2].target = new Vector3(target.x + 10, target.y + 10);
				group[3].target = new Vector3(target.x, target.y + 10);
				break;
			case 5 : group[0].target = target;	//Pentagne
				group[1].target = new Vector3(target.x + 10, target.y);
				group[2].target = new Vector3(target.x + 15, target.y + 10);
				group[3].target = new Vector3(target.x - 5, target.y + 10);
				group[4].target = new Vector3(target.x + 5, target.y + 20);
				break;
			case 6 : group[0].target = target;	//Ligne
				group[1].target = new Vector3(target.x + 10, target.y);
				group[2].target = new Vector3(target.x + 20, target.y);
				group[3].target = new Vector3(target.x - 10, target.y);
				group[4].target = new Vector3(target.x + 30, target.y);
				group[5].target = new Vector3(target.x - 20, target.y);
				break;
			case 7 : group[0].target = target;	//7
				group[1].target = new Vector3(target.x - 10, target.y);
				group[2].target = new Vector3(target.x - 20, target.y);
				group[3].target = new Vector3(target.x - 5, target.y - 5);
				group[4].target = new Vector3(target.x - 10, target.y - 10);
				group[5].target = new Vector3(target.x - 15, target.y - 15);
				group[6].target = new Vector3(target.x - 20, target.y - 20);
				break;
			case 8 : group[0].target = target;	//Flèche
				group[1].target = new Vector3(target.x + 10, target.y - 5);
				group[2].target = new Vector3(target.x - 10, target.y - 5);
				group[3].target = new Vector3(target.x - 20, target.y - 10);
				group[4].target = new Vector3(target.x + 20, target.y - 10);
				group[5].target = new Vector3(target.x, target.y - 15);
				group[6].target = new Vector3(target.x, target.y - 25);
				group[7].target = new Vector3(target.x, target.y - 35);
				break;
			case 9 : group[0].target = target;	//Dé
				group[1].target = new Vector3(target.x + 10, target.y);
				group[2].target = new Vector3(target.x - 10, target.y);
				group[3].target = new Vector3(target.x + 10, target.y - 10);
				group[4].target = new Vector3(target.x - 10, target.y - 10);
				group[5].target = new Vector3(target.x, target.y - 10);
				group[6].target = new Vector3(target.x - 10, target.y - 20);
				group[7].target = new Vector3(target.x, target.y - 20);
				group[8].target = new Vector3(target.x + 10, target.y - 20);
				break;
			}
		}
		
		foreach(Individual unit in group)	//Indique aux individue qu'il possède une cible
		{
			unit.gotDest = true;
		}
	}

	public void GenerateBM()
	{
		Playercursor.pos (0, 0);
		int dim = MapDimension;// rand.Next (3, 5);
		BoardMap [,] BM = new BoardMap[dim, dim];

		for (int i = 0; i<dim; i++) 
		{
			for(int j = 0;j<dim;j++)
			{
				BM[i,j] = new BoardMap(this, rand.Next (0,4));
				ColorButton(BM[i,j], i , j);
			}
		} 
		GameObject bttntxt = GameObject.Find ("text" + Playercursor.x + Playercursor.y);
		bttntxt.GetComponent<Text>().text = "Vous";
	}

	public void ColorButton (BoardMap BoM, int i, int j)
	{
		GameObject button = GameObject.Find ("Button" + i + j);

		Button b = button.GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = BoM.seen;
		cb.pressedColor = BoM.acti;
		cb.highlightedColor = BoM.over;
		b.colors = cb;

		//GameObject.Find ("buttons" + i + j).GetComponent<Button>().colors.normalColor = BM [i, j].seen;
	}

}
