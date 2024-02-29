using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField]
    bool chegou_Destino = true;
    Vector3 destino;
    bool xablau = false;
    public GameObject rangeAREA;
    Vector3 AreaOriginal;
    public int range;
    public float runSpeed;
    Rigidbody rb;
    Animator ani;

    public bool atk;
    SphereCollider bola;

    bool alvoPlayer = false;
    bool vigiarZona = true;
    // Start is called before the first frame update
    void Start()
    {
        bola = GetComponentInChildren<SphereCollider>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        destino = Vector3.zero;
        chegou_Destino = true;
        AreaOriginal = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (atk)
        {
            return;
        }

        if (vigiarZona)
        {
            Vigia();
        }

        if (alvoPlayer)
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
            ani.SetInteger("CTRLgeral", 0);
        }

        if (chegou_Destino == false)
        {
            transform.LookAt(destino);
            transform.position = Vector3.MoveTowards(transform.position, destino, runSpeed);
            ani.SetInteger("CTRLgeral", 1);
        }
        if (Vector3.Distance(transform.position, destino) < 0.5f)
        {
            xablau = true;
            ani.SetInteger("CTRLgeral", 0);
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
        vigiarZona = false; 
        if (chegou_Destino == false)
        {
            transform.LookAt(destino);
            transform.position = Vector3.MoveTowards(transform.position, destino, runSpeed);
            ani.SetInteger("CTRLgeral", 1);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            xablau = true;
            chegou_Destino = false;
            alvoPlayer = true;
            destino = collision.gameObject.transform.position;
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            xablau = false;
            chegou_Destino = true;
            alvoPlayer = false;
            vigiarZona = true;
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
