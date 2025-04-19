using GameDesign;
using UnityEngine;

public class ObjectWatcher : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        if (target == null) // 目标被销毁
        {
            EventManager.TriggerEvent<TutorialEndEvent>();
            Destroy(gameObject); 
        }
    }
}
