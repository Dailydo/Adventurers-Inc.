using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterCardClamp : MonoBehaviour
{
    public GameObject _UICharacterCardPrefab;         //Should be instantiated on Start()
    public Vector3 _offset;                         //Offset in with world referential
    public GameObject _rootCanvas;

    public GameObject _UICharacterCard = null;

    private void Awake()
    {
        _UICharacterCard = Instantiate(_UICharacterCardPrefab);
        _UICharacterCard.transform.SetParent(_rootCanvas.transform);
        _UICharacterCard.name = transform.parent.name + "_UICharacterCard";
    }

    private void Update()
    {
        Vector3 characterCardPos = Camera.main.WorldToScreenPoint(this.transform.position + _offset);
        _UICharacterCard.transform.position = characterCardPos;
    }
}
