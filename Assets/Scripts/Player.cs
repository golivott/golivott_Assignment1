using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // config for ball's speed
    public float forceMultiplier = 10f;
    public Text score; // used to update scoreboard
    
    // Update is called once per frame
    void Update()
    {
        // Setting movement direction
        Vector3 moveDir = Vector3.zero;
        
        // Add or subtract 1 in the corresponding input direction   
        if (Input.GetKey(KeyCode.UpArrow)) moveDir.z += 1; // += is used so when user inputs 2 opposite arrows movement is 0
        if (Input.GetKey(KeyCode.DownArrow)) moveDir.z += -1;
        if (Input.GetKey(KeyCode.LeftArrow)) moveDir.x += -1;
        if (Input.GetKey(KeyCode.RightArrow)) moveDir.x += 1;

        gameObject.GetComponent<ConstantForce>().force = moveDir.normalized * forceMultiplier; // Changes the force applied to ball in the direction times the speed
    }

    private void OnTriggerEnter(Collider other) // updating score based on what pickup it collied with
    {
        score.text = (Convert.ToInt32(score.text) + other.GetComponent<Pickup>().points).ToString();
    }
}
