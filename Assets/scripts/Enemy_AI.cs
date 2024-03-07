using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField]
    bool chegou_Destino = true;
    Vector3 destino;
    Vector3 alvo_Mob;
    bool xablau = false;
    Vector3 AreaOriginal;
    public int range;
    public float runSpeed;
    Rigidbody rb;
    Animator ani;
    bool rotacao = true;
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
        alvo_Mob = Vector3.zero;
        chegou_Destino = true;
        AreaOriginal = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(rotacao){
            transform.Rotate(0, transform.rotation.y, 0);
        }
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
        if (chegou_Destino == true && alvoPlayer)
        {
            float posicaoX = Random.Range(AreaOriginal.x - range, AreaOriginal.x + range);
            float posicaoZ = Random.Range(AreaOriginal.z - range, AreaOriginal.z + range);
            destino = new Vector3(posicaoX, transform.position.y, posicaoZ);
            chegou_Destino = false;
            // ani.SetInteger("CTRLgeral", 0);
        }

        if (chegou_Destino == false && alvoPlayer)
        {
            transform.LookAt(destino);
            transform.position = Vector3.MoveTowards(transform.position, destino, runSpeed);
            // ani.SetInteger("CTRLgeral", 1);
        }
        if (Vector3.Distance(transform.position, destino) < 1f)
        {
            xablau = true;
            // ani.SetInteger("CTRLgeral", 0);
            Invoke("esperar", 1f);
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
        if (alvoPlayer)
        {
            transform.LookAt(alvo_Mob);
            transform.position = Vector3.MoveTowards(transform.position, alvo_Mob, runSpeed);
            // ani.SetInteger("CTRLgeral", 1);
            
        }
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            xablau = true;
            chegou_Destino = false;
            alvoPlayer = true;
            alvo_Mob = collision.gameObject.transform.position;
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            vigiarZona = true;
            xablau = false;
            chegou_Destino = true;
            alvoPlayer = false;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Chao")
        {
            chegou_Destino = true;
            Vigia();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            collision.gameObject.GetComponent<Capturado>().pego();
        }

    }
}
