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

	// Use this for initialization
	void Start () 
	{
		visible = false;
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
}
