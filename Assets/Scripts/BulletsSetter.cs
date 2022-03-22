using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletsSetter : MonoBehaviour
{
    private RaycastHit _hit;
    private float _bulletTouchTimer;


    public float bulletTouchTimerTest;
    [SerializeField] private float moveDelay;
    [SerializeField] private string bulletTag = "Bullet";
    [Space][Space]
    [SerializeField] private PanelAndSelectManager ps;
    [Space]
    [SerializeField] private List<Transform> bPosList = new List<Transform>();
    [SerializeField] private List<GameObject> bList = new List<GameObject>();

    // Constructions
    private bool IsSelectedBullet => ps.selectedGo != null && ps.selectedGo.CompareTag(bulletTag);
    private Ray Ray => ps.Ray;
    private Touch Touch => ps.Touch;

    private void Awake()
    {
        AwakeInit();
    }

    void AwakeInit()
    {
        ps = FindObjectOfType<PanelAndSelectManager>();
        _bulletTouchTimer = bulletTouchTimerTest + moveDelay;
    }
    
    private void Update()
    {
        BulletSet();
    }
    private void BulletSet()
    {
        if (Input.touchCount <= 0) return;
        
        if (Touch.phase == TouchPhase.Began)
        {
            _bulletTouchTimer = bulletTouchTimerTest + moveDelay;
            BulletRay();
        }
        
        if (Touch.phase == TouchPhase.Stationary)
        {
            _bulletTouchTimer -= Time.deltaTime;
            
            if (_bulletTouchTimer <= 0)
            {
                BulletRay();
                _bulletTouchTimer = bulletTouchTimerTest + moveDelay;
            }

        }
    }
    
    private void BulletRay()
    {
        if (Physics.Raycast(Ray, out _hit))
        {
            if(ps.selectedGo == null) return;
            if(!ps.selectedGo.gameObject.CompareTag(bulletTag)) return;
            if(!IsSelectedBullet) return;

            BulletCarrier();
        }
    }

    private void BulletCarrier()
    {
        if(bList.Count == 0 || bPosList.Count == 0) return;
        
        bList[0].transform.DOLocalMove(bPosList[0].localPosition, moveDelay);
        bList.Remove(bList[0]);
        bPosList.Remove(bPosList[0]);
    }
}
