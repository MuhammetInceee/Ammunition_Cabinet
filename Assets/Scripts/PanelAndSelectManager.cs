using System;
using System.Collections;
using System.Collections.Generic;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;
using MuhammetInce.HelperUtils;
using UnityEngine.Serialization;

public class PanelAndSelectManager : MonoBehaviour
{

    
    private RaycastHit hit;
    
    [Header("Layers")]
    [SerializeField] private int defaultLayer = 0;
    
    
    [Header("Floats"), Space]
    [SerializeField] private float bigScaleFactor = 2.5f;
    [SerializeField] private float defaultScaleFactor = 1.3f;
    [SerializeField] private float horizontalSpeed = 0.2f;
    [SerializeField] private float lowerPanelControlHeight = 500;
    
    [Header("Objects"), Space]
    [SerializeField] private Camera mainCamera;
    public GameObject selectedGo;
    
    [Header("Booleans"), Space]
    [SerializeField] private bool gOSelected;
    [SerializeField] private bool canSelect = true;
    
    
    // Constructions
    public Ray Ray => mainCamera.ScreenPointToRay(Input.mousePosition);
    public Touch Touch => Input.GetTouch(0);
    private bool CanPlayArea => Input.mousePosition.y < lowerPanelControlHeight;
    private Vector3 Pos
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    private void Update()
    {
        UpdateInit();
    }

    private void UpdateInit()
    {
        if (!CanPlayArea) return;
        CasesMovement();
        if(canSelect)
            SelectCase();
        else
            DeSelectCase();
    }
    private void CasesMovement()
    {
        if (Input.touchCount <= 0) return;
        if (!gOSelected)
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
        if(!canSelect) return;
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            print("clicked");
            SelectCaseRay();
            
        }
    }
    private void DeSelectCase()
    {
        if(canSelect) return;
        if (Input.touchCount <= 0) return;
        
        if (Touch.phase == TouchPhase.Ended)
        {
            print("clicked");
            DeSelectCaseRay();
        }
    }
    private void SelectCaseRay()
    {
        if (gameObject.layer != defaultLayer) return;

        if (Physics.Raycast(Ray, out hit))
        {
            if (gOSelected) return;
            selectedGo = hit.collider.gameObject;
            HelperUtils.CaseBigger(selectedGo, bigScaleFactor, 0.5f);
            gOSelected = true;
            canSelect = false;
        }
    }

    private void DeSelectCaseRay()
    {
        if(gameObject.layer != defaultLayer) return;
        
        if (Physics.Raycast(Ray, out hit))
        {
            if (!gOSelected) return;
            
            if (hit.collider.gameObject != selectedGo) return;
            
            HelperUtils.CaseSmaller(selectedGo, defaultScaleFactor, 0.5f);
            selectedGo = null;
            gOSelected = false;
            canSelect = true;
        }
    }
}

