using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour
{
    public TextMeshProUGUI Ranking;
    public static List<Usuario> usuarios = new List<Usuario>();
    int top_1;
    int top_2;
    int top_3;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Request_Manager.BuscaRanking();
            Debug.Log("rb");
        }
    }

    public async void BotaoEntrar()
    {

        if (yes)
        {
            yes = false;
            Usuario usuario = await Request_Manager.BuscaUsuario(inputNome.text);
            if (usuario != null)
            {
                GameManager.usuario_ = usuario;
                SceneManager.LoadScene("SEILAA");
            }
            else
            {
                if(inputNome.text != null)
                {
                    usuario = await Request_Manager.CriaUsuario(inputNome.text);
                    yes = true;
                    BotaoEntrar();
                }
            }
        }
        
        
    }

    public void  RanKING()
    {
        Debug.Log("ch");
        for (int i = 0; i<usuarios.Count; i++)
        {

            if (usuarios[i].pontos > top_3)
            {
                if (usuarios[i].pontos > top_2)
                {
                    if (usuarios[i].id > top_1)
                    {
                        top_1 = usuarios[i].id;
                        Debug.Log(top_1);
                    }
                    else
                    {
                        top_2 = usuarios[i].id;
                        Debug.Log(top_2);
                    }
                }
                else
                {
                    top_3 = usuarios[i].id;
                    Debug.Log(top_3);
                }
            }
        }

    }
}
