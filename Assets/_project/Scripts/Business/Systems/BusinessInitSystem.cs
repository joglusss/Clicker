using Clicker.Business.Component;
using Clicker.Business.Config;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Business.Systems
{
    public class BusinessInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;
        
        public void Init()
        {
            InitBusiness();
        }

        private void InitBusiness()
        {
            for (int i = 0; i < _staticData.Businesses.Length; i++)
            {
                var business = _staticData.Businesses[i];
                
                EcsEntity entity = _ecsWorld.NewEntity();
                
                entity.Get<BusinessConfig>() =  business;
                
                entity.Get<BusinessComponent>() = new  BusinessComponent()
                {
                    Name = business.Name,
                    Level = i == 0 ? 1 : 0
                };
                
                var view = Object.Instantiate(
                    _staticData.BusinessViewPrefab,
                    _sceneData.BusinessContainer);

                entity.Get<BusinessViewComponent>().Link = view;
            }
        }
    }
}