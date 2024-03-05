using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int vidaTotal;
    public int vidaAtual;

    RectTransform barraVida;
    RectTransform barraTotal;
    Transform BarraCanvas;



    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaTotal;
        barraVida = transform.Find("BarraVida").transform.Find("vidaAtual").GetComponent<RectTransform>();
        barraTotal = transform.Find("BarraVida").transform.Find("vidaTotal").GetComponent<RectTransform>();
        BarraCanvas = transform.Find("BarraVida");
    }

    // Update is called once per frame
    void Update()
    {
        BarraCanvas.LookAt(Camera.main.transform);
    }

    public void Golpe(int dano)
    {
        vidaAtual = vidaAtual - dano;
        AtualizarHud();
    }
    public void AtualizarHud(){
        float reducao = vidaAtual / vidaTotal;
        reducao = reducao * barraTotal.sizeDelta.x;

        barraVida.sizeDelta = new Vector2(reducao, barraVida.sizeDelta.y);
        float position = (barraTotal.sizeDelta.x - barraVida.sizeDelta.x) / 2;
        barraVida.anchoredPosition = new Vector2(position, barraVida.anchoredPosition.y) ;
    }
}
