using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    [SerializeField] GameObject painel;
    GameObject timer;
    GameObject gm;
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
        gm = GameObject.Find("GameManager");
        if (gm !=null)
        {
            Debug.Log("Achou");
        }
        cam = GameObject.Find("Camera").gameObject;
        timer = transform.Find("timer").gameObject;
        timer.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))  
        {
        }
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
        cam.SetActive(false);
        painel.SetActive(false);
        
    }

    [TargetRpc]
    public void LigaTimer(NetworkConnection conn, int minutos, int segundos)
    {
        GetComponent<SyncTimer>().Timer(minutos,segundos);

    }

    [ServerRpc]
    public void Tank_Select(NetworkConnection conn, GameObject _p, Vector3 sp)
    {
        GameObject p = Instantiate(_p, sp, Quaternion.identity);
        base.Spawn(p, conn);
        gm.GetComponent<GameManager>().PlayerCount(GetComponent<SpawnPlayer>().Owner, p);
    }
}
