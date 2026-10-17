namespace Clicker.Business.Component
{
    [System.Serializable]
    public struct BusinessComponent
    {
        public string Name;
        public int Level;
        public int Upgrade1;
        public int Upgrade2;
        public float Process;
        public bool AvailableLevelUp;
        public bool AvailableUpgrade1;
        public bool AvailableUpgrade2;
    }
}