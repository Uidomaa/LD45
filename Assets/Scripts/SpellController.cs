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

    private void Start()
    {
        AudioManager.instance.PlayCastSpell();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Time.fixedDeltaTime * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaySpellMiss();
        explosionPS.Play();
        if (collision.gameObject.CompareTag("Ghost"))
        {
            AudioManager.instance.PlaySpellHit();
            if (collision.gameObject.GetComponent<GhostController>())
            {
                if (collision.gameObject.GetComponent<GhostController>().HitByPlayer()) { 
                    GameManager.instance.DefeatedGhost();}
            }
            else
                Debug.LogError("Ghost missing GhostController!");
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
