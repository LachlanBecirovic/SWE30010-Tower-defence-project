using System.IO;
using UnityEngine;


namespace TowerDefense.HighScores
{
   public class HighScores : MonoBehaviour
        {
            [SerializeField] private int maxHighScoresEntries = 5;
            [SerializeField] private Transform highscoresHolderTransform = null;
            [SerializeField] private GameObject highscoreEntryObject = null;

            [Header("Test")]
            [SerializeField] HighScoresEntryData testEntryData = new HighScoresEntryData();

            private string SavePath => $"{Application.persistentDataPath}/highscores.json"; //Location of high score data

            private void Start()
            {
               HighScoresSaveData savedScores =  GetSavedScores();
               
               UpdateUI(savedScores);

               SaveScores(savedScores);
            }

            [ContextMenu("Add Test Entry")]
            public void AddTestEntry()
            {
                AddEntry(testEntryData);
            }

            public void AddEntry(HighScoresEntryData highScoresEntryData) //Adds new entry to list 
            {
                HighScoresSaveData savedScores = GetSavedScores();
                
                bool scoreAdded = false;

                {
                    for(int i = 0; i < savedScores.highscores.Count; i++)
                    {
                        if(highScoresEntryData.entryScore > savedScores.highscores[i].entryScore)
                        {
                            savedScores.highscores.Insert(i, highScoresEntryData);
                            scoreAdded = true;
                            break;
                        }
                    }

                    if(!scoreAdded && savedScores.highscores.Count < maxHighScoresEntries)
                    {
                        savedScores.highscores.Add(highScoresEntryData);
                    }

                    if(savedScores.highscores.Count > maxHighScoresEntries)
                    {
                        savedScores.highscores.RemoveRange(maxHighScoresEntries, savedScores.highscores.Count - maxHighScoresEntries);
                    }

                    UpdateUI(savedScores);
                    SaveScores(savedScores);
                }

            } 

            private void UpdateUI(HighScoresSaveData savedScores)
            {
                foreach(Transform child in highscoresHolderTransform) //After added entry, destroy old components and repopulate new components for the new list of scores
                {
                    Destroy(child.gameObject);
                }

                foreach(HighScoresEntryData highScores in savedScores.highscores) //Loop through all entries and create new components
                {
                    Instantiate(highscoreEntryObject, highscoresHolderTransform).GetComponent<HighScoresEntryUI>().Initialise(highScores);
                }
            }

            private HighScoresSaveData GetSavedScores() //Function to get saved scores
            {
                if(!File.Exists(SavePath))
                {
                    File.Create(SavePath).Dispose(); //Create SavePath if doesn't exist
                    return new HighScoresSaveData();
                }

                using(StreamReader stream = new StreamReader(SavePath))
                {
                    string json = stream.ReadToEnd(); //Reads the whole file and copy into string

                    return JsonUtility.FromJson<HighScoresSaveData>(json);
                }
            }

            private void SaveScores(HighScoresSaveData highscoresSaveData) //Function to write data into save file
            {
                 using(StreamWriter stream = new StreamWriter(SavePath))
                 {
                     string json = JsonUtility.ToJson(highscoresSaveData, true);
                     stream.Write(json);
                 }
                 
            }
        }
}

