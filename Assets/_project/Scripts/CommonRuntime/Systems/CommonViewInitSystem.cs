using Clicker.Common.Component;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Common.View
{
    public class CommonViewInitSystem : IEcsInitSystem
    {
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _world;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            
            var view = Object.Instantiate(_staticData.CommonViewPrefab, _sceneData.Canvas);
            entity.Get<CommonViewComponent>() = new CommonViewComponent()
            {
                Link = view
            };
        }
    }
}