using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LifeEnemy : MonoBehaviour
{

    [SerializeField] private int vida = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(vida);
    }

    public void life(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
