using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using MuhammetInce.HelperUtils;
using TMPro;

public class DragAndDropManager : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 _heldFirstPos;
    private Vector3 _heldPos;

    [Header("Scripts")] [SerializeField] private GameEndedManager gameEndedManager;
    [SerializeField] private PanelAndSelectManager ps;


    [Space] [Header("PlaceHolder"), Space] public List<GameObject> placeHolder = null;

    [Header("Floats"), Space] private float _yValue;
    [SerializeField] private float zDepth = 5f;
    [SerializeField] private float movableDistance = 510;
    [SerializeField] private float reachTime = 1f;

    [Header("Objects"), Space] [SerializeField]
    private Camera mainCamera;

    [SerializeField] private GameObject heldObject;
    [SerializeField] private GameObject currentHolder;

    [Header("Booleans"), Space] public static bool IsChildNull;
    [SerializeField] private bool isChildNullIns;
    [SerializeField] private bool dragBegin;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool rightPos;

    [Header("Materials"), Space] [SerializeField]
    private Material targetHoloMaterial;

    [SerializeField] private Material targetGreenMaterial;

    // Properties
    private Touch Touch => ps.Touch;
    private Ray Ray => mainCamera.ScreenPointToRay(Input.mousePosition);
    private float InputX => Input.GetTouch(0).position.x;
    private float InputY => Input.GetTouch(0).position.y;

    private int TargetLayer
    {
        get => ps.targetLayer;
        set => ps.targetLayer = value;
    }

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
        IsChildNull = false;
    }

    private void UpdateInit()
    {
        TouchPhases();
        RightPlaceChecker();
        isChildNullIns = IsChildNull;
    }

    private void TouchPhases()
    {
        if (Input.touchCount <= 0) return;

        if (IsChildNull) return;

        if (!ps.gOSelected) return;

        if (ps.selectedGo == null) return;

        if (Touch.phase == TouchPhase.Began)
        {
            SelectedCase();
        }

        if (Touch.phase == TouchPhase.Moved)
        {
            _yValue = Input.mousePosition.y;
            if (_yValue <= movableDistance && !isMoving) return;

            On_Drag();
            isMoving = true;
        }

        if (Touch.phase == TouchPhase.Ended)
        {
            On_Drop();
            isMoving = false;
        }
    }

    private void SelectedCase()
    {
        if (!Physics.Raycast(Ray, out hit)) return;

        if (hit.collider.gameObject != ps.selectedGo) return;

        dragBegin = true;
        if (ps.selectedGo.transform.childCount == 0) return;
        heldObject = ps.selectedGo.transform.GetChild(ps.selectedGo.transform.childCount - 1).gameObject;
        HelperUtils.LayerChangerIgnoreRaycast(ps.selectedGo);
        _heldPos = heldObject.transform.position;
        _heldFirstPos = new Vector3(_heldPos.x, _heldPos.y, _heldPos.z);
        HolderVisualizer();
    }

    private void On_Drag()
    {
        if (ps.selectedGo == null) heldObject = null;

        if (heldObject == null) return;

        heldObject.transform.SetParent(null);
        heldObject.transform.localScale = new Vector3(1, 1, 1);
        if (!dragBegin) return;

        heldObject.transform.localPosition = mainCamera.ScreenToWorldPoint(new Vector3(InputX, InputY, zDepth));

        if (!Physics.Raycast(Ray, out hit)) return;

        //rightPos = hit.collider.gameObject == currentHolder;

        if (hit.collider.gameObject == currentHolder)
            rightPos = true;
        else
            rightPos = false;
    }

    private void On_Drop()
    {
        if (!Physics.Raycast(Ray, out hit)) return;

        if (!dragBegin) return;

        //if (hit.collider.CompareTag(heldObject.tag))
        if (rightPos)
        {
            var rotation = currentHolder.transform.rotation.eulerAngles;
            Vector3 currenHolderRot = new Vector3(rotation.x,
                rotation.y, rotation.z);

            heldObject.transform.DOMove(hit.transform.position, reachTime);
            heldObject.transform.DORotate(currenHolderRot, reachTime);
            StartCoroutine(HolderDestroyRetarderCoroutine());
            if (ps.tutorialHand.activeInHierarchy) ps.tutorialHand.SetActive(false);
        }
        else
        {
            if (heldObject == null) return;

            if (Input.mousePosition.y < 600) heldObject.transform.position = _heldPos;

            else heldObject.transform.DOMove(_heldFirstPos, 0.5f);

            heldObject.transform.SetParent(ps.selectedGo.transform);
            if (currentHolder == null) return;

            currentHolder.SetActive(false);
            currentHolder = null;
            HelperUtils.LayerChangerDefault(ps.selectedGo);
            UIManager.Instance.CrossVisualizer();
        }

        if (ps.selectedGo.transform.childCount == 0) IsChildNull = true;
        dragBegin = false;
        heldObject = null;
    }

    private void HolderVisualizer()
    {
        currentHolder = placeHolder
            .Where(a => a.layer == TargetLayer)
            .FirstOrDefault(b => !b.activeInHierarchy);

        if (currentHolder is null) return;

        currentHolder.SetActive(true);
    }

    private void RightPlaceChecker()
    {
        if (currentHolder == null) return;

        currentHolder.GetComponent<MeshRenderer>().material = rightPos ? targetGreenMaterial : targetHoloMaterial;
    }

    private IEnumerator HolderDestroyRetarderCoroutine()
    {
        var waitForSecond = new WaitForSeconds(reachTime);
        yield return waitForSecond;

        placeHolder.Remove(currentHolder);
        Destroy(currentHolder);
        currentHolder = null;
        rightPos = false;
        HelperUtils.LayerChangerDefault(ps.selectedGo);
        GameEndedCheck();
        UIManager.Instance.PraiseVisualizer();
    }

    private void GameEndedCheck()
    {
        if (placeHolder.Count != 0) return;

        gameEndedManager.GameEnded();
        ps.DeSelectEnded();
    }
}