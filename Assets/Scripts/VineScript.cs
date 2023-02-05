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

    public SeedScript Seed;
    public float CurrentZScale = 0f;
    public bool fullyGrown = false;
    public bool isRootStem = false;

    public float angleVariance = 30;

    public Transform EndPoint;
    public float BranchChance = 1f / 6f;

    private float MaxTreeHeight;
    public float ParentAccumulatedTreeHeight;

    private List<VineScript> ChildVineScripts = new List<VineScript> { };

    private bool MarkedForDeath = false;

    public float DeathSeconds = 2f;

    private float DeathSecondsLeft;
    public void Initialize(SeedScript seed, VineScript parent, float maxTreeHeight, float accumulatedLength)
    {
        CurrentZScale = 0f;
        fullyGrown = false;
        MaxTreeHeight = maxTreeHeight;
        ParentAccumulatedTreeHeight = accumulatedLength;
        Seed = seed;

        targetLength = Random.Range(minDistance, maxDistance);
        Parent = parent;
        transform.localScale = new Vector3(1, 1, CurrentZScale);



        GetComponent<Rigidbody>().isKinematic = isRootStem;

    }

    void Update()
    {
        if(!fullyGrown)
        {
            Grow();
        }
        if(MarkedForDeath) DeathSecondsLeft -= Time.deltaTime;
        if (MarkedForDeath && DeathSecondsLeft <= 0) DIE();

    }

    void DIE() {
        Destroy(gameObject);
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

        VineScript newChildVine = Instantiate(Seed.VinePrefab, EndPoint.position, transform.rotation);
        ChildVineScripts.Add(newChildVine);

        newChildVine.transform.Rotate(new Vector3(xRotation, yRotation, 0));
        newChildVine.Initialize(Seed, this, MaxTreeHeight, ParentAccumulatedTreeHeight + targetLength);

        // FixedJoint newFixedJoint = newChildVine.gameObject.AddComponent<FixedJoint>();
        newChildVine.GetComponent<FixedJoint>().connectedBody = GetComponent<Rigidbody>();

    }

    void OnCut()
    {
        GetComponent<FixedJoint>().breakForce = 0f;
        OnBranchBreak();

    }

    public void OnBranchBreak()
    {
        // Debug.Log("Bronken");
        if(MarkedForDeath) return;
        MarkedForDeath = true;
        DeathSecondsLeft = DeathSeconds;
        print(ChildVineScripts.Count);
        foreach (VineScript childVine in ChildVineScripts)
        {
           childVine.OnBranchBreak();
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.TryGetComponent<Cutter>(out Cutter cutter)) 
        {
            OnCut();

        }
    }
}
