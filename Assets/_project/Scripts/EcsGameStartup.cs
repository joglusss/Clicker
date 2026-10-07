using Clicker.Business.Systems;
using Clicker.Common.View;
using Clicker.Data;
using Clicker.Save.Systems;
using UnityEngine;
using Leopotam.Ecs;

namespace Clicker
{
    public class EcsGameStartup : MonoBehaviour
    {
        [SerializeField] StaticData _staticData;
        [SerializeField] SceneData _sceneData;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        
        public void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        
            AddInjections();
            AddOneFrames();
            AddSystems();
        
            _systems.Init();
        }

        public void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if (_systems == null) return;
        
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }

        private void AddSystems()
        {
            _systems
                .Add(new BusinessInitSystem())
                .Add(new CommonViewInitSystem())
                .Add(new BusinessViewSystem())
                .Add(new BusinessInputSystem())
                .Add(new BusinessSystem())
                .Add(new CommonViewSystem())
                .Add(new SaveSystem());
        }

        private void AddInjections()
        {
            _systems
                .Inject(_staticData)
                .Inject(_sceneData)
                .Inject(new RuntimeData());
        }

        private void AddOneFrames()
        {
        }
    }
}

