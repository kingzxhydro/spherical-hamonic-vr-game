using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListStorerScript : MonoBehaviour
{
    public List<int> llist = new List<int>();
    public List<int> mlist = new List<int>();

    public List<int> superllist = new List<int>();
    public List<int> supermlist = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Receivellist(List<int> newList)
    {
        llist = newList; // Assign the received list
    }

    public void Receivemlist(List<int> newList)
    {
        mlist = newList; // Assign the received list
    }

    public void Receivesuperllist(List<int> newList)
    {
        superllist = newList; // Assign the received list
    }

    public void Receivesupermlist(List<int> newList)
    {
        supermlist = newList; // Assign the received list
    }
}
