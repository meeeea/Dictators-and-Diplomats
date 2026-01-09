class NoiseMap<T> where T: new()
{
    private int _width;
    private int _height;

    public int width => _width;
    public int height => _height;

    private T[][] Map;

        public T this[int x, int y] 
        {
            get => Map[x][y];
            set => Map[x][y] = value;
        }

    public NoiseMap(int width, int height) {
        _width = width;
        _height = height;

        Map = new T[width][];
        for (int i = 0; i < width; i++)
        {
            Map[i] = new T[height];
            for (int k = 0; k < height; k++)
            {
                Map[i][k] = new T();
            }
        }
    }
    


}