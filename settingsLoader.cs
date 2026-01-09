public class SettingsLoader
{
    public static List<Settings> settingsList = new();



    public class Settings
    {
        private string _name = "Name Not Loaded";
        public string name => _name;
        private int _mapWidth;
        private int _mapHeight;
        public int mapWidth => _mapWidth;
        public int mapHeight => _mapHeight;
        private List<NoiseMapSettings> Maps = new();



        public class NoiseMapSettings
        {
            private string _name = "Name Not Loaded";
            public string name => _name;
            private float _freqx;
            private float _freqy;
            public float freqx => _freqx;
            public float freqy => _freqy;

            private int _amplitude;
            public int amplitude => amplitude;


        }
    }
}