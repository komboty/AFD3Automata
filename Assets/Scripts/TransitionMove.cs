using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Script que mueve al objeto que tenga este script por un conjunto de transiciones.
/// </summary>
public class TransitionMove : MonoBehaviour
{
    // Transiciones a recorrer.
    public List<SplineContainer> splineContainer;
    // Velocidad a recorre la transiciones.
    public float speed;
    // Se esta recorriendo la transiciones?
    public bool isPlay = false;

    // Auxiliares.
    private float distancePercentage = 0f;
    private float splineLength;
    private int numSplineContainer = 0;
    private bool isEndSplineContainer = false;

    void Update()
    {
        // Si se quiere que se siga recorriendo.
        if (isPlay)
            Move();
    }

    /// <summary>
    /// Mueve al objeto que tiene este script por las transiciones.
    /// </summary>
    public void Move()
    {
        if (!isEndSplineContainer)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;

            Vector3 currentPosition = splineContainer[numSplineContainer].EvaluatePosition(distancePercentage);
            transform.position = currentPosition + Vector3.up;

            if (distancePercentage > 1f)
            {
                isEndSplineContainer = numSplineContainer == splineContainer.Count - 1;
                if (isEndSplineContainer)
                    return;

                numSplineContainer++;
                UpdateValues();
            }

            Vector3 nextPosition = splineContainer[numSplineContainer].EvaluatePosition(distancePercentage + 0.05f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
        }
    }

    /// <summary>
    /// Actualiza los valores de movimiento.
    /// </summary>
    public void UpdateValues()
    {
        distancePercentage = 0f;
        splineLength = splineContainer[numSplineContainer].CalculateLength();
    }

    /// <summary>
    /// Agrega una transicion al conjunto de transiciones.
    /// </summary>
    /// <param name="newSplineContainer">Transicion a agregar</param>
    public void AddSplineContainer(SplineContainer newSplineContainer)
    {
        splineContainer.Add(newSplineContainer);
    }

    /// <summary>
    /// Copia todos los valores de otro objeto en movimento.
    /// </summary>
    /// <param name="otherTransitionMove"></param>
    public void CopyValuesTo(TransitionMove otherTransitionMove)
    {
        distancePercentage = otherTransitionMove.distancePercentage;
        splineLength = otherTransitionMove.splineLength;
        numSplineContainer = otherTransitionMove.numSplineContainer;
        isEndSplineContainer = otherTransitionMove.isEndSplineContainer;        
    }
}
