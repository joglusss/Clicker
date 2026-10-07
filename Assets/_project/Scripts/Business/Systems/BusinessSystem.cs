using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Common;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Business.Systems
{
    public class BusinessSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData;
        private EcsFilter<BusinessComponent, BusinessConfig> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get1(i);
                ref var config = ref _filter.Get2(i);
            
                if(data.Level <= 0) continue;

                data.Process += Time.deltaTime;
                data.Process = Mathf.Clamp(data.Process, 0, config.ProcessDuration);
            
                if (Mathf.Approximately(data.Process, config.ProcessDuration))
                {
                    data.Process = 0;
                    _runtimeData.Money += GameMath.CalculateIncome(data, config);
                }
            }

        }
    }
}