using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NameDisplay : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    [SerializeField] TextMeshProUGUI display;
    public void SetTarget(Transform t, string name_)
    {
        target = t;
        display.text = name_;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(90f, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        offset.y = 0.5f;
        offset.z = -1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;
    }
}
