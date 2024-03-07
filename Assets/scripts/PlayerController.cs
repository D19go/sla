using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float velocidade = 10f;
    public static float gravidade = 9.87f;

    public static CharacterController cc;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        float direcao_x = InputController.inputHorizontal * velocidade * Time.deltaTime;
        float direcao_z = InputController.inputVertical * velocidade * Time.deltaTime;
        float direcao_y = -gravidade * Time.deltaTime;

        // Rotação do personagem
        Vector3 frente = Camera.main.transform.forward;
        Vector3 direita = Camera.main.transform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        frente = frente * direcao_z;
        direita = direita * direcao_x;

        if( direcao_x != 0 || direcao_z != 0)
        {
            float angulo = Mathf.Atan2( frente.x + direita.x, frente.z + direita.z ) * Mathf.Rad2Deg;
            Quaternion rotacao = Quaternion.Euler(0, angulo, 0);
            //transform.rotation = rotacao;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, 0.15f);
        }

        Vector3 direcao_vertical = Vector3.up * direcao_y;
        Vector3 direcao_horizontal = frente + direita;

        Vector3 movimento = direcao_vertical + direcao_horizontal;
        cc.Move(movimento);
        

    }
}
