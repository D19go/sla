using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class PlayerController : NetworkBehaviour
{
    public static bool timerOVER = false;  
    public float velocidade;
    public GameObject escudo;
    bool esc = false;

    //-------------------------------------

    public int force;
    public int especialForce;

    public Transform conteiner;
    public Transform exit;
    public GameObject bullet;
    public GameObject bullet2;

    public GameObject especial;
    public GameObject especial2;

    //-------------------------------------

    float rotationX = 10;
    public float speedRotetion = 5f;

    //-------------------------------------

    public int vida = 200;
    bool b1 = false;
    bool e2 = false;
    bool cursor = false;

    Vector3 rotationLocal = Vector3.zero;
    


    // Start is called before the first frame update
    void Start()
    {
        conteiner = GameObject.Find("Disparo_Conteiner").GetComponent<Transform>();
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
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 movi = new Vector3 (hori,0, vert);
        movi = movi * velocidade * Time.deltaTime;

        Vector3 mousePosition = Input.mousePosition;

        rotationX -= Input.GetAxis("Mouse Y") * speedRotetion;
        transform.rotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X") * speedRotetion, 0);
        transform.Translate(movi);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidade = 30;
        }
        else
        {
            velocidade = 15;
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
                Atirar(bullet, force);
            }
            else
            {
                Atirar(bullet2, force);
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!e2)
            {
                Atirar(especial, especialForce);

            }else
            {
                Atirar(especial2, especialForce);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coletavel")
        {
            GameManager.MudaPontos(1);
            Destroy(other.gameObject);
        }
    }

    void Atirar(GameObject prefab, int forca)
    {
        GameObject nBala = Instantiate(prefab, exit.position, Quaternion.identity);
        nBala.transform.parent = conteiner;
        nBala.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * forca * Time.deltaTime, ForceMode.Impulse);
    }

    IEnumerator Ataque()
    {
        esc = !esc;
        escudo.SetActive(esc);
        yield return new WaitForSeconds(2);
        esc = !esc;
        escudo.SetActive(esc);
    }

    public void Hit(int dano)
    {
        if (esc)
        {
            return;
        }
        vida -= dano;
        gameObject.GetComponent<HUD_Player>().Barra(dano);
        if (vida <= 0){
            Destroy(gameObject);
        }
    }

}
