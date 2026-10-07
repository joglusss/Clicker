using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Common;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Business.Systems
{
    public class BusinessInputSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData;
        private EcsFilter<BusinessComponent, BusinessViewComponent, BusinessConfig> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var businessComponent = ref _filter.Get1(i);
                ref var businessViewComponent = ref _filter.Get2(i);
                ref var businessConfig = ref _filter.Get3(i);

                if (businessViewComponent.Link.ConsumeLevelUpClick() 
                    && GameMath.CalculateLevelCost(businessComponent, businessConfig) <= _runtimeData.Money)
                {
                    _runtimeData.Money -= GameMath.CalculateLevelCost(businessComponent, businessConfig);
                    businessComponent.Level++;
                    Debug.Log($"name {businessComponent.Name} upgrade level to {businessComponent.Level}");
                }

                if (businessViewComponent.Link.ConsumeUpgrade1Click()
                    && businessConfig.Grades1.Cost <= _runtimeData.Money)
                {
                    _runtimeData.Money -= businessConfig.Grades1.Cost;
                    businessComponent.Upgrade1 = businessConfig.Grades1.Multiplier;
                    Debug.Log($"name {businessComponent.Name} upgrade (1), Cost {businessConfig.Grades1.Cost}");
                }
                
                if (businessViewComponent.Link.ConsumeUpgrade2Click()
                    && businessConfig.Grades2.Cost <= _runtimeData.Money)
                {
                    _runtimeData.Money -= businessConfig.Grades2.Cost;
                    businessComponent.Upgrade2 = businessConfig.Grades2.Multiplier;
                    Debug.Log($"name {businessComponent.Name} upgrade (2)");
                }
            }
        }
    }
}