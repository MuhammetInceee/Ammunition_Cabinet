using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawScripts : MonoBehaviour
{
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if(transform.localPosition.x == 0.15f)
        {
            transform.DOLocalMoveX(-0.15f, 1.5f);
        }
        else if(transform.localPosition.x == -0.15f)
        {
            transform.DOLocalMoveX(0.15f, 1.5f);
        }
        transform.Rotate(0,0,10);
    }
}
