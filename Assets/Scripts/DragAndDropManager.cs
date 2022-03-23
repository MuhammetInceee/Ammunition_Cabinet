using System;
using DG.Tweening;
using MuhammetInce.HelperUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class DragAndDropManager : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private PanelAndSelectManager ps;
    [Header("Floats"), Space]
    [SerializeField] private float _zDepth = 0.1f;
    
    [Header("Objects"), Space]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject heldObject;
    
    
    // Actions
    private Touch Touch => ps.Touch;
    private Ray Ray => mainCamera.ScreenPointToRay(Input.mousePosition);
    private float InputX => Input.GetTouch(0).position.x;
    private float InputY => Input.GetTouch(0).position.y;

    private void Awake()
    {
        AwakeInit();
    }

    private void Update()
    {
        UpdateInit();
    }

    private void AwakeInit()
    {
        ps = FindObjectOfType<PanelAndSelectManager>();
        if (mainCamera == null) FindObjectOfType<Camera>();
    }

    private void UpdateInit()
    {
        TouchPhases();
    }

    private void TouchPhases()
    {
        if (Input.touchCount <= 0) return;
        
        if(!ps.gOSelected) return;
        
        if (Touch.phase == TouchPhase.Began)
        {
            SelectedCase();
        }
        
        if (Touch.phase == TouchPhase.Moved)
        {
            On_Drag();
        }

        if (Touch.phase == TouchPhase.Ended)
        {
            On_Drop();
        }
    }

    private void SelectedCase()
    {
        if (!Physics.Raycast(Ray, out hit)) return;

        if (hit.collider.gameObject != ps.selectedGo) return;
        
        heldObject = ps.selectedGo.transform.GetChild(0).gameObject;
        
        if (ps.selectedGo == null) heldObject = null;
        
        if(heldObject == null) return; 
        
        heldObject.transform.SetParent(null);
        heldObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void On_Drag()
    {
        heldObject.transform.localPosition = mainCamera.ScreenToWorldPoint(new Vector3(InputX,InputY,_zDepth));
        //TODO
        //Hologram Codes
    }

    private void On_Drop()
    {
        if (!Physics.Raycast(Ray, out hit)) return;

        if (!hit.collider.CompareTag(heldObject.tag)) return;
        print("sea");
        print(hit.collider.gameObject.name);

        heldObject.transform.DOMove(hit.transform.position, 1f);
    }
}