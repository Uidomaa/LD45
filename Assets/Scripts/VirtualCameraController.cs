using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public GameObject[] virtualCams;

    // Start is called before the first frame update
    void Start()
    {
        //SwitchToVirtualCam(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToVirtualCam (int newCamIndex)
    {
        for (int i = 0; i < virtualCams.Length; i++)
        {
            virtualCams[i].SetActive(i == newCamIndex ? true : false);
        }
    }
}
