using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public SeedScript SeedPrefab;

    public float ElapsedTime;

    public float SpawnInterval = 3f;

    void Update()
    {
        ElapsedTime += Time.deltaTime;
        CheckTimer();
    }

    void CheckTimer()
    {
        if (ElapsedTime > SpawnInterval)
        {
            ElapsedTime = 0f;
            PlaceSeed();
        } 
            

    }

    public void PlaceSeed()
    {
        float RandomX = Random.Range(-8, 8);
        float RandomY = .85f;
        float RandomZ = Random.Range(-5, 5);

        Vector3 RandomPostion = new Vector3(RandomX, RandomY, RandomZ);

        Instantiate(SeedPrefab, RandomPostion, Quaternion.Euler(-90, 0, 0));


    }
}
