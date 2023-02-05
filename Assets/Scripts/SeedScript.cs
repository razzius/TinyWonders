using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour
{
    public VineScript VinePrefab;

    public float MaxTreeHeight = 10f;

    void GrowTree()
    {
        VineScript RootVine =
            Instantiate(VinePrefab,
            transform.position,
            Quaternion.LookRotation(Vector3.up));

        RootVine.isRootStem = true;

        RootVine.Initialize(this, null, Random.Range(8, 16), 0);
    }

    void Start()
    {
        GrowTree();
    }
}
