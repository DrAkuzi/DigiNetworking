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
            AddForceServerRpc();

        horizontal = Input.GetAxis("Horizontal");

        if(horizontal != 0)
            RotateServerRpc(horizontal * Time.deltaTime);
    }

    [ServerRpc]
    void AddForceServerRpc()
    {
        //Debug.Log("force added");
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * force);
    }

    [ServerRpc]
    void RotateServerRpc(float horizontal_)
    {
        rb.angularVelocity = Vector3.zero;
        transform.Rotate(rotSpeedVec * horizontal_);
    }    
}
