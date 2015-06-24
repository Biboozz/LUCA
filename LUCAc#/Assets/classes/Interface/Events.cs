using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class Events : MonoBehaviour {

	private int timer;
	private int time_event;
	public System.Random rnd = new System.Random ();

	// Use this for initialization
	void Start () 
	{
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer == 0)
			time_event = rnd.Next (18000, 28800);	//Entre 5min et 8min
		else if (timer == time_event) 
		{
			CallRndEvent ();
			timer = 0;
		}
		else
			timer++;
	}

	public void CallRndEvent()
	{
		int choice = rnd.Next (0, 10);
		switch (choice) {
		case 0 : AcideAttack();
			break;
		case 1 : Degenerate();
			break;
		case 2 : Météorite();
			break;
		case 3 : MultiplicationMassive();
			break;
		case 4 : RandomSkill();
			break;
		case 5 : Degenerate();
			break;
		case 6 : Degenerate();
			break;
		case 7 : Degenerate();
			break;
		case 8 : Degenerate();
			break;
		case 9 : Degenerate();
			break;
		}
	}

	public void AcideAttack()	//Attaque acide tue % des individus
	{

	}

	public void Degenerate()	//Une espèces débloque toutes les compétences
	{

	}

	public void Météorite()		//Nouvelles espèces apparaisse
	{

	}

	public void MultiplicationMassive()	//Nombre d'individus d'une espèce augmente énormément
	{

	}

	public void RandomSkill()	//Ajout une compétence aléatoire au joueur
	{

	}
}
