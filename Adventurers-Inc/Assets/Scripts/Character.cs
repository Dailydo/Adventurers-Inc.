using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public string _name = "John Doe";       //Character's name
    public string[] _nameCandidates;

    private TextMeshPro _3dText;            //Reference to the TextMeshPro script in the 3dName game object


    void Awake()
    {
        _3dText = transform.Find("3dName").GetComponent<TextMeshPro>();
        Set3dName(_name);
    }


    //Sets the character name (variable + 3d display) on "newName"
    void Set3dName (string newName)
    {
        _name = newName;
        _3dText.text = newName;
    }

    //Assigns a random name to the character
    public void AssignRandomName()
    {
        string currentName = _name;
        int maxIndex = _nameCandidates.Length;
        string newName = currentName;

        while (newName == currentName)
        {
            newName = _nameCandidates[Random.Range(0, maxIndex)];
        }
        //Randomization part to be truly implemented later

        _name = newName;
        Set3dName(newName);
    }
}
