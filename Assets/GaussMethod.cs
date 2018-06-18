using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GaussMethod : MonoBehaviour {

    private void Substitute(
            int n,
            double[,] w,
            double[] b,
            double[] x,
            int[] pivot)
    {
        double sum;
        int i, j, n1 = n - 1;

        if (n == 1)
        {
            x[0] = b[0] / w[0, 0];
            return;
        }

        x[0] = b[pivot[0]];

        for (i = 1; i < n; i++)
        {
            for (j = 0, sum = 0; j < i; j++)
                sum += w[i, j] * x[j];
            x[i] = b[pivot[i]] - sum;
        }

        x[n1] /= w[n1, n1];

        for (i = n - 2; i >= 0; i--)
        {
            for (j = i + 1, sum = 0; j < n; j++)
                sum += w[i, j] * x[j];
            x[i] = (x[i] - sum) / w[i, i];
        }
    }

    public bool GaussianElimimation(
        int n,
        double[,] w,
        double[] b,
        double[] x)
    {
        double awikod, col_max, ratio, row_max, temp;
        double[] d = new double[n];
        int flag = 1, i, i_star, j, k;
        int[] pivot = new int[n];

        for (i = 0; i < n; i++)
        {
            pivot[i] = i;
            row_max = 0;
            for (j = 0; j < n; j++)
                row_max = Math.Max(row_max, Math.Abs(w[i, j]));
            if (row_max == 0)
            {
                flag = 0;
                row_max = 1;
            }
            d[i] = row_max;
        }
        if (n <= 1) return flag != 0;

        for (k = 0; k < n - 1; k++)
        {
            col_max = Math.Abs(w[k, k]) / d[k];
            i_star = k;
            for (i = k + 1; i < n; i++)
            {
                awikod = Math.Abs(w[i, k]) / d[i];
                if (awikod > col_max)
                {
                    col_max = awikod;
                    i_star = i;
                }
            }
            if (col_max == 0)
                flag = 0;
            else
            {
                if (i_star > k)
                {
                    flag *= -1;
                    i = pivot[i_star];
                    pivot[i_star] = pivot[k];
                    pivot[k] = i;
                    temp = d[i_star];
                    d[i_star] = d[k];
                    d[k] = temp;
                    for (j = 0; j < n; j++)
                    {
                        temp = w[i_star, j];
                        w[i_star, j] = w[k, j];
                        w[k, j] = temp;
                    }
                }

                for (i = k + 1; i < n; i++)
                {
                    w[i, k] /= w[k, k];
                    ratio = w[i, k];
                    for (j = k + 1; j < n; j++)
                        w[i, j] -= ratio * w[k, j];
                }
            }
        }

        if (w[n - 1, n - 1] == 0) flag = 0;

        if (flag == 0)
            return false;

        Substitute(n, w, b, x, pivot);
        return true;
    }


}
