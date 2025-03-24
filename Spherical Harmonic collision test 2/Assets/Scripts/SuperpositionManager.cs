using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SuperpositionManager : MonoBehaviour
{
    private Material mat;

    void Start()
    {
        // Get the material attached to this GameObject
        mat = GetComponent<Renderer>().material;

        // Retrieve and send harmonic data to the shader
        UpdateShader();
    }

    public void UpdateShader()
    {
        // Find the Harmonic GameObject and get its first child
        GameObject OG = GameObject.Find("Harmonic");
        if (OG == null)
        {
            Debug.LogError("Harmonic GameObject not found!");
            return;
        }

        GameObject Child1 = OG.transform.GetChild(0).gameObject;

        // Retrieve lists from the script
        List<int> llist = Child1.GetComponent<PenisBalls>().llist;
        List<int> mlist = Child1.GetComponent<PenisBalls>().mlist;

        if (llist == null || mlist == null || llist.Count != mlist.Count)
        {
            Debug.LogError("Harmonic lists are null or mismatched in size!");
            return;
        }

        int NumHarmonics = llist.Count;
        print("NumHarmonics" + NumHarmonics);

        // Send number of harmonics to shader
        mat.SetInt("_NumHarmonics", NumHarmonics);

        float[] lArray = new float[NumHarmonics];
        float[] mArray = new float[NumHarmonics];

        for (int i = 0; i < NumHarmonics; i++)
        {
            lArray[i] = (float)llist[i];
            mArray[i] = (float)mlist[i];

            mat.SetFloatArray("_LArray", lArray);
            mat.SetFloatArray("_MArray", mArray);

            Array.Clear(lArray, 0, lArray.Length);
            Array.Clear(mArray, 0, mArray.Length);
        }
    }
}
