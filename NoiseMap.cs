using System.Numerics;

class NoiseMap
{
    private int _width;
    private int _height;

    public int width => _width;
    public int height => _height;

    private float _minValue;
    private float _maxValue;
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