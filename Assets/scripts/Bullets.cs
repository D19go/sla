using FishNet.Connection;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : NetworkBehaviour
{
    int dano = 5;
    int multiplicador = 2;
    int repique = 0;
    int time = 2;
    
    Rigidbody rb;

    // Start is called before the first frame update
    override public void OnStartServer()
    {
        base.OnStartServer();
        rb = GetComponent<Rigidbody>();
        if (transform.tag == "Bullet") 
        {
            Timer();
        } else if (transform.tag == "Bullet2")

        {
            Timer();
            dano *= multiplicador;
        }

        if (transform.tag == "Especial")
        {
            time = 10;
            Timer();
            dano *= multiplicador * multiplicador;
        }else if (transform.tag == "Especial2")
        {
            time = 10;
            Timer();
            multiplicador = 10;
            dano *= multiplicador;
        }
        
    }

    private void OnCollisionEnter(Collision coli)
    {
        if (transform.tag == "Bullet")
        {
            if (coli.gameObject.tag != "Player")
            {
                base.Despawn(gameObject);
            }
        }

        if (coli.gameObject.tag == "Player")
        {
            coli.gameObject.GetComponent<PlayerController>().Hit(coli.gameObject.GetComponent<PlayerController>().Owner, dano);
            base.Despawn(gameObject);
        }

        if (transform.tag == "Especial")
        {
            rb.isKinematic = true;
        }
        else if(transform.tag == "Especial2")
        {
            repique++;
            if (repique >= 6)
            {
                base.Despawn(gameObject);
            }
        }

    }


    void Timer()
    {
        StartCoroutine(destroy());
        IEnumerator destroy()
        {
            yield return new WaitForSeconds(time);
            base.Despawn(gameObject);
        }
    }
}
