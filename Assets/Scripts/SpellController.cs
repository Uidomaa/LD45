using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public ParticleSystem explosionPS;

    private ParticleSystem[] pses;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pses = GetComponentsInChildren<ParticleSystem>(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * Time.fixedDeltaTime * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionPS.Play();
        if (collision.gameObject.CompareTag("Ghost"))
        {
            GameManager.instance.HitGhost();
        }
        foreach (var ps in pses)
        {
            ParticleSystem.MainModule psMain = ps.main;
            psMain.loop = false;
            ps.transform.SetParent(null);
        }
        Destroy(gameObject);
    }
}
