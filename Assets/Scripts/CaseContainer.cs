using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuhammetInce.HelperUtils;

public class CaseContainer : MonoBehaviour
{
    [Header("Floats")]
    [SerializeField] private float bigScaleFactor;
    [SerializeField] private float defaultScaleFactor;
    [SerializeField] private float horizontalSpeed;
    
    [Header("Objects")]
    [Space]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject hittedGO;
    [SerializeField] private GameObject selectedGO;
    
    [Header("Bools")]
    [Space]
    [SerializeField] private bool _gOSelected;
    [SerializeField] private bool selectActive = true;

    // Constructions
    private Ray Ray => mainCamera.ScreenPointToRay(Input.mousePosition);
    private Touch Touch => Input.GetTouch(0);
    private Vector3 Pos
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Update()
    {
        CasesMovement();
        
        if(selectActive)
            SelectCase();
        else
            DeSelectCase();
    }

    private void CasesMovement()
    {
        if (Input.touchCount <= 0) return;
        if (!_gOSelected)
        {
            if (Touch.phase == TouchPhase.Moved)
            {
                HelperUtils.IgnoreRayLayerAsync(gameObject,150);
                Pos = new Vector3(transform.position.x + Touch.deltaPosition.x * (horizontalSpeed * Time.deltaTime),
                    Pos.y, Pos.z);
            }

            if (Touch.phase == TouchPhase.Ended)
            {
                HelperUtils.DefaultLayerAsync(gameObject,150);
            }
        }
    }

    private void SelectCase()
    {
        if(!selectActive) return;
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            print("clicked");
            SelectCaseRay();
            
        }
    }

    private void DeSelectCase()
    {
        if(selectActive) return;
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            print("clicked");
            DeSelectCaseRay();
            
        }
    }

    private void SelectCaseRay()
    {
        if (gameObject.layer != 0) return;

        if (Physics.Raycast(Ray, out var hit))
        {
            if (_gOSelected) return;
            
            hittedGO = hit.collider.gameObject;
            selectedGO = hittedGO;
            HelperUtils.CaseBigger(selectedGO, bigScaleFactor, 0.5f);
            _gOSelected = true;
            selectActive = false;
            
        }
    }

    private void DeSelectCaseRay()
    {
        if(gameObject.layer != 0) return;
        
        if (Physics.Raycast(Ray, out var hit))
        {
            if(_gOSelected)
            {
                if (hit.collider.gameObject == selectedGO)
                {
                    HelperUtils.CaseSmaller(selectedGO, defaultScaleFactor, 0.5f);
                    _gOSelected = false;
                    selectActive = true;
                }
            }
        }
    }
}