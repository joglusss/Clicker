using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Common;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Business.Systems
{
    public class BusinessViewSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, BusinessViewComponent, BusinessConfig> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var data =  ref _filter.Get1(i);
                ref var view = ref _filter.Get2(i);
                ref var config = ref _filter.Get3(i);
                
                int income = GameMath.CalculateIncome(data, config);
                if (view.LastIncome != income || !view.Initialized)
                {
                    view.Link.SetIncome(income);
                    view.LastIncome = income;
                }

                if (view.LastName != data.Name || !view.Initialized)
                {
                    view.Link.SetName(data.Name);
                    view.LastName = data.Name;
                }

                if (view.LastLevel != data.Level || !view.Initialized)
                {
                    view.Link.SetLevel(data.Level);
                    view.LastLevel = data.Level;
                }

                if (config.ProcessDuration != 0 || !view.Initialized)
                {
                    float process = data.Process / config.ProcessDuration;
                    if (!Mathf.Approximately(process, view.LastProcess))
                    {
                        view.Link.SetProcess(process);
                        view.LastProcess = process;
                    }
                }

                if (view.LastAvailableLevelUp != data.AvailableLevelUp || !view.Initialized)
                {
                    view.Link.SetInteractableLevelUp(data.AvailableLevelUp);
                    view.LastAvailableLevelUp = data.AvailableLevelUp;
                }

                if (view.LastAvailableUpgrade1 != data.AvailableUpgrade1 || !view.Initialized)
                {
                    view.Link.SetInteractableGrade1(data.AvailableUpgrade1);
                    view.LastAvailableUpgrade1 = data.AvailableUpgrade1;
                }

                if (view.LastAvailableUpgrade2 != data.AvailableUpgrade2 || !view.Initialized)
                {
                    view.Link.SetInteractableGrade2(data.AvailableUpgrade2);
                    view.LastAvailableUpgrade2 = data.AvailableUpgrade2;
                }

                int levelCost = GameMath.CalculateLevelCost(data, config);
                if (view.LastLevelCost != levelCost || !view.Initialized)
                {
                    view.Link.SetNameLevelUp(levelCost);
                    view.LastLevelCost = levelCost;
                }

                if (view.LastGrade1 != data.Upgrade1 || !view.Initialized)
                {
                    if(config.Grades1.Multiplier == data.Upgrade1)
                        view.Link.SetNameGrade1Bought();
                    else
                        view.Link.SetNameGrade1(config.Grades1.Cost, config.Grades1.Multiplier);
                    
                    view.LastGrade1 = data.Upgrade1;
                }


                if (view.LastGrade2 != data.Upgrade2 || !view.Initialized)
                {
                    if(config.Grades2.Multiplier == data.Upgrade2)
                        view.Link.SetNameGrade2Bought();
                    else
                        view.Link.SetNameGrade2(config.Grades2.Cost, config.Grades2.Multiplier);
                    
                    view.LastGrade2 = data.Upgrade2;
                }
                
                view.Initialized = true;
            }
        }
    }
}