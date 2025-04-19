using UnityEngine;
using UnityEngine.Events;

namespace GameDesign
{
    public class PlayerExperience : MonoBehaviour
    {
        private float baseExpRequired = 10f;
        [SerializeField] public float currentExp { get; private set; }
        [SerializeField] public int currentLevel { get; private set; }

        private UnityAction<float> experienceEventListener;

        void Awake()
        {
            experienceEventListener = new UnityAction<float>(AddExperience);
        }

        void OnEnable()
        {
            EventManager.StartListening<ExperienceEvent, float>(experienceEventListener);
        }

        void OnDisable()
        {
            EventManager.StopListening<ExperienceEvent, float>(experienceEventListener);
        }

        private void AddExperience(float exp)
        {
            currentExp += exp;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            float requiredExp = GetExperienceForNextLevel();

            while (currentExp >= requiredExp)
            {
                currentExp -= requiredExp;
                currentLevel++;
                switch (currentLevel)
                {
                    case 1:
                        LightController.sunny();
                        break;
                    case 2:
                        LightController.dim();
                        break;
                    case 3:
                        LightController.dim();
                        break;
                    case 4:
                        LightController.dark();
                        break;
                    case 5:
                        LightController.sunny();
                        break;
                    default:
                        LightController.sunny();
                        break;
                }

                EventManager.TriggerEvent<LevelUpEvent, int>(currentLevel);

                requiredExp = GetExperienceForNextLevel();
            }
        }

        public float GetExperienceForNextLevel()
        {
            return baseExpRequired * (currentLevel + 1);
        }
    }
}
