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

    // Update is called once per frame
    public void IP()
    {
        tugboat.SetClientAddress(input_IP.text);
        nc.OnClick_Client();
        painel.SetActive(false);
    }
}
