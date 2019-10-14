using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatSpell : MonoBehaviour
{
    public static CatSpell instance;

    //public Transform witch;
    public Transform[] catGhosts;
    public GameObject cat;
    public ParticleSystem transformationPS;
    public GameObject creditsCat;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cat.SetActive(false);
        creditsCat.SetActive(false);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(DoCatSpell());
        }
    }
#endif

    public IEnumerator DoCatSpell ()
    {
        CrystalBallScript.instance.gameObject.SetActive(false);
        FindObjectOfType<VirtualCameraController>().SwitchToVirtualCam(2);// Zoom in
        //Move cat ghosts to table
        foreach (var catGhost in catGhosts)
        {
            catGhost.gameObject.SetActive(true);
            catGhost.LookAt(transformationPS.transform);
            catGhost.DOMove(transformationPS.transform.position, 5f).SetEase(Ease.InCubic);
        }
        yield return new WaitForSeconds(4.5f);
        transformationPS.Play();
        yield return new WaitForSeconds(0.5f);
        //Disable catghosts
        foreach (var catGhost in catGhosts)
            catGhost.gameObject.SetActive(false);
        cat.SetActive(true);
    }

    public void ShowCreditsCat ()
    {
        cat.SetActive(false);
        creditsCat.SetActive(true);
    }
}
