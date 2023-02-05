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

    public float angleVariance = 30;

    public Transform EndPoint;
    public VineScript VinePrefab;

    public float BranchChance = 1f / 6f;

    public float MaxTreeHeight = 10f;
    public float ParentAccumulatedTreeHeight;

    public void Initialize(VineScript parent, float maxTreeHeight, float accumulatedLength)
    {
        CurrentZScale = 0f;
        fullyGrown = false;
        MaxTreeHeight = maxTreeHeight;
        ParentAccumulatedTreeHeight = accumulatedLength;

        targetLength = Random.Range(minDistance, maxDistance);
        Parent = parent;
        transform.localScale = new Vector3(1, 1, CurrentZScale);

    }

    void Start()
     {
        if(isRootStem) 
        {
            Initialize(null, MaxTreeHeight, 0);
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
        if(ParentAccumulatedTreeHeight + targetLength > MaxTreeHeight) return;
        int BranchesToSpawn = Random.value < BranchChance ? 2 : 1;
        for (int i = 0; i < BranchesToSpawn; i++)
        {
            SpawnBranch();
        
        }
    }

    void SpawnBranch()
    {
        float xRotation = Random.Range(-angleVariance, angleVariance);
        float yRotation = Random.Range(-angleVariance, angleVariance);

        VineScript newChildVine = Instantiate(VinePrefab, EndPoint.position, transform.rotation);
        newChildVine.transform.Rotate(new Vector3(xRotation, yRotation, 0));
        newChildVine.Initialize(this, MaxTreeHeight, ParentAccumulatedTreeHeight + targetLength);

    }
}
