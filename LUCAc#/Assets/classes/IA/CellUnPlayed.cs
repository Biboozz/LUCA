using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class CellUnPlayed : MonoBehaviour {

	private Individual I;
	private List<moleculePack> cellMolecules = new List<moleculePack>();	//Liste des molecules de la cellules
	private molecule moleculetarget;

	// Use this for initialization
	void Start () 
	{
		I = gameObject.GetComponentInParent<Individual>();
		cellMolecules = I.cellMolecules;

	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (cellMolecules [0].moleculeType);	//Acces nom de la molecule
		Debug.Log (cellMolecules [0].count);	// Acces montant de la molecule
	}
}
