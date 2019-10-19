using System;
using System.Collections.Generic;

namespace TowerDefense.HighScores
{
    [Serializable]
    public class HighScoresSaveData
    {
    public List<HighScoresEntryData> highscores = new List<HighScoresEntryData>();
    }
}
