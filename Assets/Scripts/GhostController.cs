using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public int hp = 1;
    public Transform goal;
    public GameObject deathPS;

    private NavMeshAgent agent;
    private bool isChasingPlayer = true;

    void Start()
    {
        if (deathPS != null)
            deathPS.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdateTarget());
    }

    private IEnumerator UpdateTarget ()
    {
        while (isChasingPlayer)
        {
            agent.destination = goal.position;
            yield return new WaitForSeconds(1f);
        }
    }

    private void GhostDead ()
    {
        deathPS.transform.SetParent(null);
        deathPS.SetActive(true);
        Destroy(gameObject, 0.1f);
    }

    /// <summary>
    /// True if ghost was defeated
    /// </summary>
    /// <returns>Was ghost defeated</returns>
    public bool HitByPlayer ()
    {
        hp--;
        if (hp < 1)
        {
            GhostDead();
            return true;
        }
        else
            return false;
    }

    public void HitPlayer ()
    {
        //When player is hit by ghost
        isChasingPlayer = false;
    }
}
