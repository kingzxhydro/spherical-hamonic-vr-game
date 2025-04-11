using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LevelProgressor : MonoBehaviour
{
    public List<int> lArrayCorrect1 = new List<int> { 4, 5 };
    public List<int> mArrayCorrect1 = new List<int> { 3, 4 };

    public List<int> lArrayCorrect2 = new List<int> { 0, 0 };
    public List<int> mArrayCorrect2 = new List<int> { 0, 0 };

    public List<int> lArrayCorrect3 = new List<int> { 0, 0 };
    public List<int> mArrayCorrect3 = new List<int> { 0, 0 };

    public int levelCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Level loader based on if correct harmonics
    public void LoadLevel()
    {

        Debug.Log("LoadLevel called");
        var arelistsequal = false;

        GameObject ListStorer = GameObject.Find("ListStorer");
        List<int> currentlistL = ListStorer.GetComponent<ListStorerScript>().llist;
        List<int> currentlistM = ListStorer.GetComponent<ListStorerScript>().mlist;

        List<int> pairuno = new List<int> { 0, 0 };
        pairuno[0] = currentlistL[0];
        pairuno[1] = currentlistM[0];

        List<int> pairdos = new List<int> { 0, 0 };
        pairdos[0] = currentlistL[1];
        pairdos[1] = currentlistM[1];

        List<int> paircorrectone = new List<int> { 0, 0 };
        paircorrectone[0] = lArrayCorrect1[0];
        paircorrectone[1] = mArrayCorrect1[0];

        List<int> paircorrecttwo = new List<int> { 0, 0 };
        paircorrecttwo[0] = lArrayCorrect1[1];
        paircorrecttwo[1] = mArrayCorrect1[1];



        //for (var i = 0; i < currentlistL.Count; i++)
        //{
        //if (correctlistL[i] != currentlistL[i] || correctlistM[i] != currentlistM[i])
        //{
        //    arelistsequal = false;
        //}
        // }



        if ((pairuno[0] == paircorrectone[0] && pairuno[1] == paircorrectone[1]))
        {
            if (pairdos[0] == paircorrecttwo[0] && pairdos[1] == paircorrecttwo[1])
            {
                arelistsequal = true;
            }
        }

        if (pairuno[0] == paircorrecttwo[0] && pairuno[1] == paircorrecttwo[1])
        {
            if (pairdos[0] == paircorrectone[0] && pairdos[1] == paircorrectone[1])
            {
                arelistsequal = true;
            }
        }

        if (arelistsequal == true)
        {
            print("Hooray you did it!!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            levelCount += 1;
        }


    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(levelCount);
    }

}
