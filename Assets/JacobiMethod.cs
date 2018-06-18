using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class JacobiMethod : MonoBehaviour {

    public GameObject IterationsManager;

    public double Jacobi(
            int n,
            double[,] a,
            double[] b,
            double[] x0,
            int maxIterations,
            double tolerance)
    {
        IterationsManager.GetComponent<IterationsManager>().ClearList();

        double t = 0.0;
        double[] x = new double[n];
        int its = 0;

        for (int i = 0; i < n; i++)
            x[i] = x0[i];

        while (its < maxIterations)
        {
            its++;

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        sum += a[i, j] * x0[j];
                    }
                }

                x[i] = (b[i] - sum) / a[i, i];
            }

            t = 0.0;

            for (int i = 0; i < n; i++)
                t += Math.Pow(x[i] - x0[i], 2.0);

            t = Math.Sqrt(t);

            if (t < tolerance)
                break;

            for (int i = 0; i < n; i++)
                x0[i] = x[i];

            Debug.Log("x = " + x[0] + " y = " + x[1] + " z = " + x[2] + " t = " + x[3]);
            IterationsManager.GetComponent<IterationsManager>().InstantiatePrefab(its, x[0], x[1], x[2], x[3]);
        }

        return t;
    }
}
