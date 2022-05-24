using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementCustom : MonoBehaviour
{
    public CharacterController controller;
    public float health = 100f;
    public float speed = 5f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            controller.enabled = false;
            Debug.Log("MOVEMENT STOPPED HMMMMMMMMMMMMMMMMMMMM?");
            //SceneManager.LoadScene("MainMenuScene");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);



        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log("ME DEAD?: " + health);
        health = health - damage;
        Debug.Log("ME DEAD: " + health);

    }
}
