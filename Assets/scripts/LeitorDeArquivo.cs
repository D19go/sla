using System.IO;
using UnityEngine;

public class LeitorDeArquivo : MonoBehaviour
{
    public TextAsset txt;
    void Start()
    {
        LerArquivo("Saves.txt");
    }

    void LerArquivo(string nomeDoArquivo)
    {

        // Verifica se o arquivo existe antes de tentar lê-lo
        if (txt != null)
        {
            string[] linhas = txt.text.Split('\n');
            foreach (string linha in linhas)
            {
                Debug.Log(linha);
            }
        }
    }
}
