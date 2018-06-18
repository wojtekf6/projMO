using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationsManager : MonoBehaviour {

    public GameObject iterationsPrefab;
    public List<GameObject> prefabsList;

    public void InstantiatePrefab(int iterationNumber, double x, double y, double z, double t)
    {
        GameObject prefab = Instantiate(iterationsPrefab, gameObject.transform);
        prefab.GetComponent<iterationPrefabScript>().Initialize(iterationNumber, x, y, z, t);
        prefabsList.Add(prefab);
    }

    public void ClearList ()
    {
        prefabsList.ForEach(p => Destroy(p));
        prefabsList = new List<GameObject>();
    }
}
