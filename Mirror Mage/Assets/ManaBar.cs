using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider;

    private int maxMana = 100;
    private int currentMana;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static ManaBar instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentMana = maxMana;
        manaSlider.maxValue = maxMana;
        manaSlider.value = maxMana;
    }

    public void SpendMana(int amount)
    {
        if (currentMana - amount >= 0)
        {
            currentMana -= amount;
            manaSlider.value = currentMana;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenMana());
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }

    private IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(2);

        while(currentMana < maxMana)
        {
            currentMana += maxMana / 100;
            manaSlider.value = currentMana;
            yield return regenTick;
        }
        regen = null;
    }
}
