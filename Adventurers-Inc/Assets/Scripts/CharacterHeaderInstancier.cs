using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHeaderInstancier : MonoBehaviour
{
    public GameObject _UI_CharacterHeaderPrefab;         //Should be instantiated on Start()
    public Vector3 _offset;                         //Offset in with world referential
    public GameObject _header = null;


    private void Start()
    {
        _header = Instantiate(_UI_CharacterHeaderPrefab);
        _header.transform.SetParent(GameObject.Find("Canvas").transform);           //Position in scene hierarchy
        _header.name = transform.parent.name + "_UICharacterHeader";                //Give identifiable name

        _header.GetComponent<UI_CharacterHeader>()._character = transform.parent.GetComponent<Character>();         //Provide headerScript a reference toward its character
    }

    //Maintain the header close to the character in the 3d view
    private void Update()
    {
        Vector3 characterCardPos = Camera.main.WorldToScreenPoint(this.transform.position + _offset);
        _header.transform.position = characterCardPos;
    }
}
