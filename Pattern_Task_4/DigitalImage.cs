using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
namespace PatternTask3
{
    class DigitalImage
    {
        
            public byte[][] pixels;
            public byte label;
            public int[] oneDpixels;
            public Matrix<double> vector;//= Matrix<double>.Build.Dense(28 * 28, 1);
            public DigitalImage(byte[][] pixels,
              byte label)
            {
                this.pixels = new byte[28][];
                this.oneDpixels = new int[28 * 28];
                this.vector = Matrix<double>.Build.Dense(28 * 28, 1);
                for (int i = 0; i < this.pixels.Length; ++i)
                    this.pixels[i] = new byte[28];
                int accforonedpixels=0;
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        this.pixels[i][j] = pixels[i][j];
                        this.oneDpixels[accforonedpixels++] = (int)pixels[i][j];
                        double s1 = pixels[i][j];
                        if (s1 == 255)
                        {
                            s1 = 2;
                        }
                        else
                        {

                            s1 = -2;

                        }
                        this.vector[(i*28+j), 0] = (double)s1;
                    }
                }    

                this.label = label;
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        if (this.pixels[i][j] == 0)
                            s += " "; // white
                        else if (this.pixels[i][j] == 255)
                            s += "O"; // black
                        else
                            s += "."; // gray
                    }
                    s += "\n";
                }
                s += this.label.ToString();
                return s;
            } // ToString

        }

    
}
