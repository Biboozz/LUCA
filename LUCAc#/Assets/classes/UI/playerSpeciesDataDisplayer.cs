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

	private Text nbIndividual;
	private Text name;
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
		name = transform.FindChild("name").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_species != null)
		{
			nbIndividual.text = _species.Individuals.Count.ToString();
			name.text = _species.name;
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
				_moleculeAverages.AddItem(MP.moleculeType.name + " - " + MP.count.ToString());
			}
		}
	}

	void OnGUI()
	{
		_moleculeAverages.ReDraw ();
		_objectiveMolecules.ReDraw ();
		GUI.skin.button.fontSize = 11;
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
				name.text = _species.name;
				USW.player = _species;
			}
		}
	}
}
