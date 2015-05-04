using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;

public class unlockSkillWindow : MonoBehaviour {
	
	private skill _skill;
	private ListBox _requirements;
	private Species _player;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public skill skill
	{
		get
		{
			return _skill;
		}
		set
		{
			if (value != null)
			{
				_skill = value;
			}
		}
	}

	public Species player
	{
		get
		{
			return _player;
		}
		set
		{
			if (value != null)
			{
				_player = value;
			}
		}
	}
}
