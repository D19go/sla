using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 movi = new Vector3 (hori,0, vert);
        movi = movi * velocidade * Time.deltaTime;

        transform.Translate(movi);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coletavel")
        {
            GameManager.MudaPontos(1);
            Destroy(other.gameObject);
        }
    }
}
