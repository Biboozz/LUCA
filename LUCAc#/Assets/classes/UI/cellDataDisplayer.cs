using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class cellDataDisplayer : MonoBehaviour {

	private ListBox cellMolecules;
	private int _lastMoleculeSelected;
	private Individual _target;
	public displayPerkTree DPT;
	public selectedSpeciesDataDisplayer SSDD;
	public GameObject representation;
	public resourcesManager RM;

	// Use this for initialization
	void Start () {
		Transform t = gameObject.transform.FindChild ("cellMoleculesListBox");
		Rect listBoxRect = ((RectTransform)t).rect;
		listBoxRect.position = new Vector2 (Screen.width * 0.86f, Screen.height * 0.23f);
		cellMolecules = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
	}
	
	// Update is called once per frame
	void Update () {
	
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
				RM.displayRessources(RM.molecules.Find(m => m.name == cellMolecules.listItems[value -1].ToString()));
			}
		}
	}

	void drawListBox()
	{
		//Click Test
		if (cellMolecules.ReDraw())
		{
			lastMoleculeSelected = cellMolecules.GetSelectedID();
		}
		//----------
	}

	void OnGUI()
	{
		drawListBox ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.label.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	public void displayData(List<moleculePack> MPList)
	{
		cellMolecules.Clear ();
		foreach (moleculePack MP in MPList) 
		{
			cellMolecules.AddItem(MP.moleculeType.name, MP.count);
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
			if (value != null)
			{
				_target = value;
				displayData(_target.cellMolecules);
				transform.FindChild("toggleIsPlayed").gameObject.GetComponent<Toggle>().isOn = _target.isPlayed;
				transform.FindChild("ATP").gameObject.GetComponent<Text>().text = _target.ATP.ToString();
				transform.FindChild("cellSkillButton").gameObject.GetComponent<Button>().interactable = (_target != null);
				transform.FindChild("cellSpeciesButton").gameObject.GetComponent<Button>().interactable = (_target != null);
				representation.transform.FindChild("Membrane").gameObject.GetComponent<Image>().color = _target.species.color;
				representation.transform.FindChild("core").gameObject.GetComponent<Image>().color = _target.species.color;
			}
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
