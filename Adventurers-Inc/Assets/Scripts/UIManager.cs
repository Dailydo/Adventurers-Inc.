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

    //What to do when an item is clicked onto
    public void LogClickedItem(GameObject item)
    {
        Debug.Log(item.name + " clicked.");
    }

}
