using FishNet.Example;
using FishNet.Transporting;
using FishNet.Transporting.Tugboat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Match : MonoBehaviour
{
    public GameObject gm;
    public Tugboat tugboat;
    public TMP_InputField input_IP;
    public NetworkHudCanvases nc;
    public GameObject painel, cam, duracao, nJogadores, caixinha, nPlayers, timerOpc;
    int numJogadores = 0;
    int timer = 0;
    bool hConfig = false;
    bool click = false;

    public void box()
    {
        click = !click;
        if (click)
        {
            caixinha.GetComponentInChildren<TextMeshProUGUI>().text = "X";
            nJogadores.SetActive(true);
            nJogadores.GetComponent<TextMeshProUGUI>().color = Color.black;
            duracao.SetActive(true);
            duracao.GetComponent<TextMeshProUGUI>().color = Color.black;
            nPlayers.SetActive(true);
            timerOpc.SetActive(true);
            hConfig = click;

        }
        else
        {
            caixinha.GetComponentInChildren<TextMeshProUGUI>().text = "";
            nJogadores.SetActive(true);
            nJogadores.GetComponent<TextMeshProUGUI>().color = Color.grey;
            duracao.SetActive(true);
            duracao.GetComponent<TextMeshProUGUI>().color = Color.grey;
            nPlayers.SetActive(false);
            timerOpc.SetActive(false);
            hConfig = click;

        }
    }

    public void ConfigNumJogadores(int func)
    {
        if (func == 1)
        {
            numJogadores = func;
        }
        if (func == 2)
        {
            numJogadores = func;
        }
        if (func == 3)
        {
            numJogadores = func;
        }
        if (func == 4)
        {
            numJogadores = func;
        }
        Debug.Log(func);

    }
    public void ConfigTimer(int func)
    {
        if (func == 1)
        {
            timer = func;
        }
        if (func == 2)
        {
            timer = func;
        }

    }

    public void CriarHost()
    {
        if (hConfig)
        {
            tugboat.SetServerBindAddress(input_IP.text, (IPAddressType)0);
            tugboat.SetClientAddress(input_IP.text);
            nc.OnClick_Server();
            nc.OnClick_Client();
            painel.SetActive(false);
            StartCoroutine(wait());
        }
        else
        {
            Debug.Log("Você não configurou o host");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        gm.GetComponent<GameManager>().HostConfig(timer, numJogadores);
    }

    public void IP()
    {
        tugboat.SetClientAddress(input_IP.text);
        nc.OnClick_Client();
        painel.SetActive(false);
    }
}
