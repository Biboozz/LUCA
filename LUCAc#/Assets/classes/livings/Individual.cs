using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Diagnostics;
using System.Linq;
using UnityEngine.UI;

public class Individual : MonoBehaviour
{
	private bool _alive = true;
	private Species _species;
	private int _survivedTime = 0;
	private bool _isPlayed = false;
    private bool _isSelectioned = false;
	private bool _gotDest = false;
	private Vector3 _target;
	private List<moleculePack> _cellMolecules = new List<moleculePack>();
	private int _ATP;
	private bool _consumeATP = true;
	private List<Individual> _group;
	public environment place;
	public resourcesManager _RM;

	public GameObject descriptionBox;
	private float _duration = 10.0f;	//Diminuer pour plus vite
	private float _speed = 0.05f;	//Quand on augmente va plus vite
	private int _delay = 0;

	private int _splitDelay = UnityEngine.Random.Range(1800, 36000);
	private int _splitIncrement;

	private int vieilDelta = 0;
	private int actionDelay = 45;

	public GameObject cytoplasm;

	#region accessors
	public List<Individual> group			{ 	get { return _group; } 					set { _group = value; 			} 	}
	public int survivedTime 				{   get { return _survivedTime; 	} 	}
	public resourcesManager RM 				{ 	get { return _RM; 				}		set { _RM = value;				}	}
	public bool alive 						{ 	get { return _alive; 			}		set { _alive = value; 			} 	}
	public Species species 					{ 	get { return _species; 			} 		set { _species = value; 		} 	}
	public bool isPlayed 					{ 	get { return _isPlayed; 		} 		set { _isPlayed = value; 		}	}
	public int ATP							{ 	get { return _ATP; 				} 		set { _ATP = value; 			} 	}
	public List<moleculePack> cellMolecules	{ 	get { return _cellMolecules; 	} 											}
	public bool gotDest						{	get { return _gotDest;			}		set { _gotDest = value;			}	}
	public Vector3 target					{	get { return _target;			}		set { _target = value;			}	}
	public bool consumeATP					{	get { return _consumeATP;		}		set { _consumeATP = value;		}	}
	public float duration 					{ 	get { return _duration; 		} 		set { _duration = value; 		} 	}
	public float speed						{	get { return _speed;			}		set { _speed = value;			}	}
	public int delay						{ 	get { return _delay; 			} 		set { _delay= value; 			} 	}

	public bool isSelectioned
	{ 	
		get 
		{ 
			return _isSelectioned; 	
		} 		
		set 
		{ 
			_isSelectioned = value;
			if (value)
			{
				descriptionBox.GetComponent<cellDataDisplayer>().target = this;
			}
		} 	
	}
	#endregion

	// Use this for initialization
	void Start ()
	{
		delay = UnityEngine.Random.Range (0, 420);
		_RM = place.gameObject.GetComponent<resourcesManager> ();
		transform.Rotate (0, 0, UnityEngine.Random.Range(0,360));
		GetComponent<actionManager> ().Initialize ();
		//GetComponent<actionManager> ().addAction (action);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeScale >= 1f)		//Test si le temps est en pause ou pas
		{
			gameObject.transform.GetChild (3).gameObject.SetActive (_isSelectioned); //selection de la cellule
			if ((!_isSelectioned && !_gotDest) || (_isSelectioned && !_isPlayed && !_gotDest)) //Les cellules du joueur non selectionnees se deplacent
			{
				transform.Translate(speed, 0f, 0f);
				transform.Rotate(0, 0, UnityEngine.Random.Range(-2, 3));

				toCorrectPosition(20f); //si mur, changement d'angle
			}

			if (_splitIncrement == _splitDelay) //delai avant la prochaine division atteint
			{
				_splitIncrement = 0;
				GetComponent<actionManager> ().addAction (division); //ordre de division, priorité nule
			}
			_splitIncrement++;
			
			vieilDelta++; //vieillisement de la cellule
			if (vieilDelta >= 60) 
			{
				vieilDelta = 0;
				_survivedTime++; //calcul du temps survécu de la cellule en secondes
				float f = cytoplasm.GetComponent<SpriteRenderer>().color.r - 0.5f / (float)species.individualLifeTime;  //vieillissement visible de la cellule
				Color C = new Color(f, f, f);
				cytoplasm.GetComponent<SpriteRenderer>().color = C;
				if (_survivedTime >= _species.individualLifeTime)
				{
					alive = false; //si la cellule est trop vieille, elle meurt. Les cellules mortes sont supprimées dans l'update de la classe espèce
				}
			}

			actionDelay--;
			if (actionDelay == 0) //délai d'action atteint
			{
				actionDelay = 10; //6 ajouts d'actions par seconde par seconde
				GetComponent<actionManager> ().addAction (interract, 0, 4); //action d'interraction avec l'environement, priorité nule, 4 secondes de délai avant expiration
				GetComponent<actionManager> ().addAction (eatToxic); 		//action d'interraction avec l'environement pour les molécules toxiques, priorité nule
			}
		}


	}

