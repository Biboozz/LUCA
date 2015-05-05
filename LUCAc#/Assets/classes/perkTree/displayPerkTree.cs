using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

public class displayPerkTree : MonoBehaviour {

	public Canvas canvas;
	public GameObject GUIhexagon;
	public GameObject Link;
	private bool _shown;
	public List<skill> perkTree;
	public Image[] images;
	private List<skillType> types;
	private Species _focusedSpecies;
	public unlockSkillWindow USW;

	// Use this for initialization
	void Start () {
		_shown = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			display();
		}
	}

	public Species focusedSpecies
	{
		get
		{
			return _focusedSpecies;
		}
		set
		{
			_focusedSpecies = value;
			update(value);
		}
	}

	public bool shown
	{
		get
		{
			return _shown;
		}
	}

	public void Initialize(List<skill> skillList)
	{
		System.Random rdm = new System.Random(0);
		types = new List<skillType> ();
		foreach (skill S in skillList) 
		{
			skillType st = types.Find(T => T.name == S.type);
			if (st == null)
			{
				st = new skillType(S.type);
				st.Color = new Color(((float)rdm.Next(256))/255f,((float)rdm.Next(30))/255f,((float)rdm.Next(256))/255f);
				this.types.Add(st);
			}
			S.typeNColor = st;
			S.hex = (GameObject)Instantiate (GUIhexagon, new Vector3 (transform.localPosition.x, transform.localPosition.z, 0), canvas.transform.rotation);
			S.hex.transform.SetParent (canvas.transform.FindChild("UI Skill Tree"), false);
			S.hex.transform.GetChild(1).GetComponent<displaySkillDescription>().Skill = S;
			S.hex.transform.GetChild(1).GetComponent<displaySkillDescription>().perkTree = skillList;
			S.hex.GetComponentInChildren<Text>().text = S.name;
			S.hex.GetComponent<moveSkill>().Position = canvas.transform.FindChild("UI Skill Tree").gameObject;
			for (int i = 0; i < 6; i++)
			{
				float angle = Mathf.PI * (1 + 2 * i) / 6;
				if (S.neighbors[i] != null)
				{
					Link = (GameObject)Instantiate(Link, new Vector3(S.hex.transform.position.x - Mathf.Cos(angle) * 14f * S.hex.transform.localScale.z, S.hex.transform.position.y - Mathf.Sin(angle) * 14f * S.hex.transform.localScale.z, 0), transform.rotation);
					Link.transform.localScale = new Vector3(1,1,1);
					Link.transform.Rotate(0,0, 180 + ((i+2)%6)*60);
					Link.transform.SetParent(S.hex.transform);
					//trt image
				}
			}
			S.hex.SetActive(false);
			S.hex.transform.FindChild("skillDescriptionWindow").gameObject.GetComponent<displaySkillDescription>().USW = USW;
		}
		place (skillList.Find(b => b.name == "En vie"));
		perkTree = skillList;
	}

	public void redraw(skill center)
	{

	}

	private void place (skill S)
	{
		if (S != null) 
		{
			for (int i = 0; i < 6; i++)
			{
				float angle = Mathf.PI * (1 + 2 * i) / 6;
				if (S.neighbors[i] != null && !S.neighbors[i].treated)
				{
					S.neighbors[i].treated = true;
					S.neighbors[i].hex.transform.position = new Vector3(S.hex.transform.position.x - Mathf.Cos(angle) * 28f * S.hex.transform.localScale.z, S.hex.transform.position.y - Mathf.Sin(angle) * 28f * S.hex.transform.localScale.z, 0);
					place (S.neighbors[i]);
				}
			}
		}
	}

	public void update(Species species)
	{
		reset ();
		activateUnlockable (displayUnlocked (species), species);
	}

	public void update()
	{
		update(GetComponent<environment>().livings[0]);
	}

	public void reset()
	{
		foreach (skill S in perkTree) 
		{
			S.hex.GetComponent<Image>().sprite = images[2].sprite;
			S.hex.GetComponent<Image>().color = Color.white;
			S.hex.transform.FindChild("Text").GetComponent<Text>().color = Color.black;
			S.hex.GetComponent<Button>().interactable = false;
			S.hex.transform.FindChild("skillDescriptionWindow").FindChild("unlockButton").gameObject.SetActive(false);
			S.hex.transform.FindChild("skillDescriptionWindow").FindChild("objectiveButton").gameObject.SetActive(false);
		}
	}
		    
	public bool isUnlockable(skill S, Species species)
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
					if (mpCell.count > S.devCosts.cellMolecules[j].count)
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
		if (species.Individuals.Count == 0) 
		{
			return false;
		} 
		else 
		{
			return (c * 100 / species.Individuals.Count >= 60);
		}

	}

	private void activateUnlockable(List<skill> unlockedNeighborhood, Species species)
	{
		for (int i = 0; i < unlockedNeighborhood.Count; i++)
		{
			if (isUnlockable(unlockedNeighborhood[i], species))
			{
				unlockedNeighborhood[i].hex.GetComponent<Image>().sprite = images[1].sprite;
				unlockedNeighborhood[i].hex.GetComponent<Button>().interactable = true;
				unlockedNeighborhood[i].hex.transform.FindChild("skillDescriptionWindow").FindChild("objectiveButton").gameObject.SetActive(species.isPlayed);
				unlockedNeighborhood[i].hex.transform.FindChild("skillDescriptionWindow").FindChild("unlockButton").gameObject.SetActive(species.isPlayed);
			}
		}
	}

	public bool isUnlocked(skill S, Species species)
	{

		return (S.innate || (species.unlockedPerks.Find(T => T.ID == S.ID) != null));
	}

	private List<skill> displayUnlocked (Species species)
	{
		List<skill> treated = new List<skill> ();
		List<skill> neighborhood = new List<skill> ();
		for (int i = 0; i < this.perkTree.Count; i++) 
		{
			treated.Add(perkTree[i]);
			if (isUnlocked(this.perkTree[i], species))
			{
				perkTree[i].hex.GetComponent<Image>().sprite = images[0].sprite;
				perkTree[i].hex.GetComponent<Image>().color = perkTree[i].typeNColor.Color;
				perkTree[i].hex.transform.GetChild(0).GetComponent<Text>().color = IdealTextColor(perkTree[i].hex.GetComponent<Image>().color);
				perkTree[i].hex.GetComponent<Button>().interactable = true;
				for( int j = 0; j < 6; j++)
				{
					if ((this.perkTree[i].neighbors[j] != null) && !isUnlocked(this.perkTree[i].neighbors[j], species))
					{
						if (neighborhood.Find(T => T.ID == this.perkTree[i].neighbors[j].ID) == null)
						{
							neighborhood.Add(perkTree[i].neighbors[j]);
						}
					}
				}
			}
		}
		return neighborhood;
	}

	private Color IdealTextColor(Color bg)
	{
		int [] bgInt = new int[3];
		bgInt[0] = (int)(bg.r * 255f);
		bgInt[1] = (int)(bg.g * 255f);
		bgInt[2] = (int)(bg.b * 255f);
		int nThreshold = 105;
		int bgDelta = Convert.ToInt32 ((bgInt [0] * 0.299) + (bgInt [1] * 0.587) + (bgInt [2] * 0.114));
		if (255 - bgDelta < nThreshold) 
		{
			return new Color(0,0,0,1);
		} 
		else 
		{
			return new Color(1,1,1,1);
		}
	}


	public void display()
	{
		if (_focusedSpecies != null) 
		{
			display (_focusedSpecies);
		} 
		else 
		{
				reset ();
				if (_shown) 
				{
					update();
					foreach (skill S in perkTree) 
					{
						S.hex.SetActive(false);
					}
				} 
				else 
				{
					update();
					foreach (skill S in perkTree) 
					{
						S.hex.SetActive(true);
					}
				}
				_shown = !_shown;
		}
	}

	public void display(Species Spe)
	{
		reset ();
		if (_shown) 
		{
			update(Spe);
			foreach (skill S in perkTree) 
			{
				S.hex.SetActive(false);
			}
		} 
		else 
		{
			update(Spe);
			foreach (skill S in perkTree) 
			{
				S.hex.SetActive(true);
			}
		}
		_shown = !_shown;
	}
}
