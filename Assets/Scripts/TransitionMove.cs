using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TransitionMove : MonoBehaviour
{
    public List<SplineContainer> splineContainer;    
    public float speed;
    public bool isPlay = false;

    private float distancePercentage = 0f;
    private float splineLength;
    private int numSplineContainer = 0;
    private bool isEndSplineContainer = false;

    void Update()
    {
        if (isPlay)
            Move();
    }

    public void Move()
    {
        if (!isEndSplineContainer)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;

            Vector3 currentPosition = splineContainer[numSplineContainer].EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

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

    public void UpdateValues()
    {
        distancePercentage = 0f;
        splineLength = splineContainer[numSplineContainer].CalculateLength();
    }

    public void AddSplineContainer(SplineContainer newSplineContainer)
    {
        splineContainer.Add(newSplineContainer);
    }

    public void CopyValuesTo(TransitionMove otherTransitionMove)
    {
        distancePercentage = otherTransitionMove.distancePercentage;
        splineLength = otherTransitionMove.splineLength;
        numSplineContainer = otherTransitionMove.numSplineContainer;
        isEndSplineContainer = otherTransitionMove.isEndSplineContainer;        
    }
}
