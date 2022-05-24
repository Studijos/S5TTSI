using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovementCustom))]
public class Player : MonoBehaviour
{
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

    private PlayerMovementCustom playerMovementCustom;

    private void Awake()
    {
        playerMovementCustom = GetComponent<PlayerMovementCustom>();
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        if (currentArmor > 0)
        {
            armorBrokeSound.Play();
            currentArmor -= damage;
        }
        else if(currentHealth > 20)
        {
            hurtSound.Play();
            currentHealth -= damage;
        }

        else if (currentHealth <= 20)
        {
            currentHealth -= damage;
            gameOverUI.SetActive(true);
            deathSound.Play();
            playerMovementCustom.enabled = false;
            Time.timeScale = 0f;
            dieText.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            img.enabled = false;
            //SceneManager.LoadScene("MainScene");
            //Cursor.lockState = CursorLockMode.Locked;

        }

        healthBar.SetHealth(currentHealth);
        armorBar.SetArmor(currentArmor);
    }
}
