using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Linq;

public class SpeciesSkill : MonoBehaviour {

	public environment Environment;
	public displayPerkTree PerkTree;
	public Species speciesplayed;
	private int delay;
	public System.Random rnd = new System.Random ();

	private List<skill> unlockableSkill = new List<skill>{};
	private List<Species> SpeciesUnPlayed = new List<Species>{};

	// Use this for initialization
	void Start () 
	{
		delay = 1;
	}

	void Update()
	{
		if ((delay % 10800) == 0) //5min
		{
			TimedUpdate ();
			delay = 1;
		}
		delay++;
	}

	public void TimedUpdate ()
	{
		foreach(Species H in Environment.livings)	//Récupération de la liste des espèces
		{
			SpeciesUnPlayed.Add(H);
		}	

		SpeciesUnPlayed = SpeciesUnPlayed.Distinct().ToList<Species>();	//Supprime les doublons - A regler les doublons dans Environment.livings

		List<Species> R = new List<Species>{};	//Liste des espèces à supprimer

		foreach (Species especes in SpeciesUnPlayed)
		{
			if (especes.isPlayed)	//Si l'espèce est une espèce joué alors on la supprime de la liste
			{
				R.Add(especes);
			}
		}
		foreach (Species S in R) //Retirer les espèces jouées
		{
			SpeciesUnPlayed.Remove(S);
		}

		foreach (Species O in SpeciesUnPlayed)	//Pour chaque espèce non joué 
		{
			unlockableSkill = PerkTree.displayUnlocked(O);
			
			if(unlockableSkill.Count > 0)	//Si liste pas vide, sinon rien
			{
				int RandomSkill = rnd.Next(0, unlockableSkill.Count);	//Choisis un skill aléatoirement dans ceux débloquable
				
				O.naturalUnlock(unlockableSkill[RandomSkill]);	//Le débloque

				Debug.Log("L'espèce " + O.name + " a débloqué la compétence " + unlockableSkill[RandomSkill].name);
			}
		}
	}
}
