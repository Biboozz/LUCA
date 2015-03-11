﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Linq;

public class Individual : MonoBehaviour
{
	private int _lifeTime;
	public bool alive = true;
	public Species species;
	private int _survivedTime = 0;
	private bool _isPlayed = false;
    private bool _isSelectioned = false;
    private Vector3 click_position;
    private Vector3 cell_position;
	private float distance = 4.5f;
	private bool direction = false;


    public bool isSelectioned
    {
        get { return _isSelectioned; }
        set { _isSelectioned = value; }
    }

	public bool isPlayed
	{
		get { return _isPlayed; }
	}

	public List<moleculePack> cellMolecules = new List<moleculePack>();
	public int ATP;
	private int coolDown = 0;
	private bool initialized = false;
	private environment place;

	#region accessors
	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}

	public int lifetime
	{
		get
		{
			return _lifeTime;
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
		gameObject.transform.GetChild (3).gameObject.SetActive (_isSelectioned);
        if (_isSelectioned == false)
        {
            transform.Translate(0.05f, 0f, 0f);
            transform.Rotate(0, 0, UnityEngine.Random.Range(-2, 3));
            toCorrectPosition();
            if (coolDown >= 10 && initialized)
            {
                coolDown = 0;
                _survivedTime = _survivedTime + 1;
                alive = (_survivedTime < _lifeTime);
                action();
            }
            else
            {
                coolDown++;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))    //if right click
            {
				float _X;
				float _Y;

                cell_position = transform.position;		//Coords of cells selected
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
				click_position  = ray.origin + (ray.direction * distance);    //Coords of Right Click

				click_position = new Vector3(click_position.x, 0.1f, click_position.z);


				Debug.Log(click_position);

				direction = true;	//direction defini by right click
            }

			if(direction)
			{
				cell_position = Vector3.MoveTowards(cell_position, click_position, 10f);
				toCorrectPosition();
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
		this.species = species;
		transform.SetParent(place.transform);
		this.place = place;
		this._isPlayed = isPlayed;
		cellMolecules = molecules;
		this.ATP = ATP;
	}
	
	void toCorrectPosition()
	{
		Vector3 pos = transform.position;
		if (pos.x < 1) 
		{
			pos.x = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(20,30));
		}
		if (pos.x > 2000) 
		{
			pos.x = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(20,30));
		}
		if (pos.z < 1) 
		{
			pos.z = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(20,30));
		}
		if (pos.z > 2000) 
		{
			pos.z = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(20,30));
		}
		transform.position = pos;
	}

	void OnMouseDown()
	{
		Debug.Log("lol");
	}
}
