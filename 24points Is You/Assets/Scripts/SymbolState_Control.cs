using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SymbolState_Control : MonoBehaviour
{
    public Text _text;
    public string _symboltext;
    public List<string> equation;

    // public List<string> symbolEqu;

    // public Dictionary<int,GameObject> sym_equ_dic;
    // public Dictionary<int,GameObject> fin_equ_dic;
    private float answer;
    public GameObject TheAnswer;
    private GameObject Canvas;
    public Transform TheHoriAnswerPos;
    public Transform TheVerAnswerPos;
    private GameObject winPic;

    private void Awake()
    {
        _text.text = _symboltext;
        Canvas = GameObject.Find("Canvas");
    }


    private void Update()
    {
        //每一帧计算能不能成等shi
        if (_text.text == "=")
        {
            if (GetAnHoriAnswer())
            {
                CreateAnAnswer(equation);

                GameObject _finalAnswer = Instantiate(TheAnswer, TheHoriAnswerPos.position, quaternion.identity,
                    Canvas.transform);
                _finalAnswer.GetComponent<NumberStateControl>().number = answer.ToString();
                _finalAnswer.GetComponent<NumberStateControl>().numberText.text = answer.ToString();
                if (_finalAnswer.GetComponent<NumberStateControl>().number=="24")
                {
                    Canvas.GetComponent<WinControl>().Setwin();
                }
            }
        }
        else if (_text.text == "||")
        {
            if (GetAnVertiAnswer())
            {
                CreateAnAnswer(equation);

                GameObject _finalAnswer = Instantiate(TheAnswer, TheVerAnswerPos.position, quaternion.identity,
                    Canvas.transform);
                _finalAnswer.GetComponent<NumberStateControl>().numberText.text = answer.ToString();
                _finalAnswer.GetComponent<NumberStateControl>().number = answer.ToString();
                if (_finalAnswer.GetComponent<NumberStateControl>().number=="24")
                {
                    Canvas.GetComponent<WinControl>().Setwin();
                }
            }
        }
    }

    public bool JudgeState(Vector2 dirFromBefore)
    {
        if (_text.text == "-")
        {
            return Minus(dirFromBefore);
        }
        else if (_text.text == "+")
        {
            return Plus(dirFromBefore);
        }
        else if (_text.text == "|")
        {
            return VerticalLine(dirFromBefore);
        }
        else if (_text.text == "/")
        {
            return Delete(dirFromBefore);
        }
        else if (_text.text == "x")
        {
            return Multiply(dirFromBefore);
        }
        else if (_text.text == @"\")
        {
            return RightLine(dirFromBefore);
        }
        else if (_text.text == "=")
        {
            return HoriEqual(dirFromBefore);
        }
        else if (_text.text == "||")
        {
            return VertiEqual(dirFromBefore);
        }

        return false;
    }


    public bool Minus(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        if (hit.collider.CompareTag("Symbol"))
        {
            if (hit.collider.GetComponent<SymbolState_Control>() != null)
            {
                string nextSyType = hit.collider.GetComponent<SymbolState_Control>()._text.text;
                if (nextSyType == "|")
                {
                    Debug.Log("aaaaa");
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "+";
                    return true;
                }

                if (nextSyType == "-" && (dir == Vector2.left || dir == Vector2.right))
                {
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "=";
                    return true;
                }
            }
        }

        return false;
    }

    public bool Plus(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        return false;
    }

    public bool VerticalLine(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        if (hit.collider.CompareTag("Symbol"))
        {
            if (hit.collider.GetComponent<SymbolState_Control>() != null)
            {
                string nextSyType = hit.collider.GetComponent<SymbolState_Control>()._text.text;
                if (nextSyType == "-")
                {
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "+";
                    return true;
                }
            }

            if (dir == Vector2.up || dir == Vector2.down || hit.collider.GetComponent<SymbolState_Control>() != null)
            {
                string nextSyType = hit.collider.GetComponent<SymbolState_Control>()._text.text;
                if (nextSyType == "|")
                {
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "||";
                    return true;
                }
            }
        }

        return false;
    }

    public bool Delete(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        if (hit.collider.CompareTag("Symbol"))
        {
            if (hit.collider.GetComponent<SymbolState_Control>() != null)
            {
                string nextSyType = hit.collider.GetComponent<SymbolState_Control>()._text.text;
                if (nextSyType == @"\")
                {
                    // Debug.Log("aaaaa");
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "x";
                    return true;
                }
            }
        }

        return false;
    }

    public bool Multiply(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        return false;
    }

    public bool RightLine(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        if (hit.collider.CompareTag("Symbol"))
        {
            if (hit.collider.GetComponent<SymbolState_Control>() != null)
            {
                string nextSyType = hit.collider.GetComponent<SymbolState_Control>()._text.text;
                if (nextSyType == "/")
                {
                    // Debug.Log("aaaaa");
                    Destroy(this.gameObject);
                    hit.collider.GetComponent<SymbolState_Control>()._text.text = "x";
                    return true;
                }
            }
        }

        return false;
    }

    public bool HoriEqual(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        return false;
    }

    public bool VertiEqual(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("箱子打到" + hit);
        if (!hit)
        {
            transform.Translate(dir);
            return true;
        }

        return false;
    }


    //等号发出射线
    public bool GetAnHoriAnswer()
    {
        Vector2 rightray_pos =
            new Vector2(this.transform.position.x + (Vector2.right.x) * 0.5f,
                transform.position.y + (Vector2.right.y) * 0.5f);
        RaycastHit2D rayRight = Physics2D.Raycast(rightray_pos, Vector2.right, 0.5f);

        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (Vector2.left.x) * 0.5f,
                transform.position.y + (Vector2.left.y) * 0.5f);
        RaycastHit2D rayLeft = Physics2D.Raycast(ray_pos, Vector2.left, 0.5f);
        // Debug.DrawRay(ray_pos, Vector2.left, Color.red, 0.5f);
        if (!rayLeft)
        {
            return false;
        }

        if (rayLeft.collider.CompareTag("Number") && !rayRight)
        {
            if (rayLeft.collider.GetComponent<NumberStateControl>() != null)
            {
                if (rayLeft.collider.GetComponent<NumberStateControl>().IsGettingAnswer(Vector2.left))
                {
                    equation = rayLeft.collider.GetComponent<NumberStateControl>().equ;
                    equation.Add(rayLeft.collider.GetComponent<NumberStateControl>().number);
                    // Debug.Log(equation[0] + "aaaaaa" + equation[1] + "bbbbbb" + equation[2]);
                    Destroy(rayLeft.collider.gameObject);
                    Destroy(this.gameObject);
                    return true;
                }
            }
        }

        return false;
    }


    public bool GetAnVertiAnswer()
    {
        Vector2 upray_pos =
            new Vector2(this.transform.position.x + (Vector2.up.x) * 0.5f,
                transform.position.y + (Vector2.up.y) * 0.5f);
        RaycastHit2D rayUp = Physics2D.Raycast(upray_pos, Vector2.up, 0.5f);

        Vector2 downray_pos =
            new Vector2(this.transform.position.x + (Vector2.down.x) * 0.5f,
                transform.position.y + (Vector2.down.y) * 0.5f);
        RaycastHit2D rayDown = Physics2D.Raycast(downray_pos, Vector2.down, 0.5f);
        // Debug.DrawRay(ray_pos, Vector2.left, Color.red, 0.5f);
        if (!rayUp)
        {
            return false;
        }

        if (rayUp.collider.CompareTag("Number") && !rayDown)
        {
            if (rayUp.collider.GetComponent<NumberStateControl>() != null)
            {
                if (rayUp.collider.GetComponent<NumberStateControl>().IsGettingAnswer(Vector2.up))
                {
                    equation = rayUp.collider.GetComponent<NumberStateControl>().equ;
                    equation.Add(rayUp.collider.GetComponent<NumberStateControl>().number);
                    // Debug.Log(equation[0] + "aaaaaa" + equation[1] + "bbbbbb" + equation[2]);
                    Destroy(rayUp.collider.gameObject);
                    Destroy(this.gameObject);
                    return true;
                }
            }
        }

        return false;
    }

    public bool SymbolCalculate(Vector2 dir)
    {
        Vector2 ray_pos =
            new Vector2(this.transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        if (!hit)
        {
            return false;
        }

        if (hit.collider.CompareTag("Number"))
        {
            if (hit.collider.GetComponent<NumberStateControl>() != null)
            {
                if (hit.collider.GetComponent<NumberStateControl>().IsGettingAnswer(dir))
                {
                    equation = hit.collider.GetComponent<NumberStateControl>().equ;
                }

                // else
                // {
                equation.Add(hit.collider.GetComponent<NumberStateControl>().number);
                equation.Add(_text.text);
                // sym_equ_dic.Add(hit.collider.gameObject,hit.collider.GetComponent<NumberStateControl>().number);
                // sym_equ_dic.Add(this.gameObject,_text.text);
                // sym_equ_dic.Add(0,hit.collider.gameObject);
                // sym_equ_dic.Add(1,this.gameObject);
                Destroy(hit.collider.gameObject);
                // }

                return true;
            }
        }

        return false;
    }


    private void CreateAnAnswer(List<string> equation)
    {
        int a = equation.Count;
        // for (int i = 0; i < a; i++)
        // {
        //     if (equation[i] == "x")
        //     {
        //         equation[i - 1] = (float.Parse(equation[i - 1]) * float.Parse(equation[i + 1])).ToString();
        //         equation.Remove(equation[i + 1]);
        //         equation.Remove(equation[i]);
        //         // Debug.Log("iiiiiiiiiiii==="+i+"aaaaaaaa==="+a);
        //         a = equation.Count+1;
        //         Debug.Log("iiiiiiiiiiii===" + i + "aaaaaaaa===" + a);
        //     }
        //
        //     if (equation[i] == "/")
        //     {
        //         if (float.Parse(equation[i - 1]) % float.Parse(equation[i + 1]) == 0)
        //         {
        //             equation[i - 1] = (float.Parse(equation[i - 1]) / float.Parse(equation[i + 1])).ToString();
        //             equation.Remove(equation[i]);
        //             equation.Remove(equation[i + 1]);
        //             a = equation.Count+1;
        //         }
        //     }
        // }


        for (int i = 1; i < equation.Count; i++)
        {
            if (equation[i] == "x")
            {
                equation[i - 1] = (float.Parse(equation[i - 1]) * float.Parse(equation[i + 1])).ToString();
                // Debug.Log(equation.Count);
                equation.Remove(equation[i + 1]);
                // Debug.Log(equation.Count);
                // Debug.Log(equation[i]);
                equation.Remove(equation[i]);
                // Debug.Log("count=" + equation.Count);
            }
        }
        // if (equation[i] == "/")
        // {
        //     if (float.Parse(equation[i - 1]) % float.Parse(equation[i + 1]) == 0)
        //     {
        //         equation[i - 1] = (float.Parse(equation[i - 1]) / float.Parse(equation[i + 1])).ToString();
        //         equation.Remove(equation[i]);
        //         equation.Remove(equation[i + 1]);
        //         // a = equation.Count;
        //     }
        // }

        for (int j = 0; j < equation.Count; j++)
        {
            // Debug.Log("iiiiiiiiiiii" + i);
            if (equation[j] == "+")
            {
                equation[j + 1] = (float.Parse(equation[j - 1]) + float.Parse(equation[j + 1])).ToString();
            }

            if (equation[j] == "-")
            {
                equation[j + 1] = (float.Parse(equation[j - 1]) - float.Parse(equation[j + 1])).ToString();
            }
        }

        // Debug.Log("a=" + a);
        answer = float.Parse(equation[equation.Count - 1]);
        //
        // float firstNum;
        // float secNum;
        // float.TryParse(equation[0], out firstNum);
        // float.TryParse(equation[2], out secNum);
        // // // float.TryParse(, out firstNum);
        // // float.TryParse(equation[2], out secNum);
        // if (equation[1] == "-")
        // {
        //     answer = firstNum - secNum;
        // }
        //
        // else if (equation[1] == "+")
        // {
        //     answer = firstNum + secNum;
        // }
        //
        // else if (equation[1] == "x")
        // {
        //     answer = firstNum * secNum;
        // }
        //
        // else if (equation[1] == "/")
        // {
        //     if (firstNum % secNum == 0)
        //     {
        //         answer = firstNum / secNum;
        //     }
        // }
    }
}