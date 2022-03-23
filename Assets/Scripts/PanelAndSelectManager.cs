using UnityEngine;
using MuhammetInce.HelperUtils;

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

    [Header("Objects"), Space] [SerializeField]
    private Camera mainCamera;
    public GameObject selectedGo;

    [Header("Booleans"), Space] 
    public bool gOSelected;
    [SerializeField] private bool canSelect = true;


    // Actions
    public Ray Ray => mainCamera.ScreenPointToRay(Input.mousePosition);
    public Touch Touch => Input.GetTouch(0);
    private bool CanPlayArea => Input.mousePosition.y < lowerPanelControlHeight;

    // Constructions
    private Vector3 Pos
    {
        get => transform.position;
        set => transform.position = value;
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
        if (mainCamera == null) FindObjectOfType<Camera>();
    }

    private void UpdateInit()
    {
        if (!CanPlayArea) return;

        CasesMovement();

        if (canSelect)
            SelectCase();
        else
            DeSelectCase();
    }

    private void CasesMovement()
    {
        if (Input.touchCount <= 0) return;

        if (gOSelected) return;

        if (Touch.phase == TouchPhase.Moved)
        {
            HelperUtils.IgnoreRayLayerAsync(gameObject, 150);
            Pos = new Vector3(Pos.x + Touch.deltaPosition.x * (horizontalSpeed * Time.deltaTime), Pos.y, Pos.z);
        }

        if (Touch.phase == TouchPhase.Ended)
        {
            HelperUtils.DefaultLayerAsync(gameObject, 150);
        }
    }

    private void SelectCase()
    {
        if (!canSelect) return;
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            SelectCaseRay();
        }
    }

    private void DeSelectCase()
    {
        if (canSelect) return;
        if (Input.touchCount <= 0) return;

        if (Touch.phase == TouchPhase.Ended)
        {
            DeSelectCaseRay();
        }
    }

    private void SelectCaseRay()
    {
        if (gameObject.layer != defaultLayer) return;

        if (!Physics.Raycast(Ray, out hit)) return;

        if (gOSelected) return;
        
        selectedGo = hit.collider.gameObject;
        HelperUtils.CaseBigger(selectedGo, bigScaleFactor, 0.5f);
        gOSelected = true;
        canSelect = false;

    }

    private void DeSelectCaseRay()
    {
        if (gameObject.layer != defaultLayer) return;

        if (!Physics.Raycast(Ray, out hit)) return;

        if (!gOSelected) return;
        
        if (hit.collider.gameObject != selectedGo) return;

        HelperUtils.CaseSmaller(selectedGo, defaultScaleFactor, 0.5f);
        selectedGo = null;
        gOSelected = false;
        canSelect = true;
    }
}

// TODO
// StateManager yaz
// Stateleri ata
