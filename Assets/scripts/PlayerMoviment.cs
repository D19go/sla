using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static CharacterController cc;//Variável para definição do CharacterController para cc
    public static float speed = 49;//Variável flutuável para definição da velocidade
    public static float gravidade = 10;//Variável flutuável para definição da gravidade

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Variáveis criadas para definição efetiva do movimento tendo em vista os controles usados aplicados a velocidade ou gravidade
        //multiplicado pelo tempo local
        float direcaoX = InputController.inputHorizontal * speed * Time.deltaTime;
        float direcaoZ = InputController.inputVertical * speed * Time.deltaTime;
        float direcaoY = -gravidade * Time.deltaTime;

        //Condição para verificação do Estado do pulo se Pulando,
        //caso verificada condição será aplicada força na direçao Y através da função SmoothStep(Suavidade de movimento)
        //que tem como atributos o valor mínimo, máximo, pelo tempo indicado
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
        {
                direcaoY = (Mathf.SmoothStep(gravidade, gravidade * 1.1f, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo));
                direcaoY = direcaoY * Time.deltaTime;
            
        }

        //Condição para verificação do Estado do pulo se Caindo,
        //caso verificada condição será aplicada força na direçao Y através da função REVERSA ACIMA SmoothStep(Suavidade de movimento)
        //que tem como atributos o valor mínimo, máximo, pelo tempo indicado
        if (PlayerJump.estadoPulo == EstadoPulo.Caindo)
        {
            direcaoY = (Mathf.SmoothStep(-gravidade * 0.9f, -gravidade * 1.5f, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo));
            direcaoY = direcaoY * Time.deltaTime;
        }

        //Rotação do personagem
        Vector3 frente = Camera.main.transform.forward;
        Vector3 direita = Camera.main.transform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        frente *= direcaoZ;
        direita *= direcaoX;
        //manter a posição olhando para a última posição
        if (direcaoX != 0 && direcaoZ != 0)
        {
            float angulo = Mathf.Atan2(frente.x + direita.x, frente.z + direita.z) * Mathf.Rad2Deg;
            Quaternion rotacao = Quaternion.Euler(0, angulo, 0);
            //transform.rotation = rotacao;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, 0.45f);
        }

        Vector3 direcao_vertical = Vector3.up * direcaoY;
        Vector3 direcao_horizontal = frente + direita;


        //Linha para definição do movimento de acordo com variáveis acionadas através dos controles utilizados
        Vector3 movimento = direcao_vertical + direcao_horizontal;
        //Por fim, a aplicação do movimento definido acima
        cc.Move(movimento * speed * Time.deltaTime);
    }
}