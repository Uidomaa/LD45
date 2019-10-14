using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public GameObject[] virtualCams;

    public void SwitchToVirtualCam (int newCamIndex)
    {
        for (int i = 0; i < virtualCams.Length; i++)
        {
            virtualCams[i].SetActive(i == newCamIndex ? true : false);
        }
    }
}
