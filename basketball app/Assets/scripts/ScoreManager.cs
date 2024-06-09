using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private ParticleSystem scoreParticleSystem; // Reference to the particle system
    private int score;

    private void Awake()
    {
        // Find the GameObject named "score count" and get its TMP_Text component
        GameObject scoreObject = GameObject.Find("score count");
        if (scoreObject != null)
        {
            score_text = scoreObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("No GameObject found with the name 'score count'.");
        }

        // Initialize the score and update the text
        score = 0;
        if (score_text != null)
        {
            score_text.text = score.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            score++;
            if (score_text != null)
            {
                score_text.text = score.ToString();
            }

            // Play the particle effect if it is assigned
            if (scoreParticleSystem != null)
            {
                scoreParticleSystem.Play();
            }
        }
    }
}
