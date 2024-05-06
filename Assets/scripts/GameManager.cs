using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Request_Manager rm;
    public static Usuario usuario_;
    GameObject spawner;
    static TextMeshProUGUI points;
    TextMeshProUGUI uName;
    string nomeUsuario;
    public static int pontos;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnnerItens");
        if (usuario_.nome != null)
        {
            spawner.GetComponent<SpawnColetavel>().ok = true;
        }

        uName = GameObject.Find("Canvas").transform.Find("nome").GetComponent<TextMeshProUGUI>();
        points = GameObject.Find("pontos_Num").GetComponent<TextMeshProUGUI>();
        pontos = usuario_.pontos;
        points.text = pontos.ToString();
        nomeUsuario = usuario_.nome;
        uName.text = nomeUsuario;

        Debug.Log(usuario_.nome);
        Debug.Log(usuario_.pontos);
    }

    // Update is called once per frame
    public static void MudaPontos(int i)
    {
        pontos += i;
        points.text = pontos.ToString();
        Request_Manager.pontuacao = pontos;
        Request_Manager.AutoSave(usuario_.id);
    }

    
    
}
