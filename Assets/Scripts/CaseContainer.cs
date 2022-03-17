using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CaseContainer : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject hittedGO;
    [SerializeField] private GameObject selectedGO;

    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _gOSelected;

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
                IgnoreRayAsync(150);
                Pos = new Vector3(transform.position.x + Touch.deltaPosition.x * (horizontalSpeed * Time.deltaTime),
                    Pos.y, Pos.z);
            }
            
            if (Touch.phase == TouchPhase.Ended)
            {
                DefaultAsync(150);
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

            if (_gOSelected) return;
            selectedGO = hittedGO;
            _gOSelected = true;
            
            CaseBigger(selectedGO);
        }
    }

    private void CaseBigger(GameObject obj)
    {
        //TODO
        //DoTween Kur
        //Selected Objeyi Büyüt
    }
    

    //async Func
    private async void DefaultAsync(int milliseconds)
    {
        await Task.Delay(milliseconds);
        gameObject.layer = 0;
    }

    private async void IgnoreRayAsync(int milliseconds)
    {
        await Task.Delay(milliseconds);
        gameObject.layer = 2;
    }
}