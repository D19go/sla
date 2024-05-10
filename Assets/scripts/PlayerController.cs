using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class PlayerController : MonoBehaviour
{
    public static bool timerOVER = false;  
    float velocidade;
    public GameObject escudo;
    bool esc = false;
    public float speedBase;
    [SerializeField] private int rotacaoTanque;
    [SerializeField] private GameObject torreta;
    [SerializeField] private GameObject canoTorreta;

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
    public float speedRotation = 5f;

    //-------------------------------------

    public int vida = 200;
    bool b1 = false;
    bool e2 = false;
    bool cursor = false;
    Rigidbody rb;

    Vector3 rotationLocal = Vector3.zero;
    


    // Start is called before the first frame update
    void Start()
    {
        velocidade = speedBase;
        rb = GetComponent<Rigidbody>();
        conteiner = GameObject.Find("Disparo_Conteiner").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 mousePosition = Input.mousePosition;

        canoTorreta.transform.Rotate(Input.GetAxis("Mouse Y") * speedRotation, 0, 0);
        torreta.transform.Rotate(0,0,Input.GetAxis("Mouse X"));
        Vector3 direcaoFrente = transform.forward; // Obtenha a direção para a frente com base na orientação atual do objeto

        

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + direcaoFrente * velocidade * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + -direcaoFrente * velocidade * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, transform.rotation.y + -rotacaoTanque * Time.fixedDeltaTime,0 );
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, transform.rotation.y + rotacaoTanque * Time.fixedDeltaTime, 0);
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

    private void OnCollisionStay(Collision coli)
    {
    
        if (coli.gameObject.layer != 4 && Vector3.Dot(transform.forward, coli.contacts[0].normal) < 0 )
        {
            velocidade = 1;
        }
    }

    private void OnCollisionExit(Collision coli)
    {
        if (coli.gameObject.layer != 4)
        {
            velocidade = speedBase;
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
        nBala.GetComponent<Rigidbody>().AddForce(exit.forward * forca * Time.fixedDeltaTime, ForceMode.Impulse);
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
