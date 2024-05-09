using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    int dano = 5;
    int multiplicador = 2;
    int repique = 0;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (transform.tag == "Bullet") 
        {
            Destroy(gameObject, 2f);
        } else if (transform.tag == "Bullet2")

        {
            Destroy(gameObject, 5f);
            dano *= multiplicador;
        }

        if (transform.tag == "Especial")
        {
            Destroy(gameObject, 10f);
            dano *= multiplicador * multiplicador;
        }else if (transform.tag == "Especial2")
        {
            Destroy(gameObject, 10f);
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
                Destroy(gameObject);
            }
        }

        if (coli.gameObject.tag == "Player")
        {
            if (coli.transform.parent != transform.parent)
            { 
                coli.gameObject.GetComponent<PlayerController>().Hit(dano);
                Destroy(gameObject);
            }
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
                Destroy(gameObject);
            }
        }

        

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
