using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class PathsManager : MonoBehaviour
{
    // Constantes del juego.
    public Constants constants;

    public Transform transition;
    public GameObject pathA;
    public GameObject pathB;
    public GameObject uiUnionMode;
    public GameObject uiButtons;
    public StatesManager statesManager;

    private Ray ray;
    private RaycastHit raycastHitInfo;    

    // Auxiliares
    private GameObject pathPrefab;
    private string pathName;
    public Transform auxPointsA;
    public Transform auxPointsB;
    public Color uiTransitionColorClick = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    private bool isUnionMode = false;
    private int numButtonClick;


    private void Update()
    {
        //if (Input.GetMouseButtonDown(1) && isModJoint)
        //    ActiveAll();


        if (Input.GetMouseButtonDown(0) && isUnionMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHitInfo) && 
                raycastHitInfo.transform.CompareTag(constants.TAG_TRANSITIONS))
            {
                CreatePhat();                
                DisableUnionMode();
                raycastHitInfo.transform.GetComponent<OnMouseModelAnimation>().ExitAnimation();
                // Se cambia el color del simbolo que se selcciono.
                Button buttonClick = transform.GetChild(numButtonClick).GetComponent<Button>();
                ColorBlock colorBlock = buttonClick.colors;
                colorBlock.normalColor = uiTransitionColorClick;
                buttonClick.colors = colorBlock;
            }
                
        }
    }

    public void InitPathA()
    {
        ActivateUnionMode(1);
        pathPrefab = pathA;
        pathName = constants.SYMBOL_A_NAME;
        numButtonClick = 0;
    }

    public void InitPathB()
    {
        ActivateUnionMode(0);
        pathPrefab = pathB;
        pathName = constants.SYMBOL_B_NAME;
        numButtonClick = 1;
    }

    public void CancelPath()
    {
        if (isUnionMode)
            DisableUnionMode();
    }

    public void ActivateUnionMode(int numButtonDisable)
    {
        //Debug.Log("DesActiveAll");
        // Se desactivan todos los canvas
        statesManager.CanvasesSetActive(false); 
        uiButtons.SetActive(false);

        gameObject.SetActive(true);
        transform.GetChild(numButtonDisable).gameObject.SetActive(false);
        isUnionMode = true;
        uiUnionMode.SetActive(true);
        uiUnionMode.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => CancelPath());

        statesManager.ModelsAnimationSetActive(true);
    }

    public void DisableUnionMode()
    {
        //Debug.Log("ActiveAll");
        // Se activan todos los canvas
        statesManager.CanvasesSetActive(true);
        uiButtons.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);

        isUnionMode = false;
        uiUnionMode.SetActive(false);
        uiUnionMode.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();

        statesManager.ModelsAnimationSetActive(false);
    }

    public void DeletePath(string pathName)
    {
        for (int i = 0; i < transition.childCount; i++)
        {
            Transform path = transition.GetChild(i);
            if (path.name.Equals(pathName))
                Destroy(path.gameObject);
        }
    }

    public void CreatePhat()
    {
        DeletePath(pathName);

        GameObject newPath = Instantiate(pathPrefab);
        newPath.name = pathName;
        newPath.transform.position = transition.position;
        newPath.transform.SetParent(transition);

        //Debug.Log("transform.position " + raycastHitInfo.transform.position);
        //Debug.Log("raycastHitInfo.point " + raycastHitInfo.point);
        Vector3 targetPostion = raycastHitInfo.transform.position - transition.parent.position;
        //Debug.Log("transform.position - state " + targetPostion);
        Transform transitionPoints = pathName.Equals(constants.SYMBOL_A_NAME) ? auxPointsA : auxPointsB;

        // Dependiendo del usuario se pondran puntos auxiliares para que
        // no se crucen con otras transacciones.
        int numAuxPoints = targetPostion == Vector3.zero ? 0 : 1;
        Transform auxPoints = transitionPoints.GetChild(numAuxPoints);
        Transform spline = newPath.transform.GetChild(0);
        for (int i = 0; i < auxPoints.childCount; i++)
            AddKnotTo(spline, auxPoints.GetChild(i).position - transition.parent.position);

        AddKnotTo(spline, targetPostion);

        SplineInstantiate[] splinesInstantiate = spline.GetComponents<SplineInstantiate>();
        //Debug.Log(splinesInstantiate.Count());
        splinesInstantiate[1].InstantiateMethod = SplineInstantiate.Method.InstanceCount;
        splinesInstantiate[2].InstantiateMethod = SplineInstantiate.Method.InstanceCount;
        //foreach (SplineInstantiate splineInstantiate in splinesInstantiate)
        //    splineInstantiate.enabled = true;
    }

    public void AddKnotTo(Transform spline, Vector3 postion)
    {
        float3 knotPostion = new float3(postion.x, 0f, postion.z);
        BezierKnot knot = new BezierKnot(knotPostion);
        SplineContainer splineContainer = spline.GetComponent<SplineContainer>();
        splineContainer.Spline.Add(knot, TangentMode.AutoSmooth);
    }
}
