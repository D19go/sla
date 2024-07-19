using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    [SerializeField] GameObject painel;
    GameObject cam;
    int timeLocal;
    // Start is called before the first frame update
    override public void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            painel.SetActive(false);
            return;
        }
        cam = GameObject.Find("Camera").gameObject;
    }

    
    public void Escolha_Tank(int _p)
    {
        if (!base.IsOwner)
            return;
        if (_p == 1)
        {
            Tank_Select(GetComponent<SpawnPlayer>().Owner, p1, new Vector3(0, 3, -180));
        }else if (_p == 2)
        {
            Tank_Select(GetComponent<SpawnPlayer>().Owner, p2, new Vector3(0,3,180));
        }
        painel.SetActive(false);
    }

    [ServerRpc]
    public void Tank_Select(NetworkConnection conn, GameObject _p, Vector3 sp)
    {
        GameObject p = Instantiate(_p, sp, Quaternion.identity);
        base.Spawn(p, conn);
    }
}
