using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano_Todos : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            other.gameObject.GetComponent<PlayerCombo>().Golpe(2);
        }

        if (other.GetComponent<LifeEnemy>() != null)
        {
            other.gameObject.GetComponent<LifeEnemy>().life(5);
        }
    }
}
