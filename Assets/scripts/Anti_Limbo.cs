using System.Collections;
using System.Collections.Generic;
using FishNet.Example.Scened;
using FishNet.Object;
using UnityEngine;

public class Anti_Limbo : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            coli.gameObject.transform.position = new Vector3(0, 2, 0);

            /*if (coli.gameObject.TryGetComponent<PLAYER_CTRL1>(out PLAYER_CTRL1 p1))
            {
                p1.Reset_Transform(coli.gameObject.GetComponent<PLAYER_CTRL1>().Owner);
            }
            else
            {
                coli.gameObject.GetComponent<PLAYER_CTRL2>().Reset_Transform(coli.gameObject.GetComponent<PLAYER_CTRL2>().Owner);
            }*/
        }
    }
}
