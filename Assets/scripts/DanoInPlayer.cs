using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoInPlayer : MonoBehaviour
{
    int dano = 2;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerCombo>().Golpe(dano);
        }
    }
}
