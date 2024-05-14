using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Player : MonoBehaviour
{
    GameObject player;
    private float VidaAtual = 100;
    private float vidaTotal = 100;
    public Image lifebar;

    bool slots1_2 = false;
    bool slots3_4 = false;
    public Image slot1;
    public Image slot2;

    public Image slot3;
    public Image slot4;

    public static bool golpeando = true;

    public static HUD_Player instancia;

    void Awake()
    {
        slots1_2 = false;
        slots3_4 = false;
        if (transform.tag == "Player")
        {
            vidaTotal = GetComponent<PlayerController>().vida;
        }

        VidaAtual = vidaTotal;
    }

    public void Habilidade(int slot)
    {
        if (slot == 1)
        {
            //slots1_2 comeca falso
            slots1_2 = !slots1_2;
            if (!slots1_2)
            {
                slot1.enabled = true;
                slot2.enabled = false;
            }
            else
            {
                slot1.enabled = false;
                slot2.enabled = true;
            }
        }else if (slot == 2)
        {
            slots3_4 = !slots3_4;
            if (!slots3_4)
            {
                slot3.enabled = true;
                slot4.enabled = false;
            }
            else
            {
                slot3.enabled = false;
                slot4.enabled = true;
            }
        }

    }

    public void Barra(int dano)
    {
        
        VidaAtual -= dano;
        float fillAmount = VidaAtual / vidaTotal;
        lifebar.fillAmount = fillAmount;
    }
}
