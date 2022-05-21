using UnityEngine;
using Cinemachine;
namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]private ScriptableListSettings levels;
        [SerializeField]private Gameplay.PlayerController player;
        [SerializeField] private int numberLevel;
        [SerializeField] private SaveData saveData;
        [SerializeField] private Level currLevel;
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
        /// <summary>
        /// ������� ������������ ������
        /// </summary>
        public void RestartLevel()
        {
            GenerateLevel();
            player.SetIsCanMove(true);
        }
        /// <summary>
        /// ������� ��������� ������
        /// </summary>
        private void GenerateLevel()
        {
            if (currLevel != null)
            {
                Destroy(currLevel.gameObject);
            }
            currLevel = Instantiate(levels.Levels[saveData.NumberLevel % levels.Levels.Count], positionRespawn.position, positionRespawn.rotation);
            player.pathCreator = currLevel.pathCreator;

            player.GetComponent<Gameplay.CollectionBlock>().ClearBlocks();
            player.ResetBeginPosition();
        }
        /// <summary>
        /// ������� ���������� ������ � ���������� ������
        /// </summary>
        public void IncreaseLevel()
        {
            saveData.NumberLevel++;
            PlayerPrefs.SetString("Save",JsonUtility.ToJson(saveData));
        }
        /// <summary>
        /// ������� ������� ���������� ������
        /// </summary>
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