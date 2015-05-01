using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class selectedSpeciesDataDisplayer : MonoBehaviour {

	private Species _species;
	private int lastSelected;
	private Text name;
	private Text nbIndividual;
	private Toggle isPlayedToggle;
	private Toggle color;
	private ListBox speciesCellsMoleculesAverage;

	// Use this for initialization
	void Start () 
	{
		name = transform.FindChild ("name").gameObject.GetComponent<Text> ();
		nbIndividual = transform.FindChild ("nbIndividual").gameObject.GetComponent<Text> ();
		isPlayedToggle = transform.FindChild("isPlayedToggle").gameObject.GetComponent<Toggle> ();
		color = transform.FindChild("speciesColorToggle").gameObject.GetComponent<Toggle> ();
		Transform t = gameObject.transform.FindChild ("moleculeRateListbox");
		Rect listBoxRect = ((RectTransform)t).rect;
		listBoxRect.position = new Vector2 (Screen.width * 0.03f, Screen.height * 0.25f);
		speciesCellsMoleculesAverage = new ListBox (listBoxRect, new Rect (0, 0, 140, 150), false, true);
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
			lastSelected = speciesCellsMoleculesAverage.GetSelectedID();
		}
		//----------
	}
	
	void OnGUI()
	{
		drawListBox ();
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
				name.text = _species.name;
				nbIndividual.text = _species.Individuals.Count.ToString();
				isPlayedToggle.isOn = _species.isPlayed;
				ColorBlock CB = color.colors;
				CB.disabledColor = _species.color;
				color.colors = CB;
			}
		}
	}
}
