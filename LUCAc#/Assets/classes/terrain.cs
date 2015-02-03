using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class terrain : MonoBehaviour {



	public GameObject prefab;
	public int numberOfObjects = 6;
	public float radius = 200f;
	private List<perkData> perkTree = new List<perkData>();
	
	void Start() 
	{
		readPerkTreeFile ();
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

		private void readPerkTreeFile()
		{
			string path = findPerkTreeFile();
			if (path == "") {
				Debug.Log ("perkTree file not found" + path);
			} 
			else 
			{
				StreamReader SR = new StreamReader(path);
				int i = 0;
				string line = "";
				perkData temp = new perkData();
				while (line != "ENDFILE") 
				{
					line = SR.ReadLine();

					switch(i)
					{
						case 1:
							temp.name = line.Substring(4, line.Length - 4);
							i = 0;
							break;
						case 2:
							temp.ID = Convert.ToInt32(line.Substring(4, line.Length - 4));
							i = 0;
							break;
						case 3:
							temp.description = line.Substring(4, line.Length - 4);
							i = 0;
							break;
						case 4:
							string s = line.Substring(4, line.Length - 4);
							int n = 0;
							while (n < 5)
							{
								string t = "";
								int j = 0;
								while (s[j] != ' ' && j < s.Length)
								{
									t = t + s[j]; //à améliorer!!
									j++;
								}
								
								temp.neigborsID[n] = Convert.ToInt32(t);
								s = s.Substring(t.Length + 1, s.Length - t.Length - 1);
								
								
								n++;
							}
							if (n == 5)
							{
								temp.neigborsID[n] = Convert.ToInt32(s);
							}
							i = 0;
							break;
					}

					if (line == "PERK") 
					{
						;
					}
					if (line == "    PNAME") 
					{
						i = 1;
					}
					if (line == "    PID") 
					{
						i = 2;
					}
					if (line == "    PDESCRIPTION") 
					{
						i = 3;
					}
					if (line == "    PNEIGHBORS") 
					{
						i = 4;
					}
					if (line == "ENDPERK")
					{
						perkTree.Add(new perkData(temp.name, temp.neigborsID, temp.description, temp.ID));
						Debug.Log ("Added perk : '" + temp.name + "' to the main perkTree"); 
						i = 0;
					}
				}
			}
		}

	}
