using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class ConsoleInitializer : MonoBehaviour {

	public environment Environment;
	public displayPerkTree SpecsTree;
	public Events events;

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
		repo.RegisterCommand("addmolecule", AddMolecule);
		repo.RegisterCommand("acideattack", AcideAttack);
		repo.RegisterCommand("degenerate", Degenerate);
		repo.RegisterCommand("meteorite", Meteorite);
		repo.RegisterCommand("massivemultiplication", MassMulti);
		repo.RegisterCommand("randomskill", Rndskill);
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
		try {
			args = formatArgs (args);
		} catch (System.Exception) {
			return "Les paramètres saisis sont incorrects";
		}
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
		try {
			args = formatArgs (args);
		} catch (System.Exception) {
			return "Les paramètres saisis sont incorrects";
		}
		if (args.Length == 1) {
			foreach (Species especes in Environment.livings) 
			{
				if (especes.isPlayed) 
				{
					skill S = SpecsTree.perkTree.Find (T => T.name == args [0]);
					if (S == null) 
					{
						return "Le nom du skill n'existe pas";
					} 
					else 
					{
						especes.forceUnlockSkill (S);
						return "Vous avez débloqué la compétence " + S.name;
					}
				}
			}
		} 
		else if (args.Length == 2) {
			skill F = SpecsTree.perkTree.Find (T => T.name == args [1]);
			Species S = Environment.livings.Find(G => G.name == args[0]);
			if(S == null)
			{
				return "Le nom de l'espèce n'existe pas";
			}
			else
			{
				if (F == null) 
				{
					return "Le nom du skill n'existe pas";
				} 
				else 
				{
					S.forceUnlockSkill (F);
					return "Vous avez débloqué la compétence " + F.name;
				}
			}
		} 
		else 
		{
			return "Vous avez saisis trop de paramètres";
		}
		return "";
	}

	public string Kill(params string[] args) {
		try {
			args = formatArgs (args);
		} catch (System.Exception) {
			return "Les paramètres saisis sont incorrects";
		}
		foreach (Individual I in Environment.selectedI) 
		{
			Destroy(I.gameObject);
			Environment.selectedI = new List<Individual>();
		}
		return "Vous avez tué les cellules sélectionnées";
	}

	public string AddMolecule(params string[] args) {
		try {
			args = formatArgs (args);
		} catch (System.Exception) {
			return "Les paramètres saisis sont incorrects";
		}
		molecule M;
		int result;
		Species S;
		if (args.Length == 3) 
		{
			S = Environment.livings.Find (G => G.name == args [0]);
			if (S == null) 
			{
				return "Le nom de l'espèce n'existe pas";
			} 
			else 
			{
				M = Environment.molecules.Find (T => T.name == args [1]);
				if (M == null) 
				{
					return "Le nom de la molécule n'existe pas";
				}
				else 
				{
					if (int.TryParse (args [2], out result)) 
					{
						if (result > 0) 
						{
							foreach (Individual I in S.Individuals) 
							{
								I.cellMolecules.Find (MP => MP.moleculeType.ID == M.ID).count += result;
							}
							return "Vous avez ajoutez " + result + " de la molécule " + M.name + " a l'espèce " + S.name;
						}
					}
				}
			}
		}
		else 
		{
			return "Les paramètres saisis sont incorrects";
		}
		return "";
	}

	public string AcideAttack(params string[] args) 
	{
		events.AcideAttack ();
		return "Lance l'évènement aléatoire de l'attaque acide";
	}

	public string Degenerate(params string[] args) 
	{
		events.Degenerate ();
		return "Lance l'évènement aléatoire de la dégénérescence";
	}

	public string Meteorite(params string[] args) 
	{
		events.Météorite ();
		return "Lance l'évènement aléatoire de la météorite";
	}

	public string MassMulti(params string[] args) 
	{
		events.MultiplicationMassive ();
		return "Lance l'évènement aléatoire de la multiplication massive d'une espèce";
	}

	public string Rndskill(params string[] args) 
	{
		events.RandomSkill ();
		return "Lance l'évènement aléatoire de l'ajout d'une compétence aléatoire";
	}

	public string Help(params string[] args) {
		return "Pour indiquer le nom d'une espèce ou d'une molécules qui est un nom composé, renseignez les avec des guillemets\n" +
			"god -- Vie illimitée\n" +
			"speed [nombre] -- modifie la vitesse de déplacement de vos cellules\n" +
			"speedbase -- remet la vitesse de base\n" +
			"allunlock -- débloque toutes les compétences de l'arbre\n" +
			"unlock [species name] [skill name] -- Par default l'espèce sélectionné est la votre. Permet de débloquer un skill pour une espèce choisie\n" +
			"addmolecule [species name] [molecule name] [quantité] -- Ajoute la quantité indiquée de la molécule précisée pour tous les individus de l'espèce choisie\n" +
			"kill -- tue toutes les cellules sélectionnées\n" +
			"acideattack -- Lance l'évènement aléatoire de l'attaque acide\n" +
			"degenerate -- Lance l'évènement aléatoire de la dégénérescence\n" +
			"meteorite -- Lance l'évènement aléatoire de la météorite\n" +
			"massivemultiplication  -- Lance l'évènement aléatoire de la multiplication massive d'une espèce\n" +
			"randomskill -- Lance l'évènement aléatoire de l'ajout d'une compétence aléatoire\n" +
			"help -- cette commande";
	}

	private string[] formatArgs(string[] args)
	{
		int i = 0;
		List<string> formated = new List<string> ();
		while (i < args.Length) 
		{
			string s = args[i];
			if(!s.StartsWith ("\""))
			{
				formated.Add (s);
			}
			else
			{
				while (args[i][args[i].Length -1] != '"')
				{
					i++;
					if (i >= args.Length)
					{
						throw (new System.Exception("invalid format"));
					}
					s += " " + args[i];
				}
				string S = "";
				foreach (char c in s)
				{
					if (c != '"')
					{
						S += c;
					}
				}
				formated.Add(S);
			}
			i++;
		}
		string[] str = new string[formated.Count];
		formated.CopyTo (str);
		return str;
	}
}

