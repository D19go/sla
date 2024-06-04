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
    [SerializeField] Transform Spawn1;
    [SerializeField] Transform Spawn2;
    [SerializeField] GameObject painel;
    GameObject cam;
    bool timeLocal = false;
    // Start is called before the first frame update
    override public void OnStartClient()
    {
        base.OnStartClient();
        Spawn1 = GameObject.Find("SpawnTime1").transform;
        Spawn2 = GameObject.Find("SpawnTime2").transform;
        cam = GameObject.Find("Camera").gameObject;
    }

    
    public void Escolha_Tank(int _p)
    {
        if (!base.IsOwner)
            return;
        if (_p == 1)
        {
            Tank_Select(LocalConnection, p1);
        }else if (_p == 2)
        {
            Tank_Select(LocalConnection, p2);
        }
        painel.SetActive(false);
    }

    public void localSpawn(int time)
    {
        if (time == 1)
        {
            timeLocal = true;
        }
        else if (time == 2)
        {
            timeLocal = false;
        }
    }

    [ServerRpc]
    void Tank_Select(NetworkConnection conn, GameObject _p)
    {
        if (timeLocal)
        {
            GameObject p_ = Instantiate(_p, Spawn1.position, Quaternion.identity);
            base.Spawn(p_, conn);
            cam.SetActive(false);
        }
        else
        {
            GameObject p_ = Instantiate(_p, Spawn2.position, Quaternion.identity);
            base.Spawn(p_, conn);
            cam.SetActive(false);
        }

    }
}
