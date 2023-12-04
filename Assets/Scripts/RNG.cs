using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class RNG : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    NetworkVariable<int> randomNumber = new NetworkVariable<int>();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        text.text = randomNumber.Value.ToString();
        randomNumber.OnValueChanged += (int prevVal, int newVal) =>
        {
            text.text = newVal.ToString();
        };
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer)
            return;

        if (other.CompareTag("Player"))
            GenerateNum();
    }

    void GenerateNum()
    {
        randomNumber.Value = Random.Range(0, 1000);
        //text.text = num.ToString();
        //DisplayRNGClientRpc(num);
    }

    [ClientRpc]
    void DisplayRNGClientRpc(int num)
    {
        text.text = num.ToString();
    }
}
