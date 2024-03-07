using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform jogador;

    public float sensibilidade = 5f;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {

        jogador = GameObject.FindWithTag("Player").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = jogador.position - new Vector3(0, -1, 0);    

        mouseX += Input.GetAxis("Mouse X") * sensibilidade;
        mouseY += Input.GetAxis("Mouse Y") * sensibilidade;

        mouseY = Mathf.Clamp( mouseY, -90f, 90f );

        transform.rotation = Quaternion.Euler( -mouseY, mouseX, 0 );

    }
}
