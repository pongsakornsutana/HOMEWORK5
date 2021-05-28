using System;

namespace home5555
{
    class Program
    {
            static void Main(string[] args)
            {
                Console.Write("Address of the imported image data file. : ");
            string inputFileimage = Console.ReadLine(); 
            Console.WriteLine("InputFile");
                double[,] imageDataInputFile = ReadImageDataFromFile(inputFileimage);

                Console.Write("Address of the Convolution Kernel data file. : ");
                string DataFileConvolutionKernel = Console.ReadLine(); 

         
            Console.WriteLine("DataFileConvolutionKernel");
                double[,] imageDataConvolution = ReadImageDataFromFile(DataFileConvolutionKernel);

                Console.Write("Address of the output image data file. : ");
            string DataFileOutput = Console.ReadLine(); 


            double[,] Repeated_textureArray = new double[7, 7];
                Console.WriteLine("Repeated texture");
                for (int i = 0; i <= 6; i++)
                {
                    int newi = ((i - 1) + 5) % 5;
                    for (int j = 0; j <= 6; j++)
                    {
                        int newj = ((j - 1) + 5) % 5;
                        Repeated_textureArray[i, j] = imageDataInputFile[newi, newj];
                        Console.Write("{0}  ", imageDataInputFile[newi, newj]);
                    }
                    Console.WriteLine();
                }

                double[,] ConvolutionKernelArray = new double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        ConvolutionKernelArray[i, j] = imageDataConvolution[i, j];
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Convolve");
                double[,] DataFileOutputArray = Convolve(Repeated_textureArray, ConvolutionKernelArray);

                WriteImageDataToFile(DataFileOutput, DataFileOutputArray);
            }
            static double[,] Convolve(double[,] repeated_textureArray, double[,] convolutionKernelArray)
            {

                double[,] dataFileOutputArray = new double[5, 5];
                for (int i = 0; i < 5; i++)
                {

                    int repeated_Row1 = i * 1; int repeated_Row2 = i + 1; int repeated_Row3 = i + 2;
                    for (int j = 0; j < 5; j++)
                    {
                        int repeated_column1 = j * 1; int repeated_column2 = j + 1; int repeated_column3 = j + 2;
                        dataFileOutputArray[i, j] = (repeated_textureArray[repeated_Row1, repeated_column1] * convolutionKernelArray[0, 0])
                            + (repeated_textureArray[repeated_Row1, repeated_column2] * convolutionKernelArray[0, 1])
                            + (repeated_textureArray[repeated_Row1, repeated_column3] * convolutionKernelArray[0, 2])
                            + (repeated_textureArray[repeated_Row2, repeated_column1] * convolutionKernelArray[1, 0])
                            + (repeated_textureArray[repeated_Row2, repeated_column2] * convolutionKernelArray[1, 1])
                            + (repeated_textureArray[repeated_Row2, repeated_column3] * convolutionKernelArray[1, 2])
                            + (repeated_textureArray[repeated_Row3, repeated_column1] * convolutionKernelArray[2, 0])
                            + (repeated_textureArray[repeated_Row3, repeated_column2] * convolutionKernelArray[2, 1])
                            + (repeated_textureArray[repeated_Row3, repeated_column3] * convolutionKernelArray[2, 2]);

                        Console.Write("{0}    ", dataFileOutputArray[i, j]);
                    }
                    Console.WriteLine();
                }
                return dataFileOutputArray;
            }
            static double[,] ReadImageDataFromFile(string imageDataFilePath)
            {
                string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
                int imageHeight = lines.Length;
                int imageWidth = lines[0].Split(',').Length;
                double[,] imageDataArray = new double[imageHeight, imageWidth];

                for (int i = 0; i < imageHeight; i++)
                {
                    string[] items = lines[i].Split(',');
                    for (int j = 0; j < imageWidth; j++)
                    {
                        imageDataArray[i, j] = double.Parse(items[j]);
                        Console.Write("{0}  ", imageDataArray[i, j]);
                    }
                    Console.WriteLine();
                }
                return imageDataArray;
            }
            static void WriteImageDataToFile(string imageDataFilePath, double[,] imageDataArray)
            {
                string imageDataString = "";
                for (int i = 0; i < imageDataArray.GetLength(0); i++)
                {
                    for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                    {
                        imageDataString += imageDataArray[i, j] + ", ";
                    }
                    imageDataString += imageDataArray[i, imageDataArray.GetLength(1) - 1];
                    imageDataString += "\n";

                }
                System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
            }
        }
}
