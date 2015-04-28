using UnityEngine;
using System.Collections;
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

	// Use this for initialization
	void Start () {
		_shown = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			display();
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
		foreach (skill S in skillList) 
		{
			S.hex = (GameObject)Instantiate (GUIhexagon, new Vector3 (transform.localPosition.x, transform.localPosition.z, 0), canvas.transform.rotation);
			S.hex.transform.SetParent (canvas.transform, false);
			S.hex.GetComponentInChildren<Text>().text = S.name;
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
		}
		place (skillList.Find(b => b.name == "En vie"));
		perkTree = skillList;
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

	private void reset()
	{
		foreach (skill S in perkTree) 
		{
			S.hex.GetComponent<Image>().sprite = images[2].sprite;
			S.hex.GetComponent<Button>().interactable = false;
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
		return (c * 100 / species.Individuals.Count >= 60);
	}

	private void activateUnlockable(List<skill> unlockedNeighborhood, Species species)
	{
		for (int i = 0; i < unlockedNeighborhood.Count; i++)
		{
			if (isUnlockable(unlockedNeighborhood[i], species))
			{
				unlockedNeighborhood[i].hex.GetComponent<Image>().sprite = images[1].sprite;
				unlockedNeighborhood[i].hex.GetComponent<Button>().interactable = true;
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
	

	public void display()
	{
		reset ();
		if (_shown) 
		{
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
