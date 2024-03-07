using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_IA : MonoBehaviour
{
    [SerializeField]
    bool chegou_Destino = true;
    Vector3 destino;
    bool xablau = false;
    Vector3 AreaOriginal;
    int range = 160;
    public float runSpeed;
    Rigidbody rb;
    Animator ani;

    public bool atk;
    SphereCollider bola;

    bool alvo_Hunter = false;
    bool rondar_Zona = true;
    // Start is called before the first frame update
    void Start()
    {
        bola = GetComponentInChildren<SphereCollider>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        destino = Vector3.zero;
        chegou_Destino = true;
        AreaOriginal = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (atk)
        {
            return;
        }

        if (rondar_Zona)
        {
            Vigia();
        }

        if (alvo_Hunter)
        {
            Cacador();
        }

        
    }
    void Vigia()
    {
        if (xablau)
            return;
        if (chegou_Destino == true)
        {
            float posicaoX = Random.Range(AreaOriginal.x - range, AreaOriginal.x + range);
            float posicaoZ = Random.Range(AreaOriginal.z - range, AreaOriginal.z + range);
            destino = new Vector3(posicaoX, transform.position.y, posicaoZ);
            chegou_Destino = false;
            // ani.SetInteger("CTRLgeral", 0);
        }

        if (chegou_Destino == false)
        {
            transform.LookAt(destino);
            transform.position = Vector3.MoveTowards(transform.position, destino, runSpeed);
            // ani.SetInteger("CTRLgeral", 1);
        }
        if (Vector3.Distance(transform.position, destino) < 0.5f)
        {
            xablau = true;
            // ani.SetInteger("CTRLgeral", 0);
            Invoke("esperar", 2f);
        }

    }
    void esperar()
    {
        chegou_Destino = true;
        xablau = false;
    }

    void Cacador()
    {
        rondar_Zona = false;
        runSpeed += 0.2f;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Hunter")
        {
            xablau = true;
            chegou_Destino = false;
            alvo_Hunter = true;
            destino = collision.gameObject.transform.position;
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Hunter")
        {
            xablau = false;
            chegou_Destino = true;
            alvo_Hunter = false;
            rondar_Zona = true;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Chao")
        {
            chegou_Destino = true;
        }

    }
}
