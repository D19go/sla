using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Anti_Limbo : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            coli.gameObject.GetComponent<PlayerController>().Reset_Transform(coli.gameObject.GetComponent<PlayerController>().Owner);
        }
    }
}
