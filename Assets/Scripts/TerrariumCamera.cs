using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TerrariumCamera : MonoBehaviour
{
    public bool OffsetOnXAxis = false;
    public bool OffsetInPositiveDirection = false;
    
    private CinemachineVirtualCamera virtualCamera;
    
    void Start()
    {
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        SetOffset();
    }

    void Update()
    {
    }

    /// <summary>
    /// Based on the size of the target, set the offset of the virtual camera.
    /// </summary>
    private void SetOffset()
    {
       var targetSize = virtualCamera.LookAt.GetComponent<Renderer>().bounds.size;

       var offset = new Vector3(0f, 2 * targetSize.y, 0f);
       if (OffsetOnXAxis)
       {
           offset.x = 2f * targetSize.x;
           if (!OffsetInPositiveDirection)
           {
               offset.x = -offset.x;
           }
       }
       else
       {
           offset.z = 2f * targetSize.z;
           if (!OffsetInPositiveDirection)
           {
               offset.z = -offset.z;
           }
       }

       virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = offset;
    }
}
