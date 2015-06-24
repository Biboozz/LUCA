using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class playerSpeciesDataDisplayer : MonoBehaviour {

	private Species _species;
	private skill _objective;
	private ListBox _moleculeAverages;
	private ListBox _objectiveMolecules;
	private int _lastMoleculeSelectedAv;
	private int _lastMoleculeSelectedOb;
	public resourcesManager RM;

	private Text nbIndividual;
	private Text _name;
	private Toggle color;

	public unlockSkillWindow USW;

	// Use this for initialization
	void Start () 
	{
		Transform t = gameObject.transform.FindChild ("objectiveListbox");
		Rect listBoxRect = ((RectTransform)t).rect;
		listBoxRect.position = new Vector2 (Screen.width * 0.115f, Screen.height * 0.71f);
		_objectiveMolecules = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
		listBoxRect.position = new Vector2 (Screen.width * 0.005f, Screen.height * 0.71f);
		_moleculeAverages = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
		color = transform.FindChild("speciesColorToggle").gameObject.GetComponent<Toggle>();
		nbIndividual = transform.FindChild("nbIndividual").gameObject.GetComponent<Text>();
		_name = transform.FindChild("name").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_species != null)
		{
			nbIndividual.text = _species.Individuals.Count.ToString();
			_name.text = _species.name;
			List<moleculePack> moleculePackSum = new List<moleculePack>();
			_moleculeAverages.Clear();
			foreach (Individual I in _species.Individuals)
			{
				foreach (moleculePack mp in I.cellMolecules)
				{
					moleculePack MPS = moleculePackSum.Find(MP => MP.moleculeType.ID == mp.moleculeType.ID);
					if (MPS == null)
					{
						moleculePackSum.Add(mp);
					}
					else
					{
						MPS.count = MPS.count + mp.count;
					}
				}
			}
			foreach (moleculePack MP in moleculePackSum)
			{
				MP.count = MP.count / _species.Individuals.Count;
				_moleculeAverages.AddItem(MP.moleculeType.name, MP.count);
			}
		}
	}

	void OnGUI()
	{
		if (_moleculeAverages.ReDraw ()) 
		{
			lastMoleculeSelectedAv = _moleculeAverages.GetSelectedID();
		}
		if (_objectiveMolecules.ReDraw ()) 
		{
			lastMoleculeSelectedOb = _objectiveMolecules.GetSelectedID();
		}
		GUI.skin.button.fontSize = 11;
		GUI.skin.label.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	public Species species
	{
		get
		{
			return _species;
		}
		set
		{
			_species = value;
			if (_species != null)
			{
				ColorBlock CB = color.colors;
				CB.disabledColor = _species.color;
				color.colors = CB;
				nbIndividual.text = _species.IndividualsNumber.ToString();
				_name.text = _species.name;
				USW.player = _species;
			}
		}
	}

	private int lastMoleculeSelectedAv
	{
		get
		{
			return _lastMoleculeSelectedAv;
		}
		set
		{
			if (_lastMoleculeSelectedAv == value)
			{
				RM.hide ();
				_lastMoleculeSelectedAv = -1;
			}
			else
			{
				_lastMoleculeSelectedAv = value;
				RM.displayRessources(RM.molecules.Find(m => m.name == _moleculeAverages.listItems[value -1].ToString()));
			}
		}
	}

	private int lastMoleculeSelectedOb
	{
		get
		{
			return _lastMoleculeSelectedOb;
		}
		set
		{
			if (_lastMoleculeSelectedOb == value)
			{
				RM.hide ();
				_lastMoleculeSelectedOb = -1;
			}
			else
			{
				_lastMoleculeSelectedOb = value;
				RM.displayRessources(RM.molecules.Find(m => m.name == _objectiveMolecules.listItems[value -1].ToString()));
			}
		}
	}

	public void setObjective(skill S)
	{
		_objectiveMolecules.Clear ();
		if (S.devCosts.ATP != 0) 
		{
			_objectiveMolecules.AddItem("ATP", S.devCosts.ATP);
		}
		foreach (moleculePack mp in S.devCosts.cellMolecules) 
		{
			_objectiveMolecules.AddItem(mp.moleculeType.name, mp.count);
		}
	}
}
