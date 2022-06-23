using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberStateControl : MonoBehaviour
{
    public float _OneGrid;
    public Text numberText;
    public string number;
    public List<string> equ;
    // public GameObject win_pic;
    // public Dictionary<int, GameObject> equ_dic;



    private void Awake()
    {
        numberText.text = number;
        // equ_dic = new Dictionary<int, GameObject>();
        // win_pic=GameObject.Find("Win_pic");
        // win_pic.SetActive(false);

    }

    // private void Update()
    // {
    //     if (numberText.text=="24")
    //     {
    //         win_pic.SetActive(true);
    //     }
    // }

    public bool NumberBeingPushed(Vector2 dir)
    {
        
        Vector2 ray_pos = new Vector2(transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到"+hit);
        if (!hit)
        {
            transform.Translate(dir * _OneGrid);
            return true;
        }
        return false;
    }


    public bool IsGettingAnswer(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        if (!hit)
        {
            return false;
        }
        if (hit.collider.CompareTag("Symbol"))
        {
            if (hit.collider.GetComponent<SymbolState_Control>()!=null)
            {
                if (hit.collider.GetComponent<SymbolState_Control>()._text.text!="=")
                {
                    if (hit.collider.GetComponent<SymbolState_Control>().SymbolCalculate(dir))
                    {
                        equ=hit.collider.GetComponent<SymbolState_Control>().equation;
                        // equ_dic = hit.collider.GetComponent<SymbolState_Control>().sym_equ_dic;
                        // equ_dic.Add(2,this.gameObject);
                        Destroy(hit.collider.gameObject);
                        return true;
                    }
                    
                }
            }
        }
        return false;
    }


    // public bool CanGetSecEqu(Vector2 dir)
    // {
    //     
    //     return false;
    // }
}