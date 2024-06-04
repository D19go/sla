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
    int force = 5000;
    
    Rigidbody rb;

    // Start is called before the first frame update
    override public void OnStartClient()
    {
        base.OnStartClient();
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
        
        rb.AddForce(transform.forward * force * Time.fixedDeltaTime, ForceMode.Impulse);
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
            if (TryGetComponent<PLAYER_CTRL1>(out PLAYER_CTRL1 p1))
            {
                p1.Hit(coli.gameObject.GetComponent<PLAYER_CTRL2>().Owner, dano);
            }
            else
            {
                coli.gameObject.GetComponent<PLAYER_CTRL2>().Hit(coli.gameObject.GetComponent<PLAYER_CTRL2>().Owner, dano);
            }
        }

        if (transform.tag == "Especial")
        {
            rb.isKinematic = true;
        }
        else if(transform.tag == "Especial2")
        {
            rb.isKinematic = true;
            transform.Find("Sphere").gameObject.SetActive(true);
            StartCoroutine(bye());
            IEnumerator bye()
            {
                yield return new WaitForSeconds(0.2f);
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
