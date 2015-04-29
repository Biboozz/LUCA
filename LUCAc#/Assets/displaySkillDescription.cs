﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Collections.Generic;

public class displaySkillDescription : MonoBehaviour {

	private skill _skill;
	private bool visible;
	private List<skill> _perkTree;

	// Use this for initialization
	void Start () 
	{
		visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public skill Skill
	{
		get
		{
			return _skill;
		}
		set
		{
			_skill = value;
			transform.GetChild(1).GetComponent<Text> ().text = _skill.description;
			transform.GetChild(0).GetComponent<Text> ().text = _skill.name;
		}
	}

	public List<skill> perkTree
	{
		get
		{
			return _perkTree;
		}
		set
		{
			_perkTree = value;
		}
	}

	public void click()
	{
		visible = !visible;
		_skill.hex.transform.SetAsLastSibling();
		_skill.hex.transform.GetChild (1).GetComponent<Image> ().enabled = visible;
		foreach (Text T in _skill.hex.transform.GetChild (1).GetComponentsInChildren<Text>()) 
		{
			T.enabled = visible;
		}
	}
}
