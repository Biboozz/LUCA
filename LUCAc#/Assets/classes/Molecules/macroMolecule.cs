using UnityEngine;
using System.Collections;

public class macroMolecule : molecule
{
	private int _minSize;
	private int _maxSize;

	public int minSize
	{
		get
		{
			return _minSize;
		}
	}

	public int maxSize
	{
		get
		{
			return _maxSize;
		}
	}



	public macroMolecule(int minSize, int maxSize, int simpleID, int macroID, int ID)
	{
		_minSize = minSize;
		_maxSize = maxSize;
		_simpleID = simpleID;
		_macroID = macroID;
		_ID = ID;
		_isMacro = true;
	}

}
