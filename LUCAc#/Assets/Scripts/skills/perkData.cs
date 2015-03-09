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
		public GameObject hex;						// hexagon on the screen representing the perk
		public float x;								// position x
		public float z;								// position y
		public GameObject[] links = new GameObject[6];

		public actionData cost;
		public actionData products;
		public bool active;
		public perkType type;

		public perkData (string name, int[] neighborsID, string description, int ID) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
		}

		public perkData (string name, int[] neighborsID, string description, int ID, actionData cost, actionData products, bool active) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
			this.cost = cost;
			this.products = products;
			this.active = active;
		}

		public perkData (string name, int[] neighborsID, string description, int ID, actionData cost, actionData products, bool active, perkType type) //use this for initialisation
		{
			this.neigborsID = neighborsID;
			this.description = description;
			this.ID = ID;
			this.drawn = false;
			this.cost = cost;
			this.products = products;
			this.active = active;
			this.type = type;
		}

		public perkData () //overload constructor
		{
			this.drawn = false;
		}

	}
}

