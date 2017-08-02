using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using System.Collections;

namespace PatternTask3
{
     class Classifier
    {

        public AllClasses seperatedclassesForTraining = new AllClasses();
        public AllClasses seperatedclassesfortesting = new AllClasses();
        double[] accfordiagonalvalues = new double[10];
        static int[] NumOfClasses = new int[10];
        static double[] Det = new double[10];
        double[] prior = new double[10];
        public int[,] confusionMatrix = new int[10, 10];
        Matrix<double> x = Matrix<double>.Build.Dense(28 * 28, 1);
        double[] likelihood = new double[10];
        double[] posterior = new double[10];
        double evidence = 0;
        double max_posterior;
        int classID = 0;
        double B_h = 2;
        double B_l = -2;
        int th = 0;
     
        Matrix<double> MU1 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU2 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU3 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU4 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU5 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU6 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU7 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU8 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU9 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> MU10 = Matrix<double>.Build.Dense(28 * 28, 1);

        Matrix<double> CovarianceMatrix1 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix2 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix3 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix4 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix5 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix6 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix7 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix8 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix9 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> CovarianceMatrix10 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);


        Matrix<double> InvCovarianceMatrix1 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix2 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix3 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix4 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix5 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix6 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix7 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix8 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix9 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
        Matrix<double> InvCovarianceMatrix10 = Matrix<double>.Build.Dense(28 * 28, 28 * 28);

       
        public void Testing()
        {
            
            for (int i = 0; i < 10; i++)
            {
              Testing_S(i);

            }
        
        }

        public bool Load_Trainig_Data(int NumTrainingSet)
        {
           // int NumTrainingSet = 60000;
            
            ReadMNIST trainningdata = new ReadMNIST();
            ArrayList ImagesFortraining = trainningdata.TrainData(NumTrainingSet);
            seperatedclassesForTraining.FillClasses(ImagesFortraining, NumTrainingSet);

            calculatenumofTraining();

            return true;
        
        }

        private void calculatenumofTraining()
        {

            

            for (int i = 0; i < 10; i++)
            {
                int N = ((ArrayList)seperatedclassesForTraining.All[i]).Count;
                NumOfClasses[i] = N;

            }

        }

        public void Load_Testing_Data(int NumTestingSet)
        {
            //int NumTestingSet = 10000;
            
            ReadMNIST testdata = new ReadMNIST();
            ArrayList Imagesfotesting = testdata.TestData(NumTestingSet);

            seperatedclassesfortesting.FillClasses(Imagesfotesting, NumTestingSet);

        }

        public void MU()
        {

            int Max = NumOfClasses.Max();

            for (int i = 0; i < Max; i++)
            {
                for (int j = 0; j < 28 * 28; j++)
                {


                    if (i < NumOfClasses[0])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[0])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }


                        MU1[j, 0] += s1;
                    }


