using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AssemblyCSharp
{
	public class BoardMap
	{
		private System.Random rand = new System.Random();

		private environment _env;
		private List<moleculePack> pass = new List<moleculePack>();
		private BMType material;
		public string MaterialName = "";
		//public int materialID = rand.Next(0,4);
		public Color seen;
		public Color acti;
		public Color over;
		public GameObject Button;
		
		public BoardMap (environment env, int MaterialIndex)
		{
			_env = env;
			RandomMaterial (MaterialIndex);
			GeneratePass ();
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
					MaterialName = "Milieu Visqueux";
					break;
				}

			default :
				{
					seen = Color.yellow;
					break;
				}
			}
		}

		public void GeneratePass()
		{
			int bigiter = rand.Next (1, 4);

			for (int loop = 0; loop<bigiter; loop++)
			{
				int RandPass = rand.Next (9000, 12000);
				int index = (rand.Next (0, 100))%( _env.molecules.Count);
				pass.Add(new moleculePack(RandPass/bigiter, _env.molecules[index])); // A gerrer en fonction de la rareté de la mollecule
			}
			
		}
	}
}

