using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class penisballs : MonoBehaviour
{
    public int l1;
    public int l2;
    public int m1;
    public int m2;

    public Slider lSlider;
    public Slider mSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lSlider.onValueChanged.AddListener(delegate { Values(); });
        mSlider.onValueChanged.AddListener(delegate { Values(); });
    }

    void Values()
    {
        GameObject OG = GameObject.Find("Harmonic");
        GameObject Child1 = OG.transform.GetChild(0).gameObject;
        //l1 = OG.transform.Find("Sphere1").GetComponent<SphericalHarmonicsController>().l;
        //m1 = OG.transform.Find("Sphere1").GetComponent<SphericalHarmonicsController>().m;
        l1 = Child1.GetComponent<SphericalHarmonicsController>().l;
        m1 = Child1.GetComponent<SphericalHarmonicsController>().m;


        GameObject OG2 = GameObject.Find("HarmonicPrefab(Clone)");
        GameObject Child2 = OG2.transform.GetChild(0).gameObject;      
        //l2 = OG2.transform.Find("Sphere2").GetComponent<SphericalHarmonicsController>().l;
        //m2 = OG2.transform.Find("Sphere2").GetComponent<SphericalHarmonicsController>().m;
        l2 = Child2.GetComponent<SphericalHarmonicsController>().l;        
        m2 = Child2.GetComponent<SphericalHarmonicsController>().m;
        

        List<int> llist = new List<int>();
        List<int> mlist = new List<int>();

        llist.Add(l1);
        llist.Add(l2);
        mlist.Add(m1);
        mlist.Add(m2);

        print(llist[0]);
        print(mlist[0]);
        
    }
}
