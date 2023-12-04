using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Cube : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsServer)
            transform.Rotate(0, Random.Range(0, 360), 0);
    }

}
