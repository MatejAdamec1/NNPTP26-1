using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => 
            Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Derive()
        {
            Polynomial derivedPolynomial = new Polynomial();
            for (int coefficientIndex = 1; coefficientIndex < Coefficients.Count; coefficientIndex++)
            {
                derivedPolynomial.Coefficients.Add(Coefficients[coefficientIndex].Multiply(new ComplexNumber() { RealPart = coefficientIndex }));
            }

            return derivedPolynomial;
        }

        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
        }

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
