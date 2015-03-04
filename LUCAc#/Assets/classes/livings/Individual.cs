using UnityEngine;
using System.Collections;

public class Individual
{
	public GameObject cell;
	private int _lifeTime;
	public bool alive = true;
	private Species _species;
	public environment place;
	private int _survivedTime = 0;
	public bool isPlayed = false;


	#region acessors
	public int survivedTime
	{
		get
		{
			return _survivedTime;
		}
	}

	public int lifetime
	{
		get
		{
			return _lifeTime;
		}
	}

	public Species species
	{
		get 
		{
			return _species;
		}
		set
		{
			_species = value;
			cell.GetComponent<cell> ().species = value;
		}
	}
	#endregion


	#region constructors
	public Individual (GameObject cell, Species species, environment place, int lifeTime, bool isPlayed)
	{
		this.cell = cell;
		this.species = species;
		this.place = place;
		_lifeTime = lifeTime;
		this.isPlayed = isPlayed;
		cell cellBehavior = cell.GetComponent<cell> ();
		cellBehavior.place = place;
		cellBehavior.species = species;
		cellBehavior.individual = this;
	}

	public Individual (GameObject cell, environment place, int lifeTime, bool isPlayed)
	{
		this.cell = cell;
		this.place = place;
		_lifeTime = lifeTime;
		this.isPlayed = isPlayed;
		cell cellBehavior = cell.GetComponent<cell> ();
		cellBehavior.place = place;
		cellBehavior.species = species;
		cellBehavior.individual = this;
	}
	#endregion



	public void update()
	{
		_survivedTime = _survivedTime + 1;
		alive = (_survivedTime < _lifeTime);
		cell.GetComponent<cell> ().action();
	}
}
