using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public static PropManager instance;

    public Transform[] prop11Types;

    private void Awake()
    {
        instance = this;//Each level has its own PropManager
    }

    public Transform GetRandomProp ()
    {
        return prop11Types[Random.Range(0, prop11Types.Length)];
    }
}
