using Clicker.Business.Component;
using Clicker.Business.Config;
using Leopotam.Ecs;

namespace Clicker.Business.Systems
{
    public class BusinessInputSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, BusinessViewComponent, BusinessConfig> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var businessComponent = ref _filter.Get1(i);
                ref var businessViewComponent = ref _filter.Get2(i);
                
                if (businessViewComponent.Link.ConsumeLevelUpClick() && businessComponent.AvailableLevelUp)
                {
                    _filter.GetEntity(i).Get<BusinessUpgradeEvent>() =
                        new BusinessUpgradeEvent() { Type = UpgradeType.Level };
                }

                if (businessViewComponent.Link.ConsumeUpgrade1Click() && businessComponent.AvailableUpgrade1)
                {
                    _filter.GetEntity(i).Get<BusinessUpgradeEvent>() =
                        new BusinessUpgradeEvent() { Type = UpgradeType.Upgrade1 };
                }
                
                if (businessViewComponent.Link.ConsumeUpgrade2Click() && businessComponent.AvailableUpgrade2)
                {
                    _filter.GetEntity(i).Get<BusinessUpgradeEvent>() =
                        new BusinessUpgradeEvent() { Type = UpgradeType.Upgrade2 };
                }
            }
        }
    }
}