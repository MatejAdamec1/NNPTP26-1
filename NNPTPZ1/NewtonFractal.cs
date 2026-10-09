using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private int width;
        private int height;

        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        private string outputPath;

        private const int ExpectedArgumentCount = 7;

        private Bitmap bitmapImage;

        private static Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        public NewtonFractal(string[] args)
        {
            if (ParseArgs(args))
            {
                bitmapImage = new Bitmap(width, height);
            }
            else
            {
                throw new ArgumentException("Invalid arguments.");
            }
        }

        private bool ParseArgs(string[] args)
        {
            if (args.Length < ExpectedArgumentCount)
            {
                Console.WriteLine("Not enough arguments provided.");
                return false;
            }
            try
            {
                width = int.Parse(args[0]);
                height = int.Parse(args[1]);
                xMin = double.Parse(args[2]);
                xMax = double.Parse(args[3]);
                yMin = double.Parse(args[4]);
                yMax = double.Parse(args[5]);
                outputPath = args[6];
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid argument format.");
                return false;
            }
            return true;
        }

        private void PrintPolynomialAndDerivedPolynomial(Polynomial polynomial, Polynomial derivedPolynomial)
        {
            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);
        }

        public void CalculateNewtonFractal(int[] coefficients)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial polynomial = CreatePolynomialFromCoefficients(coefficients);
            Polynomial derivedPolynomial = polynomial.Derive();

            PrintPolynomialAndDerivedPolynomial(polynomial, derivedPolynomial);

            double xStep = (xMax - xMin) / width;
            double yStep = (yMax - yMin) / height;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber currentEstimate = new ComplexNumber() { RealPart = x, ImaginaryPart = y};

                    if (currentEstimate.RealPart == 0)
                        currentEstimate.RealPart = 0.0001;
                    if (currentEstimate.ImaginaryPart == 0)
                        currentEstimate.ImaginaryPart = 0.0001;

                    int iteration = SolveEquationUsingNewtonsIteration(polynomial, derivedPolynomial, ref currentEstimate);
                    int id = FindRoot(roots, currentEstimate);
                    ColorizePixel(j, i, iteration, id);
                }
            }
            return;
        }

        private static int SolveEquationUsingNewtonsIteration(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber currentEstimate)
        {
            int iteration = 0;
            for (int q = 0; q < 30; q++)
            {
                var diff = polynomial.Eval(currentEstimate).Divide(derivedPolynomial.Eval(currentEstimate));
                currentEstimate = currentEstimate.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    q--;
                }
                iteration++;
            }

            return iteration;
        }

        private Polynomial CreatePolynomialFromCoefficients(int[] coefficients)
        {
            Polynomial resultingPolynomial = new Polynomial();

            foreach (int coefficient in coefficients)
            {
                resultingPolynomial.Coefficients.Add(new ComplexNumber() { RealPart = coefficient });
            }
            return resultingPolynomial;
        }

        private int FindRoot(List<ComplexNumber> polynomialRoots, ComplexNumber currentEstimate)
        {
            bool isRootKnown = false;
            int root = 0;
            for (int w = 0; w < polynomialRoots.Count; w++)
            {
                if (Math.Pow(currentEstimate.RealPart - polynomialRoots[w].RealPart, 2) + Math.Pow(currentEstimate.ImaginaryPart - polynomialRoots[w].ImaginaryPart, 2) <= 0.01)
                {
                    isRootKnown = true;
                    root = w;
                }
            }
            if (!isRootKnown)
            {
                polynomialRoots.Add(currentEstimate);
                root = polynomialRoots.Count;
            }

            return root;
        }

        private void ColorizePixel(int x, int y, int iterations, int rootIndex)
        {
            Color color = colors[rootIndex % colors.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iterations * 2), 255),
                Math.Min(Math.Max(0, color.G - iterations * 2), 255),
                Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            bitmapImage.SetPixel(x, y, color);
        }

        public void SaveOutputImage()
        {
            bitmapImage.Save(outputPath ?? "../../../out.png");
            bitmapImage.Dispose();
        }
    }
}
