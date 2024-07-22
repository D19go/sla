using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class SpawnColetavel : NetworkBehaviour
{
    public float timeSpawn = 0.5f;
    public GameObject coletavel1;
    public GameObject coletavel2;
    public float AreaZ = 199;
    public float AreaX = 124;

    public bool ok;
    // Start is called before the first frame update
    override public void OnStartServer()
    {
        base.OnStartServer();
        // InvokeRepeating("Gerador", 3, timeSpawn);
        
    }

    [Server]
    void Gerador()
    {
        
        if (ok)
        {
            float AleAreX = Random.Range(-AreaX, AreaX);
            float AleAreZ = Random.Range(-AreaZ, AreaZ);

            Vector3 localSpawn = new Vector3(AleAreX,0.5f,AleAreZ);

            int num = Random.Range(1, 3);
            if (num == 1)
            {
                GameObject novo = Instantiate(coletavel1, transform);
                novo.transform.position = localSpawn;
                base.Spawn(novo);
            }
            else
            {
                GameObject novo = Instantiate(coletavel2,transform);
                novo.transform.position = localSpawn;
                base.Spawn(novo);
            }
        }
    }
}
