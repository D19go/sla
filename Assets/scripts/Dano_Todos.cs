using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano_Todos : MonoBehaviour
{
    public bool jogador = false;
    // Start is called before the first frame update
    void Start()
    {
        verifica();
    }

    void verifica()
    {
        if (transform.tag == "Player")
        {
            jogador = true;
            Debug.Log("jogador" + jogador);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterStatus>() != null && jogador == false)
        {
            //DANO NO JOGADOR
            other.gameObject.GetComponent<CharacterStatus>().Golpe(2);
        }

        if (other.GetComponent<CharacterStatus>() != null && jogador == false)
        {
            //DANO NO INIMIGO
            other.gameObject.GetComponent<CharacterStatus>().Golpe(5);
        }
    }
}
