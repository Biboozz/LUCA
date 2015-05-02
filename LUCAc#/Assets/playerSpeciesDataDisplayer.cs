using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class playerSpeciesDataDisplayer : MonoBehaviour {

	private Species _species;
	private skill _objective;
	private ListBox _moleculeAverages;
	private ListBox _objectiveMolecules;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//_moleculeAverages.ReDraw ();
		//_objectiveMolecules.ReDraw ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}
}
