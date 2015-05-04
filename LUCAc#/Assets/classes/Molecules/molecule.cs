using UnityEngine;
using System.Collections;

public abstract class molecule
{
	protected bool _isMacro;
	protected int _ID;
	protected string _name;
	protected int _simpleID;
	protected int _macroID;

	public int simpleID
	{
		get
		{
			return _simpleID;
		}
	}

	public int macroID
	{
		get
		{
			return _macroID;
		}
	}

	public int ID
	{
		get 
		{
			return _ID;
		}
	}

	public string name
	{
		get 
		{
			return _name;
		}
	}

	public bool isMacro
	{
		get
		{
			return _isMacro;
		}
	}
}
