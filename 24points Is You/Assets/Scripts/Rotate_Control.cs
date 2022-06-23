using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Control : MonoBehaviour
{
    public string degree45;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            throw new NotImplementedException();
        }
    }

    private void DetectSymbol()
    {
        RaycastHit2D hit_up = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
        if (hit_left.collider.CompareTag("Symbol"))
        {
            string symbol = hit_left.collider.GetComponent<SymbolState_Control>()._text.text;
            if (symbol=="-")
            {
                
            }
        }
    }
}
