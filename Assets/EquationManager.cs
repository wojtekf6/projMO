using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class EquationManager : MonoBehaviour {

    public bool isGauss;
    public Text[] OutputResults;
    public GameObject Results;
    public Text[] ErrorLabels;
    public GameObject[] Panels;

    public Equation[] Equations = new Equation[4];
    public InputField[] constantValues = new InputField[4];

    public InputField maxIterationsInput;
    public InputField toleranceInput;

    private int maxIterations;
    private double tolerance;

    private double[,] matrix = new double[4,4];
    private double[] constantMatrix = new double[4];
    private double[] results_x = new double[4];

    private void Start()
    {
        
    }

	public void OnButtonStart ()
    {
        ErrorLabels.ToList().ForEach(l => l.gameObject.SetActive(false));
        OutputResults.ToList().ForEach(r => r.gameObject.SetActive(false));
        Results.SetActive(false);

        if(ConvertInputToInt())
        {
            if (isCorrect())
            {
                if (isGauss)
                {
                    GaussFnc();
                    OutputResults.ToList().ForEach(r => r.gameObject.SetActive(true));
                    Results.SetActive(true);
                }
                else
                {
                    int errorType = chceckForJacobi();
                    if(errorType.Equals(1))
                    {
                        JacobiFnc();
                        Panels[1].SetActive(true);
                        Panels[0].SetActive(false);
                        OutputResults.ToList().ForEach(r => r.gameObject.SetActive(true));
                        Results.SetActive(true);
                    }
                    else 
                    {
                        Panels[0].SetActive(true);
                        Panels[1].SetActive(false);

                        if (errorType.Equals(0))
                        {
                            ErrorLabels[3].gameObject.SetActive(true);
                        }
                        else if (errorType.Equals(-1))
                        {
                            ErrorLabels[2].gameObject.SetActive(true);
                        }
                        else
                        {
                            ErrorLabels[0].gameObject.SetActive(true);
                        }
                    }

                }
            }
            else
            {
                ErrorLabels[1].gameObject.SetActive(true);
            }
        }
        else
        {
            ErrorLabels[0].gameObject.SetActive(true);
        }
    }

    private void GaussFnc ()
    {
        bool result = gameObject.GetComponent<GaussMethod>().GaussianElimimation(4, matrix, constantMatrix, results_x);

        if(result)
        {
            for (int i = 0; i < 4; i++)
            {
                OutputResults[i].text = results_x[i].ToString();
            }
        }
        else
        {
            Debug.Log("UnknownError_code0");
        }
    }

    private void JacobiFnc()
    {

        double t = gameObject.GetComponent<JacobiMethod>().Jacobi(4, matrix, constantMatrix, results_x, maxIterations, tolerance);
        Debug.Log(t);

        for (int i = 0; i < 4; i++)
        {
            OutputResults[i].text = results_x[i].ToString();
        }
    }

    private bool isCorrect ()
    {
        int[] ColumnsNumbers = new int[4];
        ColumnsNumbers.ToList().ForEach(c => c = 0);

        double mainDet = Determinant(matrix);
        Debug.Log(mainDet);

        if(mainDet.Equals(0)) 
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    static int SignOfElement(int i, int j)
    {
        if ((i + j) % 2 == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    static double[,] CreateSmallerMatrix(double[,] input, int i, int j)
    {
        int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
        double[,] output = new double[order - 1, order - 1];
        int x = 0, y = 0;
        for (int m = 0; m < order; m++, x++)
        {
            if (m != i)
            {
                y = 0;
                for (int n = 0; n < order; n++)
                {
                    if (n != j)
                    {
                        output[x, y] = input[m, n];
                        y++;
                    }
                }
            }
            else
            {
                x--;
            }
        }
        return output;
    }

    static double Determinant(double[,] input)
    {
        int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
        if (order > 2)
        {
            double value = 0;
            for (int j = 0; j < order; j++)
            {
                double[,] Temp = CreateSmallerMatrix(input, 0, j);
                value = value + input[0, j] * (SignOfElement(0, j) * Determinant(Temp));
            }
            return value;
        }
        else if (order == 2)
        {
            return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
        }
        else
        {
            return (input[0, 0]);
        }
    }

    private void printMatrix (double[,] matrix)
    {
        string[] results = new string[4];
        for (int i = 0; i < 4; i ++)
        {
            for (int j = 0; j < 4; j++)
            {
                results[i] += matrix[i, j] + " ";
            }
            Debug.Log(results[i]);
        }
    }

    private int chceckForJacobi ()
    {
        for (int i = 0; i < 4; i++)
        {
            if(matrix[i,i].Equals(0))
            {
                return -1;
            }
        }

        if (maxIterationsInput.text.Equals("") || toleranceInput.text.Equals(""))
        {
            return 0;
        }

        if (int.TryParse(maxIterationsInput.text, out maxIterations) && double.TryParse(toleranceInput.text, out tolerance)) 
        {
            maxIterations = (int.Parse(maxIterationsInput.text));
            tolerance = (double.Parse(toleranceInput.text));
        }
        else
        {
            return -2;
        }

        return 1;
    }

    private bool ConvertInputToInt ()
    {
        for (int i = 0; i < 4; i++)
        {
            results_x[i] = 0;
            if (constantValues[i].text.Equals(""))
            {
                constantValues[i].text = "0";
            }

            if (double.TryParse(constantValues[i].text, out constantMatrix[i]))
            {
                constantMatrix[i] = (double.Parse(constantValues[i].text)) * 10;
            }
            else
            {
                Debug.Log("BadConversion_code0");
                return false;
            }
            
            for (int j = 0; j < 4; j++)
            {
                if (Equations[i].equation_i[j].text.Equals(""))
                {
                    Equations[i].equation_i[j].text = "0";
                }

                if(double.TryParse(Equations[i].equation_i[j].text, out matrix[i, j]))
                {
                    matrix[i, j] = (double.Parse(Equations[i].equation_i[j].text)) * 10;
                }
                else
                {
                    Debug.Log("BadConversion_code1"); 
                    return false;
                }
            }
        }

        return true;
    }
}

[System.Serializable]
public class Equation
{
    public InputField[] equation_i = new InputField[4];
}
