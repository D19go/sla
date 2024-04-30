using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Request_Manager rm;
    public static Usuario usuario;
    GameObject spawner;
    static TextMeshProUGUI points;
    public static int pontos;

    // Start is called before the first frame update
    void Start()
    {

        spawner = GameObject.Find("SpawnnerItens");
        spawner.GetComponent<SpawnColetavel>().ok = true;

        points = GameObject.Find("pontos_Num").GetComponent<TextMeshProUGUI>();
        pontos = usuario.pontos;
        points.text = pontos.ToString();
        InvokeRepeating("LoadSaves()",5,1);

    }

    // Update is called once per frame
    public static void MudaPontos(int i)
    {
        pontos += i;

        points.text = pontos.ToString();
    }

    void LoadSaves()
    {
        Request_Manager.AutoSave();
    }
    
}
