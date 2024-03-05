using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerJump;

//pesquisei e n�o entendi muuuito bem, mas pelo que vi �
//uma forma de declarar valores de forma permanente e ao mesmo tempo
//fazer com que todos os outros scripts possam ler e alterar os valores  
public enum EstadoPulo
{
    //s�o os 4 estados que o jogador tem at� agora
    Pulando,
    Subindo,
    Caindo,
    Solo
}

public class PlayerJump : MonoBehaviour
{
    // crio uma variavel do tipo estado, e deixo ela vazia
    public static EstadoPulo estadoPulo;
    public static int NumPulo = 0;

    //crio mais uma variavel de aceso geral e passo o valor de 2, que � o tempo padr�o para o pulo
    public static float tempoPulo = 2f;
    //crio outra variavel para contar o tempo que passou desde que come�ou o pulo
    public static float tempoDecorridoPulo = 0;

    //fa�o uma variavel para determinar a distancia maxima do raycast 
    public static float raycastCabeca = 2.8f;

    // crio mais uma variavel especifica 
    public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        // passo os valores corretos para as variaveis do tipo especifico
        cc = GetComponent<CharacterController>();
        estadoPulo = EstadoPulo.Solo;
    }

    // Update is called once per frame
    void Update()
    {
        //verifico se houve um click no input e se o estado � igual a solo(est� no ch�o ou em alguma superficie)
        if (InputController.inputPulo)
        {
            NumPulo++;
            if (NumPulo < 2)
            {
                Debug.Log(NumPulo + " if espa�o");
                //mudo o estado da variavel para pulando
                estadoPulo = EstadoPulo.Pulando;        
            }
        }

        //verifico se a variavel de outro script est� igual ao estado pulando
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
        {

            //inicio a contagem do tempo do pulo
            tempoDecorridoPulo += Time.deltaTime;

            if (NumPulo == 2)
            {
                Debug.Log(NumPulo + " if tempo pulo");
                tempoDecorridoPulo = 0;
                //mais uma verifica��o para ver se o tempo de pulo j� chegou no tempo limite
                if (tempoDecorridoPulo >= tempoPulo)
                {
                    //mudo novamente o valor da variavel estadopulo para que o pulo acabe
                    PlayerJump.estadoPulo = EstadoPulo.Caindo;
                    //zero o tempo do pulo para evitar um bug de pulo infinito
                    tempoDecorridoPulo = 0;
                    NumPulo = 3 ;
                }
            }

            //mais uma verifica��o para ver se o tempo de pulo j� chegou no tempo limite
            if (tempoDecorridoPulo >= tempoPulo && NumPulo != 2)
            {
                //mudo novamente o valor da variavel estadopulo para que o pulo acabe
                PlayerJump.estadoPulo = EstadoPulo.Caindo;
                //zero o tempo do pulo para evitar um bug de pulo infinito
                tempoDecorridoPulo = 0;
                NumPulo = 0;
            }
            

        }

        //fao uma verifica��o para ver se o personagem est� caindo
        if (estadoPulo == EstadoPulo.Caindo)
        {
            //come�o a adicionar for�a para baixo, assim o jogador come�a a cair de vagar e vai ficando mais rapido
            tempoDecorridoPulo += PlayerMovement.gravidade * Time.deltaTime;
        }

        //verifico se o jogador esta em alguma superficie e se o estado dele � igual a caindo
        if (cc.isGrounded && estadoPulo == EstadoPulo.Caindo)
        {
            //zero o tempo de pulo novamente
            tempoDecorridoPulo = 0;
            //mudo o estado para solo, dizendo que o jogador j� est� no ch�o
            estadoPulo = EstadoPulo.Solo;
        }
    }

    //void para verificar se o jogador bateu em algo acima da cabe�a
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //com o ray cast direciona, verifico se o jogador est� pulando e se o ray cast est� retornando algo
        if (estadoPulo == EstadoPulo.Pulando && Physics.Raycast(transform.position, Vector3.up, raycastCabeca))
        {

            //assim que algo for detectado mudo o estado do jogador para que ele comece a cair
            estadoPulo = EstadoPulo.Caindo;
            //zero mais uma vez o tempo de pulo
            tempoDecorridoPulo = 0;
        }

    }
}