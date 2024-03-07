using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EstadoPulo
{
    Pulando,
    Caindo,
    Solo
}

public class PlayerJump : MonoBehaviour
{

    public static EstadoPulo estadoPulo;

    public static float tempoPulo = 0.5f;
    public static float tempoDecorridoPulo = 0;

    float raycastCabeca = 2.8f;

    CharacterController cc;

    int quantidadePulos = 0;

    // Start is called before the first frame update
    void Start()
    {
        estadoPulo = EstadoPulo.Solo;
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        if( InputController.inputPulo)
        {
            quantidadePulos++;
            if (quantidadePulos == 2)
            {
                tempoDecorridoPulo = 0;
                estadoPulo = EstadoPulo.Pulando;
            }
        }

        if (InputController.inputPulo && estadoPulo == EstadoPulo.Solo)
        {
            estadoPulo = EstadoPulo.Pulando;
        }

        if (estadoPulo == EstadoPulo.Pulando)
        {

            tempoDecorridoPulo += Time.deltaTime;

            if (tempoDecorridoPulo >= tempoPulo)
            {
                estadoPulo = EstadoPulo.Caindo;
                tempoDecorridoPulo = 0;
            }

        }

        if (estadoPulo == EstadoPulo.Caindo)
        {
            tempoDecorridoPulo += PlayerMovement.gravidade * Time.deltaTime;
        }

        if (cc.isGrounded && estadoPulo == EstadoPulo.Caindo)
        {
            tempoDecorridoPulo = 0;
            estadoPulo = EstadoPulo.Solo;
            quantidadePulos = 0;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (estadoPulo == EstadoPulo.Pulando && Physics.Raycast(transform.position, Vector3.up, raycastCabeca))
        {
            estadoPulo = EstadoPulo.Caindo;
            tempoDecorridoPulo = 0;
        }

    }

}
