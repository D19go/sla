using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    bool CanATK = true;
    int aniClick = 0;
    public Animator ani;
    GameObject areaDano;
    // Start is called before the first frame update
    void Start()
    {
        areaDano = GameObject.Find("BoxDano");
        areaDano.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (InputController.inputPrincipal && CanATK)
        {
            RealizaGolpe();
        }
    }
    public  void RealizaGolpe()
    {
        aniClick++;

        CanATK = false;

           
        if (aniClick == 1)
        {
            ani.SetInteger("BOX", 1);
            areaDano.SetActive(true);
        }
        else if (aniClick == 2)
        {
            ani.SetInteger("BOX", 2);
            areaDano.SetActive(true);
        }
        else if (aniClick == 3)
        {
            ani.SetInteger("BOX", 3);
            areaDano.SetActive(true);
            aniClick = 0;
        }

        if (aniClick > 3)
        {
            aniClick = 0;
        }
    }

    void FimGolpe()
    {
        CanATK = true;
        areaDano.SetActive(false);
        ani.SetInteger("BOX", 0);
    }

    
}
