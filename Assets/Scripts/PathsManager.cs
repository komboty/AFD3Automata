using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

/// <summary>
/// Script que maneja las transiciones de un estado.
/// </summary>
public class PathsManager : MonoBehaviour
{
    // Objeto que agrupa las transiciones de un Estado.
    public Transform transitions;
    // Transiciones prefabricadas.
    public GameObject pathA;
    public GameObject pathB;
    // Interfaz de usuario que muestra botones de construcion.
    public GameObject uiUnionMode;
    // Interfaz de usuario que muestra botones del nivel.
    public GameObject uiButtons;
    public GameObject uiButtonReturn;
    // Recompensas.
    public GameObject uiRewards;
    // Nombre del estado
    public TextMeshProUGUI stateName;
    // Estados que estan en el nivel.
    public StatesManager statesManager;

    // Auxiliar para saber la posicion del mouse.
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
        // Si se dio click y esta en modo union de dos Estados (crear una transicion)
        if (Input.GetMouseButtonDown(0) && isUnionMode)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out raycastHitInfo) && 
            //    raycastHitInfo.transform.CompareTag(Constants.instance.TAG_TRANSITIONS))
            //{
            if (Physics.Raycast(ray, out raycastHitInfo))
            {                
                Transform stateTarget = raycastHitInfo.transform;
                // Si el usuario no dio click en un Estado, No hacer nada.
                if (!stateTarget.CompareTag(Constants.instance.TAG_TRANSITIONS))
                    return;

                // Se crea la transicion.
                CreatePhat();
                DisableUnionMode();
                
                // Se finaliza la animacion de creacion del Esatdo.
                stateTarget.GetComponent<OnMouseModelAnimation>().ExitAnimation();
                
                // Se cambia el color del simbolo que se selcciono en el canvas del Estado.
                Button buttonClick = transform.GetChild(numButtonClick).GetComponent<Button>();
                ColorBlock colorBlock = buttonClick.colors;
                colorBlock.normalColor = uiTransitionColorClick;
                buttonClick.colors = colorBlock;
                
                // Se almacena la transicion en el auxiliar.
                string targetName = stateTarget.parent.GetComponentInChildren<PathsManager>().stateName.text;                
                UserData.instance.SOLUTION_MOD2[stateName.text + "," + pathName] = targetName;
            }
                
        }
    }

    /// <summary>
    /// Incia la creacion de una transicon del simbolo A.
    /// </summary>
    public void InitPathA()
    {
        ActivateUnionMode(1);
        pathPrefab = pathA;
        pathName = Constants.instance.SYMBOL_A_NAME;
        numButtonClick = 0;
    }

    /// <summary>
    /// Incia la creacion de una transicon del simbolo B.
    /// </summary>
    public void InitPathB()
    {
        ActivateUnionMode(0);
        pathPrefab = pathB;
        pathName = Constants.instance.SYMBOL_B_NAME;
        numButtonClick = 1;
    }

    /// <summary>
    /// Cancela la creacion de una transicion.
    /// </summary>
    public void CancelPath()
    {
        if (isUnionMode)
            DisableUnionMode();
    }

    /// <summary>
    /// Activa el modo union (creacion de una transicion)
    /// </summary>
    /// <param name="numButtonDisable">Numero del boton que no se selecciono</param>
    public void ActivateUnionMode(int numButtonDisable)
    {
        //Debug.Log("DesActiveAll");
        // Se desactivan todos los canvas
        statesManager.CanvasesSetActive(false); 
        // Se desactivan todos los botones
        uiButtons.SetActive(false);
        uiButtonReturn.SetActive(false);
        //uiRewards.SetActive(false);
        // Se activa el canvas actual.
        gameObject.SetActive(true);
        // Se deshabilita el boton que no se dio click.
        transform.GetChild(numButtonDisable).gameObject.SetActive(false);
        // Se activa el modo union.
        isUnionMode = true;
        uiUnionMode.SetActive(true);
        // Al boton cancelar modo union, se pone su evento.
        uiUnionMode.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => CancelPath());
        // Se activan las animaciones de creacion de una transicion a todos los Estados.
        statesManager.ModelsAnimationSetActive(true);
    }

    /// <summary>
    /// Desactiva el modo union (creacion de una transicion)
    /// </summary>
    public void DisableUnionMode()
    {
        //Debug.Log("ActiveAll");
        // Se activan todos los canvas
        statesManager.CanvasesSetActive(true);
        // Se activan todos los botones
        uiButtons.SetActive(true);
        uiButtonReturn.SetActive(true);
        //uiRewards.SetActive(true);
        // Se activan todos los botones del canvas actual.
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);
        // Se desactiva el modo union.
        isUnionMode = false;
        uiUnionMode.SetActive(false);
        // Al boton cancelar modo union, se quita su evento.
        uiUnionMode.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        // Se desactivan las animaciones de creacion de una transicion a todos los Estados.
        statesManager.ModelsAnimationSetActive(false);
    }

    /// <summary>
    /// Destruye una transicion.
    /// </summary>
    /// <param name="pathName">Nombre de la transicion a destruir</param>
    public void DeletePath(string pathName)
    {
        for (int i = 0; i < transitions.childCount; i++)
        {
            Transform path = transitions.GetChild(i);
            if (path.name.Equals(pathName))
                Destroy(path.gameObject);
        }
    }

    /// <summary>
    /// Crea una transicion.
    /// </summary>
    public void CreatePhat()
    {
        // Si hay una transicion con el mismo nombre se destuye.
        DeletePath(pathName);
        // Se crea una transicion.
        GameObject newPath = Instantiate(pathPrefab);
        newPath.name = pathName;
        newPath.transform.position = transitions.position;
        newPath.transform.SetParent(transitions);

        // Se obtiene la posicion del otro Estado al que se unira la transicion.
        Vector3 targetPostion = raycastHitInfo.transform.position - transitions.parent.position;
        //Debug.Log("transform.position - state " + targetPostion);
        Transform transitionPoints = pathName.Equals(Constants.instance.SYMBOL_A_NAME) ? auxPointsA : auxPointsB;

        // Se agregan puntos intermedios a la transaccion creada.
        // 0 si la transicion es al mismo Estado y 1 si es a otro Estado.
        int numAuxPoints = targetPostion == Vector3.zero ? 0 : 1;
        Transform auxPoints = transitionPoints.GetChild(numAuxPoints);
        Transform spline = newPath.transform.GetChild(0);
        for (int i = 0; i < auxPoints.childCount; i++)
            AddKnotTo(spline, auxPoints.GetChild(i).position - transitions.parent.position);

        AddKnotTo(spline, targetPostion);

        // Se ajustan las texturas de la transicion.
        SplineInstantiate[] splinesInstantiate = spline.GetComponents<SplineInstantiate>();
        splinesInstantiate[1].InstantiateMethod = SplineInstantiate.Method.InstanceCount;
        splinesInstantiate[2].InstantiateMethod = SplineInstantiate.Method.InstanceCount;
    }

    /// <summary>
    /// Agrega un punto a una transicion.
    /// </summary>
    /// <param name="spline">Transicion</param>
    /// <param name="postion">Punto a agregar</param>
    public void AddKnotTo(Transform spline, Vector3 postion)
    {
        float3 knotPostion = new float3(postion.x, 0f, postion.z);
        BezierKnot knot = new BezierKnot(knotPostion);
        SplineContainer splineContainer = spline.GetComponent<SplineContainer>();
        splineContainer.Spline.Add(knot, TangentMode.AutoSmooth);
    }
}
