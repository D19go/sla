using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoController : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (InputController.inputPulo)
        {
            ani.SetBool("JUMP", true);
            ani.SetBool("JUMP", false);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            ani.SetBool("run", true);
        } else
        {
            ani.SetBool("run", false);
        }
    }
    
}
