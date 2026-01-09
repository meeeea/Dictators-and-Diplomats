using System.Text;

class Draw
{
    public static bool BMPDraw(Bitmap noiseMap, string fileName = "output")
    {
//        try
//        {
            using (FileStream fs = File.Create($"./Pics/{fileName}.BMP"))
            {
                byte[] fileLen = BitConverter.GetBytes(
                    54 + noiseMap.height * (3 * noiseMap.width + (4 - ( 3 * noiseMap.width % 4))));

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
                fs.Write(BitConverter.GetBytes(noiseMap.width));
                // Height of bitmap
                fs.Write(BitConverter.GetBytes(noiseMap.height));
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
                int buffer = 4 - (3 * noiseMap.width % 4);

                for (int i = 0; i < noiseMap.height; i++)
                {
                    for (int k = 0; k < noiseMap.width; k++)
                    {
                        fs.Write(noiseMap[k, i].RGB());                        
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

    public class Bitmap
    {

        public static implicit operator Bitmap(NoiseMap noiseMap)
        {
            
            Draw.Bitmap Bitmap = new Draw.Bitmap(noiseMap.width, noiseMap.height);

        float scale = 255 / (noiseMap.maxValue - noiseMap.minValue);

        int index = 0;
        for (int i = 0; i < noiseMap.width; i++)
        {
            for (int k = 0; k < noiseMap.height; k++)
            {
                Bitmap[i, k] = new Draw.Color((byte) (int) Math.Floor((noiseMap[i, k] - noiseMap.minValue)  * scale));
                index++;
            }
        }


        return Bitmap;
        }


        private int _width;
        private int _height;

        public int width => _width;
        public int height => _height;

        private Color[][] Map;

        public Color this[int x, int y] 
        {
            get => Map[x][y];
            set => Map[x][y] = value;
        }

        public Bitmap(int width, int height) {
            _width = width;
            _height = height;

            Map = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                Map[i] = new Color[height];
                for (int k = 0; k < height; k++)
                {
                    Map[i][k] = new Color();
                }
            }
        }
    }
}