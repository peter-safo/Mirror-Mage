using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float maxMana = 100f;
    public float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //currentMana = maxMana;
        //manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ManaBar.instance.SpendMana(20);
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

   public  void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //void SpendMana(float mana)
    //{
    //    currentMana -= mana;
    //    manaBar.SetMana(currentMana);
    //}
}
