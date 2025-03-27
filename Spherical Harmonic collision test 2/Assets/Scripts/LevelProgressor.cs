using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Levelprogressor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Loadlevel()
    {
        var arelistsequal = false;

        GameObject correct = GameObject.Find("super");
        List<int> correctlistL = correct.GetComponent<SuperpositionManager>().larraycorrect;
        List<int> correctlistM = correct.GetComponent<SuperpositionManager>().marraycorrect;
        GameObject storage = GameObject.Find("ListStorer");
        List<int> currentlistL = storage.GetComponent<ListStorerScript>().llist;
        List<int> currentlistM = storage.GetComponent<ListStorerScript>().mlist;

        List<int> pairuno = new List<int>{0,0};
        pairuno[0] = currentlistL[0];
        pairuno[1] = currentlistM[0];

        List<int> pairdos = new List<int>{0,0};
        pairdos[0] = currentlistL[1];
        pairdos[1] = currentlistM[1];

        List<int> paircorrectone = new List<int> { 0, 0 };
        paircorrectone[0] = correctlistL[0];
        paircorrectone[1] = correctlistM[0];

        List<int> paircorrecttwo = new List<int>{ 0, 0 };
        paircorrecttwo[0] = correctlistL[1];
        paircorrecttwo[1] = correctlistM[1];



        //for (var i = 0; i < currentlistL.Count; i++)
        //{
            //if (correctlistL[i] != currentlistL[i] || correctlistM[i] != currentlistM[i]) 
            //{
            //    arelistsequal = false;
            //}
       // }

  
       
        if (pairuno[0] == paircorrectone[0] && pairuno[1] == paircorrectone[1])
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
        }

        
    }

}
