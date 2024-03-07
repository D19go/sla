using UnityEngine;

public class Capturado : MonoBehaviour
{
    public GameObject spawner;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawn");
    }
    public void pego(){
        Destroy(gameObject);

    }
}
