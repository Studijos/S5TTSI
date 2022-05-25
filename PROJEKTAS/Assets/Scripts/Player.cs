using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovementCustom))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int maxHealth = 100;
    public int currentHealth;
    public int maxAmor = 100;
    public int currentArmor;

    [SerializeField]
    public AudioSource deathSound;

    [SerializeField]
    public AudioSource hurtSound;

    [SerializeField]
    public AudioSource armorBrokeSound;

    public HealthBar healthBar;
    public ArmorBar armorBar;

    [SerializeField]
    private Text dieText;

    [SerializeField]
    public GameObject gameOverUI;

    [SerializeField]
    public Image img;

    public GameObject MessagePanel;

    public GameObject bloodSceen;

    //public MouseLookCustom mouse;

    float lastAttackTime = 0;
    float attackCooldown = 0.5f;

    private void Awake()
    {
        //playerMovementCustom = GetComponent<PlayerMovementCustom>();
        Instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = 100;
        healthBar.SetHealth(maxHealth);
        armorBar.SetArmor(maxAmor);
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            IncreaseHealth(10);
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            bloodSceen.SetActive(false);
        }

    }

    public void TakeDamage(int damage)
    {
        lastAttackTime = Time.time;
        if (currentArmor > 0)
        {
            armorBrokeSound.Play();
            currentArmor -= damage;
        }
        else if(currentHealth > 20)
        {
            bloodSceen.SetActive(true);
            hurtSound.Play();
            currentHealth -= damage;
        }

        else if (currentHealth <= 20)
        {
            currentHealth -= damage;
            gameOverUI.SetActive(true);
            deathSound.Play();
            //playerMovementCustom.enabled = false;
            dieText.gameObject.SetActive(true);
            //mouse.sensitivity = 0;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            img.enabled = false;
            //SceneManager.LoadScene("MainScene");
            //Cursor.lockState = CursorLockMode.Locked;

        }

        healthBar.SetHealth(currentHealth);
        armorBar.SetArmor(currentArmor);
    }

    public void IncreaseHealth(int value)
    {
        currentHealth += value;
        healthBar.SetHealth(currentHealth);
    }

    public void IncreaseArmor(int value)
    {
        currentArmor += value;
        armorBar.SetArmor(currentArmor);
    }


    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel(string text)
    {
        MessagePanel.SetActive(false);
    }
}
