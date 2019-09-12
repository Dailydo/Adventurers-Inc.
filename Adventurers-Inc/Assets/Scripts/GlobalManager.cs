using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{

    public static GlobalManager instance = null;
    
    public string[] _firstNames;
    public string[] _titles;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public string GetRandomName()
    {
        string name = null;

        //Assign random name
        int maxIndex = _firstNames.Length;
        name = _firstNames[Random.Range(0, maxIndex)];

        //Add random title
        maxIndex = _titles.Length;
        name += " " + _titles[Random.Range(0, maxIndex)];

        return name;
    }
}
