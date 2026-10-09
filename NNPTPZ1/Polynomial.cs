using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            /// <summary>
            /// Coefficients of polynomial,
            /// index is the power of x
            /// </summary>
            public List<ComplexNumber> Coefficients { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Polynomial() => 
                Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) =>
                Coefficients.Add(coefficient);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial derivedPolynome = new Polynomial();
                for (int coefficientIndex = 1; coefficientIndex < Coefficients.Count; coefficientIndex++)
                {
                    derivedPolynome.Coefficients.Add(Coefficients[coefficientIndex].Multiply(new ComplexNumber() { RealPart = coefficientIndex }));
                }

                return derivedPolynome;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber result = ComplexNumber.Zero;
                ComplexNumber xPower = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };

                for (int coefficientIndex = 0; coefficientIndex < Coefficients.Count; coefficientIndex++)
                {
                    ComplexNumber coefficient = Coefficients[coefficientIndex];
                    result = result.Add(coefficient.Multiply(xPower));

                    xPower = xPower.Multiply(x);
                }

                return result;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string result = "";
                for (int coefficientIndex = 0; coefficientIndex < Coefficients.Count; coefficientIndex++)
                {
                    result += Coefficients[coefficientIndex];
                    if (coefficientIndex > 0)
                    {
                        for (int power = 0; power < coefficientIndex; power++)
                        {
                            result += "x";
                        }
                    }
                    if (coefficientIndex + 1 < Coefficients.Count) { 
                        result += " + ";
                    }
                }
                return result;
            }
        }
    }
}
