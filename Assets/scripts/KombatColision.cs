using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KombatColision : MonoBehaviour
{
    int dano = 5;
    private void OnTriggerEnter(Collider Golpe)
    {
        if (Golpe.gameObject.tag == "Inimigo")
        {
            Golpe.gameObject.GetComponent<LifeEnemy>().life(dano);
        }
    }

}
