using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _zSpeed;
    [SerializeField] private float _xSpeed;
    public static bool isFinish;

    public static float _stopTime;
    public Touch _touch;

    public float _touchBoarder;

    void Start()
    {
        isFinish = false;
        _stopTime = -1;
    }
    void LateUpdate()
    {
        if(_stopTime < 1 && !isFinish)
        {
            PlayerMovementAndroid();
            transform.Translate(0, 0, _zSpeed*Time.deltaTime);
        }
        if (isFinish)
        {
            transform.Translate(0, 0, _zSpeed*Time.deltaTime);
        }
        else if(_stopTime > 0)
        {
            _stopTime -= Time.deltaTime;
        }
        if(transform.position.x < -4f)
        {
            transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 4f)
        {
            transform.position = new Vector3(4f, transform.position.y, transform.position.z);
        }
    }
    public void PlayerMovementAndroid()
    {
        if(Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchBoarder = transform.position.x + _touch.deltaPosition.x * (_xSpeed * Time.deltaTime);

            if(_touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(_touchBoarder, transform.position.y, transform.position.z);
                
            }
        }
    }
    public void PlayerMovementPC()
    {
        float horizontal = Input.GetAxis("Horizontal") * _xSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, _zSpeed * Time.deltaTime);
    }
}
