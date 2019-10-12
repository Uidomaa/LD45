using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTrophyController : MonoBehaviour
{
    public GameObject[] catTrophies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < catTrophies.Length; i++)
        {
            catTrophies[i].SetActive(i < GameManager.instance.GetCatSoulsCollected() ? true : false);
        }
    }
}
