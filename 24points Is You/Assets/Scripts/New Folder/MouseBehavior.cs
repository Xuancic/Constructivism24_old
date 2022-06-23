using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public GameObject _block;
    private GameObject _newSymbol;

    private LayerMask _isSymbol = 1 << 7;
    private LayerMask _isNum = 1 << 6;
    private LayerMask _boundaryLayer = 1 << 3;

    private Vector3 _mousePosToWorld;
    private Vector2 _mousePos;

    private RaycastHit2D _hitSymbol;
    private RaycastHit2D _hitNum;
    private RaycastHit2D _hitBoundary;

    private bool _isClicking;


    private void Update()
    {
        _mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos = new Vector2(_mousePosToWorld.x, _mousePosToWorld.y);
        if (_newSymbol != null)
        {
            PlaceBehavior();
        }
        ClickBehavior();
    }


    //点击触发：数字or符号
    private void ClickBehavior()
    {
        _hitSymbol = Physics2D.Raycast(_mousePos, Vector2.zero, 100, _isSymbol);
        _hitNum = Physics2D.Raycast(_mousePos, Vector2.zero, 100, _isNum);

        ClickBlock();
        ClickNumber();
    }

    private void ClickBlock()
    {
        if (!_isClicking)
        {
            if (Input.GetMouseButtonDown(0) && _hitSymbol)
            {
                _newSymbol = Instantiate(_block, _mousePos, quaternion.identity);
                _isClicking = true;
            }
        }

        Debug.Log("aaaaaaaaaaaaa:" + _isClicking);
        if (_newSymbol != null && _isClicking)
        {
            Debug.Log(_isClicking);
            _newSymbol.transform.position = _mousePos;
        }

        if (Input.GetMouseButtonDown(1) && _newSymbol != null)
        {
            Destroy(_newSymbol);
            _isClicking = false;
        }
    }

    private void ClickNumber()
    {
    }


    //放置鼠标上的方块
    private void PlaceBehavior()
    {
        //把isclicking改为false
        if (_isClicking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hitBoundary = Physics2D.Raycast(_mousePos, Vector2.zero, 100,_boundaryLayer); 
                if (_hitBoundary)
                {
                    Debug.Log("bbbb"+_hitBoundary);
                    Vector2 _placePos = _mousePos;
                    _newSymbol.transform.position = _placePos;
                    _isClicking = false;
                }
            }
        }
    }
}