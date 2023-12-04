using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    [SerializeField] float force;
    [SerializeField] float rotSpeed;
    Rigidbody rb;

    float horizontal;
    Vector3 rotSpeedVec;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotSpeedVec = new Vector3(0, rotSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(transform.forward * force);

        horizontal = Input.GetAxis("Horizontal");

        if(horizontal != 0)
            transform.Rotate(rotSpeedVec * horizontal * Time.deltaTime);
    }
}
