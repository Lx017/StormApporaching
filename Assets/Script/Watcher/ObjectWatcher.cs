using GameDesign;
using UnityEngine;

public class ObjectWatcher : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        if (target == null) // Ŀ�걻����
        {
            EventManager.TriggerEvent<TutorialEndEvent>();
            Destroy(gameObject); 
        }
    }
}
