using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuhammetInce.HelperUtils;

public class CaseContainer : MonoBehaviour
{
    [SerializeField] private float bigScaleFactor;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject hittedGO;
    [SerializeField] private GameObject selectedGO;

    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _gOSelected;

    // Constructions
    private float DefaultScaleFactor => transform.localScale.x;
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
        SelectCase();
    }

    private void CasesMovement()
    {
        if (Input.touchCount <= 0) return;
        if (!_isSelected)
        {
            if (Touch.phase == TouchPhase.Moved)
            {
                Helpers.IgnoreRayLayerAsync(gameObject,150);
                Pos = new Vector3(transform.position.x + Touch.deltaPosition.x * (horizontalSpeed * Time.deltaTime),
                    Pos.y, Pos.z);
            }

            if (Touch.phase == TouchPhase.Ended)
            {
                Helpers.DefaultLayerAsync(gameObject,150);
            }
        }
    }

    private void SelectCase()
    {
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            print("clicked");
            SelectCaseRay();
        }
    }

    private void SelectCaseRay()
    {
        if (gameObject.layer != 0) return;

        if (Physics.Raycast(Ray, out var hit))
        {
            hittedGO = hit.collider.gameObject;
            _isSelected = true;


            if (!_gOSelected)
            {
                selectedGO = hittedGO;
                _gOSelected = true;
                Helpers.CaseBigger(selectedGO, bigScaleFactor, 0.5f);
            }
        }
    }
}