using UnityEngine;

[CreateAssetMenu(fileName = "RelicData", menuName = "Scriptable Objects/RelicData")]
public class RelicData : ScriptableObject
{
    public string relicName;
    public Sprite icon;
    public Vector3 genOffset = Vector3.zero;
    [TextArea] public string description;
    public GameObject relicPrefab;
}
