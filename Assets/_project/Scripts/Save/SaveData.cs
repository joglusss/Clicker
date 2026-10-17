using Clicker.Business.Component;

namespace Clicker.Save
{
    [System.Serializable]
    public struct SaveData
    {
        public int Money;
        public SaveBusinessData[] Components;
    }
}