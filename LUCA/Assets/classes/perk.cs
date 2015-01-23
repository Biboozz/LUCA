using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;

public class perk : MonoBehaviour {

    public perk[] neighboors;
    protected bool emptyPerk;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void draw(int x, int y, int z, ref List<perk> treatedPerks)
    {
        if (emptyPerk)
        {

        }
        else
        {
            for(int i = 0; i <= length(treatedPerks); i++)
            {
                if (isNotInList(neighboors[i], treatedPerks))
                {
                    treatedPerks.Add(neighboors[i]);
                    //neighboors[i].draw() pas fini
                }
            }
        }
    }

    public bool isNotInList(perk perk, List<perk> perks)
        {
            bool b = true;
            foreach (perk p in perks)
            {
                if (p == perk)
                {
                    b = false;
                }
            }
            return b;
        }

        public int length(List<perk> perks)
        {
            int i = 0;
            foreach (perk perk in perks)
            {
                i++;
            }
            return i;
        }
