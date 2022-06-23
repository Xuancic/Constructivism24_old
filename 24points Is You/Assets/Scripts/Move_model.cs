using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Move_model : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private Vector2 _moveDir;

    // public LayerMask _detectLayer;
    public float _OneGrid;

    public LayerMask _numberLayer;
    public LayerMask _symbolLayer;


    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        JudgeType();
    }

    private void Update()
    {
        JudgeType();
    }

//Judge type of the object
    private void JudgeType()
    {
        if (CompareTag("Static"))
        {
            NotBeMoved();
        }

        if (CompareTag("Control"))
        {
            BeControlled();
        }
    }


//Three Status
    private void NotBeMoved()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void BeControlled()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        MoveControl();
        _moveDir = Vector2.zero;
    }


//Player Moving Control
    private void MoveControl()
    {
        // Debug.DrawRay(transform.position, Vector2.right,Color.red,1.5f);
        DirectionControl();
        if (_moveDir != Vector2.zero)
        {
            // Debug.Log(_moveDir+"asdjashdajshdashdkajhdskahd");
            if (CanMoveDir(_moveDir))
            {
                // Debug.Log("nengdonga");
                Move(_moveDir);
            }
        }
    }

    //按键方向
    private void DirectionControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _moveDir = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _moveDir = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _moveDir = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _moveDir = Vector2.down;
        }

        // Debug.Log(_moveDir);
    }

    //undo
    private bool CanMoveDir(Vector2 dir)
    {
        Vector2 ray_pos = new Vector2(transform.position.x + (dir.x) * 0.5f, transform.position.y + (dir.y) * 0.5f);
        RaycastHit2D _hit = Physics2D.Raycast(ray_pos, dir, 0.2f);
        // Debug.Log("dadao:" + _hit.collider);
        if (!_hit)
        {
            return true;
        }

        if (_hit.collider.CompareTag("Number"))
        {
            if (_hit.collider.GetComponent<NumberStateControl>() != null)
            {
                return _hit.collider.GetComponent<NumberStateControl>().NumberBeingPushed(dir);
                
            }
        }

        if (_hit.collider.CompareTag("Symbol"))
        {
            if(_hit.collider.GetComponent<SymbolState_Control>()!=null)
            {
                return _hit.collider.GetComponent<SymbolState_Control>().JudgeState(dir);
            }
        }

        return false;
    }

    //物体位移
    private void Move(Vector2 dir)
    {
        transform.Translate(dir * _OneGrid);
    }
}