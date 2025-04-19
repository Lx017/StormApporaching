using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace GameDesign
{
    public class GameManager : MonoBehaviour
    {

        private bool gameEnded = false;

        private void Start()
        {

        }

        private void Update()
        {

        }


        private bool IsEnemyAlive()
        {
            return GameObject.FindGameObjectWithTag("Enemy") != null;
        }
        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameMenuScene"); // �������˵�����
        }
        private void GameWin()
        {
            gameEnded = true;
            Time.timeScale = 1;
            EventManager.TriggerEvent<GameWinEvent>();
            
        }
        public void StartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MapScene 2");
            SoundEffectPlayer.shared.SwitchLoopTo2();
        }
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            Debug.Log("Quit");
        }
    }
}
