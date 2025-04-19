using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesign
{
    public class OpenableChest : MonoBehaviour
    {
        private Animator anim;
        private bool open = false;
        private bool isOpened = false;

        [SerializeField] private float experienceReward = 20;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            if (anim == null)
                Debug.LogError("Animator could not be found on " + gameObject.name);
        }
        private void Start()
        {
            ChestManager.Instance?.RegisterChest(transform);
        }
        private void OnTriggerEnter(Collider c)
        {
            if (!isOpened && c.CompareTag("Player"))
            {

                if (SoundEffectPlayer.shared != null)
                    SoundEffectPlayer.shared.PlaySoundEffect("chestopen");
                open = true;
                anim.SetBool("open", open);
                isOpened = true;

                EventManager.TriggerEvent<ExperienceEvent, float>(experienceReward);
                ChestManager.Instance?.UnregisterChest(transform);
                Debug.Log($"Player opened the chest and gained {experienceReward} XP!");
            }
        }
    }
}
