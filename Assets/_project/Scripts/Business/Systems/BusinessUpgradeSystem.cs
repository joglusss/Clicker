using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Common;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Business.Systems
{
    public class BusinessUpgradeSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData;
        private EcsFilter<BusinessComponent, BusinessConfig, BusinessUpgradeEvent> _filterEvent;
        private EcsFilter<BusinessComponent, BusinessConfig> _filterCost;
        
        public void Run()
        {
            foreach (var i in _filterEvent)
            {
                ref var data = ref _filterEvent.Get1(i);
                ref var config = ref _filterEvent.Get2(i);
                ref var upgradeEvent = ref _filterEvent.Get3(i);
                
                switch (upgradeEvent.Type)
                {
                    case UpgradeType.Level:
                        _runtimeData.Money -= GameMath.CalculateLevelCost(data, config);
                        data.Level++;
                        Debug.Log($"name {data.Name} upgrade level to {data.Level}");
                        break;
                    
                    case UpgradeType.Upgrade1:
                        _runtimeData.Money -= config.Grades1.Cost;
                        data.Upgraded1 = true;
                        Debug.Log($"name {data.Name} upgrade (1), Cost {config.Grades1.Cost}");
                        break;
                    
                    case UpgradeType.Upgrade2:
                        _runtimeData.Money -= config.Grades2.Cost;
                        data.Upgraded2 = true;
                        Debug.Log($"name {data.Name} upgrade (2)");
                        break;
                }
            }

            foreach (var i in _filterCost)
            {
                ref var data = ref _filterCost.Get1(i);
                ref var config = ref _filterCost.Get2(i);
                
                data.AvailableLevelUp = GameMath.CalculateLevelCost(data, config) <= _runtimeData.Money;
                
                
                data.AvailableUpgrade1 = config.Grades1.Cost <= _runtimeData.Money && 
                                         !data.Upgraded1 &&
                                         data.Level > 0;
                
                data.AvailableUpgrade2 = config.Grades2.Cost <= _runtimeData.Money && 
                                         !data.Upgraded2 &&
                                         data.Level > 0;
            }
        }
    }
}