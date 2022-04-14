using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;

public class GameEndedManager : MonoBehaviour
{
    [Header("Canvases"), Space] 
    [SerializeField] private GameObject gameEndedCanvas;
    
    [Header("Doors"), Space]
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject leftDoor;
    
    [Header("DoorAngles"), Space] 
    [SerializeField] private Vector3 rightDoorCloseAngle;
    [SerializeField] private Vector3 leftDoorCloseAngle;
    [Space]
    [SerializeField] private Vector3 rightDoorOpenAngle;
    [SerializeField] private Vector3 leftDoorOpenAngle;

    [Header("Durations"), Space] 
    [SerializeField] private float doorAnimDuration;

    private void Start()
    {
        rightDoor = GameObject.Find("Right_Door");
        leftDoor = GameObject.Find("Left_Door");
    }

    public void DoorOpener()
    {
        rightDoor.transform.DORotate(rightDoorOpenAngle, doorAnimDuration);
        leftDoor.transform.DORotate(leftDoorOpenAngle, doorAnimDuration);
    }
    
    public void GameEnded()
    {
        DoorCloser();
        StartCoroutine(CanvasVisualizerCoroutine());
    }
    private void DoorCloser()
    {
        rightDoor.transform.DORotate(rightDoorCloseAngle, doorAnimDuration);
        leftDoor.transform.DORotate(leftDoorCloseAngle, doorAnimDuration);
    }

    private IEnumerator CanvasVisualizerCoroutine()
    {
        var waitForSecond = new WaitForSeconds(doorAnimDuration);
        yield return waitForSecond;
        
        gameEndedCanvas.SetActive(true);
    }
}
