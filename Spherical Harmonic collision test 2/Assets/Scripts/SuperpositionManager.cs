using System;
using System.Collections;
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
        StartCoroutine(UpdateShaderNextFrame());
    }

    private IEnumerator UpdateShaderNextFrame()
    {
        yield return null; // Wait one frame to ensure values are fully updated

        // Log the start of the function
        Debug.Log("UpdateShader function called");

        // Find the Harmonic GameObject and get its first child
        GameObject OG = GameObject.Find("Harmonic");
        if (OG == null)
        {
            Debug.LogError("Harmonic GameObject not found!");
            yield break;
        }

        GameObject Child1 = OG.transform.GetChild(0).gameObject;

        // Retrieve lists from the script
        List<int> llist = Child1.GetComponent<PenisBalls>().llist;
        List<int> mlist = Child1.GetComponent<PenisBalls>().mlist;

        if (llist == null || mlist == null || llist.Count != mlist.Count)
        {
            Debug.LogError("Harmonic lists are null or mismatched in size!");
            yield break;
        }

        int NumHarmonics = llist.Count;
        Debug.Log("NumHarmonics: " + NumHarmonics);

        // Send number of harmonics to shader
        mat.SetInt("_NumHarmonics", NumHarmonics);

        // Log harmonic values being passed to the shader
        Debug.Log("lArray: " + string.Join(",", llist));
        Debug.Log("mArray: " + string.Join(",", mlist));

        float[] lArray = new float[NumHarmonics];
        float[] mArray = new float[NumHarmonics];

        for (int i = 0; i < NumHarmonics; i++)
        {
            lArray[i] = (float)llist[i];
            mArray[i] = (float)mlist[i];
        }

        mat.SetFloatArray("_LArray", lArray);
        mat.SetFloatArray("_MArray", mArray);

        // Log values after they've been set
        Debug.Log("L Array sent to shader: " + string.Join(",", lArray));
        Debug.Log("M Array sent to shader: " + string.Join(",", mArray));
    }

}
