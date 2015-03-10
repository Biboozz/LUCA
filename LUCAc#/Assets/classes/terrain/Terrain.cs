using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using AssemblyCSharp;

public class Terrain
{
	private int Heigth;
	private int Width;
	private List<RessourceCircle> Circles = new List<RessourceCircle>();

    ///TODO: fair une fonction aui retourn une liste de mollecules a cote de la cellule.

    public Terrain(int H, int W, List<molecule> Mols)
    {
        Heigth = H;
        Width = W;

        POINT P;

        foreach (molecule M in Mols)
        {
            int boucle = UnityEngine.Random.Range(2, 6);

            for (int i = 0; i < boucle; i++)
            {
                P = new POINT();
                P.x = UnityEngine.Random.Range(0, Width + 1);
                P.y = UnityEngine.Random.Range(0, Heigth + 1);

                Circles.Add(new RessourceCircle(P, UnityEngine.Random.Range(100, 301), M, this));
            }
        }
    }

    public int get_Heigth()
    {
        return Heigth;
    }

    public int get_Width()
    {
        return Width;
    }

}
