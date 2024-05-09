using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;


public class Usuario 
{
    public int id;
    public string nome;
    public int pontos;
    public string created_at;

}


public class Request_Manager : MonoBehaviour
{
    // buscar usuario ou criar senão houver um
    //atualizar pontos pelo usuario 
    // buscar todos os usuarios
    // primeiro parametro na web "?"; para os demais parametros é "&"
    public static int  pontuacao;
    static string requestUrl;
    static string response;

    static HUDcontroller hdCTRL;

    static string apiUrl = "https://rrilihkcbjhtognlixpk.supabase.co/rest/v1/Usuarios?";
    static string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJyaWxpaGtjYmpodG9nbmxpeHBrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTIzNDY5MjgsImV4cCI6MjAyNzkyMjkyOH0.uSUDkhBOkDpoAjfJpPUotAUZEIu3QmlowXg78qF1Db8";
    
    public static async Task<Usuario> BuscaUsuario(string nome)
    {


        // concatenar = somar variaveis
        //string resquestURL = apiUrl+"nome=eq."+nome+"&"+apiKey;

        //Interpolar
        requestUrl = $"{apiUrl}nome=eq.{nome}&apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        await request.SendWebRequest();

        response = request.downloadHandler.text;

        Debug.Log(response);

        if (response == "[]")
        {

            return null;
        }

        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
        
        return usuarios[0];

    }

    public static async Task<Usuario> CriaUsuario(string nome)
    {
        requestUrl = $"{apiUrl}apikey={apiKey}";

        string json = $"{{ \"nome\": \"{nome}\", \"pontos\": {GameManager.pontos} }}";

        UnityWebRequest request = UnityWebRequest.Post(requestUrl, json, "application/json");
        await request.SendWebRequest();

        return await BuscaUsuario(nome);
    }

    public static async void AutoSave(int id)
    {
        string requestUrl = $"{apiUrl}id=eq.{id}&apikey={apiKey}";

        string json = $"{{\"pontos\": {GameManager.pontos} }}";

        UnityWebRequest request = UnityWebRequest.Put(requestUrl, json);

        request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");
        
        await request.SendWebRequest();
    }

    public static async Task<List<Usuario>> BuscaRanking() 
    {
        requestUrl = $"{apiUrl}apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        await request.SendWebRequest();

        response = request.downloadHandler.text;

        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
        
        return usuarios;
    }

//get post put delete
}
