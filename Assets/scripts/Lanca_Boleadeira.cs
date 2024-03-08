using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lanca_Boleadeira : MonoBehaviour
{
    public GameObject boleadeira;
    public GameObject boomerang;
    Camera cam;
    bool click = false;
    public int forca = 800;
    public GameObject saida;
	// Start is called before the first frame update
	void Start()
    {
       cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Cria_e_Gira();
            click = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cria_e_Gira2();
            click = true;
        }
    }

    void Cria_e_Gira() {
        if (click) { 
		    GameObject nova_bole = Instantiate(boleadeira, saida.transform.position, Quaternion.identity);
		    // Obter a direção olhando para o ponto em que a câmera está apontando
		    Vector3 direcao = cam.transform.forward;
		    // Rotacionar a bola para que ela aponte para a direção correta
		    Quaternion rotacao = Quaternion.LookRotation(direcao);
		    nova_bole.transform.rotation = rotacao * Quaternion.Euler(90, 0, 0);
            nova_bole.GetComponent<Rigidbody>().AddForce(direcao * forca * Time.deltaTime); // Ajuste a força conforme necessário
            click = false;
        }
	}

    void Cria_e_Gira2()
    {
        if (click)
        {
            GameObject nova_bole = Instantiate(boomerang, saida.transform.position, Quaternion.identity);
            // Obter a direção olhando para o ponto em que a câmera está apontando
            Vector3 direcao = cam.transform.forward;
            // Rotacionar a bola para que ela aponte para a direção correta
            Quaternion rotacao = Quaternion.LookRotation(direcao);
            nova_bole.transform.rotation = rotacao * Quaternion.Euler(90, 0, 0);
            nova_bole.GetComponent<Rigidbody>().AddForce(direcao * forca * Time.deltaTime); // Ajuste a força conforme necessário
            click = false;
        }
    }
}
