using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEndedManager : Singleton<GameEndedManager>
{
    [Header("Doors"), Space]
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject leftDoor;

    [Header("DoorAngles"), Space] 
    [SerializeField] private Vector3 rightDoorAngle;
    [SerializeField] private Vector3 leftDoorAngle;

    [Header("Durations"), Space] 
    [SerializeField] private float doorCloseDuration;

    public void DoorCloser()
    {
        rightDoor.transform.DORotate(rightDoorAngle, doorCloseDuration);
        leftDoor.transform.DORotate(leftDoorAngle, doorCloseDuration);
    }
}
