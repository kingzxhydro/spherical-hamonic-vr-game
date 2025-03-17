using System.Collections.Generic;
using UnityEngine;

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

    void UpdateShader()
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
        List<int> llist = Child1.GetComponent<penisballs>().llist;
        List<int> mlist = Child1.GetComponent<penisballs>().mlist;

        if (llist == null || mlist == null || llist.Count != mlist.Count)
        {
            Debug.LogError("Harmonic lists are null or mismatched in size!");
            return;
        }

        int numHarmonics = llist.Count;

        // Send number of harmonics to shader
        mat.SetInt("_NumHarmonics", numHarmonics);

        // Manually set L and M values (since Unity does not support SetIntArray)
        for (int i = 0; i < numHarmonics; i++)
        {
            mat.SetInt("_L" + i, llist[i]);
            mat.SetInt("_M" + i, mlist[i]);
        }
    }
}
