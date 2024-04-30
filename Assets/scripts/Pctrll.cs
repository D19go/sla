using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class Pctrll : NetworkBehaviour
{
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner == false)
        {
            transform.Find("CamP1").gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (base.IsOwner == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0,0, +1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1,0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(+1,0, 0);
        }

    }
}

