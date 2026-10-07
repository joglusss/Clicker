using UnityEngine;

namespace Clicker.Business.Config
{
    [System.Serializable]
    public struct BusinessGrade
    {
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public int Multiplier { get; private set; }
    }
}