using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public int hp = 1;
    public Transform goal;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdateTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateTarget ()
    {
        while (true)
        {
            agent.destination = goal.position;
            yield return new WaitForSeconds(1f);
        }
    }

    public bool HitByPlayer ()
    {
        hp--;
        if (hp < 1)
            return true;
        else
            return false;
    }

    public void HitPlayer ()
    {
        //TODO When player is hit by ghost
    }
}
