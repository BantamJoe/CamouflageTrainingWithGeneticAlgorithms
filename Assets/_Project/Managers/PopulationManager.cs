using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{

    #region Fields
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private int populationSize = 10;
    [SerializeField] private int trialTimeSeconds = 12;
    [SerializeField] private float minScale = .1f;
    [SerializeField] private float maxScale = 1.5f;

    private List<GameObject> population = new List<GameObject>();
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
    void Start ()
    {
        TimeElapsed = 0f;

        for (int i = 0; i < populationSize; i++)
        {
            GameObject gameObject = Instantiate(personPrefab, GetRandomPosition(), Quaternion.identity);
            gameObject.GetComponent<Chromosome>().R = Random.Range(0f, 1f);
            gameObject.GetComponent<Chromosome>().G = Random.Range(0f, 1f);
            gameObject.GetComponent<Chromosome>().B = Random.Range(0f, 1f);
            gameObject.GetComponent<Chromosome>().Scale = Random.Range(minScale, maxScale);
            population.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        TimeElapsed += Time.deltaTime;

        if (TimeElapsed > trialTimeSeconds)
        {
            BreedNewPopulation();
            TimeElapsed = 0f;
        }
    }

    private void BreedNewPopulation ()
    {
        var newPopulation = new List<GameObject>();
        // Get rid of unfit individuals.
        // Longest survivors at the end of the list.
        List<GameObject> sortedPopulation = population.OrderBy(go => go.GetComponent<Chromosome>().SurvivalTime).ToList();

        population.Clear();

        // Breed upper half of sorted list.
        for (int i = (int)(sortedPopulation.Count / 2f) - 1; i < sortedPopulation.Count - 1; i++)
        {
            population.Add(Breed(sortedPopulation[i], sortedPopulation[i + 1]));
            population.Add(Breed(sortedPopulation[i + 1], sortedPopulation[i]));

        }

        // Destroy all parents and previous population.
        for (int i = 0; i < sortedPopulation.Count; i++)
        {
            Destroy(sortedPopulation[i]);
        }
        generation++;
    }

    private GameObject Breed (GameObject parent1, GameObject parent2)
    {
        GameObject offspring = Instantiate(personPrefab, GetRandomPosition(), Quaternion.identity);
        Chromosome chromosome1 = parent1.GetComponent<Chromosome>();
        Chromosome chromosome2 = parent2.GetComponent<Chromosome>();

        if (Random.Range(0f, 100f) > 2)
        {
            // Set offspring chromosome values to the value of one or the other parent (50% chance).
            offspring.GetComponent<Chromosome>().R = Random.Range(0f, 10f) < 5 ? chromosome1.R : chromosome2.R;
            offspring.GetComponent<Chromosome>().G = Random.Range(0f, 10f) < 5 ? chromosome1.G : chromosome2.G;
            offspring.GetComponent<Chromosome>().B = Random.Range(0f, 10f) < 5 ? chromosome1.B : chromosome2.B;
            offspring.GetComponent<Chromosome>().Scale = Random.Range(0f, 10f) < 5 ? chromosome1.Scale : chromosome2.Scale;
        }
        else
        {
            // Introduce random mutations.
            offspring.GetComponent<Chromosome>().R = Random.Range(0f, 1f);
            offspring.GetComponent<Chromosome>().G = Random.Range(0f, 1f);
            offspring.GetComponent<Chromosome>().B = Random.Range(0f, 1f);
            offspring.GetComponent<Chromosome>().Scale = Random.Range(minScale, maxScale);
        }
        return offspring;
    }

    private Vector2 GetRandomPosition ()
    {
        return new Vector2(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f));
    }
}
