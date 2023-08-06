using System;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Score Settings")]
        public int currentScore;
        public int finalScore;
        public TextMeshProUGUI scoreText;

        //Actions criadas com intuitio de chamar suas respectivas fun��es nos Scripts desejados sem precisar referenci�-los.
        public static Action<int> ACT_IncrementScore;
        public static Action<int> ACT_ScoreFinal;
        private void Update()
        {
            scoreText.text = currentScore.ToString();
        }

        private void OnEnable()
        {
            ACT_IncrementScore += IncrementScore;
            ACT_ScoreFinal += ScoreFinal;
        }

        private void OnDisable()
        {
            ACT_IncrementScore -= IncrementScore;
            ACT_ScoreFinal -= ScoreFinal;
        }

        //Incrementa pontos para o player.
        public void IncrementScore(int value)
        {
            currentScore += value;
        }

        //Calcula a pontua��o final dependendo do valor passando no par�metro multiplier.
        public void ScoreFinal(int multiplier)
        {
            finalScore = currentScore * multiplier;
        }
    }
}

