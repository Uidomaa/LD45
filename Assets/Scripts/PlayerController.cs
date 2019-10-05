using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT")]
    public float moveSpeed = 10f;
    public float maxSpeed = 10f;
    [Header("MAGIC")]
    public Transform wandTip;
    public Transform magicPrefab;

    private Rigidbody rb;
    private float x;
    private float z;
    Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Catch inputs
        x += Input.GetAxis("Horizontal");
        z += Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
            CastSpell();
        FaceMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void CastSpell ()
    {
        Instantiate(magicPrefab, wandTip.position, transform.rotation);
    }

    private void Move()
    {
        moveVector.x = x;
        moveVector.y = 0f;
        moveVector.z = z;
        if (moveVector.magnitude >= 1f)
            moveVector = moveVector.normalized;
        rb.AddForce(moveVector * moveSpeed);
        // Consume
        x = z = 0f;
    }

    private void FaceMoveDirection ()
    {
        if (rb.velocity.sqrMagnitude > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity), Time.deltaTime * 10f);
    }
}
