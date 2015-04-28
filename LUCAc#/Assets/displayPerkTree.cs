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

	}

	private List<skill> updateAccessible(Species species)
	{
		foreach (skill S in species.unlockedPerks) 
		{
			if (S.unlocked)
			{
				//trt image du skill débloqué, il faut l'afficher comme débloqué
				foreach (skill T in S.neighbors)
				{
					//trt image du skill accessible
					T.accessible = true;
				}
			}
		}
	}

	private List<skill> updateUnlockable(List<skill> accessibleSkills, Species species)
	{
		foreach (skill s in accessibleSkills) 
		{
			foreach (Individual I in species.Individuals)
			{
				foreach (moleculePack mpDev in s.devCosts.cellMolecules)
				{
					moleculePack mpCell = I.cellMolecules.Find(mp => mp.moleculeType.ID = mpDev.moleculeType.ID);
					if (mpCell != null)
					{

					}
				}
			}
		}
	}

	public void display()
	{
		if (_shown) 
		{
			foreach (skill S in perkTree) 
			{
				S.hex.SetActive(false);
			}
		} 
		else 
		{
			foreach (skill S in perkTree) 
			{
				S.hex.SetActive(true);
			}
		}
		_shown = !_shown;
	}
}
