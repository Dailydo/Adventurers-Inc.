﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && (hit.transform == transform))
            {
                UIManager.instance.LogClickedItem(hit.transform.parent.gameObject);
            }
        }
    }
}
