using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using AssemblyCSharp;
using System.Linq;


//The code within this class is used to read the perktree from a file, format it and draw it.
//it is not yet finished, as you can't draw informations about a specific perk on it, but here's a good track.
//You also can't add a perk to a cell or whatever, for the same reasons.

public class perkTree : MonoBehaviour 
{
	public GameObject prefab;		//the base prefab of a hexagon, copied and instanciated to draw the tree;
	public perkData[] perks;
	public GameObject ressourcesPosition;
	public GameObject ressourcePrefab;
	//this is a perkData class tab. As there's no way to use pointers in Unity, I had to build a static graph structure with it... Taugh shit.
	//for more info about the class "perkData", please refer to the file under \Assets\classes\perkData.cs
	private string path = @"Assets\perktree.txt";
	//I chose this folder because I don't know how to get the Assets folder. Need to work on it.
	private bool valid = true;
	private bool shown = false;

    public bool _Shown
    {
        get { return shown; }
    }

	private bool ressshown = false;

	public GameObject pos;
	public GameObject cam;
	public GameObject link;

	private List<molecule> GameMolecules; 
	
	void Start()
	{
		QualitySettings.antiAliasing = 8;
		GameMolecules = readMoleculeFile (@"Assets\molecules.txt");
		if (File.Exists (path)) 
		{
			Debug.Log ("A file was found");
			perks = buildPerkTree (readPerkTreeFile (path));
		} 
		else 
		{
			Debug.Log (new Exception("No file was found at this path: " + path));
			valid = false;
		}
		drawPerkTree ();
		Terrain T = new Terrain (2000, 2000, GameMolecules);
		foreach (RessourceCircle RC in T.circles) 
		{
			RC.circleObject = (GameObject)Instantiate(ressourcePrefab, ressourcesPosition.transform.position,ressourcesPosition.transform.rotation);
			RC.circleObject.transform.SetParent(ressourcesPosition.transform);
		}
	}

