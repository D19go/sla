using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public List<GameObject> inimigo;
    [SerializeField] private bool PodeWave = false;
    [SerializeField] private int Wave = 0;
    [SerializeField] private int total = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 55; i++){
            int num = Random.Range(0, 11);
            total = 1;
            SpawnEnemy(inimigo[num]);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!PodeWave)
        {
            return;
        
        }

        if (total >= 1)  // Alterado para gerar 9 inimigos no total
        {
            // timeSpanw();
            
            Wave += 1;
            total = 0;
        }
        
    }
    void SpawnEnemy(GameObject enemyPrefab)
    {
        if(total == 1){
            // Instancie o inimigo
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Ajuste as coordenadas de deslocamento
            float offsetX = Random.Range(-160f, 160f); // Altere conforme necessário
            float offsetZ = Random.Range(-160f, 160f); // Altere conforme necessário
            newEnemy.transform.position = new Vector3(offsetX, 1, offsetZ);
            total = 0;
        }
    }

    public void Quantos(int menos1)
    {
        total += menos1;
    }

}