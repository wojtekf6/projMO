using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iterationPrefabScript : MonoBehaviour {

    public Text IterationNumber;
    public Text[] Results;

	public void Initialize(int iterationNumber, double x, double y, double z, double t)
    {
        IterationNumber.text = "Iteracja " + iterationNumber;
        Results[0].text = "" + x;
        Results[1].text = "" + y;
        Results[2].text = "" + z;
        Results[3].text = "" + t;
    }
}
