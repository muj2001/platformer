using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    // Collider2D collider;

    private void Awake()
    {
        // collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
       detectedColliders.Remove(collision);
    }
}
