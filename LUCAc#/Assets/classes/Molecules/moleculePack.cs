using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public struct moleculePack
{
	public molecule moleculeType;
	public int count;

	public moleculePack (molecule moleculeType, int count) 
	{
		this.moleculeType = moleculeType;
		this.count = count;
	}
}