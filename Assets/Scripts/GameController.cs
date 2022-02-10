using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Game objects that need to be updated during runtime
    public GameObject arena;
    public GameObject pickupPrefab;
    public GameObject player;
    public Text currentScoreDisplay;
    public Text countdown;
    
    // game config options
    public int numberOfPickups = 10;
    public int maxSpawnAttempts = 100;
    private bool _spawning = true; // used to prevent spawning to many pickups

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPickups());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Pickup") && !_spawning)
        {
            _spawning = true;
            // Resets player
            player.transform.position = new Vector3(0, 1, 0);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            // Spawns more pickups
            StartCoroutine(SpawnPickups());
        }
    }

    IEnumerator SpawnPickups() // Coroutine for spawning pickups
    {
        // Waiting 3 Seconds to spawn pickups
        countdown.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "";
        
        //Resets Score
        currentScoreDisplay.text = "0";
        
        // Spawns designated number of pickups
        for (int i = 0; i < numberOfPickups; i++)
        {
            // Attempts to spawn pickup in a valid position a set number of times
            Vector3 spawnLocation;
            int numberOfColliders = 0;
            int attempts = 0;
            do
            {
                // Calculating a random spawn position
                spawnLocation = Random.insideUnitSphere;
                spawnLocation.Set(spawnLocation.x * arena.transform.localScale.x * 0.33f, 1,
                    spawnLocation.y * arena.transform.localScale.z * 0.33f);

                // Check if that position already has colliders in that location
                Collider[] colliders = new Collider[1];
                numberOfColliders = Physics.OverlapBoxNonAlloc(spawnLocation, pickupPrefab.transform.localScale * 1.1f, colliders);
                
                // Will exit once it finds a position with 0 colliders already or the number of attempts exceeds the limit 
                attempts++;
            } while (numberOfColliders != 0 && attempts < maxSpawnAttempts);

            // Creating pickup gameObject
            GameObject pickup = Instantiate<GameObject>(pickupPrefab);

            // moving pickup to location and updating physics 
            pickup.transform.position = spawnLocation;
            Physics.SyncTransforms();

            // setting point value randomly and naming pickup accordingly
            int pointValue = Random.Range(1, 4); 
            pickup.GetComponent<Pickup>().points = pointValue;
            pickup.name = pointValue + " Point Pickup";
            
            // changing color of pickup based on point values
            switch (pointValue)
            {
                case (1):
                    // Changes every subcomponent of the pickup object to blue 
                    foreach (Renderer gameObj in pickup.GetComponentsInChildren<Renderer>())
                    {
                        gameObj.material.color = Color.blue;
                    }
                    break;
                case (2):
                    // Changes every subcomponent of the pickup object to magenta
                    foreach (Renderer gameObj in  pickup.GetComponentsInChildren<Renderer>())
                    {
                        gameObj.material.color = Color.magenta;
                    }
                    break;
                case (3):
                    // Changes every subcomponent of the pickup object to yellow
                    foreach (Renderer gameObj in  pickup.GetComponentsInChildren<Renderer>())
                    {
                        gameObj.material.color = Color.yellow;
                    }
                    break;
            }
        }
        
        _spawning = false;
    }
}
