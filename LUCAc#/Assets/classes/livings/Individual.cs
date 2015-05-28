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
	private int coolDown = 0;
	public environment place;
	public resourcesManager _RM;

	public GameObject descriptionBox;
	private float _duration = 20.0f;	//Diminuer pour plus vite
	private float _speed = 0.05f;	//Quand on augmente va plus vite
	private int _delay = 0;

	#region accessors

	public int survivedTime 				{   get { return _survivedTime; 	} 	}
	public resourcesManager RM 				{ 	get { return _RM; 				}	}
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

		if ((coolDown % species.absorb_cooldown) == 0) {
			eat (_RM);
			coolDown = 0;
		} 

		coolDown++;
	}

//	private int existmol(List<moleculePack> packs, molecule searched)
//	{
//		int i;
//
//		for (i=0; i<packs.Count; i++) {
//			if (packs [i].moleculeType == searched) {
//				return i;
//			}
//		}
//		return (-1);
//	}

	/*public void harvest(ref moleculePack Subject)
	{
		int poslist = existmol (cellMolecules, Subject.moleculeType);

		if (Subject.toxic) { //true si la mollecules est une toxine
			if (poslist > 0) {
				cellMolecules[poslist].count = cellMolecules[poslist].count ; // Ajoute des molecules a l'individu selon le taux subject.count (si elle est presente)
			} else {
				cellMolecules.Add (Subject); //Si la cellule n'est pas deja presente dans la liste alors on l'ajoute
			}
		} 
		else 
		{
			if (poslist >0)
			{
				cellMolecules [poslist].count = cellMolecules [poslist].count + Subject.count;

			}
		}
	}

	public void emite(List<moleculePack> Absorbable, moleculePack Subject)
	{
		
	}

	public void action(resourcesManager R)
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x / 20f);
		int squarey = (int)(pos.y / 20f);

		foreach (moleculePack Subject in R.moleculeRepartition[squarex,squarey]) 
		{
			harvest (ref Subject);
		}
		foreach (moleculePack Subject in _species.workCost.environmentMolecules) 
		{
			emite (R, Subject);
		}
	}*/

	public void eat(resourcesManager R)
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x / 20f);
		int squarey = (int)(pos.y / 20f);
		
		foreach (moleculePack Mi in _cellMolecules) 
		{
			foreach (moleculePack Mc in R.moleculeRepartition[squarex,squarey])
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
		foreach (moleculePack MP in cellMolecules) 
		{

		}
	}


}
