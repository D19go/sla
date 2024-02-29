using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    //cria e torna as variaveis publicas para serem usadas em todo o projeto, e já passa alguns valores para essas mesmas variaveis
    public static float inputHorizontal = 0;
    public static float inputVertical = 0;
    public static bool inputPulo = false;
    public static bool inputPrincipal = false;


    // Update is called once per frame
    void Update()
    {
        //verifica as variaveis e muda seus valores de forma gradual e "natural"
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputPulo = Input.GetKeyDown(KeyCode.Space);
        inputPrincipal = Input.GetKeyDown(KeyCode.Mouse0); 
        
    }
}