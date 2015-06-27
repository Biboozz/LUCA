using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class SpeciesSkill : MonoBehaviour {

	public environment Environment;
	public displayPerkTree PerkTree;
	public Species speciesplayed;
	private int delay;
	public System.Random rnd = new System.Random ();

	private List<skill> allskill = new List<skill>{};
	private List<skill> unlockableSkill = new List<skill>{};

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
		List<Species> B = Environment.livings;	//Récupération de la liste des espèces
		List<Species> R = new List<Species>{};	//Liste des espèces à supprimer
		foreach (Species especes in B)
		{
			if (especes.isPlayed)	//Si l'espèce est une espèce joué alors on la supprime de la liste
			{
				R.Add(especes);
			}
		}
		foreach (Species S in R) //Retirer les espèces jouées
		{
			B.Remove(S);
		}
		PickupSkill (B);
	}

	public void PickupSkill(List<Species> species)
	{
		for (int i = 0; i < species.Count; i++) 
		{
			SpeciesUnlock(species[i]);
		}
	}

	public void SpeciesUnlock(Species A)
	{
		foreach(skill P in PerkTree.perkTree)
		{
			allskill.Add(P);
		}
		
		foreach(skill F in A.unlockedPerks)
		{
			if(PerkTree.isUnlockable(F, A))	//Si la compétence est débloquable
			{
				unlockableSkill.Add(F);	//Ajout le skill a la liste finale
			}
		}
		
		if(unlockableSkill.Count > 0)	//Si liste pas vide, sinon rien
		{
			int RandomSkill = rnd.Next(0, unlockableSkill.Count);	//Choisis un skill aléatoirement dans ceux débloquable
			
			A.naturalUnlock(unlockableSkill[RandomSkill]);	//Le débloque
		}
	}
}
