using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ConsoleInitializer : MonoBehaviour {

	public environment Environment;
	public Species Especes;
	public displayPerkTree SpecsTree;
	private List<Species> _species;
	private List<Individual> cellsplayed;
	//private List<skill> _allskill;
	//private List<skill> _skillunlock;
	private bool play = false;
	private int i = 0;

	void Start () 
	{
		var repo = ConsoleCommandsRepository.Instance;
		repo.RegisterCommand("god", God);
		repo.RegisterCommand("speed", Speed);
		repo.RegisterCommand("speedbase", SpeedBase);
		repo.RegisterCommand("all_unlock", Allunlock);
		repo.RegisterCommand("help", Help);

		_species = Environment.livings;
		//_allskill = SpecsTree.perkTree;
		while (!play) 
		{
			if(_species[i].isPlayed == true)
			{
				play = true;
			}
			else
			{
				i++;
			}
		}

		cellsplayed = _species [i].Individuals;
	}
	
	public string God(params string[] args) {
		foreach(Individual I in cellsplayed)
		{
			I.ATP = 1000;
			I.consumeATP = false;	//Cellule ne consomme plus son énergie
		}
		return "Vos cellules sont désormais immortelles";
	}
	
	public string Speed(params string[] args) {
		var speed = args[0];
		foreach(Individual I in cellsplayed)
		{
			I.duration = float.Parse(speed) ;
			I.speed = 1/(float.Parse(speed) / 100);
		}
		return "Votre vitesse est désormais de " + speed;
	}

	public string SpeedBase(params string[] args) {
		foreach(Individual I in cellsplayed)
		{
			I.duration = 20f;
			I.speed = 0.05f;
		}
		return "La vitesse de base a été rétabli";
	}
	
	public string Allunlock(params string[] args) {
		//_skillunlock = _allskill;
		return "Vous avez débloqués toutes les compétences";
	}

	public string Help(params string[] args) {
		return "god -- Vie illimitée\n" +
			"speed [nombre] -- modifie la vitesse de déplacement de vos cellules\n" +
			"speedbase -- remet la vitesse de base\n" +
			"all_unlock -- débloque toutes les compétences de l'arbre\n" +
			"help -- cette commande";
	}
}

