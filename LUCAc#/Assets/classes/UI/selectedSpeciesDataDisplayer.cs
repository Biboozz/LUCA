using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class selectedSpeciesDataDisplayer : MonoBehaviour {

	private Species _species;
	//private int lastSelected;
	private Text _name;
	private Text nbIndividual;
	private Toggle isPlayedToggle;
	private Toggle color;
	private ListBox speciesCellsMoleculesAverage;
	public resourcesManager RM;
	private int _lastMoleculeSelected;

	// Use this for initialization
	void Start () 
	{
		_name = transform.FindChild ("name").gameObject.GetComponent<Text> ();
		nbIndividual = transform.FindChild ("nbIndividual").gameObject.GetComponent<Text> ();
		isPlayedToggle = transform.FindChild("isPlayedToggle").gameObject.GetComponent<Toggle> ();
		color = transform.FindChild("speciesColorToggle").gameObject.GetComponent<Toggle> ();
		Transform t = gameObject.transform.FindChild ("moleculeRateListbox");
		Rect listBoxRect = ((RectTransform)t).rect;
		listBoxRect.position = new Vector2 (Screen.width * 0.03f, Screen.height * 0.25f);
		speciesCellsMoleculesAverage = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void drawListBox()
	{
		//Click Test
		if (speciesCellsMoleculesAverage.ReDraw())
		{
			lastMoleculeSelected = speciesCellsMoleculesAverage.GetSelectedID();
		}
		//----------
	}
	
	void OnGUI()
	{
		drawListBox ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	private int lastMoleculeSelected
	{
		get
		{
			return _lastMoleculeSelected;
		}
		set
		{
			if (_lastMoleculeSelected == value)
			{
				RM.hide ();
				_lastMoleculeSelected = -1;
			}
			else
			{
				_lastMoleculeSelected = value;
				RM.displayRessources(RM.molecules.Find(m => m.name == speciesCellsMoleculesAverage.listItems[value -1].ToString()));
			}
		}
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
				_name.text = _species.name;
				nbIndividual.text = _species.Individuals.Count.ToString();
				isPlayedToggle.isOn = _species.isPlayed;
				ColorBlock CB = color.colors;
				CB.disabledColor = _species.color;
				color.colors = CB;
				List<moleculePack> moleculePackSum = new List<moleculePack>();
				speciesCellsMoleculesAverage.Clear();
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
					speciesCellsMoleculesAverage.AddItem(MP.moleculeType.name + " - " + MP.count.ToString());
				}
			}
		}
	}
}
