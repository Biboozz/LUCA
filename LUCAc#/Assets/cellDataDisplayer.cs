﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class cellDataDisplayer : MonoBehaviour {

	private ListBox cellMolecules;
	private int lastSelected;
	private Individual _target;
	public displayPerkTree DPT;
	public selectedSpeciesDataDisplayer SSDD;

	// Use this for initialization
	void Start () {
		Transform t = gameObject.transform.FindChild ("cellMoleculesListBox");
		Rect listBoxRect = ((RectTransform)t).rect;
		listBoxRect.position = new Vector2 (Screen.width * 0.86f, Screen.height * 0.23f);
		cellMolecules = new ListBox (listBoxRect, new Rect (0, 0, 140, 150), false, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void drawListBox()
	{
		//Click Test
		if (cellMolecules.ReDraw())
		{
			lastSelected = cellMolecules.GetSelectedID();
		}
		//----------
	}

	void OnGUI()
	{
		drawListBox ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	public void displayData(List<moleculePack> MPList)
	{
		cellMolecules.Clear ();
		foreach (moleculePack MP in MPList) 
		{
			cellMolecules.AddItem(MP.moleculeType.name + " - " + MP.count);
		}
	}

	public Individual target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			transform.FindChild("cellSkillButton").gameObject.GetComponent<Button>().interactable = (_target != null);
			transform.FindChild("cellSpeciesButton").gameObject.GetComponent<Button>().interactable = (_target != null);
		}
	}

	public void display()
	{
		if (_target != null) 
		{
			DPT.display(target.species);
		}
	}

	public void displaySpecies()
	{
		SSDD.species = target.species;
	}
}