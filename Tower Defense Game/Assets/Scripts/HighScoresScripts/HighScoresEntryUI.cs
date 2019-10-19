using TMPro;
using UnityEngine;

namespace TowerDefense.HighScores
{
public class HighScoresEntryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entryNameText = null; 
        [SerializeField] private TextMeshProUGUI entryScoreText = null;

        public void Initialise(HighScoresEntryData highScoresEntryData)
        {
            entryNameText.text = highScoresEntryData.entryName;
            entryScoreText.text = highScoresEntryData.entryScore.ToString();    
        }

    }
}
