using System.Security.Cryptography.X509Certificates;

class MapSet
{
    private string _name = "";
    public string name => _name;
    private int _width;
    public int width => _width;
    private int _height;
    public int height => _height;
    private List<NoiseMap> Maps = new();
    public NoiseMap this[int index] => Maps[index];

    public MapSet(SettingsLoader.Settings? settings)
    {
        if (settings == null)
        {
            return;
        }

        _name = settings.name;
        _width = settings.mapWidth;
        _height = settings.mapWidth;
        for (int i = 0; i > settings.maps.Count; i++)
        {
            Maps.Add(new NoiseMap(width, height, settings.maps[i]));

            
        }
    }

    public class NoiseMap
    {
        private string _name = "";
        public string name => _name;
        private int _width;
        private int _height;

        public int width => _width;
        public int height => _height;

        private float _minValue = 0;
        private float _maxValue = 0;
        public float minValue => _minValue;
        public float maxValue => _maxValue;

        private float[][] Map;

        public float this[int x, int y] 
        {
            get => Map[x][y];
            set => Map[x][y] = value;
        }

        public NoiseMap(int width, int height) {
            _width = width;
            _height = height;

            Map = new float[width][];
            for (int i = 0; i < width; i++)
            {
                Map[i] = new float[height];
                for (int k = 0; k < height; k++)
                {
                    Map[i][k] = new float();
                }
            }
        }

                    #pragma warning disable CS8618 // make it shut up about "Map" field not being a thing when it is.
        public NoiseMap(int width, int height, SettingsLoader.Settings.NoiseMapSettings mapSettings) =>
            new NoiseMap(new PerlinMap(width, height, mapSettings.freqx, mapSettings.freqy, mapSettings.amplitude), mapSettings.name);
                    #pragma warning restore CS8618 

        public NoiseMap(PerlinMap perlinMap, string Name = "") {
            _width = perlinMap.width;
            _height = perlinMap.height;

            int index = 0;
            Map = new float[width][];
            for (int i = 0; i < width; i++)
            {
                Map[i] = new float[height];
                for (int k = 0; k < height; k++)
                {
                    Map[i][k] = perlinMap[index];
                    index++;
                }
            }
            findMinMaxValues();
        }

        private void findMinMaxValues()
        {
            _minValue = Map[0][0];
            _maxValue = Map[0][0];

            for (int i = 0; i < _width; i++)
            {
                for (int k = 0; k < _height; k++)
                {
                    if (Map[0][0] > _maxValue)
                    {
                        _maxValue = Map[0][0];
                    }
                    if (Map[0][0] < _minValue)
                    {
                        _minValue = Map[0][0];
                    }
                }
            }
        }

    }
}