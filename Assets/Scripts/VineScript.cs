using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour
{
    public float minDistance = .5f;
    public float maxDistance = 1f;
    public float GrowthRate = .5f;
    private float targetLength;
    public VineScript Parent;
    public float CurrentZScale = 0f;
    public bool fullyGrown = false;
    public bool isRootStem = false;

    public Transform EndPoint;
    public VineScript VinePrefab;

    public void Initialize(VineScript parent)
    {
        CurrentZScale = 0f;
        fullyGrown = false;
        targetLength = Random.Range(minDistance, maxDistance);
        Parent = parent;

        transform.localScale = new Vector3(1, 1, CurrentZScale);

    }

    void Start()
     {
        if(isRootStem) 
        {
            Initialize(null);
        }
    }
    void Update()
    {
        if(!fullyGrown)
        {
            Grow();
        }
    }
    void Grow()
    {
        // how fast the vine grows
        CurrentZScale += GrowthRate * Time.deltaTime;
        if(CurrentZScale >= targetLength)
        {
            CurrentZScale = targetLength;
            transform.localScale = new Vector3(1, 1, targetLength);
            OnFullyGrown();
            return;
        }



        transform.localScale = new Vector3(1, 1, CurrentZScale);


    }

    void OnFullyGrown()
    {

        // clamp the vine 
        fullyGrown = true;
        VineScript newChildVine = Instantiate(VinePrefab, EndPoint.position, Quaternion.identity);
        newChildVine.Initialize(this);


    }
  
}
