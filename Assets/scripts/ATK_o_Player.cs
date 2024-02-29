using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_o_Player : MonoBehaviour
{
    Animator ani;
    Enemy_AI ia;
    // Start is called before the first frame update
    void Start()
    {
        ia = GetComponent<Enemy_AI>();
        ani = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ani.SetInteger("CTRLgeral", 3);
            ia.atk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ani.SetInteger("CTRLgeral", 0);
            ia.atk = false;
        }
    }
}
