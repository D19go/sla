using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI cTimer;
    Request_Manager rm;
    public static Usuario usuario_;
    public GameObject hud;
    GameObject spawner;
    static TextMeshProUGUI points;
    TextMeshProUGUI uName;
    string nomeUsuario;
    public static int pontos;
    int seg = 30;
    int min = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerEND());
        spawner = GameObject.Find("SpawnnerItens");
        if (usuario_ != null)
        {
            spawner.GetComponent<SpawnColetavel>().ok = true;
        }

        uName = GameObject.Find("Canvas").transform.Find("nome").GetComponent<TextMeshProUGUI>();
        points = GameObject.Find("pontos_Num").GetComponent<TextMeshProUGUI>();
        pontos = usuario_.pontos;
        points.text = pontos.ToString();
        nomeUsuario = usuario_.nome;
        uName.text = nomeUsuario;
        hud.GetComponent<HUDcontroller>().MostranaTela();
    }

    // Update is called once per frame
    public static void MudaPontos(int i)
    {
        pontos += i;
        points.text = pontos.ToString();
        Request_Manager.pontuacao = pontos;
        Request_Manager.AutoSave(usuario_.id);
    }

    IEnumerator TimerEND()
    {
        cTimer.text = $"{min}:{seg}";
        seg--;
        yield return new WaitForSeconds(1f);

        if (seg <= 0 && min <= 0)
        {
            PlayerController.timerOVER = true;
            cTimer.GetComponentInParent<GameObject>().SetActive(false);
        }else if (seg <= 0)
        {
            min--;
            seg = 59;
        }
            StartCoroutine(TimerEND());
    }
    
}
