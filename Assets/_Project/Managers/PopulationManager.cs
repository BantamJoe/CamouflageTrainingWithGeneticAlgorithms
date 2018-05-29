using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour {

    #region Fields
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private int populationSize = 10;

    private List<GameObject> population = new List<GameObject>();
    private int trialTimeSeconds = 10;
    private int generation = 1;
    private GUIStyle guiStyle = new GUIStyle();
    #endregion

    #region Properties
    public static float TimeElapsed { get; private set; }
    #endregion

    private void OnGUI ()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)TimeElapsed, guiStyle);
    }
    // Use this for initialization
    void Start () {
        TimeElapsed = 0f;

        for (int i = 0; i < populationSize; i++)
        {
            Vector2 position = new Vector2(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f));
            GameObject gameObject = Instantiate(personPrefab, position, Quaternion.identity);
            gameObject.GetComponent<Chromosome>().R = Random.Range(0f, 1f);
            gameObject.GetComponent<Chromosome>().G = Random.Range(0f, 1f);
            gameObject.GetComponent<Chromosome>().B = Random.Range(0f, 1f);
            population.Add(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        TimeElapsed += Time.deltaTime;

        if(TimeElapsed > trialTimeSeconds)
        {
            // TODO: BreedNewPopulation
            TimeElapsed = 0f;
        }
	}
}
