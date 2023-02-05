using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public float CurrentHeight = 1;
    void Update()
    {
        CurrentHeight += Time.deltaTime;
        transform.localScale = new Vector3(1, 1, CurrentHeight);
    }
}
