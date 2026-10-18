using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clicker.Business.Component;
using Clicker.Data;
using Leopotam.Ecs;
using Newtonsoft.Json;
using UnityEngine;

namespace Clicker.Save.Systems
{
    public class SaveSystem : IEcsRunSystem, IEcsDestroySystem, IEcsInitSystem
    {
        private EcsFilter<BusinessComponent> _businessFilter;
        private EcsFilter<SaveEvent> _saveEventFilter;
        private StaticData _staticData;
        private RuntimeData _data;

        private static string SavePath => Path.Combine(
            Application.persistentDataPath,
            "Save",
            "save.json"
            );
        
        private float _lastSaveTime;
        
        public void Init()
        {
            _lastSaveTime = Time.realtimeSinceStartup;
            
            if (Load(out SaveData save))
            {
                _data.Money = save.Money;
                
                foreach (var i in _businessFilter)
                {
                    ref var component = ref _businessFilter.Get1(i);

                    BusinessComponent businessComponent = component;
                    try
                    {
                        component = save.Components
                            .Select(SaveDataToBusinessComponent)
                            .First(x => x.Name == businessComponent.Name);
                    }
                    catch
                    {
                        Debug.LogWarning($"Saved component not found: {businessComponent.Name}"); 
                    }
                }
            }
        }
        
        public void Run()
        {
           var currentTime = Time.realtimeSinceStartup;
           if (currentTime - _lastSaveTime > _staticData.AutoSaveInterval)
           {
               _lastSaveTime = currentTime;
               Save();
           }
           else if (!_saveEventFilter.IsEmpty())
           {
               Save();
           }
        }

        public void Destroy() => Save();
        
        private bool Load(out SaveData save)
        {
            save = new SaveData();
            
            if (!File.Exists(SavePath))
                return false;
            
            try
            {
                string json = File.ReadAllText(SavePath);
                save = JsonConvert.DeserializeObject<SaveData>(json);
            }
            catch 
            {
                return false;
            }
           
            
            return true;
        }

        private void Save()
        {
            List<BusinessComponent> list = new();
            
            foreach (var i in _businessFilter)
            {
                list.Add(_businessFilter.Get1(i)); 
            }

            SaveData data = new()
            {
                Money = _data.Money,
                Components = list.Select(BusinessComponentToSaveData).ToArray()
            };
            
            string json = JsonConvert.SerializeObject(data);
            
            Directory.CreateDirectory(Path.GetDirectoryName(SavePath) ?? "");
            
            File.WriteAllText(SavePath, json);
        }

        private BusinessComponent SaveDataToBusinessComponent(SaveBusinessData data)
        {
            return new BusinessComponent()
            {
                Name = data.Name,
                Level = data.Level,
                Upgraded1 = data.Upgraded1,
                Upgraded2 = data.Upgraded2,
                Process = data.Process
            };
        }

        private SaveBusinessData BusinessComponentToSaveData(BusinessComponent businessComponent)
        {
            return new SaveBusinessData()
            {
                Name = businessComponent.Name,
                Level = businessComponent.Level,
                Upgraded1 = businessComponent.Upgraded1,
                Upgraded2 = businessComponent.Upgraded2,
                Process = businessComponent.Process
            };
        }
    }
}