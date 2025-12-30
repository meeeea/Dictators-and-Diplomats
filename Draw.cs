using System.Text;

class Draw
{
    public static bool BMPDraw(BitMap<Color> bitMap, string fileName = "output")
    {
//        try
//        {
            using (FileStream fs = File.Create($"./Pics/{fileName}.BMP"))
            {
                byte[] fileLen = BitConverter.GetBytes(
                    54 + bitMap.height * (3 * bitMap.width + (4 - ( 3 * bitMap.width % 4))));

                // Begin File Header

                // BM (header tells device file type)
                fs.Write([66, 77]);
                // File Length
                fs.Write(fileLen);
                // useless, leave 0s for now, can be anything.
                fs.Write([0, 0, 0, 0]);
                // bit packet offset.
                fs.Write([54, 0, 0, 0]);


                // Begin BID Header. Will be using BITMAPINFOHEADER

                // size of BID Header
                fs.Write([40, 0, 0, 0]);
                // Width of bitmap
                fs.Write(BitConverter.GetBytes(bitMap.width));
                // Height of bitmap
                fs.Write(BitConverter.GetBytes(bitMap.height));
                // Must be this, IDK
                fs.Write([1, 0]);
                // Pixel Quality
                fs.Write([24, 0]);
                // compression method (None)
                fs.Write([0, 0, 0, 0]);
                // the image size (0 if compression is None)
                fs.Write([16, 0, 0, 0]);
                // pixels per meter (width, height)
                fs.Write(BitConverter.GetBytes(2835));
                fs.Write(BitConverter.GetBytes(2835));
                // color pallet and "Important colors
                fs.Write([0, 0, 0, 0]);
                fs.Write([0, 0, 0, 0]);


                // pixel map
                int buffer = 4 - (3 * bitMap.width % 4);

                for (int i = 0; i < bitMap.height; i++)
                {
                    for (int k = 0; k < bitMap.width; k++)
                    {
                        fs.Write(bitMap[k, i].RGB());                        
                    }
                    if (buffer != 4)
                    {
                        for (int j = 0; j < buffer; j++)
                        {
                            fs.Write([0]);
                        }
                    }
                }
            }
//        }
//        catch (Exception E)
//        {
//            Console.WriteLine($"Exception {E.Message}");
//        }



        return false;
    }

    public class Color
    {
        private byte _red;
        private byte _green;
        private byte _blue;

        public uint r => _red;
        public uint g => _green;
        public uint b => _blue;


        public Color()
        {
            _red = 0;
            _green = 0;
            _blue = 0;
        }

        public Color(byte Value)
        {
            if (Value > 255 || Value < 0)
            {
                throw new Exception();
            }

            _red = Value;
            _green = Value;
            _blue = Value;
        }

        public Color(byte Red, byte Green = 0, byte Blue = 0)
        {
            if (Red < 0 || Red > 255)
            {
                throw new Exception();
            }
            if (Green < 0 || Green > 255)
            {
                throw new Exception();
            }
            if (Blue < 0 || Blue > 255)
            {
                throw new Exception();
            }

            _red = Red;
            _green = Green;
            _blue = Blue;
        }

        public byte[] RGB()
        {
            return new byte[] {_blue, _green, _red};
        }
    }

    public class BitMap<T> 
    where T : new()
    {
        private int _width;
        private int _height;
        public int width => _width;
        public int height => _height;
        private T[][] pixels;

        public T this[int x, int y] 
        {
            get => pixels[x][y];
            set => pixels[x][y] = value;
        }

        public BitMap(int width, int height) {
            _width = width;
            _height = height;

            pixels = new T[width][];

            for (int i = 0; i < width; i++)
            {
                pixels[i] = new T[height];
                for (int k = 0; k < height; k++)
                {
                    pixels[i][k] = new T();
                }
            }
        }

    }
}