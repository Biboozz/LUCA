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
				representationMatrix = new GameObject[100,100];
				for (int i = 0; i < 100; i++) 
				{
					for (int j = 0; j < 100; j++) 
					{
						representationMatrix[i,j] = (GameObject)Instantiate(representation, new Vector3 (representation.transform.position.x + (i * 20), representation.transform.position.z + (j * 20), 100), representation.transform.rotation);
						representationMatrix[i,j].transform.SetParent(position.transform);
					}
				}
				_moleculeRepartition = new List<moleculePack>[100,100];
				for (int i = 0; i < 100; i++)
				{
					for (int j = 0; j < 100; j++)
					{
						_moleculeRepartition[i,j] = new List<moleculePack>();
					}
				}
				foreach (molecule M in _molecules)
				{
					for (int i = 0; i < 100; i++)
					{
						for (int j = 0; j < 100; j++)
						{
							moleculePack MP = new moleculePack((i + j)* 3,M);
							MP.moleculeType.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
							_moleculeRepartition[i,j].Add(MP);
						}
					}
				}
				//test
				displayRessources(_molecules[0]);
			}
			else
			{
				Debug.Log ("resourcesManager: empty list of molecules loaded.");
			}
		}
	}

	public void displayRessources(molecule m)
	{
		int n = _molecules.FindIndex (M => m.ID == M.ID);
		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++) 
			{
				representationMatrix[i,j].GetComponent<SpriteRenderer>().color = new Color(m.color.r, m.color.g, m.color.b, 200f / (float)_moleculeRepartition[i,j][n].count);
			}
		}
	}
}
