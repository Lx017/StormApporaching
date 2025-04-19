using UnityEngine;

namespace GameDesign
{
    public class OpenableRelicChest : MonoBehaviour
    {
        private Animator anim;
        private bool open = false;
        private bool isOpened = false;

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

                EventManager.TriggerEvent<ShowRelicChoiceEvent>();
                ChestManager.Instance?.UnregisterChest(transform);

            }
        }
    }
}
