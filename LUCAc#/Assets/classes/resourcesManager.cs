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

	private int mol = 0;

	private int min;
	private int max;
	private int total = 2000000000;

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
			displayRessources(_molecules[mol]);
			mol = (mol + 1) % _molecules.Count;
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
						representationMatrix[i,j] = (GameObject)Instantiate(representation, new Vector3 (representation.transform.position.x + (i * 20), representation.transform.position.z + (j * 20), -10), representation.transform.rotation);
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
				RandomPolynom PJ = new RandomPolynom();
				RandomPolynom PI = new RandomPolynom();
				foreach (molecule M in _molecules)
				{
					PJ.newPolynom(-1f, 0f, -3f, 3f, 0f, 0f);
					if (PJ.f (0) <= 0)
					{
						PJ.C = - PJ.f (0) + 100f;
					}
					Debug.Log("fct pour la molecule " + M.name + " en j: " + PJ.A + " " + PJ.B + " " + PJ.C);
					PI.newPolynom(-1f, 0f, -3f, 3f, 0f, 0f);
					if (PI.f (0) <= 0)
					{
						PI.C = - PI.f (0) + 100f;
					}
					Debug.Log("fct pour la molecule " + M.name + " en i: " + PI.A + " " + PI.B + " " + PI.C);
					for (int i = 0; i < 100; i++)
					{
						for (int j = 0; j < 100; j++)
						{
							moleculePack MP = new moleculePack(System.Convert.ToInt32(PJ.f (j - 50) * PI.f (i - 50)), M);
							MP.moleculeType.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
							_moleculeRepartition[i,j].Add(MP);
						}
					}
				}

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
		checkBounds (n);
		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++) 
			{
				representationMatrix[i,j].GetComponent<SpriteRenderer>().color = new Color(m.color.r, m.color.g, m.color.b, ((0.8f * _moleculeRepartition[i,j][n].count) / max) + 0.1f);
			}
		}
	}

	private void checkBounds(int n)
	{
		max = _moleculeRepartition [0, 0][n].count;
		for (int i = 1; i < 100; i++) 
		{
			for (int j = 1; j < 100; j++) 
			{
				int test = _moleculeRepartition [i, j][n].count;
				if (max < test)
				{
					max = test;
				}
			}
		}
	}
}