	void Update() {
        
        if (Input.GetKeyDown (KeyCode.C)) 
		{
			if (valid) 
			{
				pos.SetActive(!shown);
				newTreePos();
				shown = !shown;
			} 
			else
			{
				Debug.Log(new Exception("this perkTree is unvalid!"));
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ressshown = !ressshown;
			ressourcesPosition.SetActive(ressshown);
		}     
		
	}


	private List<perkData> readPerkTreeFile(string path)  	//this function is used to read the file where the perktree is written. It builds a list of every readed perk from this file.										//Please refer to the end of this class to see how the file is built.
	{
		List<perkData> perkTree = new List<perkData>();
		try 
		{											//it's not really important if you don't understand what is in this try. Move on. It's just reading the file.
			StreamReader SR = new StreamReader(path);
			string line = SR.ReadLine();
			perkData p = new perkData();
			
			while (line != "ENDFILE") 
			{
				if (line == "STARTPERK")
				{
					p = new perkData();
					Debug.Log("new perk");
				}
				if (line.Contains("PNAME:")) 
				{
					p.name = line.Substring(10, line.Length - 10);
					Debug.Log(p.name);
				}
				if (line.Contains("PID:")) 
				{
					p.ID = Int32.Parse(line.Substring(8, line.Length - 8));
					Debug.Log("ID: " + p.ID);
				}
				if (line.Contains("PDESCRIPTION:"))
				{
					p.description = line.Substring(17, line.Length - 17);
					Debug.Log("description: " + p.description);
				}
				if (line.Contains("PNEIGHBORS:")) 
				{
					string[] s = line.Substring(15, line.Length - 15).Split(';');
					for (int i = 0; i < 6; i++) 
					{
						p.neigborsID[i] = Int32.Parse(s[i]);
						Debug.Log("neighboor " + i + ": " + p.neigborsID[i]);
					}
				}
				if (line.Contains("TYPE:"))
				{
					p.type = (perkType)Convert.ToInt32(line.Split(':')[1]);
					Debug.Log("type: " + p.type.ToString());
				}
				if (line.Contains("COSTDEV:"))
				{
					addData(ref p.onDevCost.cellMolecules,ref p.onDevCost, line);
				}
				if (line.Contains("COSTCELL:"))
				{
					addData(ref p.cost.cellMolecules,ref p.cost, line);
				}
				if (line.Contains("COSTENV"))
				{
					addData(ref p.cost.environmentMolecules,ref p.cost, line);
				}
				if (line.Contains("PRODDEV"))
				{
					addData(ref p.onDevProd.cellMolecules,ref p.onDevProd, line);
				}
				if (line.Contains("PRODCELL"))
				{
					addData(ref p.products.cellMolecules,ref p.products, line);
				}
				if (line.Contains("PRODENV"))
				{
					addData(ref p.products.environmentMolecules,ref p.products, line);
				}
				if (line.Contains("REQU"))
				{
					string [] data = line.Split(new char[] {';',':'});
					if (Convert.ToInt32(data [1]) == -1)
					{
						p.required = new int[] {-1};
					}
					else
					{
						p.required = new int[data.Length - 1];
						for (int i = 1; i < data.Length; i++)
						{
							p.required[i - 1] = Convert.ToInt32(data[i]);
						}
					}

				}
				if (line.Contains("ENDPERK")) 
				{
					perkTree.Add(p); 				//adding the perk to the list
					Debug.Log("added");
				}
				line = SR.ReadLine();
			}
			
		}
		catch (Exception e)
		{
			Debug.Log(e);   // in case of trouble
			valid = false;
		}
		return perkTree;
	}

	private perkData[] buildPerkTree(List<perkData> L) //this function build the tab of perkData we need for the graph from a list of perkData. Just conversion tab form list.												   //I didn't buid the tab directly because you can't know what is your file's size when you read it. However, you already have to add perks to it. That's why I used a list first, and then converted it.
	{
		perkData[] perkTree = new perkData[L.Count];
		for (int i = 0; i < perkTree.Length; i++) 
		{
			perkTree[i] = L[i];
		}
		Debug.Log ("nombre de compétences dans l'arbre: " + perkTree.Length);
		return perkTree;
	}

	public void drawPerkTree()
	{
		if (valid) 
		{
			dPT (0);
		} 
		else 
		{
			Debug.Log(new Exception("this perkTree is unvalid!"));
		}
	}

	private void dPT(int n)			//just drawing the tree after reading and formating the data from the file. (recursive)
	{
		if (!(n == -1)) //n is the perk id. If it's equal -1, it means that the perk doesn't exists, so it's a stop case.
		{
			if (!(perks[n].drawn)) //second stop case : the perk has already been treated
			{
				Vector3 p1 = new Vector3(perks[n].x, prefab.transform.position.y, perks[n].z);
				perks[n].hex = (GameObject)Instantiate(this.prefab, p1, prefab.transform.rotation);
				perks[n].hex.GetComponentInChildren<TextMesh>().text = formating(perks[n].name);
				perks[n].hex.GetComponentInChildren<TextMesh>().characterSize = 8f / Mathf.Log( (float)perks[n].name.Length);
				perks[n].drawn = true;
				perks[n].hex.transform.SetParent(pos.transform);
				for(int i = 0; i < 6; i ++)
				{
					float angle = Mathf.PI * (1 + 2 * i) / 6; //mathematics!! the angle's formula.
					if (perks[n].neigborsID[i] != -1) //condition to eliminate the problem of a "-1 index"
					{
						perks[perks[n].neigborsID[i]].x = perks[n].x - Mathf.Cos(angle) * 28f; //calculating the new postions of each neighboor of the perk. mathematics!!!
						perks[perks[n].neigborsID[i]].z = perks[n].z - Mathf.Sin(angle) * 28f;
						Vector3 lp = new Vector3(perks[n].x - Mathf.Cos(angle) * 13.9f, link.transform.position.y,perks[n].z - Mathf.Sin(angle) * 14f);
						Transform t = link.transform;
						perks[n].links[i] = ((GameObject)Instantiate(link, lp, t.rotation));
						perks[n].links[i].transform.parent = pos.transform;
						perks[n].links[i].transform.rotation = new Quaternion(0,0,0,0);
						perks[n].links[i].transform.Rotate(90,-((i+2)%6)*60,0);
					}

					dPT(perks[n].neigborsID[i]); //recursive call for each neighboor.
				}
			}
		}
	}

	public List<molecule> readMoleculeFile(string path)
	{
		try
		{
			List<molecule> molList = new List<molecule>();
			StreamReader SR = new StreamReader(path);
			int i = 0;
			while (!SR.EndOfStream)
			{
				i++;
				string [] line = SR.ReadLine().Split(' ');
				string name = "";
				for (int j = 0; j < line.Length - 1; j ++)
				{
					name = name + ' ' + line [j];
				}
				molList.Add(new simpleMolecule(i, name));
				Debug.Log("new molecule: " + name);

			}
			return (molList);
		}
		catch
		{
			Debug.LogError("error trying to read the molecule file");
			return (new List<molecule>());
		}

	}

	static string formating(string input)
	{
		if (input.Length > 20) 
		{
			string[] splitedinput = input.Split (' ');
			string output = "";
			int i = 0;
			while (i < splitedinput.Length) 
			{
				if (i % 2 == 0 && i != 0) 
				{
					output = output + '\n' + splitedinput [i];
				} 
				else 
				{
					output = output + ' ' + splitedinput [i];
				}
				i++;
			}
			byte[] bytes = Encoding.Default.GetBytes(output);
			output = Encoding.UTF8.GetString(bytes);
			return output;
		} 
		else 
		{
			byte[] bytes = Encoding.Default.GetBytes(input);
			input = Encoding.UTF8.GetString(bytes);
			return input;
		}

	}

	public void addData (ref List<moleculePack> L,ref actionData AD, string s)
	{
		string [] data = s.Split(new Char [] {':',';'});
		for (int i = 1; i < data.Length; i++)
		{
			if (data[i].Contains("["))
			{
				moleculePack MP = new moleculePack();
				string [] dataMolecule = data[i].Split(new char[] {'[',']'});
				if (dataMolecule[1].Contains("ATP"))
				{
					AD.ATP = Convert.ToInt32(dataMolecule[0]);
				}
				else
				{
					MP.count = Convert.ToInt32(dataMolecule[0]);
					int j = 0;
					int ID = Convert.ToInt32(dataMolecule[1]);
					while (j < GameMolecules.Count - 1 && (GameMolecules[j].ID != ID))
					{
						j++;
					}
					MP.moleculeType = GameMolecules[j];
					L.Add(MP);
				}
			}
		}
	}

	public void newTreePos()
	{
		Vector3 p = pos.transform.position;
		p.x = cam.transform.position.x;
		p.z = cam.transform.position.z;
		p.y = 10;
		pos.transform.position = p;

	}
}


//the test file I used :


//PERK               							beggining of a perk
//	PNAME:perk1									perk name
//	PID:0										perk ID (sup or equal 0)
//	PDESCRIPTION:ceci est une compétence		description... It's for Romain
//	PNEIGHBORS:1;2;3;4;5;6						neighborhood IDs. -1 means no neighbor
//ENDPERK										end of a perk
//PERK
//	PNAME:perk2
//	PID:1
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:-1;-1;2;0;6;-1
//ENDPERK
//PERK
//	PNAME:perk3
//	PID:2
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:-1;-1;7;3;0;1
//ENDPERK
//PERK
//	PNAME:perk4
//	PID:3
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:2;7;-1;-1;4;0
//ENDPERK
//	PERK
//	PNAME:perk5
//	PID:4
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:0;3;-1;-1;-1;5
//ENDPERK
//PERK
//	PNAME:perk6
//	PID:5
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:6;0;4;-1;-1;-1
//ENDPERK
//PERK
//	PNAME:perk7
//	PID:6
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:-1;1;0;5;-1;-1
//ENDPERK
//PERK
//	PNAME:perk8
//	PID:7
//	PDESCRIPTION:ceci est une compétence
//	PNEIGHBORS:-1;-1;-1;-1;3;2
//ENDPERK
//ENDFILE										end of the file
