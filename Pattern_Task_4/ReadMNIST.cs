using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
namespace PatternTask3
{
    class ReadMNIST
    {

        int NormalizationLimite = 155;
        public ArrayList TrainData(int NumTrainingSet)
        {
           
            FileStream ifsLabels =
            new FileStream(@"Train\train-labels.idx1-ubyte",
            FileMode.Open); // Trainning labels
            FileStream ifsImages =
             new FileStream(@"Train\train-images.idx3-ubyte",
             FileMode.Open); // Trainning images
           
            
            
            
            BinaryReader brLabels = new BinaryReader(ifsLabels);
            BinaryReader brImages = new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            ArrayList Images = new ArrayList();

            // each test image
            for (int di = 0; di < NumTrainingSet; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        if (b < NormalizationLimite)
                        {
                            b = 0;
                        }
                        else
                        {
                            b = 255;
                        }
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();
                DigitalImage dImage = new DigitalImage(pixels, lbl);
                Images.Add(dImage);
            }
            return Images;
        }




        public ArrayList TestData(int NumTestingSet)
        {

            FileStream ifsLabels =
            new FileStream(@"Test\t10k-labels.idx1-ubyte",
            FileMode.Open); // Trainning labels
            FileStream ifsImages =
             new FileStream(@"Test\t10k-images.idx3-ubyte",
             FileMode.Open); // Trainning images




            BinaryReader brLabels = new BinaryReader(ifsLabels);
            BinaryReader brImages = new BinaryReader(ifsImages);


            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            ArrayList Images = new ArrayList();

            // each test image
            for (int di = 0; di < NumTestingSet; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        if (b < NormalizationLimite)
                        {
                            b = 0;
                        }
                        else
                        {
                            b = 255;
                        }
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();
                DigitalImage dImage = new DigitalImage(pixels, lbl);
                Images.Add(dImage);
            }
            return Images;
        }




    }
}
