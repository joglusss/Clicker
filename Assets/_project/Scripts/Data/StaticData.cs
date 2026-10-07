using Clicker.Business.Config;
using Clicker.Business.View;
using Clicker.Common.View;
using UnityEngine;

namespace Clicker.Data
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        [field: SerializeField] public BusinessConfig[] Businesses { get ; private set; }
        [field: SerializeField] public BusinessView BusinessViewPrefab { get; private set; } 
        [field: SerializeField] public CommonView CommonViewPrefab { get; private set; }
    }
}
