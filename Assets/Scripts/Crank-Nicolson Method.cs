using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Crank_Nicolson_Method
{
    // solve the partial differential equation U_t = a * U_yy 

    public static double[] CrankNicolson(double Re, double deltaT, double deltaY, double[] D)
    { 
        double r = deltaT / (2.0 * deltaY * deltaY * Re);
        double[] indepTerm =new double[D.Length-2];
        indepTerm[0] = (1 - 2 * r) * D[1] + r * (D[0] + D[2]) + r * D[0];
        indepTerm[indepTerm.Length-1] = (1 - 2 * r) * D[indepTerm.Length] + r * (D[indepTerm.Length - 1] + D[indepTerm.Length + 1]) + r * D[indepTerm.Length + 1];
        for (int j=1;j<indepTerm.Length-1;j++)
        {
            indepTerm[j] = (1 - 2 * r) * D[j + 1] + r * (D[j] + D[j + 2]);
        }

        int N = D.Length-2;
        double[] A = new double[N], B = new double[N], C = new double[N], U = new double[N];

        B[0] = B[N - 1] = 1.0 + 2.0 * r;
        C[0] = A[N - 1] = -r;

        for (int i = 1; i < N - 1; i++)
        {
            A[i] = -r;
            B[i] = 1.0 + 2.0 * r;
            C[i] = -r;
        }

        for (int k = 1; k < N; k++)
        {
            if (B[k - 1] == 0)
                return null;

            double m = A[k] / B[k - 1];

            B[k] -= m * C[k - 1];
            indepTerm[k] -= m * indepTerm[k - 1];
        }

        if (B[N - 1] == 0)
            return null;

        U[N - 1] = indepTerm[N - 1] / B[N - 1];

        for (int k = N - 2; k >= 0; k--)
            U[k] = (indepTerm[k] - C[k] * U[k + 1]) / B[k];

        List<double> list = new List<double>();
        list.AddRange(U);
        list.Insert(0, D[0]);
        list.Add(D[D.Length - 1]);
        return list.ToArray();
    }
}