	private int existmol(List<moleculePack> packs, molecule searched)
	{
		int i = 0;

		foreach (moleculePack M in packs) 
		{
			if (M.moleculeType == searched){
				return i;
			}
			i++;
		}
		return (-1);
	}

	#region action------------------------------------------------------------

	public void harvest(moleculePack Tile)
	{
		if (Tile.count > 0) 
		{
			int posowned = existmol (cellMolecules,Tile.moleculeType);
			
			if (Tile.moleculeType.toxic)
			{
				if (Tile.count > species.toxic_absorb)
				{
					Tile.count -= species.toxic_absorb;
					
					if (posowned == -1)
					{
						cellMolecules.Add(new moleculePack(species.toxic_absorb,Tile.moleculeType));
					}
					else
					{
						cellMolecules[posowned].count += species.toxic_absorb;
					}
				}
				else
				{
					if (posowned == -1)
					{
						cellMolecules.Add(Tile);
					}
					else
					{
						cellMolecules[posowned].count += Tile.count;
					}
					Tile.count = 0;
				}
			}
			else
			{
				molecule temp = Tile.moleculeType;
				
				int posskill = existmol(species.workCost.environmentMolecules, temp);
				
				if(posskill >= 0)
				{
					moleculePack Skill = species.workCost.environmentMolecules[posskill];
					
					if (Tile.count > Skill.count)
					{
						Tile.count -= Skill.count;
						
						if (posowned == -1)
						{
							cellMolecules.Add(new moleculePack(Skill.count,Tile.moleculeType));
						}
						else
						{
							cellMolecules[posowned].count += Skill.count;
						}
					}
					else
					{
						if (posowned == -1)
						{
							cellMolecules.Add(Tile);
						}
						else
						{
							cellMolecules[posowned].count += Tile.count;
						}
					}
				}
			}
		}
	}

	public void emite(moleculePack Emission, List<moleculePack> Tile)
	{
		int poslist = existmol (Tile, Emission.moleculeType);

		if (poslist == -1) 
		{
			Tile.Add(Emission);
		} 
		else 
		{
			Tile[poslist].count += Emission.count;
		}
	}


