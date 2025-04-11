using UnityEngine.UI;
using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class ApplyCorrectSolution : MonoBehaviour
{
    public Material originalMaterial;  // Assign this in the Inspector
    private Material correctMaterial;  // Unique material for this instance

    public List<int> correctl = new List<int>();
    public List<int> correctm = new List<int>();

    void Start()
    {
        // Create a new material instance so changes don't affect all objects
        correctMaterial = GetComponent<Renderer>().material;

        LevelProgressor LevelProgressorScript = GameObject.Find("LevelProgressor").GetComponent<LevelProgressor>();

        correctl = LevelProgressorScript.lArrayCorrect1;
        correctm = LevelProgressorScript.mArrayCorrect1;

        int listlength = correctl.Count;

        correctMaterial.SetInt("_NumHarmonics", listlength);

        float[] lArray = new float[listlength];
        float[] mArray = new float[listlength];

        for (int i = 0; i < listlength; i++)
        {
            lArray[i] = (float)correctl[i];
            mArray[i] = (float)correctm[i];
        }

        correctMaterial.SetFloatArray("_LArray", lArray);
        correctMaterial.SetFloatArray("_MArray", mArray);
    }
}
