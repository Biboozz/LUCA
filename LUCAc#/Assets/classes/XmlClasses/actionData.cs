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
}


