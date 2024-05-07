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
    public static List<Usuario> _usuario = new List<Usuario>();
    static int top_1PONTOS = 12;
    static int top_2PONTOS = 11;
    static int top_3PONTOS = 10;

    static int top1_idStatic;
    static int top2_idStatic;
    static int top3_idStatic;

    int top_1ID;
    int top_2ID;
    int top_3ID;

    static bool ok = false;
   
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Request_Manager.BuscaRanking();
        }
        
        if (ok)
        {
            ok = false;
            top_1ID = top1_idStatic;
            top_2ID = top2_idStatic;
            top_3ID = top3_idStatic;
            MostranaTela();
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

    public static void  RanKING()
    {
        Debug.Log(_usuario.Count);
        for (int o=0; o<4; o++)
        {
            for (int i = 0; i<_usuario.Count; i++)
            {
                if (_usuario[i].pontos >= top_1PONTOS)
                {
                    top_1PONTOS = _usuario[i].pontos;
                    top1_idStatic = _usuario[i].id;
                }
                else
                {
                    if (_usuario[i].pontos >= top_2PONTOS)
                    {
                        top_2PONTOS = _usuario[i].pontos;
                        top2_idStatic = _usuario[i].id;
                    }
                    else
                    {
                        if (_usuario[i].pontos >= top_3PONTOS)
                        {
                            top_3PONTOS = _usuario[i].pontos;
                            top3_idStatic = _usuario[i].id;
                        }
                    }
                }
            }
        }
        ok = true;
    }

    void MostranaTela()
    {
        listTops[0].text = $"{_usuario[top_1ID].nome}: {top_1PONTOS}";
        listTops[1].text = $"{_usuario[top_2ID].nome}: {top_2PONTOS}";
        listTops[2].text = $"{_usuario[top_3ID].nome}: {top_3PONTOS}";
    }
}
