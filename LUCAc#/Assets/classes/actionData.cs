using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public struct actionData
{
	readonly int ATP;
	readonly List<molecule>  environmentMolecules;
	readonly List<molecule>  cellMolecules;

	public actionData(int ATP, List<molecule>  environmentMolecules, List<molecule>  cellMolecules)
	{
		this.ATP = ATP;
		this.environmentMolecules = environmentMolecules;
		this.cellMolecules = cellMolecules;
	}
}


