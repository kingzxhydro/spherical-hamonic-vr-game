using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used for the initial state (pre-superposition)
public class PenisBalls : MonoBehaviour
{
    public int l1;
    public int l2;
    public int m1;
    public int m2;

    public Slider l1Slider;
    public Slider m1Slider;

    // Make the lists accessible from other scripts
    public List<int> llist = new List<int>();
    public List<int> mlist = new List<int>();

    void Start()
    {
        l1Slider.onValueChanged.AddListener(delegate { Values(); });
        m1Slider.onValueChanged.AddListener(delegate { Values(); });
    }

    void Values()
    {
        StartCoroutine(UpdateValuesNextFrame());
    }

    IEnumerator UpdateValuesNextFrame()
    {
        yield return null; // Wait until the next frame to ensure the sliders update

        GameObject OG = GameObject.Find("Harmonic");
        GameObject Child1 = OG.transform.GetChild(0).gameObject;

        l1 = Child1.GetComponent<SphericalHarmonicsController>().l;
        m1 = Child1.GetComponent<SphericalHarmonicsController>().m;

        GameObject OG2 = GameObject.Find("HarmonicPrefab(Clone)");

        if (OG2 != null)
        {
            GameObject Child2 = OG2.transform.GetChild(0).gameObject;
            l2 = Child2.GetComponent<SphericalHarmonicsController>().l;
            m2 = Child2.GetComponent<SphericalHarmonicsController>().m;
        }

        // Clear old values before adding new ones
        llist.Clear();
        mlist.Clear();

        // Add new values
        llist.Add(l1);
        mlist.Add(m1);

        if (OG2 != null)
        {
            llist.Add(l2);
            mlist.Add(m2);
        }

        // Debug Logging
        Debug.Log("Harmonic 1: L = " + l1 + ", M = " + m1);
        if (OG2 != null)
        {
            Debug.Log("Harmonic 2: L = " + l2 + ", M = " + m2);
        }

        Debug.Log("Updated Harmonic Lists:");
        Debug.Log("L List: " + string.Join(",", llist));
        Debug.Log("M List: " + string.Join(",", mlist));
    }
}


