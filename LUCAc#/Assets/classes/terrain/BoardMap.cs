using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AssemblyCSharp
{
	public class BoardMap
	{
		private environment _env;
		private bool locked;
		private List<int> pass;
		private BMType material;
		private Color seen;

		private System.Random rand = new System.Random();

		public BoardMap ()
		{
			RandomMaterial ();
		}

		private void RandomMaterial()
		{
			int index = rand.Next (0, 4);

			switch (index) 
			{
				case 0 :
					material = BMType.Fibre;
					seen.r = 174;
					seen.g = 74;
					seen.b = 52;
				break;

				case 1 :
					material = BMType.Liquid;
					seen.r = 58;
					seen.g = 142;
					seen.b = 186;
				break;

				case 2 :
					material = BMType.Gaz;
					seen.r = 123;
					seen.g = 160;
					seen.b = 191;
				break;

				case 3 :
					material = BMType.Liquid;
					seen.r = 223;
					seen.g = 175;
					seen.b = 44;
				break;
			}
		}

		private void GeneratePass()
		{
			int bigiter = rand.Next (1, 4);
			
			int i = 0;
			for (int loop = 0; loop<bigiter; loop++)
			{
				int RandPass = rand.Next (9000, 12000);
				int iter = rand.Next (0, 100) % _env.molecules.Count;
				pass.Add(RandPass/bigiter); // A gerrer en fonction de la rareté de la mollecule
			}
			
		}
	}
}

