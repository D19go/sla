using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool timerOVER = false;  
    public float velocidade;
    public GameObject atk;
    bool atkk = false;

    float rotationX = 0;
    float limitVisionY = 45f;
    public float speedRotetion = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOVER)
        {
            return;
        }
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 movi = new Vector3 (hori,0, vert);
        movi = movi * velocidade * Time.deltaTime;

        Vector2 mousePosition = Input.mousePosition;

        rotationX -= Input.GetAxis("Mouse Y") * speedRotetion;
        //rotationX = Mathf.Clamp(rotationX, -limitVisionY, limitVisionY);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * speedRotetion, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Ataque());
        }
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

    IEnumerator Ataque()
    {
        atkk = !atkk;
        atk.SetActive(atkk);
        yield return new WaitForSeconds(1);
        atkk = !atkk;
        atk.SetActive(atkk);
    }
}
