using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class RessourceNode
{
	private molecule Molecule;
	private POINT Center;
	private int Amount;

    public RessourceNode(POINT center, molecule molecule)
    {
        Center = center;
        this.Molecule = molecule;

        Amount = UnityEngine.Random.Range(100,401); //PIF

    }

    public molecule get_Molecule()
    {
        return Molecule;
    }

    public POINT get_Center()
    {
        return Center;
    }

}
