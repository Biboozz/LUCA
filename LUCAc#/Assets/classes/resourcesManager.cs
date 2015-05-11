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
		_moleculeRepartition = new List<moleculePack>[200,200];
		representationMatrix = new GameObject[200,200];
		for (int i = 0; i < 200; i++) 
		{
			for (int j = 0; j < 200; j++) 
			{
				representationMatrix[i,j] = (GameObject)Instantiate(representation, new Vector3 (representation.transform.position.x + (i * 10), representation.transform.position.z + (j * 10), 120), representation.transform.rotation);
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
