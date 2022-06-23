using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Degree45_Control : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DetectSymbol();
        }
    }


    private void DetectSymbol()
    {
        RaycastHit2D hit_up = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
        if (hit_left.collider.CompareTag("Symbol") && hit_up.collider.CompareTag("Symbol"))
        {
            if (hit_left.collider == hit_up.collider)
            {
                string symbol = hit_left.collider.GetComponent<SymbolState_Control>()._text.text;
                if (symbol == "-")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = "/";
                }

                if (symbol == "+")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = "x";
                }
                
                if (symbol == "x")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = "+";
                }

                if (symbol == @"\")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = "-";
                }

                if (symbol == "/")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = "|";
                }

                if (symbol == "|")
                {
                    hit_left.collider.GetComponent<SymbolState_Control>()._text.text = @"\";
                }
                Destroy(gameObject);
            }
        }
    }
}