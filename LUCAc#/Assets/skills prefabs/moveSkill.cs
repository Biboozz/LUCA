using UnityEngine;
using System.Collections;

public class moveSkill : MonoBehaviour {

	public GameObject Position;
	private bool move = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		move = Input.GetKey(KeyCode.M);
	}

	public void click()
	{
		if (move)
		{
			Position.GetComponent<moveSkillTree> ().click ();
		}
	}
}
