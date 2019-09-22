using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance = null;       //singleton's instance

    public GameObject _CharacterCard;

    public GameObject _currentSelectedObject;


    public void Awake()
    {
        //singleton definition
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //Do stuff when a character is clicked onto
    public void CharacterClicked(GameObject character)
    {
        Debug.Log(character.name + " clicked.");
    }

}
