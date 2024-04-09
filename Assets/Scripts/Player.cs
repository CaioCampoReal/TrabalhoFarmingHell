using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed;
    [SerializeField]
    private Transform cameraTransform;

    private float originalSpeed;
    public float jumpForce = 5.0f;
    public bool isOnGround = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        originalSpeed = speed;
    }

    
    private void FixedUpdate()
    {
        MovePlayer();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)  && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);            
            isOnGround = false;
        }
    }

    private void MovePlayer()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        // Verifica se a tecla Shift esquerda está sendo pressionada para acelerar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = originalSpeed * 1.5f; // Aumenta a velocidade em 1.5x
        }
        else
        {
            speed = originalSpeed; // Restaura a velocidade original
        }

        Vector3 dir = transform.TransformVector(new Vector3(moveH, 0, moveV));
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
        }
    }
}
