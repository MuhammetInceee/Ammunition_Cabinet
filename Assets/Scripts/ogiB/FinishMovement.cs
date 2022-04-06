using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishMovement : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _spawnObject;
    private float _zSpeed;
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _zSpeed = _playerMovement._zSpeed;
        StartCoroutine(FinishMove());
    }
    void Update()
    {
        transform.Translate(0, 0, _zSpeed*Time.deltaTime);
    }
    public IEnumerator FinishMove()
    {
        yield return new WaitForSeconds(2f);
        transform.DOMove(_spawnObject.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
