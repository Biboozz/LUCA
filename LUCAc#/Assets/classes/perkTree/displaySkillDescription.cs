using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Collections.Generic;

public class displaySkillDescription : MonoBehaviour {

	private skill _skill;
	private bool visible;
	private List<skill> _perkTree;
	public unlockSkillWindow USW;
	public environment Env;

	// Use this for initialization
	void Start () 
	{
		visible = false;
		Env = USW.Env;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public skill Skill
	{
		get
		{
			return _skill;
		}
		set
		{
			_skill = value;
			transform.GetChild(1).GetComponent<Text> ().text = _skill.description;
			transform.GetChild(0).GetComponent<Text> ().text = _skill.name;
		}
	}

	public List<skill> perkTree
	{
		get
		{
			return _perkTree;
		}
		set
		{
			_perkTree = value;
		}
	}

	public void click()
	{
		foreach (skill S in perkTree) 
		{
			S.hex.transform.FindChild("skillDescriptionWindow").gameObject.SetActive(false);
		}
		visible = !visible;
		_skill.hex.transform.SetAsLastSibling();
		_skill.hex.transform.FindChild("skillDescriptionWindow").gameObject.SetActive(visible);
	}

	public void unlock()
	{
		USW.gameObject.SetActive (true);
		USW.skill = _skill;
	}

	private List<Individual> upgradables(skill S, Species species)
	{
		List<Individual> c = new List<Individual>();
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
					if (mpCell.count > S.devCosts.cellMolecules[j].count)
					{
						b = false;
					}
				}
			}
			if (b)
			{
				c.Add(species.Individuals[i]);
			}
		}
		return c;
	}

	public void addObjective()
	{
		Env.PSDD.setObjective (_skill);
	}
}
