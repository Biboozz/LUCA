using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class ConsoleInitializer : MonoBehaviour {

	public environment Environment;
	public displayPerkTree SpecsTree;

	public List<Species> _species;
	public Species speciesplayed;
	public List<Individual> cellsplayed;
	public List<skill> _allskill;
	public List<skill> _skillunlock;

	void Start () 
	{
		var repo = ConsoleCommandsRepository.Instance;
		repo.RegisterCommand("god", God);
		repo.RegisterCommand("speed", Speed);
		repo.RegisterCommand("speedbase", SpeedBase);
		repo.RegisterCommand("allunlock", Allunlock);
		repo.RegisterCommand("help", Help);

		//_species = Environment.livings;	//Liste des espèces
		//_allskill = SpecsTree.perkTree;	//Liste des compétences
		//_skillunlock = Especes.unlockedPerks; //Liste des compétences débloqués de l'espèce
	}
	
	public string God(params string[] args) {
		foreach(Individual I in cellsplayed)
		{
			if(I.isPlayed)
			{
				I.ATP = 1000;
				I.consumeATP = false;	//Cellule ne consomme plus son énergie
			}
		}
		return "Vos cellules sont désormais immortelles";
	}
	
	public string Speed(params string[] args) {
		var speed = args[0];
		foreach(Individual I in cellsplayed)
		{
			if(I.isPlayed)
			{
				I.duration = float.Parse(speed) ;
				I.speed = 1/(float.Parse(speed) / 100);
			}
		}
		return "Votre vitesse est désormais de " + speed;
	}

	public string SpeedBase(params string[] args) {
		foreach(Individual I in cellsplayed)
		{
			if(I.isPlayed)
			{
				I.duration = 20f;
				I.speed = 0.05f;
			}
		}
		return "La vitesse de base a été rétabli";
	}
	
	public string Allunlock(params string[] args) {
		_species = Environment.livings;	//Liste des espèces
		foreach (Species especes in _species)
		{
			if(especes.isPlayed)
			{
				especes.unlockedPerks = ;
			}
		}
		return "Vous avez débloqués toutes les compétences";
	}

	public string Help(params string[] args) {
		return "god -- Vie illimitée\n" +
			"speed [nombre] -- modifie la vitesse de déplacement de vos cellules\n" +
			"speedbase -- remet la vitesse de base\n" +
			"allunlock -- débloque toutes les compétences de l'arbre\n" +
			"help -- cette commande";
	}
}

