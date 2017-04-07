using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            // This program inputs 2 matrices, then adds them, subtracts them, multiplies them,
            // computes their determinants, and their inverses
            Console.WriteLine("\r\nThis program gets 2 matrices from the user and \r\nperforms the following calculations on them:");
            Console.WriteLine("\r\n\tAdds the matrices\r\n\tSubtracts the matrices\r\n\tMultiplies the matrices\r\n\tFinds the determinants\r\n\tFinds the Inverses\r\n\r\n");
            Console.WriteLine("******************************************************\r\n\r\n");

            int[,,] matrix = new int[7, 10, 10];  // stores 7 matrices A through G
            int[,] dummy = new int[10, 10];    // dummy matrix

            int[] idim = { -1, -1, -1, -1, -1, -1, -1 };           // x-direction dimension of each matrix (i)
            int[] jdim = new int[7];           // y-direction dimension of each matrix (j)

            double[] det = { -999, -999, -999, -999, -999, -999, -999 };      // determinant of each matrix
            string[] name = { "A", "B", "A+B", "A-B", "A*B", "A Inverse", "B Inverse" };


            int by = 0;                             // location of "by" in dimension string
            int int1 = -3, int2 = -3;               // integers used to parse input from user
            string input = "";                      // input entered by user
            string str1 = "", str2 = "";            // strings used to parse input from user
            bool quit = true, valid1 = false, valid2 = false;


            // Get dimensions of Matrices A and B from the user
            for (int i = 0; i <= 1; i++)
            {
                valid1 = false; valid2 = false;
                while (quit && (!valid1 || !valid2))
                {
                    Console.Write("\r\n\r\nEnter the dimensions of Matrix " + name[i] + "  (e.g. 2 by 2):  ");
                    input = Console.ReadLine().Trim().ToLower();
                    by = input.IndexOf("by");
                    if (input == "quit")
                    {
                        quit = false;
                    }
                    else if (input.Length >= 4 && by > 0 && (input.Length - by) >= 3)
                    {
                        str1 = input.Substring(0, by).Trim();
                        valid1 = int.TryParse(str1, out int1);
                        str2 = input.Substring(by + 2, input.Length - (by + 2)).Trim();
                        valid2 = int.TryParse(str2, out int2);
                        if (valid1 && valid2)
                        {
                            idim[i] = int1;
                            jdim[i] = int2;
                        }
                        else
                        {
                            Console.WriteLine("\r\n***** Invalid format:  Enter \"quit\" or dimensions in the format \"2 by 3\".");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\r\n***** Invalid format:  Enter \"quit\" or dimensions in the format \"2 by 3\".");
                    }
                   
                }  // end while

                if (quit) Console.WriteLine("\r\nDimensions of matrix {0} is {1} by {2}.", name[i], idim[i], jdim[i]);

            }  // end for loop


            // populate Matrices A and B from user input
            if (quit)
            {
                for (int k = 0; k < 2; k++)
                {
                    dummy = GetMatrix(k, name, idim[k], jdim[k]);
                    for (int i = 0; i < idim[k]; i++)
                    {
                        for (int j = 0; j < jdim[k]; j++)
                        {
                            matrix[k, i, j] = dummy[i, j];
                        }
                    }
                }

                // print the matrices A and B

                Console.WriteLine("\r\n\r\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();

                for (int k = 0; k <= 1; k++)
                {
                    Console.WriteLine("\r\n\r\nMatrix {0}:  \r\n", name[k]);

                    for (int i = 0; i < idim[k]; i++)
                    {
                        Console.WriteLine();
                        for (int j = 0; j < jdim[k]; j++)
                        {
                            Console.Write("\t{0} ", matrix[k,i, j]);
                        }

                    }
                }
                Console.WriteLine("\r\n\r\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();


                // add matrix A and matrix B and store in matrix C
                // subtract matrix B from matrix A and store in matrix D

                if (idim[0] == idim[1] && jdim[0] == jdim[1])
                {
                    for (int i = 0; i < idim[0]; i++)
                    {
                        for (int j = 0; j < jdim[0]; j++)
                        {
                            matrix[2,i, j] = matrix[0,i, j] + matrix[1,i, j];
                            matrix[3,i, j] = matrix[0,i, j] - matrix[1,i, j];
                            idim[2] = idim[0];
                            idim[3] = idim[0];
                            jdim[2] = jdim[0];
                            jdim[3] = jdim[0];
                        }
                    }
                }  // end if (dimensions match)
                else
                {
                    Console.WriteLine("\r\n\r\nThe matrices cannot be added or subtracted \r\nbecause they have different dimensions!");
                }

                // multiply matrix A * matrix B and store in matrix E

                if (jdim[0] == idim[1])
                {
                    int k = 4;
                    idim[k] = idim[0];
                    jdim[k] = jdim[1];

                    for (int i = 0; i < idim[k]; i++)
                    {
                        for (int j = 0; j < jdim[k]; j++)
                        {
                            matrix[k, i, j] = 0;
                            for (int iajb = 0; iajb < jdim[0]; iajb++)
                            {
                                matrix[k, i, j] = matrix[k, i, j] + matrix[0, i, iajb] * matrix[1, iajb, j];            
                            }
                        }
                    }
                }  // end if (dimensions match)
                else
                {
                    Console.WriteLine("\r\n\r\nThe matrices cannot be multiplied \r\nbecause the # col of A is not equal to the # rows of B.");
                }

                // calculate determinants of 2 x 2 matrix

                for (int k = 0; k < 7; k++)
                {
                    if (idim[k] == jdim[k] && jdim[k] == 2)
                    {
                        det[k] = Det2by2(k,matrix);
                    }
                    if( idim[k] == jdim[k] && jdim[k] == 3)
                    {
                        det[k] = Det3by3(k, matrix);
                    }
                }
                // print all matrices and their determinants
                for (int k = 0; k < 7; k++)
                {
                    if (idim[k] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\r\n\r\nMatrix {0}:", name[k]);
                        for (int i = 0; i < idim[k]; i++)
                        {
                            Console.WriteLine();
                            for (int j = 0; j < jdim[k]; j++)
                            {
                                Console.Write("\t" + matrix[k, i, j]);
                            }
                        }
                        if (det[k] != -999) Console.Write("\tDeterminant = {0}", det[k]);
                    } // end if/else
                } // end nested for loops of i, j, k


            } //end if (quit)

            Console.WriteLine("\r\n\r\nPress any key to exit ...");
            Console.ReadKey();
        } //static main

        static int[,] GetMatrix(int num, string[] matrix, int idim, int jdim)
        {
            // this method prompts for elements of the matrix
            string input= "", input1="";
            bool valid = false, velement = false;
            int value = -1;
            string[] elements = new string[100];
            int[,] array = new int[10,10];
            int k;

            while (!valid)   // quit is not implemented yet in this program
            {
                Console.WriteLine("\r\n\r\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\r\n\r\nEnter Matrix {0} elements:  ", matrix[num]);
                Console.WriteLine("\r\n\r\nExample:     --            --");
                Console.WriteLine("             |              |");
                Console.WriteLine("             |    1    2    |");
                Console.WriteLine("             |              |\t\tis entered as  \"1 2 3 4\"\r\n");
                Console.WriteLine("             |    3    4    |");
                Console.WriteLine("             |              |");
                Console.WriteLine("             --            --_");

                // initialize array
                for (int i=0; i<idim; i++)
                {
                    for (int j = 0; j<jdim; j++)
                    {
                        array[i, j] = -999;
                    }
                }

                // The following code gets rid of extra spaces between numbers entered by the user

                input1 = Console.ReadLine().Trim();
                input = input1.Substring(0, 1);
                k = 0;
                for (int i = 1; i < input1.Length; i++)
                {
                    if (input1.Substring(i, 1) == " " && input1.Substring(i - 1, 1) == " ")
                    {
                        continue;
                    }
                    else
                    {
                        k++;
                        input = input + input1.Substring(i, 1);
                    }
                }
                elements = input.Split(' ');
                int numElements = elements.Length;
                if (numElements == idim * jdim)
                {
                    value = 0;
                    k = 0;
                    for (int i = 0; i < idim; i++)
                    {
                        for (int j = 0; j < jdim; j++)
                        {
                            velement = int.TryParse(elements[k], out array[i, j]);
                            if (!velement) value++;
                            k++;
                        }
                    }
                    if (value == 0) valid = true;
                }
                if(value>0 || !valid)
                {
                    Console.WriteLine("\r\n***** Invalid Entry.  There should be {0} numbers entered.", idim * jdim);
                }
            } // end while
            return array;
        } // end of static GetMatrix
        static double Det2by2(int num, int[,,] arr)
        {
            double det = arr[num,0, 0] * arr[num,1, 1] - arr[num,0, 1] * arr[num,1, 0];
            return det;
        }  //static Det2by2
        static double Det3by3(int num, int[,,] arr)
        {
            int[,,] subArray = new int[7,2, 2];
            double det = 0;

            subArray[num,0, 0] = arr[num, 1, 1];
            subArray[num,1, 1] = arr[num, 2, 2];
            subArray[num,1, 0] = arr[num, 2, 1];
            subArray[num,0, 1] = arr[num, 1, 2];

            det += arr[num, 0, 0] * Det2by2(num, subArray);

            subArray[num, 0, 0] = arr[num, 0, 1];
            subArray[num, 1, 1] = arr[num, 2, 2];
            subArray[num, 1, 0] = arr[num, 2, 1];
            subArray[num, 0, 1] = arr[num, 0, 2];

            det += arr[num, 1, 0] * Det2by2(num, subArray);

            subArray[num, 0, 0] = arr[num, 0, 1];
            subArray[num, 1, 1] = arr[num, 1, 2];
            subArray[num, 1, 0] = arr[num, 1, 1];
            subArray[num, 0, 1] = arr[num, 0, 2];

            det += arr[num, 2, 0] * Det2by2(num, subArray);

            return det;

        }  //static Det3by3
    }  //class
}  //namespace