                    if (i < NumOfClasses[1])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[1])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU2[j, 0] += s1;
                    }


                    if (i < NumOfClasses[2])
                    {

                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[2])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU3[j, 0] += s1;


                    }


                    if (i < NumOfClasses[3])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[3])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU4[j, 0] += s1;

                    }

                    if (i < NumOfClasses[4])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[4])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU5[j, 0] += s1;
                    }


                    if (i < NumOfClasses[5])
                    {

                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[5])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU6[j, 0] += s1;


                    }


                    if (i < NumOfClasses[6])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[6])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU7[j, 0] += s1;
                    }


                    if (i < NumOfClasses[7])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[7])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU8[j, 0] += s1;

                    }

                    if (i < NumOfClasses[8])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[8])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU9[j, 0] += s1;

                    }

                    if (i < NumOfClasses[9])
                    {

                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[9])[i]).oneDpixels[j];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        MU10[j, 0] += s1;

                    }

                }
            }


            for (int i = 0; i < 28 * 28; i++)
            {
                MU1[i, 0] = MU1[i, 0] / NumOfClasses[0];
                MU2[i, 0] = MU2[i, 0] / NumOfClasses[1];
                MU3[i, 0] = MU3[i, 0] / NumOfClasses[2];
                MU4[i, 0] = MU4[i, 0] / NumOfClasses[3];
                MU5[i, 0] = MU5[i, 0] / NumOfClasses[4];
                MU6[i, 0] = MU6[i, 0] / NumOfClasses[5];
                MU7[i, 0] = MU7[i, 0] / NumOfClasses[6];
                MU8[i, 0] = MU8[i, 0] / NumOfClasses[7];
                MU9[i, 0] = MU9[i, 0] / NumOfClasses[8];
                MU10[i, 0] = MU10[i, 0] / NumOfClasses[9];

            }

        }

        public void Sigma()
        {
            int Max = NumOfClasses.Max();

            for (int i = 0; i < 28 * 28; i++)
            {
                for (int k = 0; k < Max; k++)
                {

                    if (k < NumOfClasses[0])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[0])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix1[i, i] += (s1 - MU1[i, 0]) * (s1 - MU1[i, 0]);

                    }

                    if (k < NumOfClasses[1])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[1])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix2[i, i] += (s1 - MU2[i, 0]) * (s1 - MU2[i, 0]);

                    }

                    if (k < NumOfClasses[2])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[2])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix3[i, i] += (s1 - MU3[i, 0]) * (s1 - MU3[i, 0]);

                    }

                    if (k < NumOfClasses[3])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[3])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix4[i, i] += (s1 - MU4[i, 0]) * (s1 - MU4[i, 0]);

                    }


                    if (k < NumOfClasses[4])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[4])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix5[i, i] += (s1 - MU5[i, 0]) * (s1 - MU5[i, 0]);

                    }


                    if (k < NumOfClasses[5])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[5])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix6[i, i] += (s1 - MU6[i, 0]) * (s1 - MU6[i, 0]);

                    }


                    if (k < NumOfClasses[6])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[6])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix7[i, i] += (s1 - MU7[i, 0]) * (s1 - MU7[i, 0]);

                    }

                    if (k < NumOfClasses[7])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[7])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix8[i, i] += (s1 - MU8[i, 0]) * (s1 - MU8[i, 0]);

                    }


                    if (k < NumOfClasses[8])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[8])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix9[i, i] += (s1 - MU9[i, 0]) * (s1 - MU9[i, 0]);

                    }

                    if (k < NumOfClasses[9])
                    {
                        double s1 = ((DigitalImage)((ArrayList)seperatedclassesForTraining.All[9])[k]).oneDpixels[i];
                        if (s1 == 255)
                        {
                            s1 = B_h;
                        }
                        else
                        {

                            s1 = B_l;

                        }
                        CovarianceMatrix10[i, i] += (s1 - MU10[i, 0]) * (s1 - MU10[i, 0]);

                    }

                }
            }



            for (int i = 0; i < 28 * 28; i++)
            {

                CovarianceMatrix1[i, i] = CovarianceMatrix1[i, i] / NumOfClasses[0];
                CovarianceMatrix2[i, i] = CovarianceMatrix2[i, i] / NumOfClasses[1];
                CovarianceMatrix3[i, i] = CovarianceMatrix3[i, i] / NumOfClasses[2];
                CovarianceMatrix4[i, i] = CovarianceMatrix4[i, i] / NumOfClasses[3];
                CovarianceMatrix5[i, i] = CovarianceMatrix5[i, i] / NumOfClasses[4];
                CovarianceMatrix6[i, i] = CovarianceMatrix6[i, i] / NumOfClasses[5];
                CovarianceMatrix7[i, i] = CovarianceMatrix7[i, i] / NumOfClasses[6];
                CovarianceMatrix8[i, i] = CovarianceMatrix8[i, i] / NumOfClasses[7];
                CovarianceMatrix9[i, i] = CovarianceMatrix9[i, i] / NumOfClasses[8];
                CovarianceMatrix10[i, i] = CovarianceMatrix10[i, i] / NumOfClasses[9];

            }


        }

        Matrix<double> x1 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x2 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x3 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x4 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x5 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x6 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x7 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x8 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x9 = Matrix<double>.Build.Dense(28 * 28, 1);
        Matrix<double> x10 = Matrix<double>.Build.Dense(28 * 28, 1);
        public void Testing_S(int numclass)
        {

            Matrix<double> x1 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x2 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x3 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x4 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x5 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x6 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x7 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x8 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x9 = Matrix<double>.Build.Dense(28 * 28, 1);
            Matrix<double> x10 = Matrix<double>.Build.Dense(28 * 28, 1);

            int data = ((ArrayList)seperatedclassesfortesting.All[numclass]).Count;



            for (int i = 0; i < data; i++)
            {
                for (int j = 0; j < 28 * 28; j++)
                {
                    double s1 = ((DigitalImage)((ArrayList)seperatedclassesfortesting.All[numclass])[i]).oneDpixels[j];
                    if (s1 == 255)
                    {
                        s1 = B_h;
                    }
                    else
                    {

                        s1 = -B_h;

                    }

                    x[j, 0] = s1;
                }

                x1 = x.Subtract(MU1);
                x2 = x.Subtract(MU2);
                x3 = x.Subtract(MU3);
                x4 = x.Subtract(MU4);
                x5 = x.Subtract(MU5);
                x6 = x.Subtract(MU6);
                x7 = x.Subtract(MU7);
                x8 = x.Subtract(MU8);
                x9 = x.Subtract(MU9);
                x10 = x.Subtract(MU10);

                double X1 = Matrix_mul(x1, InvCovarianceMatrix1);
                double X2 = Matrix_mul(x2, InvCovarianceMatrix2);
                double X3 = Matrix_mul(x3, InvCovarianceMatrix3);
                double X4 = Matrix_mul(x4, InvCovarianceMatrix4);
                double X5 = Matrix_mul(x5, InvCovarianceMatrix5);
                double X6 = Matrix_mul(x6, InvCovarianceMatrix6);
                double X7 = Matrix_mul(x7, InvCovarianceMatrix7);
                double X8 = Matrix_mul(x8, InvCovarianceMatrix8);
                double X9 = Matrix_mul(x9, InvCovarianceMatrix9);
                double X10 = Matrix_mul(x10, InvCovarianceMatrix10);


                likelihood[0] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[0]) / 2)) * Det[0])) * Math.Exp(-(X1 / 2));
                likelihood[1] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[1]) / 2)) * Det[1])) * Math.Exp(-(X2 / 2));
                likelihood[2] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[2]) / 2)) * Det[2])) * Math.Exp(-(X3 / 2));
                likelihood[3] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[3]) / 2)) * Det[3])) * Math.Exp(-(X4 / 2));
                likelihood[4] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[4]) / 2)) * Det[4])) * Math.Exp(-(X5 / 2));
                likelihood[5] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[5]) / 2)) * Det[5])) * Math.Exp(-(X6 / 2));
                likelihood[6] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[6]) / 2)) * Det[6])) * Math.Exp(-(X7 / 2));
                likelihood[7] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[7]) / 2)) * Det[7])) * Math.Exp(-(X8 / 2));
                likelihood[8] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[8]) / 2)) * Det[8])) * Math.Exp(-(X9 / 2));
                likelihood[9] = (1 / (Math.Pow(2 * Math.PI, ((accfordiagonalvalues[9]) / 2)) * Det[9])) * Math.Exp(-(X10 / 2));

                evidence = 0;
                for (int g = 0; g < 10; g++)
                {

                    evidence += likelihood[g] * prior[g];

                }



                for (int k = 0; k < 10; k++)
                {

                    posterior[k] = (likelihood[k] * prior[k]) / evidence;

                }

                max_posterior = posterior.Max();

                if (max_posterior == posterior[0])
                {
                    classID = 0;
                }

                else if (max_posterior == posterior[1])
                {
                    classID = 1;
                }

                else if (max_posterior == posterior[2])
                {
                    classID = 2;
                }

                else if (max_posterior == posterior[3])
                {

                    classID = 3;
                }
                else if (max_posterior == posterior[4])
                {
                    classID = 4;
                }
                else if (max_posterior == posterior[5])
                {
                    classID = 5;
                }
                else if (max_posterior == posterior[6])
                {
                    classID = 6;
                }
                else if (max_posterior == posterior[7])
                {
                    classID = 7;
                }
                else if (max_posterior == posterior[8])
                {
                    classID = 8;
                }
                else if (max_posterior == posterior[9])
                {
                    classID = 9;
                }


                confusionMatrix[numclass, classID] += 1;

            }

        }

        public double Accuracy(int numTestingSet)
        {

            double diagonal = 0; 

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == j)
                    {
                        diagonal += confusionMatrix[i, j];
                    }
                }
            }

            diagonal = (diagonal / numTestingSet) * 100;

            return diagonal;
            
        }

        private double Matrix_mul(Matrix<double> vec, Matrix<double> mat)
        {

            double res = 0;
            for (int i = 0; i < 28 * 28; i++)
                res += vec[i, 0] * vec[i, 0] * mat[i, i];


            return res;
        }
        public void Culculate_all_Det()
        {
 
            Det[0] = determinatediagonal(CovarianceMatrix1,0);
            Det[1] = determinatediagonal(CovarianceMatrix2,1);
            Det[2] = determinatediagonal(CovarianceMatrix3,2);
            Det[3] = determinatediagonal(CovarianceMatrix4,3);
            Det[4] = determinatediagonal(CovarianceMatrix5,4);
            Det[5] = determinatediagonal(CovarianceMatrix6,5);
            Det[6] = determinatediagonal(CovarianceMatrix7,6);
            Det[7] = determinatediagonal(CovarianceMatrix8,7);
            Det[8] = determinatediagonal(CovarianceMatrix9,8);
            Det[9] = determinatediagonal(CovarianceMatrix10,9);

        }

        public void Culculate_all_inv()
        {


            InvCovarianceMatrix1 = inversediagonal(CovarianceMatrix1);
            InvCovarianceMatrix2 = inversediagonal(CovarianceMatrix2);
            InvCovarianceMatrix3 = inversediagonal(CovarianceMatrix3);
            InvCovarianceMatrix4 = inversediagonal(CovarianceMatrix4);
            InvCovarianceMatrix5 = inversediagonal(CovarianceMatrix5);
            InvCovarianceMatrix6 = inversediagonal(CovarianceMatrix6);
            InvCovarianceMatrix7 = inversediagonal(CovarianceMatrix7);
            InvCovarianceMatrix8 = inversediagonal(CovarianceMatrix8);
            InvCovarianceMatrix9 = inversediagonal(CovarianceMatrix9);
            InvCovarianceMatrix10 = inversediagonal(CovarianceMatrix10);
           



        }

        public void Calculate_prior()
        {

            

            for (int g = 0; g < 10; g++)
            {

                double T = (double)NumOfClasses[g];
                double T2 = 60000;

                prior[g] = (double)(T / T2);

            }

        }

        public void calculate_non_zero_dia()
        {

           
            for (int i=0;i<28*28;i++)
            {

                
                if (CovarianceMatrix1[i, i] != th)
                    accfordiagonalvalues[0]++;


                if (CovarianceMatrix2[i, i] != th)
                    accfordiagonalvalues[1]++;

                if (CovarianceMatrix3[i, i] != th)
                    accfordiagonalvalues[2]++;

                if (CovarianceMatrix4[i, i] != th)
                    accfordiagonalvalues[3]++;

                if (CovarianceMatrix5[i, i] != th)
                    accfordiagonalvalues[4]++;

                if (CovarianceMatrix6[i, i] != th)
                    accfordiagonalvalues[5]++;

                if (CovarianceMatrix7[i, i] != th)
                    accfordiagonalvalues[6]++;

                if (CovarianceMatrix8[i, i] != th)
                    accfordiagonalvalues[7]++;

                if (CovarianceMatrix9[i, i] != th)
                    accfordiagonalvalues[8]++;

                if (CovarianceMatrix10[i, i] != th)
                    accfordiagonalvalues[9]++;

            }

            

        }

        private double determinatediagonal(Matrix<double> cov, int covn)
        {

              double multyall = 1;


            for (int i = 0; i < 28 * 28; i++)
            {

                if (cov[i, i] != th)
                {

                    multyall *= Math.Pow(cov[i, i], 0.5);

                }

            }


            return multyall;
        }

        public Matrix<double> inversediagonal(Matrix<double> cov)
        {
            Matrix<double> covtoinv = Matrix<double>.Build.Dense(28 * 28, 28 * 28);
            //  double[,] covtoinv = new double[28 * 28, 28 * 28];
            for (int i = 0; i < 28 * 28; i++)
            {



                if (cov[i, i] != th)
                {
                    covtoinv[i, i] = 1 / cov[i, i];

                }


            }

            return covtoinv;
        }

    }
    
    }



