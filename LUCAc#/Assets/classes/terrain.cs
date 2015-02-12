using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class terrain : MonoBehaviour 
{
	public GameObject prefab;
	public int numberOfObjects = 6;
	public float radius = 200f;
	public perkData[] perks;
	
	void Start() 
	{
		perks = buildPerkTree(readPerkTreeFile ());
		Vector3 v = new Vector3 (0f, 1f, 0f);
		drawPerkTree (0, v);
	}

	void loadPerkMenu()
	{
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = Mathf.PI * (1 + 2 * i) / 6;
			Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 2998);
			Instantiate(prefab, pos, Quaternion.identity);
		}
	}

	private string findPerkTreeFile()
	{
		string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData) + @"\LUCA\perktree.txt";
		if (File.Exists(path))
		{
			return path;
		}
		else
		{
			return("");
		}
	}

	private List<perkData> readPerkTreeFile()
	{
		List<perkData> perkTree = new List<perkData>();
		string path = findPerkTreeFile();
		if (path == "") {
			Debug.Log ("perkTree file not found" + path);
		} 
		else 
		{
			Debug.Log ("the file was found");
			try 
			{
				StreamReader SR = new StreamReader(path);
				string line = SR.ReadLine();
				perkData p = new perkData();

				while (line != "ENDFILE") 
				{
					if (line.Contains("PERK") )
					{
						p = new perkData();
					}
					if (line.Contains("PNAME:")) 
					{
						p.name = line.Substring(10, line.Length - 10);
					}
					if (line.Contains("PID:")) 
					{
						p.ID = Int32.Parse(line.Substring(8, line.Length - 8));
					}
					if (line.Contains("PDESCRIPTION:")) 
					{
						p.description = line.Substring(17, line.Length - 17);
					}
					if (line.Contains("PNEIGHBORS")) 
					{
						string[] s = line.Substring(15, line.Length - 15).Split(';');
						for (int i = 0; i < 6; i++) 
						{
							p.neigborsID[i] = Int32.Parse(s[i]);
						}
					}
					if (line == "ENDPERK") 
					{
						perkTree.Add(p);
					}
					line = SR.ReadLine();
				}

			}
			catch (Exception e)
			{
				Debug.Log(e);
			}
		}
		return perkTree;
	}

	private perkData[] buildPerkTree(List<perkData> L) 
	{
		perkData[] perkTree = new perkData[L.Count];
		for (int i = 0; i < perkTree.Length; i++) 
		{
			perkTree[i] = L[i];
		}
		return perkTree;
	}

	public void drawPerkTree(int n, Vector3 pos) 
	{
		if (n == -1) 
		{
			//nothing for now
		}
		else 
		{
			if (!this.perks[n].drawn) 
			{
				//this.perks[n].hex = Instantiate(this.prefab, pos, Quaternion.identity);
				Instantiate(this.prefab, pos, Quaternion.identity);
				this.perks[n].drawn = true;
				for (int i = 0; i < 6; i++) 
				{
					float angle = Mathf.PI * (1 + 2 * i) / 6;
					Vector3 p = new Vector3(Mathf.Cos(angle) * radius, 1, Mathf.Sin(angle) * radius);
					drawPerkTree(this.perks[n].neigborsID[i], p);
				}
			}
		}
	}
}
