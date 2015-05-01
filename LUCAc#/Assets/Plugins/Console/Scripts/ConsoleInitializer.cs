using UnityEngine;
using System.Collections;

public class ConsoleInitializer : MonoBehaviour {
	
	void Start () 
	{
		var repo = ConsoleCommandsRepository.Instance;
		repo.RegisterCommand("god", God);
		repo.RegisterCommand("speed", Speed);
		repo.RegisterCommand("all_unlock", Allunlock);
		repo.RegisterCommand("help", Help);
	}
	
	public string God(params string[] args) {
		//Rajouter modifications de la vie pour les cellules jouées
		return "Vos cellules sont désormais immortelles";
	}
	
	public string Speed(params string[] args) {
		var speed = args[0];
		//Rajouter modifications de la vitesse pour les cellules jouées
		return "Votre vitesse est désormais de" + speed;
	}

	public string Allunlock(params string[] args) {
		//Rajouter commandes
		return "Vous avez débloqués toutes les compétences";
	}

	public string Help(params string[] args) {
		return "god -- Vie illimitée\n" +
			"speed [nombre] -- modifie la vitesse de déplacement de vos cellules\n" +
			"all_unlock -- débloque toutes les compétences de l'arbre\n" +
			"help -- cette commande";
	}
}

