using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
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
}
