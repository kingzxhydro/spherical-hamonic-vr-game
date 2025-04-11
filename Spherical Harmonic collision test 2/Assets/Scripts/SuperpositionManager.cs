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

    }

    public void UpdateShader()
    {
        //yield return null; // Wait one frame to ensure values are fully updated
        Debug.Log("1");
        // Get the material attached to this GameObject
        GameObject superManager = GameObject.FindWithTag("SuperposedHarmonic");
        Debug.Log("2");
        GameObject superSphere = superManager.transform.GetChild(0).gameObject;
        Debug.Log("3");
        mat = superSphere.GetComponent<Renderer>().material;

        // Log the start of the function
        Debug.Log("UpdateShader function called");

        // Find the ListStorer GameObject and reteive l and m lists
        ListStorerScript listStorerScript = GameObject.Find("ListStorer").GetComponent<ListStorerScript>();
        Debug.Log("liststorerscript received");

        // Retrieve lists from the script
        List<int> llist = listStorerScript.llist;
        List<int> mlist = listStorerScript.mlist;
        Debug.Log("lists received");

        // Store lists as superposed lists seperately, for value updated to work with
        // multiple harmonics
        listStorerScript.Receivesuperllist(llist);
        listStorerScript.Receivesupermlist(mlist);
        Debug.Log("superlists updated");

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

        // Clear the existing arrays
        mat.SetFloatArray("_LArray", new float[10]);
        mat.SetFloatArray("_MArray", new float[10]);

        mat.SetFloatArray("_LArray", lArray);
        mat.SetFloatArray("_MArray", mArray);

        // Log values after they've been set
        Debug.Log("L Array sent to shader: " + string.Join(",", lArray));
        Debug.Log("M Array sent to shader: " + string.Join(",", mArray));
    }

}