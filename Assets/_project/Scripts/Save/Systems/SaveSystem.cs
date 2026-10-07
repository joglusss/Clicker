using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clicker.Business.Component;
using Clicker.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Clicker.Save.Systems
{
    public class SaveSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private EcsFilter<BusinessComponent> _filter;
        private RuntimeData _data;

        private static string SavePath => Path.Combine(
            Application.dataPath,
            "Save",
            "save.json"
            );
        
        public void Init()
        {
            if (Load(out SaveData save))
            {
                _data.Money = save.Money;
                
                foreach (var i in _filter)
                {
                    ref var component = ref _filter.Get1(i);

                    BusinessComponent businessComponent = component;
                    component = save.Components.Single(x => x.Name == businessComponent.Name);
                }
            }
        }
        
        public void Destroy()
        {
            List<BusinessComponent> list = new();
            
            foreach (var i in _filter)
            {
                list.Add(_filter.Get1(i)); 
            }

            SaveData save = new()
            {
                Money = _data.Money,
                Components = list.ToArray()
            };

            Save(save);
        }

        private bool Load(out SaveData save)
        {
            save = new SaveData();
            
            if (!File.Exists(SavePath))
                return false;

            string json = File.ReadAllText(SavePath);
            
            save = JsonUtility.FromJson<SaveData>(json);

            return true;
        }

        private void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

 
    }
}