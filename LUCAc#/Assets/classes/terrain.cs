using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;


//The code within this class is used to read the perktree from a file, format it and draw it.
//it is not yet finished, as you can't draw informations about a specific perk on it, but here's a good track.
//You also can't add a perk to a cell or whatever, for the same reasons.

public class terrain : MonoBehaviour 
{
	public GameObject prefab;		//the base prefab of a hexagon, copied and instanciated to draw the tree;
	public perkData[] perks;		//this is a perkData class tab. As there's no way to use pointers in Unity, I had to build a static graph structure with it... Taugh shit.
	//for more info about the class "perkData", please refer to the file under \Assets\classes\perkData.cs
	
	void Start() 	//Everything happening here can be moved. It's just for testing.
	{
		perks = buildPerkTree(readPerkTreeFile ());
		perks[0].x = 0;
		perks[0].z = 0;
		drawPerkTree (0);
		showPerkTree ();
	}

	private string findPerkTreeFile() // a simple function to make sure that the given folder path exists.
	{
		string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData) + @"\LUCA\perktree.txt";  	//I chose this folder because I don't know how to get the Assets folder. Need to work on it.
		if (File.Exists(path))
		{
			return path;
		}
		else
		{
			return("");  //this is used later to check if the folder path was found or not.
		}
	}

	private List<perkData> readPerkTreeFile()  	//this function is used to read the file where the perktree is written. It builds a list of every readed perk from this file.
												//Please refer to the end of this class to see how the file is built.
	{
		List<perkData> perkTree = new List<perkData>();
		string path = findPerkTreeFile();
		if (path == "") {
			Debug.Log ("perkTree file not found" + path);
		} 
		else 
		{
			Debug.Log ("the file was found");
			try {											//it's not really important if you don't understand what is in this try. Move on. It's just reading the file.
				StreamReader SR = new StreamReader(path);
				string line = SR.ReadLine();
				perkData p = new perkData();
				
				while (line != "ENDFILE") 
				{
					if (line == "PERK")
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
					if (line.Contains("PNEIGHBORS")) 
					{
						string[] s = line.Substring(15, line.Length - 15).Split(';');
						for (int i = 0; i < 6; i++) 
						{
							p.neigborsID[i] = Int32.Parse(s[i]);
							Debug.Log("neighboor " + i + ": " + p.neigborsID[i]);
						}
					}
					if (line == "ENDPERK") 
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
			}
		}
		return perkTree;
	}

	private perkData[] buildPerkTree(List<perkData> L) //this function build the tab of perkData we need for the graph from a list of perkData. Just conversion tab form list.
													   //I didn't buid the tab directly because you can't know what is your file's size when you read it. However, you already have to add perks to it. That's why I used a list first, and then converted it.
	{
		perkData[] perkTree = new perkData[L.Count];
		for (int i = 0; i < perkTree.Length; i++) 
		{
			perkTree[i] = L[i];
		}
		Debug.Log (perkTree.Length - 1);
		return perkTree;
	}

	public void drawPerkTree(int n)			//just drawing the tree after reading and formating the data from the file. (recursive)
	{
		if (!(n == -1)) //n is the perk id. If it's equal -1, it means that the perk doesn't exists, so it's a stop case.
		{
			if (!(perks[n].drawn)) //second stop case : the perk has already been treated
			{
				Vector3 p1 = new Vector3(perks[n].x, 1, perks[n].z);
				perks[n].hex = (GameObject)Instantiate(this.prefab, p1, prefab.transform.rotation);
				perks[n].drawn = true;

				for(int i = 0; i < 6; i ++)
				{
					float angle = Mathf.PI * (1 + 2 * i) / 6; //mathematics!! the angle's formula.
					if (perks[n].neigborsID[i] != -1) //condition to eliminate the problem of a "-1 index"
					{
						perks[perks[n].neigborsID[i]].x = perks[n].x - Mathf.Cos(angle) * 105f; //calculating the new postions of each neighboor of the perk. mathematics!!!
						perks[perks[n].neigborsID[i]].z = perks[n].z - Mathf.Sin(angle) * 105f;
					}

					drawPerkTree(perks[n].neigborsID[i]); //recursive call for each neighboor.
				}
			}
		}
	}

	public void showPerkTree() //show the perktree easily
	{
		foreach (perkData p in perks) 
		{
			p.hex.SetActive(true);
		}
	}


	public void hidePerkTree() //hide the perktree easily
	{
		foreach (perkData p in perks) 
		{
			p.hex.SetActive(false);
		}
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
