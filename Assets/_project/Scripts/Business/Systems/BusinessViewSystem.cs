using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Common;
using Clicker.Data;
using Leopotam.Ecs;

namespace Clicker.Business.Systems
{
    public class BusinessViewSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData;
        private EcsFilter<BusinessComponent, BusinessViewComponent, BusinessConfig> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var data =  ref _filter.Get1(i);
                ref var view = ref _filter.Get2(i);
                ref var config = ref _filter.Get3(i);
                
                view.Link.SetIncome(GameMath.CalculateIncome(data, config));
                view.Link.SetName(data.Name);
                view.Link.SetLevel(data.Level);
                if(config.ProcessDuration != 0)
                  view.Link.SetProcess(data.Process / config.ProcessDuration);
                
                bool interactableLevelUp = GameMath.CalculateLevelCost(data, config) <= _runtimeData.Money;
                bool interactable1 = data.Level != 0 
                                     && _runtimeData.Money >= config.Grades1.Cost 
                                     && config.Grades1.Multiplier != data.Upgrade1;
                bool interactable2 = data.Level != 0 
                                     && _runtimeData.Money >= config.Grades2.Cost 
                                     && config.Grades2.Multiplier != data.Upgrade2;
                
                view.Link.SetInteractableLevelUp(interactableLevelUp);
                view.Link.SetInteractableGrade1(interactable1);
                view.Link.SetInteractableGrade2(interactable2);
                
                view.Link.SetNameLevelUp(GameMath.CalculateLevelCost(data, config));
                
                if(config.Grades1.Multiplier == data.Upgrade1)
                    view.Link.SetNameGrade1Bought();
                else
                    view.Link.SetNameGrade1(config.Grades1.Cost, config.Grades1.Multiplier);
                
                if(config.Grades2.Multiplier == data.Upgrade2)
                    view.Link.SetNameGrade2Bought();
                else
                    view.Link.SetNameGrade2(config.Grades2.Cost, config.Grades2.Multiplier);
            }
        }
    }
}