using FishNet.Example;
using FishNet.Transporting.Tugboat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Match : MonoBehaviour
{
    public Tugboat tugboat;
    public TMP_InputField input_IP;
    public NetworkHudCanvases nc;
    public GameObject painel;
    public GameObject cam;

    public void Server_IP()
    {
        tugboat.SetServerBindAddress(input_IP.text, 0);
        tugboat.SetClientAddress(input_IP.text);
        nc.OnClick_Server();
        nc.OnClick_Client();
        painel.SetActive(false);
        cam.SetActive(false);
    }

    public void IP()
    {
        tugboat.SetClientAddress(input_IP.text);
        nc.OnClick_Client();
        painel.SetActive(false);
        cam.SetActive(false);
    }
}
