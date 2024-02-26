using boing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public PlayerController player;
    public Renderer playerRenderer0;
    public Renderer playerRenderer1;
    public Renderer playerRenderer2;
    public Renderer playerRenderer3;

    float flashCounter;
    public float flashLength = .1f;

    bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public float invincibilityLength;
    private float invincibilityCounter;

    public GameObject deathEffect;
    public Image blackScreen;

    bool isFadeToBlack;
    bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;


    public Text uiText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer0.enabled = !playerRenderer0.enabled;
                playerRenderer1.enabled = !playerRenderer1.enabled;
                playerRenderer2.enabled = !playerRenderer2.enabled;
                playerRenderer3.enabled = !playerRenderer3.enabled;
                flashCounter = flashLength;
            }
            if(invincibilityCounter <= 0)
            {
                playerRenderer0.enabled = true;
                playerRenderer1.enabled = true;
                playerRenderer2.enabled = true;
                playerRenderer3.enabled = true;
            }
        }
        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f,fadeSpeed*Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }
    public void HurtPlayer(int damage)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLength;

                playerRenderer0.enabled = false;
                playerRenderer1.enabled = false;
                playerRenderer2.enabled = false;
                playerRenderer3.enabled = false;

                flashCounter = flashLength;
            }
        }
        uiText.text = "Health: " + currentHealth.ToString();
    }
    public void Respawn()
    {
        //player.transform.position = respawnPoint;
        //currentHealth = maxHealth;
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        playerRenderer0.enabled = false;
        playerRenderer1.enabled = false;
        playerRenderer2.enabled = false;
        playerRenderer3.enabled = false;
        Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeFromBlack = true;

        isRespawning = false;
        invincibilityCounter = invincibilityLength;

        playerRenderer0.enabled = false;
        playerRenderer1.enabled = false;
        playerRenderer2.enabled = false;
        playerRenderer3.enabled = false;

        flashCounter = flashLength;
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;
        uiText.text = "Health: " + currentHealth.ToString();
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        uiText.text = "Health: " + currentHealth.ToString();
    }
}
