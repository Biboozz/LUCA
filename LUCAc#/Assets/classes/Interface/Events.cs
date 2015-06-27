using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class Events : MonoBehaviour {

	public environment Environment;
	public displayPerkTree SpecsTree;

	private string[] Sname = new string[]{"Methanosarcina", "Vacuolata", "Firmicutes", "Planctomycetales‎", "Verrucomicrobia", "Lentisphaerae‎"};
	private int timer;
	private int time_event;
	public Text description;
	public System.Random rnd = new System.Random ();
	private int z = 0;

	// Use this for initialization
	void Start () 
	{
		description = transform.FindChild ("descriptionEvent").gameObject.GetComponent<Text> ();
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
		gameObject.SetActive (true);
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		int nb = rnd.Next (20, 50);
		for (int i = 0; i < nb; i++) 
		{
			choice.Individuals[i].alive = false;
		}

		description.text = "Oh non une attaque acide viens d'avoir lieu, " + nb + " cellules de l'espèces " + choice.name + " viennent d'etre tués";
	}

	public void Degenerate()	//Une espèces débloque toutes les compétences
	{
		gameObject.SetActive (true);
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		foreach(skill S in SpecsTree.perkTree)
		{
			choice.forceUnlockSkill(S);
		}
		description.text = "Une dégénérescence importante causé par des cherches pharmaceutiques plus que douteuse on rendu l'espèce " + choice.name + " quasiment invincible";
	}
	
	public void Météorite()		//Nouvelles espèces apparaisse
	{
		gameObject.SetActive (true);
		Species S = new Species (Environment, new Color(((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f));
		S.cell = Environment.cellPrefab;
		S.individualLifeTime = 300;
		for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
		{
			S.Individuals.Add (Instantiate(Environment.cellPrefab).GetComponent<Individual>());
			Environment.CI.cellsplayed.Add(S.Individuals[S.Individuals.Count - 1]);
		}
		for (int i = 0; i < S.Individuals.Count; i++) 
		{
			S.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000),UnityEngine.Random.Range(0,2000), 0.1f), 20, S, Environment, false, new List<moleculePack>(), rnd.Next(500)); //apparition coordonnées random
			S.Individuals[i].descriptionBox = Environment.UICellDescriptionBox;
			S.Individuals[i].transform.FindChild("core").gameObject.GetComponent<SpriteRenderer>().color = S.color; //modif couleur core en fonction de l'espece
			S.Individuals[i].transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().color = S.color;
		}
		Environment.livings.Add (S); //ajout liste espece vivante
		S.name = Sname[z];
		z++;

		Species T = new Species (Environment, new Color(((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f,((float)rnd.Next(255))/255f));
		T.cell = Environment.cellPrefab;
		T.individualLifeTime = 300;
		for (int i = 1; i <= 100; i++) // création de 100 cellules de l'espece
		{
			T.Individuals.Add (Instantiate(Environment.cellPrefab).GetComponent<Individual>());
			Environment.CI.cellsplayed.Add(T.Individuals[T.Individuals.Count - 1]);
		}
		for (int i = 0; i < T.Individuals.Count; i++) 
		{
			T.Individuals[i].Initialize(new Vector3(UnityEngine.Random.Range(0,2000),UnityEngine.Random.Range(0,2000), 0.1f), 20, T, Environment, false, new List<moleculePack>(), rnd.Next(500)); //apparition coordonnées random
			T.Individuals[i].descriptionBox = Environment.UICellDescriptionBox;
			T.Individuals[i].transform.FindChild("core").gameObject.GetComponent<SpriteRenderer>().color = T.color; //modif couleur core en fonction de l'espece
			T.Individuals[i].transform.FindChild("membrane").gameObject.GetComponent<SpriteRenderer>().color = T.color;
		}
		Environment.livings.Add (T); //ajout liste espece vivante
		T.name = Sname[z];
		z++;

		description.text = "Une météorite c'est écrasé sur le terrain, deux nouvelles espèces sont apparues";
	}
	
	public void MultiplicationMassive()	//Nombre d'individus d'une espèce augmente énormément
	{
		gameObject.SetActive (true);
		Species choice = Environment.livings[rnd.Next(0, Environment.livings.Count - 1)];
		int nb = rnd.Next (50, 100);
		for (int i = 1; i <= nb; i++) // création de 100 cellules de l'espece
		{
			choice.Individuals[i].division();
		}

		description.text = "Une erreur de dosage d'un stagiaire a provoqué la multiplication importante de l'espèce " + choice.name + ", elle possède " + nb + " nouveaux individus";
	}

	public void RandomSkill()	//Ajout une compétence aléatoire au joueur
	{
		gameObject.SetActive (true);
		foreach (Species especes in Environment.livings) 
		{
			if (especes.isPlayed)
			{
				int nb = rnd.Next(0, SpecsTree.perkTree.Count - 1);
				especes.forceUnlockSkill (SpecsTree.perkTree[nb]);
				description.text = "C'est votre jour de chance, en effet la jolie secrétaire a renversé quelques choses dans le milieu en amenant des fichiers importants au responsable du laboratoire, vous avez donc débloqué la compétence " + SpecsTree.perkTree[nb].name;
			}
		}
	}
	
	
}
