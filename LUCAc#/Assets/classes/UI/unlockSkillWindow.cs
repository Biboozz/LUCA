using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class unlockSkillWindow : MonoBehaviour {
	
	private skill _skill;
	private Species _player;

	private ListBox _requirements;
	//private int lastSelected;
	private Text _skillName;//
	private Image _skillAppearence;
	private Text _description;//
	private Text _skillHexName;
	private Text _rate;

	private bool initialized = false;

	public displayPerkTree DPT;
	public environment Env;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void drawListBox()
	{
		//Click Test
		if (_requirements.ReDraw())
		{
			//lastSelected = _requirements.GetSelectedID();
		}
		//----------
	}
	
	void OnGUI()
	{
		drawListBox ();
		GUI.skin.button.fontSize = 11;
		GUI.skin.button.alignment = TextAnchor.LowerLeft;
	}

	public skill skill
	{
		get
		{
			return _skill;
		}
		set
		{
			DPT.hide ();
			if (value != null)
			{
				Init ();
				_skill = value;
				_skillName.text = _skill.name;
				_description.text = _skill.description;
				_skillHexName.text = _skill.type;
				_skillAppearence.sprite = _skill.hex.GetComponent<Image>().sprite;
				_rate.text = (((float)countUnlockers(_player, _skill) * 100f)/(float)_player.Individuals.Count).ToString() + "% de vos cellules peuvent debloquer cette competence (60% nécessaires)";
				if (_player == null)
				{
					throw new System.Exception("error undefined player");
				}
				else
				{
					_requirements.Clear ();
					foreach (moleculePack MPreq in _skill.devCosts.cellMolecules)
					{
						_requirements.AddItem(MPreq.moleculeType.name, MPreq.count);
					}
				}
			}
		}
	}

	private void Init()
	{

		if (!initialized) 
		{
			_skillName = transform.FindChild ("skillName").gameObject.GetComponent<Text> ();
			_description = transform.FindChild ("description").gameObject.GetComponent<Text> ();
			_skillHexName = transform.FindChild ("skillAppearence").FindChild ("skillHexName").gameObject.GetComponent<Text> ();
			_rate = transform.FindChild ("rateName").gameObject.GetComponent<Text> ();
			_skillAppearence = transform.FindChild ("skillAppearence").gameObject.GetComponent<Image> ();
			Transform t = gameObject.transform.FindChild ("requirementsListbox");
			Rect listBoxRect = ((RectTransform)t).rect;
			listBoxRect.position = new Vector2 (Screen.width * 0.52f, Screen.height * 0.27f);
			_requirements = new ListBox (listBoxRect, new Rect (0, 0, listBoxRect.width - 20, 150), false, true);
			initialized = true;
		}
	}

	public Species player
	{
		get
		{
			return _player;
		}
		set
		{
			if (value != null)
			{
				_player = value;
			}
		}
	}

	public void unlock()
	{
		if (_skill == null) 
		{
			Debug.Log("troll");
		}
		Env.livings.Find (s => s.isPlayed).naturalUnlock (_skill);
		gameObject.SetActive (false);

	}

	public void cancel()
	{
		gameObject.SetActive (false);
	}

//	private List<Individual> upgrade(skill S, Species species)
//	{
//		List<Individual> c = new List<Individual>();
//		for (int i = 0; i < species.Individuals.Count; i++) 
//		{
//			bool b = true;
//			for (int j = 0; j < S.devCosts.cellMolecules.Count && b; j++)
//			{
//				moleculePack mpCell = species.Individuals[i].cellMolecules.Find(mp => mp.moleculeType.ID == S.devCosts.cellMolecules[j].moleculeType.ID);
//				if (mpCell == null)
//				{
//					b = false;
//				}
//				else
//				{
//					if (mpCell.count < S.devCosts.cellMolecules[j].count)
//					{
//						b = false;
//					}
//				}
//			}
//			if (b)
//			{
//				c.Add(species.Individuals[i]);
//			}
//		}
//		foreach (Individual I in c) 
//		{
//			I.ATP = I.ATP - S.devCosts.ATP;
//			foreach(moleculePack mp in S.devCosts.cellMolecules)
//			{
//				I.cellMolecules.Find(r => r.moleculeType.ID == mp.moleculeType.ID).count -= mp.count;
//			}
//		}
//		return c;
//	}
//

//

//
	public static int countUnlockers(Species species, skill S)
	{
		int c = 0;
		for (int i = 0; i < species.Individuals.Count; i++) 
		{
			bool b = true;
			for (int j = 0; j < S.devCosts.cellMolecules.Count && b; j++)
			{
				moleculePack mpCell = species.Individuals[i].cellMolecules.Find(mp => mp.moleculeType.ID == S.devCosts.cellMolecules[j].moleculeType.ID);
				if (mpCell == null)
				{
					b = false;
				}
				else
				{
					if (mpCell.count < S.devCosts.cellMolecules[j].count)
					{
						b = false;
					}
				}
			}
			if (b)
			{
				c++;
			}
		}
		return c;
	}
}
