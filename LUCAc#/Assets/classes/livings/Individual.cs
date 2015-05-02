using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Linq;
using UnityEngine.UI;

public class Individual : MonoBehaviour
{
	private int _lifeTime;
	private bool _alive = true;
	private Species _species;
	private int _survivedTime = 0;
	private bool _isPlayed = false;
    private bool _isSelectioned = false;
	private bool _gotDest = false;
	private Vector3 _target;
	private List<moleculePack> _cellMolecules = new List<moleculePack>();
	private int _ATP;
	private int coolDown = 0;
	private bool initialized = false;
	private environment place;
	public GameObject representation;
	public GameObject descriptionBox;

	#region accessors

	public int survivedTime 				{ 	get { return _survivedTime; 	} 											}
	public int lifetime 					{ 	get { return _lifeTime;			} 											}
	public bool alive 						{ 	get { return _alive; 			}		set { _alive = value; 			} 	}
	public Species species 					{ 	get { return _species; 			} 		set { _species = value; 		} 	}
	public bool isPlayed 					{ 	get { return _isPlayed; 		} 											}
	public int ATP							{ 	get { return _ATP; 				} 		set { _ATP = value; 			} 	}
	public List<moleculePack> cellMolecules	{ 	get { return _cellMolecules; 	} 											}
	public bool gotDest						{	get { return _gotDest;			}		set { _gotDest = value;			}	}
	public Vector3 target					{	get { return _target;			}		set { _target = value;			}	}
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
				representation.transform.FindChild("Membrane").gameObject.GetComponent<Image>().color = _species.color;
				representation.transform.FindChild("core").gameObject.GetComponent<Image>().color = _species.color;
				descriptionBox.GetComponent<cellDataDisplayer>().displayData(_cellMolecules);
				descriptionBox.transform.FindChild("toggleIsPlayed").gameObject.GetComponent<Toggle>().isOn = this.isPlayed;
				descriptionBox.transform.FindChild("ATP").gameObject.GetComponent<Text>().text = this.ATP.ToString();
				descriptionBox.GetComponent<cellDataDisplayer>().target = this;
			}
		} 	
	}
	#endregion

	// Use this for initialization
	void Start () 
	{
		transform.Rotate (0, 0, UnityEngine.Random.Range(0,360));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeScale >= 1f)		//Test si le temps est en pause ou pas
		{
			gameObject.transform.GetChild (3).gameObject.SetActive (_isSelectioned); //selection de la cellule
			if (_isSelectioned == false && _gotDest == false) //est-elle selectionné et n'a pas de dest
			{
				transform.Translate(0.05f, 0f, 0f);
				transform.Rotate(0, 0, UnityEngine.Random.Range(-2, 3));
				
				if (coolDown >= 10 && initialized)
				{
					coolDown = 0;
					_survivedTime = _survivedTime + 1;
					_alive = (_survivedTime < _lifeTime);
					action();
				}
				else
				{
					coolDown++;
				}
				toCorrectPosition(20f); //si mur, changement d'angle
			}
		}
	}
	
	public void action () 
	{
		//not finished yet
	}

	public void Initialize(Vector3 position, int lifeTime, Species species, environment place, bool isPlayed, List<moleculePack> molecules, int ATP)
	{
		transform.position = position;
		_lifeTime = lifetime;
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
		if (pos.z < 1) 
		{
			pos.z = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.z > 2000) 
		{
			pos.z = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		transform.position = pos;
	}
}
