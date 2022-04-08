using UnityEngine;
using Cinemachine;
namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]private ScriptableListSettings Levels;
        [SerializeField]private Gameplay.PlayerController Player;
        [SerializeField] private int NumberLevel;
        [SerializeField] private SaveData saveData;
        [SerializeField] private Level CurrLevel;
        [SerializeField] private Transform positionRespawn;
        private void Awake()
        {
            if (PlayerPrefs.HasKey("Save"))
            {
                saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Save"));
            }
            else
            {
                saveData = new SaveData();
            }
        }
        public void RestartLevel()
        {
            GenerateLevel();
            Player.SetIsCanMove(true);
        }
        private void GenerateLevel()
        {
            if (CurrLevel != null)
            {
                Destroy(CurrLevel.gameObject);
            }
            CurrLevel = Instantiate(Levels.Levels[saveData.NumberLevel % Levels.Levels.Count], positionRespawn.position, positionRespawn.rotation);
            Player.pathCreator = CurrLevel.pathCreator;
            Player.ResetBeginPosition();
            Player.GetComponent<Gameplay.CollectionBlock>().ClearBlocks();
        }
        public void IncreaseLevel()
        {
            saveData.NumberLevel++;
            PlayerPrefs.SetString("Save",JsonUtility.ToJson(saveData));
        }
        public void NextLevel()
        {
            GenerateLevel();
        }
    }
}
[System.Serializable]
public struct SaveData
{
    public int NumberLevel;
}