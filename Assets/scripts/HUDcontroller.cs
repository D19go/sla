using FishNet.Example;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour
{
    public List<TextMeshProUGUI> listTops = new List<TextMeshProUGUI>();
    List<Usuario> listaUsuarios;
    int top_1PONTOS = 12;
    int top_2PONTOS = 11;
    int top_3PONTOS = 10;

    int id_TOP1;
    int id_TOP2;
    int id_TOP3;
   
    GameObject enter;

   
    public TMP_InputField inputNome;
    bool yes = true;
    
    private void Awake()
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

    

    public async void MostranaTela()
    {
        List<Usuario> usuarios = await Request_Manager.BuscaRanking();
        for (int o = 0; o < 3; o++)
        {

            for (int i = 0; i<usuarios.Count; i++)
            {
                if (usuarios[i].pontos >= top_1PONTOS)
                {
                    top_1PONTOS = usuarios[i].pontos;
                    id_TOP1 = i;
                }
                else
                {
                    if (usuarios[i].pontos >= top_2PONTOS)
                    {
                        top_2PONTOS = usuarios[i].pontos;
                        id_TOP2 = i;
                    }
                    else
                    {
                        if (usuarios[i].pontos >= top_3PONTOS)
                        {
                            top_3PONTOS = usuarios[i].pontos;
                            id_TOP3 = i;
                        }
                    }
                }
            }
        }

        listTops[0].text = $"{usuarios[id_TOP1].nome}: {usuarios[id_TOP1].pontos}";
        listTops[1].text = $"{usuarios[id_TOP2].nome}: {usuarios[id_TOP2].pontos}";
        listTops[2].text = $"{usuarios[id_TOP3].nome}: {usuarios[id_TOP3].pontos}";


    }

    

}
