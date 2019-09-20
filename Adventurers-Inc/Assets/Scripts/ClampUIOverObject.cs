using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampUIOverObject : MonoBehaviour
{
    public GameObject _UI_CharacterHeaderPrefab;         //Should be instantiated on Start()
    public Vector3 _offset;                         //Offset in with world referential

    public GameObject _UICharacterHeader = null;

    private void Awake()
    {
        _UICharacterHeader = Instantiate(_UI_CharacterHeaderPrefab);
        _UICharacterHeader.transform.SetParent(GameObject.Find("Canvas").transform);
        _UICharacterHeader.name = transform.parent.name + "_UICharacterHeader";
    }

    private void Update()
    {
        Vector3 characterCardPos = Camera.main.WorldToScreenPoint(this.transform.position + _offset);
        _UICharacterHeader.transform.position = characterCardPos;
    }
}
