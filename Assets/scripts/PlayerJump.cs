using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerJump;

//pesquisei e não entendi muuuito bem, mas pelo que vi é
//uma forma de declarar valores de forma permanente e ao mesmo tempo
//fazer com que todos os outros scripts possam ler e alterar os valores  
public enum EstadoPulo
{
    //são os 4 estados que o jogador tem até agora
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

    //crio mais uma variavel de aceso geral e passo o valor de 2, que é o tempo padrão para o pulo
    public static float tempoPulo = 2f;
    //crio outra variavel para contar o tempo que passou desde que começou o pulo
    public static float tempoDecorridoPulo = 0;

    //faço uma variavel para determinar a distancia maxima do raycast 
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
        //verifico se houve um click no input e se o estado é igual a solo(está no chão ou em alguma superficie)
        if (InputController.inputPulo)
        {
            NumPulo++;
            if (NumPulo < 2)
            {
                Debug.Log(NumPulo + " if espaço");
                //mudo o estado da variavel para pulando
                estadoPulo = EstadoPulo.Pulando;        
            }
        }

        //verifico se a variavel de outro script está igual ao estado pulando
        if (PlayerJump.estadoPulo == EstadoPulo.Pulando)
        {

            //inicio a contagem do tempo do pulo
            tempoDecorridoPulo += Time.deltaTime;

            if (NumPulo == 2)
            {
                Debug.Log(NumPulo + " if tempo pulo");
                tempoDecorridoPulo = 0;
                //mais uma verificação para ver se o tempo de pulo já chegou no tempo limite
                if (tempoDecorridoPulo >= tempoPulo)
                {
                    //mudo novamente o valor da variavel estadopulo para que o pulo acabe
                    PlayerJump.estadoPulo = EstadoPulo.Caindo;
                    //zero o tempo do pulo para evitar um bug de pulo infinito
                    tempoDecorridoPulo = 0;
                    NumPulo = 3 ;
                }
            }

            //mais uma verificação para ver se o tempo de pulo já chegou no tempo limite
            if (tempoDecorridoPulo >= tempoPulo && NumPulo != 2)
            {
                //mudo novamente o valor da variavel estadopulo para que o pulo acabe
                PlayerJump.estadoPulo = EstadoPulo.Caindo;
                //zero o tempo do pulo para evitar um bug de pulo infinito
                tempoDecorridoPulo = 0;
                NumPulo = 0;
            }
            

        }

        //fao uma verificação para ver se o personagem está caindo
        if (estadoPulo == EstadoPulo.Caindo)
        {
            //começo a adicionar força para baixo, assim o jogador começa a cair de vagar e vai ficando mais rapido
            tempoDecorridoPulo += PlayerMovement.gravidade * Time.deltaTime;
        }

        //verifico se o jogador esta em alguma superficie e se o estado dele é igual a caindo
        if (cc.isGrounded && estadoPulo == EstadoPulo.Caindo)
        {
            //zero o tempo de pulo novamente
            tempoDecorridoPulo = 0;
            //mudo o estado para solo, dizendo que o jogador já está no chão
            estadoPulo = EstadoPulo.Solo;
        }
    }

    //void para verificar se o jogador bateu em algo acima da cabeça
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //com o ray cast direciona, verifico se o jogador está pulando e se o ray cast está retornando algo
        if (estadoPulo == EstadoPulo.Pulando && Physics.Raycast(transform.position, Vector3.up, raycastCabeca))
        {

            //assim que algo for detectado mudo o estado do jogador para que ele comece a cair
            estadoPulo = EstadoPulo.Caindo;
            //zero mais uma vez o tempo de pulo
            tempoDecorridoPulo = 0;
        }

    }
}