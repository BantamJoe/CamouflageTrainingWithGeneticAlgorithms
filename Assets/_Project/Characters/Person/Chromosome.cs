using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour {

    #region Properties
    // Gene for color.
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }

    // Gene for scale.
    public float Scale { get; set; }

    public float SurvivalTime { get; set; }
    #endregion

    #region Fields
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    #endregion

    // Use this for initialization
    void Start ()
    {
        SurvivalTime = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        spriteRenderer.color = new Color(R, G, B);
        this.transform.localScale = new Vector2(Scale, Scale);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown ()
    {
        isDead = true;
        SurvivalTime = PopulationManager.TimeElapsed;
        Debug.LogFormat("Dead at: {0}", SurvivalTime);
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
    }
}
