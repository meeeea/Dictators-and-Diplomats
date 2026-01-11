using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class MapSet
{
    protected string _name = "";
    public string name => _name;
    protected int _width;
    public int width => _width;
    protected int _height;
    public int height => _height;
    protected List<NoiseMap> Maps = new();
    public NoiseMap this[int index] => Maps[index];
    protected int _count = 0;
    public int Count => _count;

    public MapSet(SettingsLoader.Settings? settings)
    {
        if (settings == null)
        {
            return;
        }

        _name = settings.name;
        _width = settings.mapWidth;
        _height = settings.mapWidth;
        for (int i = 0; i < settings.maps.Count; i++)
        {
            Maps.Add(new NoiseMap(width, height, settings.maps[i]));
            _count++;
            if (Maps[i].name == "continentMap")
            {
                Maps.Add(new LandMap(Maps[i]));
                _count++;
            }
        }
    }

    public class NoiseMap
    {
        protected string _name = "";
        public string name => _name;
        protected int _width;
        protected int _height;

        public int width => _width;
        public int height => _height;

        protected float _minValue = 0;
        protected float _maxValue = 0;
        public float minValue => _minValue;
        public float maxValue => _maxValue;

        protected float[][] Map;

        public float this[int x, int y] 
        {
            get => Map[x][y];
            set => Map[x][y] = value;
        }

        public static implicit operator Draw.Bitmap(NoiseMap noiseMap)
        {
            Draw.Bitmap Bitmap = new Draw.Bitmap(noiseMap.width, noiseMap.height);

            noiseMap.findMinMaxValues();

            float scale = 255 / (noiseMap.maxValue - noiseMap.minValue);
            Console.WriteLine(noiseMap.maxValue);

            for (int i = 0; i < noiseMap.width; i++)
            {
                for (int k = 0; k < noiseMap.height; k++)
                {
                    Bitmap[i, k] = new Draw.Color((byte) (int) Math.Floor((noiseMap[i, k] - noiseMap.minValue)  * scale));

                }
            }


            return Bitmap;
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
        public NoiseMap(int width, int height, SettingsLoader.Settings.NoiseMapSettings mapSettings) :
            this(new PerlinMap(width, height, mapSettings.frequencyX, mapSettings.frequencyY, mapSettings.amplitude), mapSettings.name) {}
                    #pragma warning restore CS8618 

        public NoiseMap(PerlinMap perlinMap, string Name = "") {
            _width = perlinMap.width;
            _height = perlinMap.height;

            _name = Name;

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

        public void findMinMaxValues()
        {
            _minValue = Map[0][0];
            _maxValue = Map[0][0];

            for (int i = 0; i < _width; i++)
            {
                for (int k = 0; k < _height; k++)
                {
                    if (Map[i][k] > _maxValue)
                    {
                        _maxValue = Map[i][k];
                    }
                    if (Map[i][k] < _minValue)
                    {
                        _minValue = Map[i][k];
                    }
                }
            }
        }

    }

    public class LandMap : NoiseMap
    {

        public static implicit operator Draw.Bitmap(LandMap noiseMap)
        {
            Draw.Bitmap Bitmap = new Draw.Bitmap(noiseMap.width, noiseMap.height);


            float scale = 255 / noiseMap.maxValue;

            for (int i = 0; i < noiseMap.width; i++)
            {
                for (int k = 0; k < noiseMap.height; k++)
                {
                    Bitmap[i, k] = new Draw.Color((byte) (int) Math.Floor(noiseMap[i, k]  * scale));

                }
            }


            return Bitmap;
        }
        public LandMap(MapSet.NoiseMap continentMap) : base(continentMap.width, continentMap.height)
        {
            _name = continentMap.name;

            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (Map[i][k] < 0)
                    {
                        Map[i][k] = 0;
                    }
                }
            }
        }



    }

}