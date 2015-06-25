using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class Events : MonoBehaviour {

	public environment Environment;
	public displayPerkTree SpecsTree;

	private int timer;
	private int time_event;
	private Text _description = null;
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
			gameObject.SetActive (true);
			CallRndEvent ();
			timer = 0;
		}
		else
			timer++;
	}

	public void cancel()
	{
		gameObject.SetActive (false);
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
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		int nb = rnd.Next (20, 50);
		for (int i = 0; i < nb; i++) 
		{
			choice.Individuals[i].alive = false;
		}
		_description.text = "Oh non une attaque acide viens d'avoir lieu, " + nb + " cellules de l'espèces " + choice.name + " viennent d'etre tués";
	}

	public void Degenerate()	//Une espèces débloque toutes les compétences
	{
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		foreach(skill S in SpecsTree.perkTree)
		{
			choice.forceUnlockSkill(S);
		}
		_description.text = "Une dégénérescence importante causé par des cherches pharmaceutiques plus que douteuse on rendu l'espèce " + choice.name + "quasiment invincible";
	}
	
	public void Météorite()		//Nouvelles espèces apparaisse
	{
		Species S = new Species (Environment, new Color(((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f));
		S.cell = Environment.cellPrefab;
		S.individualLifeTime = 300;
		for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
		{
			S.Individuals.Add (Instantiate(Environment.cellPrefab).GetComponent<Individual>());
			Environment.CI.cellsplayed.Add(S.Individuals[S.Individuals.Count - 1]);
		}
		S.name = "Methanosarcina";

		Species T = new Species (Environment, new Color(((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f));
		T.cell = Environment.cellPrefab;
		T.individualLifeTime = 300;
		for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
		{
			T.Individuals.Add (Instantiate(Environment.cellPrefab).GetComponent<Individual>());
			Environment.CI.cellsplayed.Add(T.Individuals[T.Individuals.Count - 1]);
		}
		T.name = "Vacuolata";

		_description.text = "Une météorite c'est écrasé sur le terrain, deux nouvelles espèces sont apparues";
	}
	
	public void MultiplicationMassive()	//Nombre d'individus d'une espèce augmente énormément
	{
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		int nb = rnd.Next (50, 100);
		for (int i = 1; i <= nb; i++) // création de 100 cellules de l'espece
		{
			choice.Individuals.Add (Instantiate(Environment.cellPrefab).GetComponent<Individual>());
			Environment.CI.cellsplayed.Add(choice.Individuals[choice.Individuals.Count - 1]);
		}

		_description.text = "Une erreur de dosage d'un stagiaire a provoqué la multiplication importante de l'espèce " + choice.name + ", elle possède " + nb + " nouveaux individus";
	}

	public void RandomSkill()	//Ajout une compétence aléatoire au joueur
	{
		foreach (Species especes in Environment.livings) 
		{
			if (especes.isPlayed)
			{
				int nb = rnd.Next(0, SpecsTree.perkTree.Count - 1);
				especes.forceUnlockSkill (SpecsTree.perkTree[nb]);
				_description = "C'est votre jour de chance, en effet la jolie sécrétaire a renversé quelques choses dans le milieu en amenant des fichiers importants au responsable du laboratoire, vous avez donc débloqué la compétence " + SpecsTree.perkTree[nb].name;
			}
		}
	}
	
	
}
