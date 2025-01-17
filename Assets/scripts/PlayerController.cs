using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint; 
    public bool hasPowerup;
     private float poweupStrength = 15.0f;
     public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focalpoint");
    }

    // Update is called once per frame
    void Update()
    {
     float forwardInput = Input.GetAxis("Vertical");
     playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);  
     powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) 
        {
            powerupIndicator.gameObject.SetActive(true);
            hasPowerup = true;
            Destroy(other.gameObject);  
            StartCoroutine(PowerUpCountdownRoutine());

        IEnumerator PowerUpCountdownRoutine()
        {
            powerupIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(7);
            hasPowerup = false;
        }
        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Player Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * poweupStrength, ForceMode.Impulse);
        }

       
    }

}
