using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class actionData
{
	public int ATP;
	public List<moleculePack> environmentMolecules;
	public List<moleculePack> cellMolecules;

	public actionData(int ATP, List<moleculePack>  environmentMolecules, List<moleculePack>  cellMolecules)
	{
		this.ATP = ATP;
		this.environmentMolecules = environmentMolecules;
		this.cellMolecules = cellMolecules;
	}

	public actionData() : this(0, new List<moleculePack>(), new List<moleculePack>()) 
	{

	}

	public static actionData operator + (actionData ac1, actionData ac2)
	{
		actionData result = new actionData ();
		result.ATP = ac1.ATP + ac2.ATP;
		foreach (moleculePack mpc1 in ac1.cellMolecules) 
		{
			moleculePack mpc2 = ac2.cellMolecules.Find(mp => mp.moleculeType.ID == mpc1.moleculeType.ID);
			if (mpc2 != null)
			{
				result.cellMolecules.Add(new moleculePack(mpc1.count + mpc2.count, mpc1.moleculeType));
			}
			else
			{
				result.cellMolecules.Add(new moleculePack(mpc1.count, mpc1.moleculeType));
			}
		}
		foreach (moleculePack mpc2 in ac2.cellMolecules) 
		{
			moleculePack mpc3 = result.cellMolecules.Find(mp => mp.moleculeType.ID == mpc2.moleculeType.ID);
			if (mpc3 == null)
			{
				result.cellMolecules.Add(new moleculePack(mpc2.count, mpc2.moleculeType));
			}
		}
		foreach (moleculePack mpc1 in ac1.environmentMolecules) 
		{
			moleculePack mpc2 = ac2.environmentMolecules.Find(mp => mp.moleculeType.ID == mpc1.moleculeType.ID);
			if (mpc2 != null)
			{
				result.environmentMolecules.Add(new moleculePack(mpc1.count + mpc2.count, mpc1.moleculeType));
			}
			else
			{
				result.environmentMolecules.Add(new moleculePack(mpc1.count, mpc1.moleculeType));
			}
		}
		foreach (moleculePack mpc2 in ac2.environmentMolecules) 
		{
			moleculePack mpc3 = result.environmentMolecules.Find(mp => mp.moleculeType.ID == mpc2.moleculeType.ID);
			if (mpc3 == null)
			{
				result.environmentMolecules.Add(new moleculePack(mpc2.count, mpc2.moleculeType));
			}
		}
		return result;
	}
}


