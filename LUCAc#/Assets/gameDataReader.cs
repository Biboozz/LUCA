using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using AssemblyCSharp;

public class gameDataReader : MonoBehaviour {
	
	private List<skill> skillList;
	private List<molecule> moleculeList;

	// Use this for initialization
	void Start () 
	{
		skillList = new List<skill> ();
		moleculeList = new List<molecule> ();
		ReadXML();
		buildLinks ();
		GetComponentInParent<displayPerkTree> ().Initialize (skillList);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void ReadXML()
	{
		System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<serialisable>));
		System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Nicolas\Documents\game data.xml");
		List<serialisable> deserialisedItems = new List<serialisable>();
		deserialisedItems = (List<serialisable>)reader.Deserialize(file);
		foreach (serialisable S in deserialisedItems) 
		{
			switch(S.SerialisableType)
			{
				case serialisableTypes.molecule :
					moleculeList.Add((molecule)S);
					break;
				case serialisableTypes.skill :
					skillList.Add((skill)S);
					break;
				case serialisableTypes.unvalid :
					Debug.LogError("Unvalidd skill detected, please check the game data file");
					break;
			}
		}
	}

	private void buildLinks ()
	{
		foreach (molecule m in moleculeList) 
		{
			if (m.complex)
			{
				m.componant = moleculeList.Find(c => c.ID == m.componantID);
			}
		}
		foreach (skill S in skillList) 
		{
			for (int i = 0; i < 6; i++)
			{
				if (S.neighborsID[i] != -1)
				{
					S.neighbors[i] = skillList.Find(n => n.ID == S.neighborsID[i]);
				}
				else
				{
					S.neighbors[i] = null;				
				}
			}
			foreach (int i in S.requiredID)
			{
				S.required.Add(skillList.Find(r => r.ID == i));
			}
		}
	}
}
