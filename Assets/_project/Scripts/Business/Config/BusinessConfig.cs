using UnityEngine;

namespace Clicker.Business.Config
{
    [System.Serializable]
    public struct BusinessConfig
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int BaseCost { get; private set; }
        [field: SerializeField] public int BaseIncome { get; private set; }
        [field: SerializeField] public int ProcessDuration { get; private set; }
        [field: SerializeField] public BusinessGrade Grades1 { get; private set; }
        [field: SerializeField] public BusinessGrade Grades2 { get; private set; }
    }
}