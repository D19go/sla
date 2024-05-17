using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using Unity.VisualScripting;
using FishNet.Connection;
using FishNet.Example;

public class PlayerController : NetworkBehaviour
{
    public static bool timerOVER = false;  
    float velocidade;
    public GameObject escudo;
    bool esc = false;
    public float speedBase;
    [SerializeField] private int rotacaoTanque;
    [SerializeField] private GameObject torreta;
    [SerializeField] private GameObject canoTorreta;
    [SerializeField] GameObject dead;

    //-------------------------------------

    public int force;
    public int especialForce;

    public GameObject conteiner;
    public Transform exit;
    public GameObject bullet;
    public GameObject bullet2;

    public GameObject especial;
    public GameObject especial2;
    public float timerShoot = 5f;
    bool shoot = true;

    //-------------------------------------

    float rotationX = 10;
    public float speedRotation = 5f;

    //-------------------------------------

    public int vida = 200;
    bool b1 = false;
    bool e2 = false;
    bool cursor = false;
    Rigidbody rb;
    NetworkHudCanvases nc;
    Vector3 rotationLocal = Vector3.zero;
    

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            return; 
        }
        transform.Find("hud").gameObject.SetActive(true);
        velocidade = speedBase;
        rb = GetComponent<Rigidbody>();
        conteiner = transform.Find("Disparo_Conteiner").gameObject;
        conteiner.SetActive(true);
        transform.Find("corpo").transform.Find("base").transform.Find("base").transform.Find("cano").Find("CameraMain").gameObject.SetActive(true);
        nc = GameObject.Find("NetworkManager").transform.Find("NetworkHudCanvas").GetComponent<NetworkHudCanvases>();
        dead = GameObject.Find("Canvas").transform.Find("dead").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!base.IsOwner)
        {
            return;
        }
        if (timerOVER)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursor = !cursor;
            if (cursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = cursor;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = cursor;
            }
        }

        canoTorreta.transform.Rotate(Input.GetAxis("Mouse Y"), 0,0);
        torreta.transform.Rotate(0,0,Input.GetAxis("Mouse X") * speedRotation);
        Vector3 direcaoFrente = transform.up; // Obtenha a direção para a frente com base na orientação atual do objeto

        

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + -direcaoFrente * velocidade * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + direcaoFrente * velocidade * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,0, -rotacaoTanque * Time.fixedDeltaTime);
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,0, rotacaoTanque * Time.fixedDeltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            velocidade *= 2;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidade = speedBase;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && !esc)
        {
            StartCoroutine(Ataque());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            e2 =! e2;
            GetComponent<HUD_Player>().Habilidade(2);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            b1 =! b1;
            GetComponent<HUD_Player>().Habilidade(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Quaternion desiredRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = desiredRotation;

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!b1)
            {
                Server_AtirarRpc(base.Owner, bullet, force);
            }
            else
            {
                Server_AtirarRpc(base.Owner, bullet2, force);
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && shoot)
        {
            shoot = !shoot;   
            if (!e2)
            {
                Server_AtirarRpc(base.Owner, especial, especialForce);

            }else
            {
                Server_AtirarRpc(base.Owner,especial2, especialForce);
            }
        }
    }

    private void OnCollisionStay(Collision coli)
    {
        if (!base.IsOwner)
        {
            return;
        }
        if (coli.gameObject.layer != 4 && Vector3.Dot(transform.forward, coli.contacts[0].normal) < 0 && coli.gameObject.layer != 3)
        {
            velocidade = 1;
        }
    }

    [ServerRpc]
    void Server_AtirarRpc(NetworkConnection conn,GameObject prefab, int forca)
    {
        GameObject nBala = Instantiate(prefab, exit.position, exit.rotation);
        nBala.transform.parent = conteiner.transform;
        nBala.GetComponent<Rigidbody>().AddForce(exit.forward * forca * Time.fixedDeltaTime, ForceMode.Impulse);
        base.Spawn(nBala);
        StartCoroutine(ResetarTiro());
        IEnumerator ResetarTiro()
        {
            yield return new WaitForSeconds(timerShoot);
            Target_Recall_Tiro(conn);
        }
    }

    [TargetRpc]
    void Target_Recall_Tiro(NetworkConnection conn)
    {
       shoot = !shoot;
    }

    IEnumerator Ataque()
    {
        if (IsOwner)
        {
            esc = !esc;
            escudo.SetActive(esc);
            yield return new WaitForSeconds(2);
            esc = !esc;
            escudo.SetActive(esc);
        }
    }


    [TargetRpc]
    public void Hit(NetworkConnection conn, int dano)
    {
        if (!base.IsOwner)
        {
            return;
        }
        if (esc)
        {
            return;
        }
        vida -= dano;
        transform.GetComponent<HUD_Player>().Barra(dano);
        if (vida <= 0){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            dead.SetActive(true);
            nc.OnClick_Client();
        }
    }

    private void OnCollisionExit(Collision coli)
    {
        if (!base.IsOwner)
        {
            return;
        }
        if (coli.gameObject.layer != 4 && coli.gameObject.layer != 3)
        {
            velocidade = speedBase;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!base.IsOwner)
        {
            return;
        }
        if (other.gameObject.tag == "Coletavel")
        {
            GameManager.MudaPontos(1);
            Destroy(other.gameObject);
        }
    }

    [TargetRpc]
    public void Reset_Transform(NetworkConnection conn)
    {
        transform.position = new Vector3(0, 2, 0);
    }
}
