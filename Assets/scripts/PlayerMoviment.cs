using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static CharacterController cc;//Vari�vel para defini��o do CharacterController para cc
    public static float speed = 49;//Vari�vel flutu�vel para defini��o da velocidade
    public static float gravidade = 10;//Vari�vel flutu�vel para defini��o da gravidade

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Vari�veis criadas para defini��o efetiva do movimento tendo em vista os controles usados aplicados a velocidade ou gravidade
        //multiplicado pelo tempo local
        float direcaoX = InputController.inputHorizontal * speed * Time.deltaTime;
        float direcaoZ = InputController.inputVertical * speed * Time.deltaTime;
        float direcaoY = -gravidade * Time.deltaTime;

        //Condi��o para verifica��o do Estado do pulo se Pulando,
        //caso verificada condi��o ser� aplicada for�a na dire�ao Y atrav�s da fun��o SmoothStep(Suavidade de movimento)
        //que tem como atributos o valor m�nimo, m�ximo, pelo tempo indicado
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
        {
            direcaoY = (Mathf.SmoothStep(gravidade, gravidade * 9.28f, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo));
            direcaoY = direcaoY * Time.deltaTime;
            
        }

        //Condi��o para verifica��o do Estado do pulo se Caindo,
        //caso verificada condi��o ser� aplicada for�a na dire�ao Y atrav�s da fun��o REVERSA ACIMA SmoothStep(Suavidade de movimento)
        //que tem como atributos o valor m�nimo, m�ximo, pelo tempo indicado
        if (PlayerJump.estadoPulo == EstadoPulo.Caindo)
        {
            direcaoY = (Mathf.SmoothStep(-gravidade * 0.9f, -gravidade * 1.5f, PlayerJump.tempoDecorridoPulo / PlayerJump.tempoPulo));
            direcaoY = direcaoY * Time.deltaTime;
        }

        //Rota��o do personagem
        Vector3 frente = Camera.main.transform.forward;
        Vector3 direita = Camera.main.transform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        frente *= direcaoZ;
        direita *= direcaoX;
        //manter a posi��o olhando para a �ltima posi��o
        if (direcaoX != 0 && direcaoZ != 0)
        {
            float angulo = Mathf.Atan2(frente.x + direita.x, frente.z + direita.z) * Mathf.Rad2Deg;
            Quaternion rotacao = Quaternion.Euler(0, angulo, 0);
            //transform.rotation = rotacao;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, 0.45f);
        }

        Vector3 direcao_vertical = Vector3.up * direcaoY;
        Vector3 direcao_horizontal = frente + direita;


        //Linha para defini��o do movimento de acordo com vari�veis acionadas atrav�s dos controles utilizados
        Vector3 movimento = direcao_vertical + direcao_horizontal;
        //Por fim, a aplica��o do movimento definido acima
        cc.Move(movimento * speed * Time.deltaTime);
    }
}