using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Linq;

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

	#region accessors
	public int survivedTime 		{ 	get { return _survivedTime; 	} 											}
	public int lifetime 			{ 	get { return _lifeTime;			} 											}
	public bool isSelectioned 		{ 	get { return _isSelectioned; 	} 		set { _isSelectioned = value; 	} 	}
	public bool alive 				{ 	get { return _alive; 			}		set { _alive = value; 			} 	}
	public Species species 			{ 	get { return _species; 			} 		set { _species = value; 		} 	}
	public bool isPlayed 			{ 	get { return _isPlayed; 		} 											}
	public int ATP					{ 	get { return _ATP; 				} 		set { _ATP = value; 			} 	}
	public bool gotDest				{	get { return _gotDest;			}		set { _gotDest = value;			}	}
	public Vector3 target			{	get { return _target;			}		set { _target = value;			}	}
	
	#endregion

	// Use this for initialization
	void Start () 
	{
		transform.Rotate (0, 0, UnityEngine.Random.Range(0,360));
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.GetChild (3).gameObject.SetActive (_isSelectioned);
        if (_isSelectioned == false && _gotDest == false) //random movement
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
			toCorrectPosition(20f);
        }
        else //player control
        {
			if (Input.GetKey(KeyCode.Q))      //Left
			{
				transform.Translate(Vector3.right * Time.deltaTime * 15, Space.World);
			}
			
			if (Input.GetKey(KeyCode.D))     //Right
			{
				transform.Translate(Vector3.right * Time.deltaTime * -15, Space.World);
			}
			
			if (Input.GetKey(KeyCode.S))      //Down
			{
				transform.Translate(Vector3.forward * Time.deltaTime * 15, Space.World);
			}
			
			if (Input.GetKey(KeyCode.Z))       //Top
			{
				transform.Translate(Vector3.forward * Time.deltaTime * -15, Space.World);
			}
			toCorrectPosition(0f);
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
