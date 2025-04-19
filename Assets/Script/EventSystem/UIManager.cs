using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace GameDesign
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private GameObject levelUpPanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gamePausePanel;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private GameObject relicChoicePanel;
        [SerializeField] private TMP_Text gameOverText;

        [SerializeField] private List<Button> relicOptionButtons;
        [SerializeField] private List<TMP_Text> relicNameTexts;
        [SerializeField] private List<TMP_Text> relicDescriptionTexts;
        [SerializeField] private List<Image> relicIconImages;

        [SerializeField] private RelicPool relicPool;

        public Button optionButton1, optionButton2, optionButton3;
        private TMP_Text typeText1, valueText1;
        private TMP_Text typeText2, valueText2;
        private TMP_Text typeText3, valueText3;
        private List<UpgradeOption> options;
        private UnityAction<int> levelUpListener;
        private UnityAction gameWinListener;
        private UnityAction gameLoseListener;
        private UnityAction tutorialEndListener;
        private UnityAction chooseRelicListener;
        private bool isPaused = false;
        private List<RelicData> currentChoices = new();

        private Dictionary<UpgradeType, (float min, float max)> upgradeRanges = new Dictionary<UpgradeType, (float, float)>
    {
        { UpgradeType.Attack, (0f, 2f) },
        { UpgradeType.MaxHealth, (5f, 30f) },
        { UpgradeType.MoveSpeed, (0f, 0.5f) },
        { UpgradeType.HealthRegen, (0f, 3f)},
        { UpgradeType.Shield, (10f, 40f)},
        { UpgradeType.Defense, (1f, 10f)},
        { UpgradeType.CritRate, (0.01f, 0.10f)},
        { UpgradeType.CritMultiplier, (0.05f, 0.25f)},
        { UpgradeType.AttackSpeed, (0f, 2.0f)}
    };

        private Dictionary<string, Color> rarityColors = new Dictionary<string, Color>
    {
        { "Common", Color.gray },
        { "Rare", Color.green },
        { "Epic", Color.blue },
        { "Legendary", new Color(1f, 0.5f, 0f) },
        { "Mythical", new Color(1f, 0f, 0f) }
    };

        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
                Debug.Log("[UIManager] Singleton Initialized.");
            }
            else
            {
                Debug.LogError("[UIManager] Multiple instances detected!");
                Destroy(gameObject);
            }

            typeText1 = optionButton1.GetComponentInChildren<TextMeshProUGUI>();
            valueText1 = optionButton1.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            typeText2 = optionButton2.GetComponentInChildren<TextMeshProUGUI>();
            valueText2 = optionButton2.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            typeText3 = optionButton3.GetComponentInChildren<TextMeshProUGUI>();
            valueText3 = optionButton3.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            levelUpListener = new UnityAction<int>(ShowLevelUpPanel);
            gameWinListener = new UnityAction(ShowGameWinPanel);
            gameLoseListener = new UnityAction(ShowGameLosePanel);
            tutorialEndListener = new UnityAction(CloseTutorialPanel);
            chooseRelicListener = new UnityAction(ShowRelicChoice);
        }

        private void Start()
        {
            gameOverPanel.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }

        public void PauseGame()
        {
            EventManager.TriggerEvent<MouseControlEvent, bool>(true);
            gamePausePanel.SetActive(true);
            Time.timeScale = 0f; // ��ͣ��Ϸ
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ResumeGame()
        {
            EventManager.TriggerEvent<MouseControlEvent, bool>(false);
            gamePausePanel.SetActive(false);
            Time.timeScale = 1f; // ������Ϸ
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            EventManager.StartListening<LevelUpEvent, int>(levelUpListener);
            EventManager.StartListening<GameWinEvent>(gameWinListener);
            EventManager.StartListening<GameLoseEvent>(gameLoseListener);
            EventManager.StartListening<TutorialEndEvent>(tutorialEndListener);
            EventManager.StartListening<ShowRelicChoiceEvent>(chooseRelicListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<LevelUpEvent, int>(levelUpListener);
            EventManager.StopListening<GameWinEvent>(gameWinListener);
            EventManager.StopListening<GameLoseEvent>(gameLoseListener);
            EventManager.StopListening<TutorialEndEvent>(tutorialEndListener);
            EventManager.StopListening<ShowRelicChoiceEvent>(chooseRelicListener);
        }

        private void ShowLevelUpPanel(int newLevel)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            Time.timeScale = 0.5f;
            

            options = GenerateRandomUpgrades();

            SetButton(optionButton1, typeText1, valueText1, options[0]);
            SetButton(optionButton2, typeText2, valueText2, options[1]);
            SetButton(optionButton3, typeText3, valueText3, options[2]);
            levelUpPanel.SetActive(true);
            EventManager.TriggerEvent<MouseControlEvent, bool>(true);
        }

        private void ShowGameWinPanel()
        {
            gameOverText.text = "You Win!";
            EventManager.TriggerEvent<MouseControlEvent, bool>(true);
            gameOverPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void ShowGameLosePanel()
        {
            gameOverText.text = "You Lose!";
            EventManager.TriggerEvent<MouseControlEvent, bool>(true);
            gameOverPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void CloseTutorialPanel()
        {
            tutorialPanel.SetActive(false);
        }

        private void ShowRelicChoice()
        {
            currentChoices = relicPool.GetRandomRelics(3);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0.5f;

            int count = currentChoices.Count;
            Debug.Log(count);

            if (count == 1)
            {
                relicOptionButtons[0].gameObject.SetActive(false);
                relicOptionButtons[2].gameObject.SetActive(false);
                RelicData relic = currentChoices[0];

                relicNameTexts[1].text = relic.relicName;
                relicDescriptionTexts[1].text = relic.description;
                relicIconImages[1].sprite = relic.icon;

                relicOptionButtons[1].onClick.RemoveAllListeners();
                relicOptionButtons[1].onClick.AddListener(() => OnRelicSelected(0));
            }else if (count == 2)
            {
                relicOptionButtons[1].gameObject.SetActive(false);

                RelicData relic = currentChoices[0];

                relicNameTexts[0].text = relic.relicName;
                relicDescriptionTexts[0].text = relic.description;
                relicIconImages[0].sprite = relic.icon;

                relicOptionButtons[0].onClick.RemoveAllListeners();
                relicOptionButtons[0].onClick.AddListener(() => OnRelicSelected(0));

                RelicData relic2 = currentChoices[1];

                relicNameTexts[2].text = relic2.relicName;
                relicDescriptionTexts[2].text = relic2.description;
                relicIconImages[2].sprite = relic2.icon;

                relicOptionButtons[2].onClick.RemoveAllListeners();
                relicOptionButtons[2].onClick.AddListener(() => OnRelicSelected(1));
            }
            else if (count == 3)
            {
                for (int i = 0; i < count; i++)
                {
                    int index = i;
                    RelicData relic = currentChoices[i];

                    relicNameTexts[i].text = relic.relicName;
                    relicDescriptionTexts[i].text = relic.description;
                    relicIconImages[i].sprite = relic.icon;

                    relicOptionButtons[i].onClick.RemoveAllListeners();
                    relicOptionButtons[i].onClick.AddListener(() => OnRelicSelected(index));
                }
            }
            
            relicChoicePanel.SetActive(true);
            EventManager.TriggerEvent<MouseControlEvent, bool>(true);
        }

        private void OnRelicSelected(int index)
        {
            Debug.Log(index);
            var player = FindObjectOfType<Player>();
            var relic = currentChoices[index];

            player.GetComponent<RelicManager>().AddRelic(relic);
            relicPool.MarkAsSelected(relic);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
            relicChoicePanel.SetActive(false);
            EventManager.TriggerEvent<MouseControlEvent, bool>(false);
            ClearRelicUI();
        }

        private void ClearRelicUI()
        {
            for (int i = 0; i < relicOptionButtons.Count; i++)
            {
                relicOptionButtons[i].onClick.RemoveAllListeners();
                relicOptionButtons[i].gameObject.SetActive(true); // 重置显示状态
                relicNameTexts[i].text = "";
                relicDescriptionTexts[i].text = "";
                relicIconImages[i].sprite = null; // 或者默认图
            }

            currentChoices.Clear();
        }
        private void SetButton(Button button, TMP_Text typeText, TMP_Text valueText, UpgradeOption option)
        {
            typeText.text = $"{option.Type}";
            valueText.text = $"+{option.Value:F1} ({option.Rarity})";

            Color rarityColor = rarityColors.ContainsKey(option.Rarity) ? rarityColors[option.Rarity] : Color.white;
            ColorBlock cb = button.colors;
            cb.normalColor = rarityColor;
            cb.highlightedColor = rarityColor * 1.2f;
            cb.pressedColor = rarityColor * 0.8f;
            cb.selectedColor = rarityColor;
            button.colors = cb;
        }

        private List<UpgradeOption> GenerateRandomUpgrades()
        {
            List<UpgradeOption> options = new List<UpgradeOption>();
            List<UpgradeType> availableTypes = new List<UpgradeType> { UpgradeType.Attack, UpgradeType.MoveSpeed, UpgradeType.MaxHealth, UpgradeType.HealthRegen, UpgradeType.Shield, UpgradeType.Defense, UpgradeType.CritRate, UpgradeType.CritMultiplier, UpgradeType.AttackSpeed };
            availableTypes = new List<UpgradeType> { UpgradeType.Attack, UpgradeType.AttackSpeed, UpgradeType.MaxHealth, UpgradeType.Defense, UpgradeType.AttackSpeed, UpgradeType.Shield, UpgradeType.HealthRegen};// for testing

            while (options.Count < 3)
            {
                UpgradeType type = availableTypes[Random.Range(0, availableTypes.Count)];
                availableTypes.Remove(type);

                UpgradeOption option = GenerateUpgradeOption(type);
                options.Add(option);
            }
            return options;
        }

        private UpgradeOption GenerateUpgradeOption(UpgradeType type)
        {
            (float minValue, float maxValue) = upgradeRanges[type];

            float range = maxValue - minValue;
            float randomValue = Random.value * 100;
            float value = 0;
            string rarity = "Common";

            if (randomValue < 50) { value = minValue + range * 0.2f; rarity = "Common"; }
            else if (randomValue < 76) { value = minValue + range * 0.4f; rarity = "Rare"; }
            else if (randomValue < 90) { value = minValue + range * 0.6f; rarity = "Epic"; }
            else if (randomValue < 97) { value = minValue + range * 0.8f; rarity = "Legendary"; }
            else { value = maxValue; rarity = "Mythical"; }

            return new UpgradeOption(type, value, rarity);
        }

        public void ApplyUpgrade(int idx)
        {
            EventManager.TriggerEvent<UpgradeAppliedEvent, UpgradeOption>(options[idx]);
            CloseLevelUpPanel();
        }

        public void CloseLevelUpPanel()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
            levelUpPanel.SetActive(false);
            EventManager.TriggerEvent<MouseControlEvent, bool>(false);
        }
    }
}

