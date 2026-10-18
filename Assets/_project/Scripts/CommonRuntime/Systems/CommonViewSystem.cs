using Clicker.Common.Component;
using Clicker.Data;
using Leopotam.Ecs;

namespace Clicker.Common.View
{
    public class CommonViewSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData;
        private EcsFilter<CommonViewComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var view = ref _filter.Get1(i);

                if (view.LastMoney != _runtimeData.Money)
                {
                    view.Link.SetMoney(_runtimeData.Money);
                    view.LastMoney = _runtimeData.Money;   
                }
            }
        }
    }
}

