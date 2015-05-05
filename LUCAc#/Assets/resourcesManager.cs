using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class resourcesManager : MonoBehaviour {
	
	private List<molecule> _molecules;
	private List<moleculePack>[,] _moleculeRepartition;
	public GameObject representation;
	private GameObject[,] representationMatrix;
	public GameObject position;
	private bool _shown;

	// Use this for initialization
	void Start () 
	{
		_moleculeRepartition = new List<moleculePack>[100,100];
		representationMatrix = new GameObject[100,100];
		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++) 
			{
				representationMatrix[i,j] = (GameObject)Instantiate(representation, new Vector3 (representation.transform.position.x + (i * 20), 1, representation.transform.position.z + (j * 20)), representation.transform.rotation);
				representationMatrix[i,j].transform.SetParent(position.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			_shown = !_shown;
			position.SetActive (_shown);
		}

	}

	public List<molecule> molecules
	{
		get
		{
			return _molecules;
		}
		set
		{
			_molecules = value;
			if (_molecules != null)
			{

			}
		}
	}
}
