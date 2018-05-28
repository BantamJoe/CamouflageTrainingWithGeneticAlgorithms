using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour {

    #region Fields
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private int populationSize = 10;

    private List<GameObject> population = new List<GameObject>();
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
