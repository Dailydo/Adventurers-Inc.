using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCharacterCard : MonoBehaviour
{
    public GameObject _characterCard;       //Should be instantiated on Start()
    public Vector3 _offset;                 //Offset in with world referential


    private void Awake()
    {
        
    }

    private void Update()
    {
        Vector3 characterCardPos = Camera.main.WorldToScreenPoint(this.transform.position + _offset);
        _characterCard.transform.position = characterCardPos;
    }
}
