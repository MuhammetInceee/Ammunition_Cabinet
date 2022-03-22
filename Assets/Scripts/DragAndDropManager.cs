using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DragAndDropManager : MonoBehaviour
{
    private RaycastHit hit;
    
    [SerializeField] private PanelAndSelectManager ps;

    // Constructions
    private Touch Touch => ps.Touch;

    private void SendRay() => ps.SendRay();

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
    }

    private void UpdateInit()
    {
        TouchPhases();
    }

    private void TouchPhases()
    {
        if (Input.touchCount <= 0) return;

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
        SendRay();
        
    }

    private void On_Drag()
    {
        
    }

    private void On_Drop()
    {
        
    }
    
}