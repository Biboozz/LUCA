using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	public class BoardMap
	{
		//private System.Random rand = new System.Random();

		private environment _env;
		private List<moleculePack> pass = new List<moleculePack>();
		private BMType material;
		public string MaterialName = "";
		//public int materialID = rand.Next(0,4);
		public Color seen;
		public Color acti;
		public Color over;
		public GameObject EvE = GameObject.Find ("EvolveEnabled");
		public GameObject EvD = GameObject.Find ("EvolveDisabled");


		
		public BoardMap (environment env, int MaterialIndex, System.Random rand)
		{
			_env = env;
			RandomMaterial (MaterialIndex);
			GeneratePass (rand);
		}

		public int Exist (molecule Mol, List<moleculePack> L)
		{
			int i = -1;
			do {

				i++;
			} while ((i < L.Count )&&(L[i].moleculeType != Mol));
			
			if(i == L.Count)
				i = -1;

			return i;
		}

		public List<moleculePack> SpecieSum(Species S)
		{
			int ind = 0;
			List<moleculePack> Sum = new List<moleculePack> ();

			foreach (Individual I in S.Individuals) 
			{
				foreach (moleculePack M in I.cellMolecules)
				{
					ind++;

					int exist = Exist (M.moleculeType, Sum);

					if (exist >= 0)
					{
						Sum[exist].count = Sum[exist].count + M.count;
					}
					else
					{
						Sum.Add (M);
					}
				}
			}

			foreach (moleculePack Mp in Sum) 
			{
				Mp.count = Mp.count/ind;
			}

			return Sum;
		}

		public bool EnoughtToPass()
		{
			bool result = true;

			int i = 0;
			do
			{
				i++;
			} while (!_env.livings[i].isPlayed);

			Species S = _env.livings [i];

			List<moleculePack> TotalSpecie = SpecieSum (S);

			foreach (moleculePack Mp in pass) 
			{
				int index = Exist (Mp.moleculeType, TotalSpecie);
				result = result & (index >= 0) & (TotalSpecie[index].count >= Mp.count);
				//Debug.Log (TotalSpecie[index].count + "," + Mp.count + ',' + result);
			}
			return result;
		}

		public bool EnoughtToPass(Individual I)
		{
			bool result = true;
			
			foreach (moleculePack M in pass) 
			{
				foreach(moleculePack Mp in I.cellMolecules)
				{
					if (M.moleculeType == Mp.moleculeType)
					{
						result = result & (M.count <= Mp.count);
						break;
					}
				}
			}
			return result;
		}

		private void RandomMaterial(int index)
		{
			switch (index) {
			case 0:
				{
					material = BMType.Fibre;
					seen = new Color32(174,74,52,255);
					over = new Color32(184,84,62,255);
					acti = new Color (1,1,1,1);
					MaterialName = "Milieux fibreux";
					break;
				}

			case 1:
				{
					material = BMType.Liquid;
					seen = new Color32(53,122,183,255);
					over = new Color32(63,132,193,255);
					acti = new Color (1,1,1,1);
					MaterialName = "Milieu liquide";
					break;
				}

			case 2:
				{
					material = BMType.Gaz;
					seen = new Color32(133,193,126,255);
					over = new Color32(143,203,136,255);
					acti = new Color (1,1,1,1);
					MaterialName = "Milieu gazeux";
					break;
				}

			case 3:
				{
					material = BMType.HalfLiquid;
					seen = new Color32(255,222,117,255);
					over = new Color32(245,212,107,255);
					acti = new Color (1,1,1,1);
					MaterialName = "Milieu semi-liquide";
					break;
				}

			default :
				{
					seen = Color.yellow;
					break;
				}
			}
		}

		public void GeneratePass(System.Random rand)
		{
			List<int> indexl = new List<int>();
			int bigiter = rand.Next (1, 4);
			int index;

			for (int loop = 0; loop<bigiter; loop++)
			{
				int RandPass = 3;// rand.Next (9000, 12000);

				do
				{
				index = (rand.Next (0, 100))%( _env.molecules.Count);
				} while  (indexl.Exists(element => element == index));

				indexl.Add(index);

				//Debug.Log (RandPass + "," + index);

				pass.Add(new moleculePack(RandPass/bigiter, _env.molecules[index])); // A gerrer en fonction de la rareté de la mollecule*/
			}
			//Debug.Log ("----------------------------");
		}

		public void PrintBoardTile()
		{
			Text Pb = GameObject.Find ("EvolveProblems").GetComponent<Text> ();
			Pb.text = "";

			GameObject.Find ("TypeName").GetComponent<Text> ().text = MaterialName;
			int i;

			for (i = 0; i<3; i++) 
			{
				GameObject.Find ("MolleculeName " + i).FindComponent<Text>().text = "";
				GameObject.Find ("MolleculeAmount " + i).FindComponent<Text>().text = "";
			}

			i = 0;

			while (i<pass.Count) 
			{
				GameObject.Find ("MolleculeName " + i).FindComponent<Text>().text = pass[i].moleculeType.name;
				GameObject.Find ("MolleculeAmount " + i).FindComponent<Text>().text = "" + pass[i].count;
				i++;
			}

			Debug.Log ("(" + _env.Playercursor.x + "," + _env.Playercursor.y + "),(" + _env.ButtonCursor.x + "," + _env.ButtonCursor.y + ")");

			bool dist = _env.Playercursor.AdjacentTile (_env.ButtonCursor) > 1;
			bool amount = EnoughtToPass ();;
			bool back = ((_env.Playercursor.x <= _env.ButtonCursor.x)&(_env.Playercursor.y <= _env.ButtonCursor.y));
			bool same = ((_env.Playercursor.x == _env.ButtonCursor.x)&(_env.Playercursor.y == _env.ButtonCursor.y));

			GameObject.Find ("EvolveProblems").GetComponent<Text> ().fontSize = 14;

			if ((!dist) & (amount) & (back) & (!same)) {
				EvE.SetActive (true);
				EvD.SetActive (false);
			} else {
				if (same)
				{
					GameObject.Find ("EvolveProblems").GetComponent<Text> ().fontSize = 22;
					Pb.text = Pb.text + "Votre cellule evolue actuellement dans ce milieu.";
				}
				else{
				if (!back){
					Pb.text = Pb.text + "Vous ne pouvez retro-évoluer. \n";
				}
				else{
					if (dist) {
						Pb.text = Pb.text + "Le terrain selectionné est trop loin de votre éspèces. \n ";
					}
					else
					{
						if (!amount) {
							Pb.text = Pb.text + "Vous n'avez pas les ressources nécéssaires. \n ";
						}
					}
				}
				}

				EvE.SetActive (false);
				EvD.SetActive (true);
			}

			Debug.Log ("(" + amount + "," + back + "," + same + ")");

			GameObject.Find ("EvolveProblems").GetComponent<Text> ().text = Pb.text;

		}
	}
}

