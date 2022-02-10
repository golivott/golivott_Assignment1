using UnityEngine;

public class Pickup : MonoBehaviour
{
    // for storing the points that pickup will grant
    public int points;

    void OnTriggerEnter(Collider other)
    {
        // Only destroy the pickup when the player collides with it
        if (other.name.Equals("Player"))
            Destroy(gameObject);
    }
    
}
