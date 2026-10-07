using UnityEngine;

namespace Clicker.Data
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Transform Canvas { get; private set; }
        [field: SerializeField] public Transform BusinessContainer { get; private set; }
    }
}