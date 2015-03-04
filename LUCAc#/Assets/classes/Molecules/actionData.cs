using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public struct actionData
{
	readonly int ATP;
	readonly List<molecule> environmentMolecules;
	readonly List<int> environmentMoleculesCounts;
	readonly List<molecule> cellMolecules;
	readonly List<int> cellMoleculesCounts;

	public actionData(int ATP, List<molecule>  environmentMolecules,List<int> environmentMoleculesCounts, List<molecule>  cellMolecules,List<int> cellMoleculesCounts)
	{
		this.ATP = ATP;
		this.environmentMolecules = environmentMolecules;
		this.environmentMoleculesCounts = environmentMoleculesCounts;
		this.cellMolecules = cellMolecules;
		this.cellMoleculesCounts = cellMoleculesCounts;
	}
}


