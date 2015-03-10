using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class RessourceCircle
{
    private molecule Molecule;
	private POINT Center;
	private List<RessourceNode> Nodes = new List<RessourceNode>();
	private int Radius;

    public RessourceCircle(POINT center, int radius, molecule molecule)
    {
        Center = center;
        Radius = radius;
        Molecule = molecule;

        POINT p = new POINT();

        int boucle = UnityEngine.Random.Range(200,301);

        for (int i = 0; i < boucle; i++)
        {
            do
            {
                p.x = UnityEngine.Random.Range(Center.x - Radius, Center.x + Radius);
                p.y = UnityEngine.Random.Range(Center.y - Radius, Center.y + Radius);

            } while (p.distance(Center) > Radius);

            Nodes.Add(new RessourceNode(p,molecule));
        }
    }

}
