namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractal fractal = new NewtonFractal(args);
            fractal.CalculateNewtonFractal(new int[] { 1, 0, 0, 1 });
            fractal.SaveOutputImage();
        }
    }
}
