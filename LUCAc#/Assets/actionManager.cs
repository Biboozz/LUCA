using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;
using System.Diagnostics;
using System.Linq;
using UnityEngine.UI;



public class actionManager : MonoBehaviour 
{
	private int coolDown = 30;

	public delegate bool action();		//action : fonction qui renvoie vrai si l'action effectuée est terminée
	private List<action> actions;		//liste des actions par ordre de priorité.

	// initialisation
	void Start () 
	{

	}

	public void Initialize()
	{
		actions = new List<action> ();
	}
	
	// détermine quand les actions doivent etre effectuées selon le coolDown.
	void Update () 
	{
		if (coolDown <= 0) 
		{
			coolDown = 30;
			execute();
		} 
		else 
		{
			coolDown--;
		}
	}

	//effectue les actions enregistrées dans la liste d'actions
	private void execute()
	{
		if (actions.Count != -1) 
		{

			int r = UnityEngine.Random.Range(0, actions.Count);
			while (r >= 0)
			{
				if (actions[r]())
				{
					actions.RemoveAt(r);
				}
				r--;
			}
		}
	}

	public void addAction(action act)
	{
		actions.Add (act);
	}

	public void addAction(action act, int priority)
	{
		if (priority < 0) 
		{
			throw new ArgumentException ("Cannot set a negative priority", "int priority");
		} 
		else 
		{
			int i = actions.Count - priority;
			if (i < 0)
			{
				i = 0;
			}
			actions.Insert(i, act);
		}
	}
}
