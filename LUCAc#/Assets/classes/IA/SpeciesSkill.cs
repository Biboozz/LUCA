using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class SpeciesSkill : MonoBehaviour {

	public environment Environment;
	public List<Species> _species;
	public Species speciesplayed;
	private int delay;

	// Use this for initialization
	void Start () 
	{
		delay = 0;
	}

	void Update()
	{
		if ((delay % 10800) == 0) //5min
		{
			TimedUpdate ();
			delay = 0;
		}
		delay++;
	}

	void TimedUpdate ()
	{
		_species = Environment.livings;	//Récupération de la liste des espèces
		foreach (Species especes in _species)
		{
			if (especes.isPlayed)	//Si l'espèce est une espèce joué alors on la supprime de la liste
			{
				_species.Remove(especes);
			}
		}


	}
}
