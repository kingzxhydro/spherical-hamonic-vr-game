using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  This script is used for the initial state (pre-superposition)
public class PenisBalls : MonoBehaviour
{
    public int l1;
    public int l2;
    public int m1;
    public int m2;

    public Slider lSlider;
    public Slider mSlider;

    // Make the lists accessible from other scripts
    public List<int> llist = new List<int>();
    public List<int> mlist = new List<int>();

    void Start()
    {
        lSlider.onValueChanged.AddListener(delegate { Values(); });
        mSlider.onValueChanged.AddListener(delegate { Values(); });
    }

    void Values()
    {
        GameObject OG = GameObject.Find("Harmonic");
        GameObject Child1 = OG.transform.GetChild(0).gameObject;

        l1 = Child1.GetComponent<SphericalHarmonicsController>().l;
        m1 = Child1.GetComponent<SphericalHarmonicsController>().m;

        GameObject OG2 = GameObject.Find("HarmonicPrefab(Clone)");

        if (OG2 != null) {

            GameObject Child2 = OG2.transform.GetChild(0).gameObject;
            l2 = Child2.GetComponent<SphericalHarmonicsController>().l;
            m2 = Child2.GetComponent<SphericalHarmonicsController>().m;
        }
        // Clear old values before adding new ones
        llist.Clear();
        mlist.Clear();

        llist.Add(l1);
        mlist.Add(m1);
        

        if (OG2 != null)
        {
            llist.Add(l2);
            mlist.Add(m2);
        }

        print(llist[0]);
        print(mlist[0]);
    }
}

