using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class resourcesManager : MonoBehaviour {
	
	private List<molecule> _molecules;
	private List<moleculePack>[,] _moleculeRepartition;
	public GameObject representation;
	private GameObject[,] representationMatrix;
	public GameObject position;
	private bool _shown;
	public GameObject displayer;

	private int min;
	private int max;

	private int counter = 0;
	private molecule focus;

	public List<moleculePack>[,] moleculeRepartition	{ 	get { return _moleculeRepartition; 	}	}

	// Use this for initialization
	void Start () 
	{

	}
	// Update is called once per frame
	void Update () 
	{
		counter++;
		if (counter == 60) 
		{
			counter = 0;
			if (focus != null)
			{
				displayRessources(focus);
			}
		}

		int countToDisplay = moleculeCount ();
		if (countToDisplay != -1) 
		{
			displayer.SetActive (true);
			displayer.transform.position = Input.mousePosition;
			displayer.transform.GetChild (0).FindChild ("count").GetComponent<Text> ().text = countToDisplay.ToString ();
		} 
		else
		{
			displayer.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			hide ();
		}
	}

	private int moleculeCount()
	{
		if (focus != null) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) 
			{
				Vector3 newPosition = hit.point;
				int x = ((int)newPosition.x)/20;
				int y = ((int)newPosition.y)/20;
				if (x < 0 || 99 < x || y < 0 || 99 < y)
				{
					return -1;
				}
				return _moleculeRepartition[x,y].Find(mp => mp.moleculeType.ID == focus.ID).count;
			}
			else 
			{
				return -1;
			}
		}
		return -1;
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
						representationMatrix[i,j] = (GameObject)Instantiate(representation, new Vector3 (representation.transform.position.x + (i * 20 + 10), representation.transform.position.z + (j * 20 + 10), -10), representation.transform.rotation);
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
						System.Random RDM = new System.Random();
						int N = (int)(200f * M.rarity);
						addSmallCircle(RDM.Next(10, 90),RDM.Next(10, 90), M);
						while (N > 0)
						{
							N--;
							addSmallCircle(RDM.Next(10, 90),RDM.Next(10, 90), M);
						}
						for (int i = 0; i < 100; i++)
						{
							for (int j = 0; j < 100; j++)
							{
								moleculePack mp = new moleculePack(0, M);
								_moleculeRepartition[i,j].Add(mp);
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

	public void addRandomForm (int decalI, int decalJ, molecule M)
	{
		RandomPolynom PJ = new RandomPolynom();
		RandomPolynom PI = new RandomPolynom();
		PJ.newPolynom(-1f, 0f, -3f, 3f, 0f, 0f);
		if (PJ.f (0) <= 0)
		{
			PJ.C = - PJ.f (0) + 100f;
		}
		PI.newPolynom(-1f, 0f, -3f, 3f, 0f, 0f);
		if (PI.f (0) <= 0)
		{
			PI.C = - PI.f (0) + 100f;
		}
		for (int i = 0; i < 100; i++)
		{
			for (int j = 0; j < 100; j++)
			{
				moleculePack MPtest = _moleculeRepartition[i,j].Find(mp => mp.moleculeType.ID == M.ID);
				if (MPtest == null)
				{
					moleculePack MP = new moleculePack(System.Convert.ToInt32(PJ.f (j - decalJ) * PI.f (i - decalI)), M);
					MP.moleculeType.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
					_moleculeRepartition[i,j].Add(MP);
				}
				else
				{
					MPtest.count = MPtest.count + System.Convert.ToInt32(PJ.f (j - decalJ) * PI.f (i - decalI));
				}
			}
		}
	}

	public void addRandomCircle (int decalI, int decalJ, molecule M)
	{
		RandomPolynom P = new RandomPolynom();
		P.newPolynom(-1f, 0f, -3f, 3f, 0f, 0f);
		if (P.f (0) <= 0)
		{
			P.C = - P.f (0) + 100f;
		}
		for (int i = 0; i < 100; i++)
		{
			for (int j = 0; j < 100; j++)
			{
				moleculePack MPtest = _moleculeRepartition[i,j].Find(mp => mp.moleculeType.ID == M.ID);
				if (MPtest == null)
				{
					moleculePack MP = new moleculePack(System.Convert.ToInt32(P.f (j - decalJ) * P.f (i - decalI)), M);
					MP.moleculeType.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
					_moleculeRepartition[i,j].Add(MP);
				}
				else
				{
					MPtest.count = MPtest.count + System.Convert.ToInt32(P.f (j - decalJ) * P.f (i - decalI));
				}
			}
		}
	}

	public void addSmallCircle (int decalI, int decalJ, molecule M)
	{
		RandomPolynom P = new RandomPolynom();
		P.newPolynom(-0.8559355f, -0.8559355f, -1.737144f, -1.737144f, 0f, 0f);
		if (P.f (0) <= 0)
		{
			P.C = - P.f (0) + 100f;
		}
		for (int i = 0; i < 100; i++)
		{
			for (int j = 0; j < 100; j++)
			{
				moleculePack MPtest = _moleculeRepartition[i,j].Find(mp => mp.moleculeType.ID == M.ID);
				if (MPtest == null)
				{
					moleculePack MP = new moleculePack(System.Convert.ToInt32(P.f (j - decalJ) * P.f (i - decalI)), M);
					MP.moleculeType.color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
					_moleculeRepartition[i,j].Add(MP);
				}
				else
				{
					MPtest.count = MPtest.count + System.Convert.ToInt32(P.f (j - decalJ) * P.f (i - decalI));
				}
			}
		}
	}

	public void displayRessources(molecule m)
	{
		focus = m;
		position.SetActive (true);
		checkBounds (m);
		max += 1000;
		min += 500;

		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++)
			{
				representationMatrix[i,j].GetComponent<SpriteRenderer>().color = new Color(m.color.r, m.color.g, m.color.b, ((1f * _moleculeRepartition[i,j].Find(M => m.ID == M.moleculeType.ID).count) / max));
			}
		}

	}

	public void hide()
	{

		position.SetActive (false);
		focus = null;
	}

	private void checkBounds(molecule m)
	{
		max = 0;
		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++) 
			{
				int test = _moleculeRepartition [i, j].Find(M => M.moleculeType.ID == m.ID).count;
				if (max < test)
				{
					max = test;
				}
			}
		}
	}

	public void ClearRessourceManager()
	{
		for (int i = 0; i < 100; i++) 
		{
			for (int j = 0; j < 100; j++) 
			{
				_moleculeRepartition[i,j].Clear();
				/*foreach (moleculePack M in _moleculeRepartition [i, j]){
					M.count = 0;
				}*/
				Destroy (representationMatrix[i,j]);
			}
		}
	}


}
