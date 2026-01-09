class PerlinMap
{
    private int _width;
    private int _height;

    public int width => _width;
    public int height => _height;

    // length of map array
    private int mapsize;

    private float[] perlinMap;
    public float this[int i] => perlinMap[i];

    private float xFreq;
    private float yFreq;
    private float _amplitude;

    public float maxValue;
    public float minValue;

    public static implicit operator Draw.Bitmap(PerlinMap Map)
    {
        Draw.Bitmap Bitmap = new Draw.Bitmap(Map.width, Map.height);

        float scale = 255 / (Map.maxValue - Map.minValue);

        int index = 0;
        for (int i = 0; i < Map.width; i++)
        {
            for (int k = 0; k < Map.height; k++)
            {
                Bitmap[i, k] = new Draw.Color((byte) (int) Math.Floor((Map.perlinMap[index] - Map.minValue)  * scale));
                index++;
            }
        }


        return Bitmap;
    }


    public PerlinMap(int Width, int Height, float freqx = (float) 10, float freqy = (float) 10, float amplitude = (float) 1)
    {
        _width = Width;
        _height = Height;
        mapsize = _width * _height;
        perlinMap = new float[mapsize];

        xFreq = freqx / width;
        yFreq = freqy / height;
        _amplitude = amplitude;

        float[] xCords = new float[mapsize];
        float[] yCords = new float[mapsize];
        int index = 0;
        for (int i = 0; i < _width; i++)
        {
            for (int k = 0; k < _height; k++)
            {
                xCords[index] = i;
                yCords[index] = k;
                index++;
            }
        }

        NoiseDotNet.Noise.GradientNoise2D(
            xCoords: xCords,
            yCoords: yCords,
            output: perlinMap,
            xFreq: xFreq,
            yFreq: yFreq,
            amplitude: _amplitude,
            (int) new Random().NextInt64()
        );

        findMinMaxValues();
    }

    private void findMinMaxValues()
    {
        minValue = perlinMap[0];
        maxValue = perlinMap[0];

        for (int i = 1; i < mapsize; i++)
        {
            if (perlinMap[i] > maxValue)
            {
                maxValue = perlinMap[i];
            }
            if (perlinMap[i] < minValue)
            {
                minValue = perlinMap[i];
            }
        }
    }
}