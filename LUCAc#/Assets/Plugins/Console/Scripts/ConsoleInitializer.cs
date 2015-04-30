using UnityEngine;
using System.Collections;

public class ConsoleInitializer : MonoBehaviour {
	
	void Start () 
	{
		var repo = ConsoleCommandsRepository.Instance;
		repo.RegisterCommand("dieu", Dieu);
		repo.RegisterCommand("vitesse", Vitesse);
	}
	
	public string Dieu(params string[] args) {
		return "Vos cellules sont désormais immortelles";
	}
	
	public string Vitesse(params string[] args) {
		var speed = args[0];
		return "Votre vitesse est désormais de" + speed;
	}
}

