using System.Text;

class Draw
{
    public static bool BMPDraw(NoiseMap<Color> noiseMap, string fileName = "output")
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

    public class Bitmap : NoiseMap<Color>
    {
        public Bitmap(int width, int height) : base(width, height)
        {
            
            
        }
    }
}