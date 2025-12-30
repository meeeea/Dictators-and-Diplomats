class PerlinMap
{
    private int _width;
    private int _height;

    public int width => _width;
    public int height => _height;

    // length of map array
    private int mapsize;

    private float[] perlinMap;

    private float xFreq;
    private float yFreq;
    private float _amplitude;

    public float maxValue;
    public float minValue;


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
        maxValue = perlinMap[1];

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


    public Draw.BitMap<Draw.Color> ToBitMap()
    {
        Draw.BitMap<Draw.Color> bitMap = new Draw.BitMap<Draw.Color>(width, height);

        float scale = 255 / (maxValue - minValue);

        int index = 0;
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                //bitMap[i, k] = new Draw.Color((byte) i, 0, (byte) k);
                bitMap[i, k] = new Draw.Color((byte) (int) Math.Floor((perlinMap[index] - minValue)  * scale));
                index++;
            }
        }


        return bitMap;
    }
}