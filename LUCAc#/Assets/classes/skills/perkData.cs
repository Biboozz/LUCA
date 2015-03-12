using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

namespace AssemblyCSharp
{

	//contains all informations you need for a specific perk
	public class perkData
	{
		public int[] neigborsID = new int[6];    	// tab of 6 integers representing the perk's neighbors
		public string description = "";				// description readed from the perk tree file
		public int ID = -1;							// base id of the perk
		public string name = "";					// game name of ther perk
		public bool drawn;							// true if the perk is drawn on the screen, false otherwise
		private GameObject _hex;						// hexagon on the screen representing the perk
		public float x;								// position x
		public float z;								// position y
		public GameObject[] links = new GameObject[6];

		public actionData cost;
		public actionData products;
		public actionData onDevCost;
		public actionData onDevProd;
		public bool innate;
		public perkType type;
		public int[] required;

		public perkData (string name, int[] neighborsID, string description, int ID) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
		}

		public perkData (string name, int[] neighborsID, string description, int ID, actionData cost, actionData products, bool innate) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
			this.cost = cost;
			this.products = products;
			this.innate = innate;
		}

		public perkData (string name, int[] neighborsID, string description, int ID, actionData cost, actionData products, bool innate, perkType type) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
			this.cost = cost;
			this.products = products;
			this.innate = innate;
			this.type = type;
		}

		public perkData () : this ("unNamed", new int[]{-1,-1,-1,-1,-1,-1}, "NoDescription", -42, new actionData(), new actionData(), false, 0)//overload constructor
		{
			this.drawn = false;
			onDevCost = new actionData();
			onDevProd = new actionData();
		}

		public GameObject hex
		{
			get
			{
				return _hex;
			}
			set
			{
				_hex = value;
				switch ((int)type)
				{
				case 0:
					_hex.GetComponent<MeshRenderer>().material.color = new Color (0, 0, 1, 1);
					_hex.GetComponentInChildren<TextMesh>().color = new Color (1, 1, 0, 1);
					break;
				case 1:
					_hex.GetComponent<MeshRenderer>().material.color = new Color (0, 1, 0, 1);
					_hex.GetComponentInChildren<TextMesh>().color = new Color (1, 0, 1, 1);
					break;
				case 2:
					_hex.GetComponent<MeshRenderer>().material.color = new Color (1, 0, 0, 1);
					_hex.GetComponentInChildren<TextMesh>().color = new Color (0, 1, 1, 1);
					break;
				case 3:
					_hex.GetComponent<MeshRenderer>().material.color = new Color (0, 1, 1, 1);
					_hex.GetComponentInChildren<TextMesh>().color = new Color (1, 0, 0, 1);
					break;
				case 4:
					_hex.GetComponent<MeshRenderer>().material.color = new Color (1, 0, 1, 1);
					_hex.GetComponentInChildren<TextMesh>().color = new Color (0, 1, 0, 1);
					break;
				}

			}
		}

	}
}

