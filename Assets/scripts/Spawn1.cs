using System.Collections.Generic;
using UnityEngine;

public class Spawn1 : MonoBehaviour
{

    public List<GameObject> inimigo;
    [SerializeField] private bool PodeWave = false;
    [SerializeField] private int Wave = 0;
    [SerializeField] private int total = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PodeWave)
        {
            return;
        
        }

        if (total >= 2)  // Alterado para gerar 9 inimigos no total
        {
            // timeSpanw();
            int num = Random.Range(0, 11);
            
            SpawnEnemy(inimigo[num]);
            Wave += 1;
            total = 0;
        }
        
    }
    void SpawnEnemy(GameObject enemyPrefab)
    {
        if(total >= 1){

            // Instancie o inimigo
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Ajuste as coordenadas de deslocamento
            float offsetX = Random.Range(-40f, 40f); // Altere conforme necessário
            float offsetZ = Random.Range(-40f, 40f); // Altere conforme necessário

            newEnemy.transform.position = new Vector3(offsetX, 1, offsetZ);

            // Adicione a força
            newEnemy.GetComponent<Rigidbody>().AddForce(Vector3.up * 20000);
        }

        
    }

    public void Quantos(int menos1)
    {
        total += menos1;
    }

}