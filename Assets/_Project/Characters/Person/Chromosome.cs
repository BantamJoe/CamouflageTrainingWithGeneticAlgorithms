using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour {

    #region Properties
    // Gene for color.
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float TimeToDie { get; set; }
    #endregion

    #region Fields
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    #endregion

    // Use this for initialization
    void Start ()
    {
        TimeToDie = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        spriteRenderer.color = new Color(R, G, B);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown ()
    {
        isDead = true;
        TimeToDie = PopulationManager.TimeElapsed;
        Debug.LogFormat("Dead at: {0}", TimeToDie);
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
    }
}
