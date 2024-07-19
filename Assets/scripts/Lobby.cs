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
        

        desc_game.Add("Escola e controle um tanque e desafie seus amigos");//0
        desc_game.Add("Um tipico jogo de tiro em primeira pessoa(fps), em um mapa grande e denso com vairas armas.");//1
        desc_game.Add("Uma arena grande para lhe dar liberdade de luta contra seus amigos sem medo usando diversos tipos de habilidades");//2
        desc_game.Add("Uma corrida PvP, jogador contra jogador e que vença o melhor o mais habilidoso");//3
        desc_game.Add("Uma corrida com naves ou aviões e qualquer coisa que possa voar");//4
        desc_game.Add("Uma corrida com um percurso gigante e sem fim");//5
        desc_game.Add("você duelando contra seu amigo, só os dois, sem poderzinho ou mapa extenso, a boa e velha porradaria sem freio");//6

        name_game.Add("Battle_Tanks");//0
        name_game.Add("FPS");//1
        name_game.Add("COMBATE MAGICO");//2
        name_game.Add("Fast");//3
        name_game.Add("Fast-Space");//4
        name_game.Add("Crazye Race");//5
        name_game.Add("1vs1");//6
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
