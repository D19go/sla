using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public int speed_Rotacao = 100;
    public int dano = 25;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed_Rotacao);
    }

    private void OnCollisionEnter(Collision coli)
    {
        if (coli.gameObject.tag == "Hunter")
        {
            coli.gameObject.GetComponent<Enemy_AI>().Dano_Life(dano);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
