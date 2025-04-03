using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        // Get the Button component and add a listener
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);

        // If audioSource isn't set, try to get it from this GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}