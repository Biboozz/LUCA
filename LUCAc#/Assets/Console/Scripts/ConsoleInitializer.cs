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
		repo.RegisterCommand("kill", Kill);
		repo.RegisterCommand("unlock", Unlock);
		repo.RegisterCommand("help", Help);
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
			if (especes.isPlayed)
			{
				foreach(skill S in SpecsTree.perkTree)
				{
					especes.forceUnlockSkill(S);
				}
			}
		}
		return "Vous avez débloqués toutes les compétences";
	}

	public string Unlock(params string[] args) {
		Species speciesselect = Environment.livings[0];
		if (args.Length == 1) {
			foreach (Species especes in Environment.livings) {
				if (especes.isPlayed) {
					skill S = SpecsTree.perkTree.Find (T => T.name == args [0]);
					if (S == null) {
						return "Le nom du skill n'existe pas";
					} else {
						especes.forceUnlockSkill (S);
					}
				}
			}
		} 
		else if (args.Length == 2) {
			skill F = SpecsTree.perkTree.Find (T => T.name == args [1]);
			speciesselect.name = args [0];
			Debug.Log (args [0] + args [1]);
			if (F == null) {
				return "Le nom du skill ou de l'espèce n'existe pas";
			} else {
				speciesselect.forceUnlockSkill (F);
			}
		} 
		else 
		{
			return "Vous avez saisis trop de paramètres";
		}
		return "Vous avez débloqué la compétence " ;
	}

	public string Kill(params string[] args) {
		foreach (Individual I in Environment.selectedI) 
		{
			Destroy(I.gameObject);
			Environment.selectedI = new List<Individual>();
		}
		return "Vous avez tué les cellules sélectionnées";
	}

	public string Help(params string[] args) {
		return "god -- Vie illimitée\n" +
			"speed [nombre] -- modifie la vitesse de déplacement de vos cellules\n" +
			"speedbase -- remet la vitesse de base\n" +
			"allunlock -- débloque toutes les compétences de l'arbre\n" +
			"unlock [species name] [skill name] -- Par default l'espèce sélectionné est la votre. Permet de débloquer un skill pour une espèce choisie\n" +
			"kill -- tue toutes les cellules sélectionnées\n" +
			"help -- cette commande";
	}
}

