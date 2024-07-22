using FishNet.Connection;
using FishNet.Example;
using FishNet.Managing.Server;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public List<TextMeshProUGUI> cTimer = new List<TextMeshProUGUI>();
    public static Usuario usuario_;
    GameObject dead;
    NetworkHudCanvases nc;
    public GameObject hud;
    static TextMeshProUGUI points;
    public static int pontos;

    bool comeco = false;
    int JogadoresON = 0;
    int numJogadores = 2;

    int tempoMIN;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        base.OnStartServer();
        if (!base.IsServerInitialized)
        {
            return;
        }
        //StartCoroutine(TimerEND());
        
        dead = GameObject.Find("Canvas").transform.Find("dead").gameObject;
        if (dead == null)
        {
            Debug.Log("asdf");
        }
        nc = GameObject.Find("NetworkManager").transform.Find("NetworkHudCanvas").transform.GetComponent<NetworkHudCanvases>();
        
        /*uName = GameObject.Find("Canvas").transform.Find("nome").GetComponent<TextMeshProUGUI>();
        points = GameObject.Find("pontos_Num").GetComponent<TextMeshProUGUI>();
        pontos = usuario_.pontos;   
        points.text = pontos.ToString();
        nomeUsuario = usuario_.nome;
        uName.text = nomeUsuario;
        hud.GetComponent<HUDcontroller>().MostranaTela();*/
    }

    public void HostConfig(int timer, int player)
    {
        if (!base.IsHostInitialized)
        {
            return;
        }
        cTimer.Add(GameObject.Find("client(Clone)").transform.Find("Canvas").transform.Find("timer").gameObject.GetComponent<TextMeshProUGUI>());
        
        if (timer == 0)
        {
            tempoMIN = 5;
        }else if (timer == 1)
        {
            tempoMIN = 10;
        }
        else if (timer == 2)
        {
            tempoMIN = 15;
        }
        else
        {
            Debug.Log("Tempo deu erro");
        }
        

        if (player == 0)
        {
            numJogadores = 2;
        }else if (player == 1)
        {
            numJogadores = 4;
        }
        else if (player == 2)
        {
            numJogadores = 6;
        }
        else if (player == 3)
        {
            numJogadores = 8;
        }
        else if (player == 4)
        {
            numJogadores = 10;
        }
        else
        {
            Debug.Log("O numero de jogadores deu erro");
        }
        GetComponent<SyncTimer>().min.Value = tempoMIN;
        GetComponent<SyncTimer>().seg.Value = 0;
    }

    void Update()
    {
        if (!base.IsHostInitialized)
        {
            return;
        }
        if (JogadoresON == numJogadores)
        {
            if (!comeco)
            {
                comeco = true;
                
                GetComponent<SyncTimer>().Timer();

            }
        }
        JogadoresON = base.ServerManager.Clients.Count;
        Debug.Log(JogadoresON);
    }
   
    public static void MudaPontos(int i)
    {
        pontos += i;
        points.text = pontos.ToString();
        Request_Manager.pontuacao = pontos;
        Request_Manager.AutoSave(usuario_.id);
    }

    [ServerRpc]
    public void PlayerCount(NetworkConnection conn)
    {
        Debug.Log("ch");
        cTimer.Clear();
        for (int i = 0; i < base.ServerManager.Clients.Count; i++)
        {
            cTimer.Add(GameObject.Find("client(Clone)").transform.Find("Canvas").transform.Find("timer").gameObject.GetComponent<TextMeshProUGUI>());
            Debug.Log(cTimer.Count);
        }
    }

    public void AtualizaTimer(int min, int seg, bool timeOver)
    {
        if (timeOver)
        {
            for (int i = 0; i < base.ServerManager.Clients.Count; ++i)
            {
                cTimer[i].text = $"Acabou o Tempo";
            }
        }
        else
        {
            for (int i = 0; i < base.ServerManager.Clients.Count; ++i)
            {
                cTimer[i].text = $"{min:D2}:{seg:D2}";
            }
        }
    }

    public void Reviver()
    {
        dead.SetActive(false);
        nc.OnClick_Client();
    }

}
