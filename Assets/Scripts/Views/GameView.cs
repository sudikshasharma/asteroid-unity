using UnityEngine;
using UnityEngine.UI;
using Asteroids.Models;
using Asteroids.Managers;
using System.Collections;
using Asteroids.Controllers;

namespace Asteroids.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Sprite[] airShipSprite;
        [SerializeField] private GameObject scrollBg;
        [SerializeField] private Button onClickPlayBtn;
        [SerializeField] private Button onClickSingleBulletSelectBtn;
        [SerializeField] private Button onClickDoubleBulletSelectBtn;
        [SerializeField] private Button onClickBlastBulletSelectBtn;
        [SerializeField] private Button onClickChangeShipModelBtn;
        [SerializeField] private Button onClickResetBtn;
        [SerializeField] private Text scoreInGameText;
        [SerializeField] private Text timeInGameText;
        [SerializeField] private Text scoreInGameOverText;
        [SerializeField] private Text timeInGameOverText;
        [SerializeField] private Vector2 bgPosOffset;
        private int currentScore;
        private Vector2 bgPos;
        private bool isDoubleShootLocked;
        private int timer;

        private void AddListeners()
        {
            onClickPlayBtn.onClick.AddListener(OnClickPlayGame);
            onClickSingleBulletSelectBtn.onClick.AddListener(OnClickSingleBulletSelect);
            onClickDoubleBulletSelectBtn.onClick.AddListener(OnClickDoubleBulletSelect);
            onClickBlastBulletSelectBtn.onClick.AddListener(OnClickBlastBulletSelect);
            onClickResetBtn.onClick.AddListener(OnClickReset);
            onClickChangeShipModelBtn.onClick.AddListener(OnClickChangeShipModel);
        }

        private void InitailizePanels()
        {
            menuPanel.SetActive(true);
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        //ButtonEvents
        private void OnClickPlayGame()
        {
            gameController.PlayGame();
        }

        private void OnClickSingleBulletSelect()
        {
            gameController.SetCurrentBulletType(BulletTypeModel.SINGLE);
        }

        private void OnClickDoubleBulletSelect()
        {
            if (!isDoubleShootLocked)
            {
                isDoubleShootLocked = true;
                onClickDoubleBulletSelectBtn.transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
                StartCoroutine(FillDoublePowerShotMeter());
                gameController.SetCurrentBulletType(BulletTypeModel.DOUBLE);
            }
        }

        private void OnClickBlastBulletSelect()
        {
            gameController.SetCurrentBulletType(BulletTypeModel.BLAST);
        }

        private void OnClickReset()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        private void OnClickChangeShipModel()
        {
            gameController.ChangeShipSprite(airShipSprite[Random.Range(0, airShipSprite.Length)]);
        }

        private void SetGamePanel()
        {
            gamePanel.SetActive(true);
            menuPanel.SetActive(false);
        }

        private void SetGameOverPanel()
        {
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }

        private void SetGameText()
        {
            scoreInGameText.text = "Score : 0";
            timeInGameText.text = "Time : 0";
        }

        private void SetGameOverText()
        {
            scoreInGameOverText.text = "Score : " + currentScore;
            timeInGameOverText.text = "Time : " + timer;
        }

        private IEnumerator StartTimer()
        {
            while (GameManager.Instance.currentGameState == GameStateModel.GAME)
            {
                timer += 1;
                timeInGameText.text = "Time : " + timer.ToString();
                yield return new WaitForSeconds(1f);
            }
        }

        public IEnumerator ScrollBg()
        {
            while (GameManager.Instance.currentGameState == GameStateModel.GAME)
            {
                scrollBg.transform.Translate(Vector2.down * Time.deltaTime * GameData.bgScrollSpeed);
                if (scrollBg.transform.position.y < bgPosOffset.y)
                {
                    scrollBg.transform.position = bgPos;
                }
                yield return null;
            }
        }

        private IEnumerator FillDoublePowerShotMeter()
        {
            while (onClickDoubleBulletSelectBtn.transform.GetChild(1).GetComponent<Image>().fillAmount > 0)
            {
                onClickDoubleBulletSelectBtn.transform.GetChild(1).GetComponent<Image>().fillAmount -= 0.1f;
                yield return new WaitForSeconds(0.5f);
            }
            isDoubleShootLocked = false;
        }

        public void Play()
        {
            timer = 0;
            currentScore = 0;
            SetGamePanel();
            SetGameText();
            StartCoroutine(StartTimer());
        }

        public void OnGameOver()
        {
            gameController.GameOver();
            SetGameOverText();
            SetGameOverPanel();
        }

        public void IncrementScore()
        {
            currentScore += 1;
            scoreInGameText.text = "Score : " + currentScore.ToString();
        }

        public void Init()
        {
            AddListeners();
            InitailizePanels();
            bgPos = scrollBg.transform.position;
            isDoubleShootLocked = false;
        }
    }
}