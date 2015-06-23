using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Diagnostics;
using System.Linq;
using UnityEngine.UI;

public class Individual : MonoBehaviour
{
	private bool _alive = true;
	private Species _species;
	private int _survivedTime = 0;
	private bool _isPlayed = false;
    private bool _isSelectioned = false;
	private bool _gotDest = false;
	private Vector3 _target;
	private List<moleculePack> _cellMolecules = new List<moleculePack>();
	private int _ATP;
	private bool _consumeATP = true;
	private List<Individual> _group;
	public environment place;
	public resourcesManager _RM;

	public GameObject descriptionBox;
	private float _duration = 20.0f;	//Diminuer pour plus vite
	private float _speed = 0.05f;	//Quand on augmente va plus vite
	private int _delay = 0;

	private int _splitDelay = UnityEngine.Random.Range(1800, 36000);
	private int _splitIncrement;

	private int vieilDelta = 0;


	#region accessors
	public List<Individual> group			{ 	get { return _group; } 					set { _group = value; 			} 	}
	public int survivedTime 				{   get { return _survivedTime; 	} 	}
	public resourcesManager RM 				{ 	get { return _RM; 				}		set { _RM = value;				}	}
	public bool alive 						{ 	get { return _alive; 			}		set { _alive = value; 			} 	}
	public Species species 					{ 	get { return _species; 			} 		set { _species = value; 		} 	}
	public bool isPlayed 					{ 	get { return _isPlayed; 		} 		set { _isPlayed = value; 		}	}
	public int ATP							{ 	get { return _ATP; 				} 		set { _ATP = value; 			} 	}
	public List<moleculePack> cellMolecules	{ 	get { return _cellMolecules; 	} 											}
	public bool gotDest						{	get { return _gotDest;			}		set { _gotDest = value;			}	}
	public Vector3 target					{	get { return _target;			}		set { _target = value;			}	}
	public bool consumeATP					{	get { return _consumeATP;		}		set { _consumeATP = value;		}	}
	public float duration 					{ 	get { return _duration; 		} 		set { _duration = value; 		} 	}
	public float speed						{	get { return _speed;			}		set { _speed = value;			}	}
	public int delay						{ 	get { return _delay; 			} 		set { _delay= value; 			} 	}

	public bool isSelectioned
	{ 	
		get 
		{ 
			return _isSelectioned; 	
		} 		
		set 
		{ 
			_isSelectioned = value;
			if (value)
			{
				descriptionBox.GetComponent<cellDataDisplayer>().target = this;
			}
		} 	
	}
	#endregion

	// Use this for initialization
	void Start ()
	{
		delay = UnityEngine.Random.Range (0, 420);
		_RM = place.gameObject.GetComponent<resourcesManager> ();
		transform.Rotate (0, 0, UnityEngine.Random.Range(0,360));
		GetComponent<actionManager> ().Initialize ();
		GetComponent<actionManager> ().addAction (eat);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeScale >= 1f)		//Test si le temps est en pause ou pas
		{
			gameObject.transform.GetChild (3).gameObject.SetActive (_isSelectioned); //selection de la cellule
			if ((!_isSelectioned && !_gotDest) || (_isSelectioned && !_isPlayed && !_gotDest)) //Les cellules du joueur non selectionnees se deplacent
			{
				transform.Translate(speed, 0f, 0f);
				transform.Rotate(0, 0, UnityEngine.Random.Range(-2, 3));

				toCorrectPosition(20f); //si mur, changement d'angle
			}
		}

		if (_splitIncrement == _splitDelay) 
		{
			_splitIncrement = 0;
			GetComponent<actionManager> ().addAction (division);
		}

		_splitIncrement++;

