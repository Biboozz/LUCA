using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class environment : MonoBehaviour {
	
	public List<Species> livings = new List<Species>();
	public GameObject cellPrefab; //just for the test, will not be needed in the futur
	private int coolDown = 0;
	public GameObject UICellImage;

	// Use this for initialization
	void Start () 
	{
		System.Random Rdm = new System.Random (25);
		for (int j = 0; j < 6; j++) 
		{
			Species S = new Species (this, new Color(((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f,((float)Rdm.Next(255))/255f));
			for (int i = 1; i <= 100; i++) 
			{
				S.Individuals.Add (Instantiate(cellPrefab).GetComponent<Individual>());
			}
			S.isPlayed = j == 0;
			for (int i = 0; i < S.Individuals.Count; i++) 
			{
				S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000), 0.1f,UnityEngine.Random.Range(0,2000)), 50000, S, this, j == 0, new List<moleculePack>(), 100);
				S.Individuals[i].representation = UICellImage;
				S.Individuals[i].transform.FindChild("core").gameObject.GetComponent<MeshRenderer>().material.color = S.color;
				S.Individuals[i].transform.FindChild("Membrane").gameObject.GetComponent<MeshRenderer>().material.color = S.color;
			}
			livings.Add (S);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		coolDown = (coolDown + 1) % 10;
		if (coolDown == 0) 
		{
			foreach (Species s in livings)
			{
				s.update();
			}
		}
	}
	

	public void remove(GameObject G)
	{
		Destroy (G);
	}


		

}
