using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    [SerializeField] float force;
    [SerializeField] float rotSpeed;
    [SerializeField] NameDisplay nameDisplay;
    [SerializeField] Color[] skins = new Color[3];
    Rigidbody rb;

    float horizontal;
    Vector3 rotSpeedVec;
    MeshRenderer mesh;
    


    struct PlayerData : INetworkSerializable
    {
        public Unity.Collections.FixedString32Bytes name_;
        public int skin;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref name_);
            serializer.SerializeValue(ref skin);
        }
    }

    NetworkVariable<PlayerData> myData = new NetworkVariable<PlayerData>(
        new PlayerData
        {
            name_ = "",
            skin = 0
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (myData.Value.name_ != "")
        {
            SetNameDisplay(myData.Value.name_.ToString());
            SetSkin(myData.Value.skin);
        }
        else if (!IsServer && IsOwner)
        {
            //SetNameDisplay(DataManager.instance.name_);
            myData.Value = new PlayerData 
            { 
                name_ = DataManager.instance.name_, 
                skin = DataManager.instance.color
            };
            SetNameDisplay(myData.Value.name_.ToString());
            SetSkin(myData.Value.skin);
        }

        myData.OnValueChanged += (PlayerData prevVal, PlayerData newVal) =>
        {
            SetNameDisplay(newVal.name_.ToString());
            SetSkin(newVal.skin);
        };
        //else if(!IsServer && IsOwner)
        //SetNameDisplay(myData.Value.name_.ToString());

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotSpeedVec = new Vector3(0, rotSpeed, 0);
        mesh = GetComponent<MeshRenderer>();

        //mesh.material.color = Color.;

        if (!IsServer && IsOwner)
        {
            //Debug.Log("getting name from local: " + DataManager.instance.name_);
            //SetNameDisplay(DataManager.instance.name_);
            //SetNameDisplay(DataManager.instance.name_);
            
        }
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
    
    [ServerRpc]
    void SetNameServerRpc(string name)
    {
        SetNameClientRpc(name);
        SetNameDisplay(name);
    }

    [ClientRpc()]
    void SetNameClientRpc(string name)
    {
        SetNameDisplay(name);
    }

    void SetNameDisplay(string name)
    {
        Debug.Log("setting name: " + name);
        
        nameDisplay.SetTarget(transform, name);
    }

    void SetSkin(int skin)
    {
        Debug.Log("setting skin: " + skin);

        if(!mesh)
            mesh = GetComponent<MeshRenderer>();

        mesh.material.color = skins[skin];
    }    
}
