using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    GameObject t1;
    GameObject t2;
    public Image jogo_destaque;
    public TextMeshProUGUI desc_Painel;
    public TextMeshProUGUI nome_;

    //as tres listas abaixo devem estar em sincronia de itens e na mesma ordem
    List<string> name_game = new List<string>();
    public List<Sprite> img_game = new List<Sprite>();
    List<string> desc_game = new List<string>();

    //
    string Jogo_selecionado;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("1Tela").gameObject;
        t2 = transform.Find("2tela").gameObject;
        
        name_game.Add("Battle_Tanks");//0
        name_game.Add("FPS");//1
        name_game.Add("Combate M�gico");//2
        name_game.Add("Fast");//3
        name_game.Add("Fast-Space");//4
        name_game.Add("Crazye Race");//5
        name_game.Add("1vs1");//6

        desc_game.Add("Uma partida r�pida de tanques contra tanques, que ven�a aquele que pontuar mais, cada abate � um ponto.");//0
        desc_game.Add("Um tipico jogo de tiro em primeira pessoa(fps), em um mapa grande e denso com vairas armas.");//1
        desc_game.Add("Magias, super for�a e outras habilidades especiais, independente de qual voc� escolher apenas seja o melhor.");//2
        desc_game.Add("Uma corrida para at� 15 jogadores, um percurso simples e divertido, onde apenas os melhores ganham.");//3
        desc_game.Add("Voe para o al�m, mas sem sair do percurso da prova. Essa � uma corrida de avi�es, naves e qualquer outra coisa que saia do ch�o.");//4
        desc_game.Add("Carros, barcos, naves e ap�, uma corrida onde apenas um ve�culo n�o d� conta, � BEM MAIS LONGA QUE OUTRAS CORRIDAS!");//5
        desc_game.Add("Duelo direto com seu amigo, 1x1, sem d� nem piedade, essa � a mais pura porradaria.");//6

    }
    public void Game_Select(int listOBJ)
    {
        jogo_destaque.sprite = img_game[listOBJ];
        desc_Painel.text = desc_game[listOBJ];
        Jogo_selecionado = name_game[listOBJ];
        nome_.text = name_game[listOBJ];
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(Jogo_selecionado);
    }

    public void play()
    {
        t1.SetActive(false);
        t2.SetActive(true);
    }
}
