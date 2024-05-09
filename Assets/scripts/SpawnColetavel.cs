using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class SpawnColetavel : NetworkBehaviour
{
    public float timeSpawn = 0.5f;
    public GameObject coletavel1;
    public GameObject coletavel2;
    public Transform Area;

    public bool ok;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Gerador", 3, timeSpawn);
    }

    // Update is called once per frame
    void Gerador()
    {
        if (!base.IsServerInitialized)
        {
            return;
        }
        if (ok)
        {
            float AreaX = Area.localScale.x / 2;
            Debug.Log(AreaX);
            float AreaZ = Area.localScale.z / 2;
            Debug.Log(AreaZ);
            float AleAreX = Random.Range(-AreaX, AreaX);
            float AleAreZ = Random.Range(-AreaZ, AreaZ);

            Vector3 localSpawn = new Vector3(AleAreX,0.5f,AleAreZ);

            int num = Random.Range(1, 3);
            if (num == 1)
            {
                GameObject novo = Instantiate(coletavel1, transform);
                novo.transform.position = localSpawn;

            }
            else
            {
                GameObject novo = Instantiate(coletavel2,transform);
                novo.transform.position = localSpawn;
            }
        }
    }
}
