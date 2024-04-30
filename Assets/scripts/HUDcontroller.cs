using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour
{


    GameObject enter;

    public TMP_InputField inputNome;
    bool yes = true;
    private void Start()
    {
        enter = GameObject.Find("quadrado_Login").transform.Find("botao").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            BotaoEntrar();
        }
    }

    public async void BotaoEntrar()
    {

        enter.SetActive(false); 

        if (yes)
        {
            yes = false;
            Usuario usuario = await Request_Manager.BuscaUsuario(inputNome.text);
            if (usuario != null)
            {
                GameManager.usuario = usuario;
                SceneManager.LoadScene("SEILAA");
            }
            else
            {
                usuario = await Request_Manager.CriaUsuario(inputNome.text);
                yes = true;
                BotaoEntrar();
            }
        }
        
        
    }
}
