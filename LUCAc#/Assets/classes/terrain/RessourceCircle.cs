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

    public RessourceCircle(POINT center, int radius, molecule molecule, Terrain T)
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

            } while ((p.distance(Center) > Radius) & (p.x < T.get_Width()) & (p.y < T.get_Heigth()) & (p.x >= 0) & (p.y >= 0));

            Nodes.Add(new RessourceNode(p,molecule));
        }
    }

}