	public bool action()
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x / 20f);
		int squarey = (int)(pos.y / 20f);

		//foreach (moleculePack Tile in R.moleculeRepartition[squarex,squarey]) 
		foreach (moleculePack Tile in _RM.moleculeRepartition[squarex,squarey]) 
		{
			harvest (Tile);
		}
		foreach (moleculePack Subject in _species.workProducts.cellMolecules) 
		{
			emite (Subject, _RM.moleculeRepartition[squarex,squarey]);
		}
		return false;
	}

	#endregion action--------------------------------------------------------------------

	public bool eat()
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x/ 20f);
		int squarey = (int)(pos.y/ 20f);
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (0 > squarex)
		{
			squarex = 0;
		}
		if (0 > squarey) 
		{
			squarey = 0;
		}
		foreach (moleculePack Mi in _cellMolecules) 
		{
			foreach (moleculePack Mc in _RM.moleculeRepartition[squarex,squarey])
			{
				if ((Mi.moleculeType == Mc.moleculeType)&&(Mc.count > 0))
				{
					
					if (Mc.count > _species.absorb_amount) 
					{
						Mi.count += _species.absorb_amount;
						Mc.count -= _species.absorb_amount;
					}
					else
					{
						Mi.count += Mc.count;
						Mc.count = 0;
					}
				}
			}
		}
		return false;
	}
	

	public void Initialize(Vector3 position, int lifeTime, Species species, environment place, bool isPlayed, List<moleculePack> molecules, int ATP)
	{
		transform.position = position;
		_species = species;
		transform.SetParent(place.transform);
		this.place = place;
		_isPlayed = isPlayed;
		_cellMolecules = molecules;
		_ATP = ATP;
	}

	public void toCorrectPosition(float angle) //correct the cell position, rotate it of the angle value
	{
		Vector3 pos = transform.position;
		if (pos.x < 1) 
		{
			pos.x = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.x > 2000) 
		{
			pos.x = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.y < 1) 
		{
			pos.y = 1;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		if (pos.y > 2000) 
		{
			pos.y = 2000;
			transform.Rotate(0, 0, UnityEngine.Random.Range(angle, angle + angle / 2));
		}
		transform.position = pos;
	}

	public void splitGive(Individual I)
	{
		foreach (moleculePack mp in cellMolecules) 
		{
			mp.count = mp.count / 2;
			moleculePack nmp = new moleculePack(mp.count, mp.moleculeType);
			I.cellMolecules.Add(nmp);
		}
	}

	public bool division()
	{
		GameObject son = (GameObject)Instantiate (gameObject);
		son.transform.rotation = transform.rotation;
		son.name = "cellSon";
		splitGive (son.GetComponent<Individual> ());
		_species.addCell (son.GetComponent<Individual>());
		son.GetComponent<Individual>().descriptionBox = descriptionBox;
		son.transform.FindChild ("cytoplasm").gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		son.GetComponent<Individual> ().speed = speed;
		ATP = ATP / 2;
		son.GetComponent<Individual>().Initialize(transform.position, 0, this._species, this.place, this.isPlayed, new List<moleculePack>(), ATP);
		splitGive (son.GetComponent<Individual> ());
		son.GetComponent<CellUnPlayed> ().Initialize ();
		return true;
	}

	private bool specificEat(moleculePack mp)
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x/ 20f);
		int squarey = (int)(pos.y/ 20f);
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (0 > squarex)
		{
			squarex = 0;
		}
		if (0 > squarey) 
		{
			squarey = 0;
		}
		moleculePack MP = RM.moleculeRepartition [squarex, squarey].Find (mpE => mpE.moleculeType.ID == mp.moleculeType.ID);
		if (MP == null || MP.count == 0) 
		{

			return true;
		} 
		else 
		{
			if (MP.count < mp.count)
			{
				MP.count = 1;
			}
			else
			{

				MP.count -= mp.count;
			}
			return true;
		}
	}



	private bool interract()
	{
		if (species.unlockedPerks.Count != 0) 
		{
			skill S = species.unlockedPerks [UnityEngine.Random.Range (0, species.unlockedPerks.Count)];
			bool b = true;
			if (S.workCosts.environmentMolecules.Count != 0) 
			{
				int i = 0;
				while (b && i < S.workCosts.environmentMolecules.Count) 
				{
					b = b && specificEat (S.workCosts.environmentMolecules [i]);
					i++;
				}
			}


			if (b && S.workCosts.cellMolecules.Count != 0) 
			{
				int i = 0;
				while (b && i < S.workCosts.cellMolecules.Count) 
				{
					b = b && !specificConsume (S.workCosts.cellMolecules [i]);
					i++;
				}
			}

			if (b && S.workProducts.environmentMolecules.Count != 0) {
				foreach (moleculePack mp in S.workProducts.environmentMolecules) {
					specificRelease (mp);
				}
			}

			if (b && S.workProducts.cellMolecules.Count != 0) 
			{

				foreach (moleculePack mp in S.workProducts.cellMolecules) 
				{
					moleculePack MP = cellMolecules.Find (m => m.moleculeType.ID == mp.moleculeType.ID);
					MP.count += mp.count;
				}
			}
			return !b;
		} 
		else 
		{
			return false;
		}
	}

	private bool specificConsume(moleculePack mp)
	{
		moleculePack mpC = cellMolecules.Find (m => m.moleculeType.ID == mp.moleculeType.ID);
		if (mpC == null || mpC.count < mp.count) 
		{
			return true;
		} 
		else
		{
			mpC.count -= mp.count;
			return false;
		}
	}

	private bool specificRelease(moleculePack mp)
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x/ 20f);
		int squarey = (int)(pos.y/ 20f);
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (0 > squarex)
		{
			squarex = 0;
		}
		if (0 > squarey) 
		{
			squarey = 0;
		}
		moleculePack MP = RM.moleculeRepartition [squarex, squarey].Find (mpE => mpE.moleculeType.ID == mp.moleculeType.ID);
		MP.count += mp.count;
		return false;
	}

	public bool eatToxic()
	{
		Vector3 pos = transform.position;
		int squarex = (int)(pos.x/ 20f);
		int squarey = (int)(pos.y/ 20f);
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (squarex > 99) 
		{
			squarex = 99;
		}
		if (0 > squarex)
		{
			squarex = 0;
		}
		if (0 > squarey) 
		{
			squarey = 0;
		}
		moleculePack MP = RM.moleculeRepartition [squarex, squarey] [UnityEngine.Random.Range (0, RM.moleculeRepartition [squarex, squarey].Count)];
		if (MP.moleculeType.toxic)
		{
			bool b = true;
			string[] str = new string[2];
			str[0] = "none";
			str[1] = "0";
			int i = 0;
			while (b && i < species.immunities.Count)
			{
				str = species.immunities[i].Split(new char[] {' '});
				b = !(str[0] == MP.moleculeType.toxineType);
				i++;
			}
			if (b)
			{
				ATP -= MP.moleculeType.toxineStrength * MP.count;
				MP.count--;
			}
			else
			{
				int.TryParse(str[1], out i);
				if (i < MP.moleculeType.toxineStrength)
				{
					ATP -= MP.moleculeType.toxineStrength * MP.count;
					MP.count--;
				}
			}
		}
		return false;
	}

	private bool phagocyt()
	{
		return true;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.GetComponent<Individual> ().canBeEaten (this)) 
		{
			UnityEngine.Debug.Log ("je te bouffe");
		}
		else 
		{
			UnityEngine.Debug.Log ("pas touche");
		}
	}

	public bool canBeEaten(Individual I)
	{
		return I.species.name != _species.name;
	}
}
