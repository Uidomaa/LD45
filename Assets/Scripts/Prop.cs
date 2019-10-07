using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomProp();
    }

    private void SpawnRandomProp ()
    {
        Quaternion propRotation = Quaternion.Euler(-90f, Random.Range(0f, 359f), 0f);
        Instantiate(PropManager.instance.GetRandomProp(), transform.position, propRotation);
    }
}
