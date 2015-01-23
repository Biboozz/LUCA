using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    // For the moment, there's no menu so the game start at the very beggining of the program :
        initiateGame();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {

            loadPerkMenu();

        }

	}
    
    // Use this section to put anything happening at the very begining of the game
    // Different from "Start", which starts at the beggining of the program
    void initiateGame()
    {
        
    }


    void loadPerkMenu()
    {

    }
}