		vieilDelta++;
		if (vieilDelta >= 60) 
		{
			vieilDelta = 0;
			_survivedTime++;
			if (_survivedTime >= _species.individualLifeTime)
			{
				alive = false;
			}
		}
	}

	private int existmol(List<moleculePack> packs, molecule searched)
	{
		int i = 0;

		foreach (moleculePack M in packs) 
		{
			if (M.moleculeType == searched){
				return i;
			}
			i++;
		}
		return (-1);
	}

	#region action------------------------------------------------------------

	public void harvest(moleculePack Tile)
	{
		if (Tile.count > 0) 
		{
			int posowned = existmol (cellMolecules,Tile.moleculeType);
			
			if (Tile.moleculeType.toxic)
			{
				if (Tile.count > species.toxic_absorb)
				{
					Tile.count -= species.toxic_absorb;
					
					if (posowned == -1)
					{
						cellMolecules.Add(new moleculePack(species.toxic_absorb,Tile.moleculeType));
					}
					else
					{
						cellMolecules[posowned].count += species.toxic_absorb;
					}
				}
				else
				{
					if (posowned == -1)
					{
						cellMolecules.Add(Tile);
					}
					else
					{
						cellMolecules[posowned].count += Tile.count;
					}
					Tile.count = 0;
				}
			}
			else
			{
				molecule temp = Tile.moleculeType;
				
				int posskill = existmol(species.workCost.environmentMolecules, temp);
				
				if(posskill >= 0)
				{
					moleculePack Skill = species.workCost.environmentMolecules[posskill];
					
					if (Tile.count > Skill.count)
					{
						Tile.count -= Skill.count;
						
						if (posowned == -1)
						{
							cellMolecules.Add(new moleculePack(Skill.count,Tile.moleculeType));
						}
						else
						{
							cellMolecules[posowned].count += Skill.count;
						}
					}
					else
					{
						if (posowned == -1)
						{
							cellMolecules.Add(Tile);
						}
						else
						{
							cellMolecules[posowned].count += Tile.count;
						}
					}
				}
			}
		}
	}

	public void emite(moleculePack Emission, List<moleculePack> Tile)
	{
		int poslist = existmol (Tile, Emission.moleculeType);

		if (poslist == -1) 
		{
			Tile.Add(Emission);
		} 
		else 
		{
			Tile[poslist].count += Emission.count;
		}
	}

	//public void action(resourcesManager R)
	public void action()
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x / 20f);
		int squarey = (int)(pos.y / 20f);

		//foreach (moleculePack Tile in R.moleculeRepartition[squarex,squarey]) 
		foreach (moleculePack Tile in _RM.moleculeRepartition[squarex,squarey]) 
		{
			harvest (Tile);
		}
		foreach (moleculePack Subject in _species.workProducts.cellMolecules) 
		{
			emite (Subject, _RM.moleculeRepartition[squarex,squarey]);
		}
	}

	#endregion action--------------------------------------------------------------------

	public bool eat()
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x/ 20f);
		int squarey = (int)(pos.y/ 20f);
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (0 > squarex)
		{
			squarex = 0;
		}
		if (0 > squarey) 
		{
			squarey = 0;
		}
		foreach (moleculePack Mi in _cellMolecules) 
		{
			foreach (moleculePack Mc in _RM.moleculeRepartition[squarex,squarey])
			{
				if ((Mi.moleculeType == Mc.moleculeType)&&(Mc.count > 0))
				{
					
					if (Mc.count > _species.absorb_amount) 
					{
						Mi.count += _species.absorb_amount;
						Mc.count -= _species.absorb_amount;
					}
					else
					{
						Mi.count += Mc.count;
						Mc.count = 0;
					}
				}
			}
		}
		return false;
	}
	

	public void Initialize(Vector3 position, int lifeTime, Species species, environment place, bool isPlayed, List<moleculePack> molecules, int ATP)
	{
		transform.position = position;
		_species = species;
		transform.SetParent(place.transform);
		this.place = place;
		_isPlayed = isPlayed;
		_cellMolecules = molecules;
		_ATP = ATP;
	}

	public void toCorrectPosition(float angle) //correct the cell position, rotate it of the angle value
	{
		Vector3 pos = transform.position;
		if (pos.x < 1) 
		{
			pos.x = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.x > 2000) 
		{
			pos.x = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.y < 1) 
		{
			pos.y = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.y > 2000) 
		{
			pos.y = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		transform.position = pos;
	}

	public void splitGive(Individual I)
	{
		foreach (moleculePack mp in cellMolecules) 
		{
			mp.count = mp.count / 2;
			moleculePack nmp = new moleculePack(mp.count, mp.moleculeType);
			I.cellMolecules.Add(nmp);
		}
	}

	public bool division()
	{
		GameObject son = (GameObject)Instantiate (_species.cell);
		splitGive (son.GetComponent<Individual> ());
		_species.addCell (son.GetComponent<Individual>());
		son.GetComponent<Individual>().descriptionBox = descriptionBox;
		ATP = ATP / 2;
		son.GetComponent<Individual>().Initialize(transform.position, 0, this._species, this.place, this.isPlayed, new List<moleculePack>(), ATP);
		splitGive (son.GetComponent<Individual> ());
		son.GetComponent<CellUnPlayed> ().Initialize ();
		return true;
	}
}
