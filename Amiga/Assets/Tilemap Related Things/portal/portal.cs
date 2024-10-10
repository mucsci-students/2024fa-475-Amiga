using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private List<Transform> destinations;  // List of possible destinations

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalObjects.Contains(collision.gameObject))
        {
            return;  // Avoid re-teleporting the same object
        }

        // Select a random destination from the list of destinations
        Transform randomDestination = GetRandomDestination();
        if (randomDestination != null)
        {
            // Add the object to the destination's tracking list
            Portal destinationPortal = randomDestination.GetComponent<Portal>();
            if (destinationPortal != null)
            {
                destinationPortal.portalObjects.Add(collision.gameObject);
            }

            // Teleport the object to the randomly chosen destination
            collision.transform.position = randomDestination.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the object from this portal's tracking when it leaves
        portalObjects.Remove(collision.gameObject);
    }

    private Transform GetRandomDestination()
    {
        // Ensure there are available destinations to choose from
        if (destinations.Count > 0)
        {
            int randomIndex = Random.Range(0, destinations.Count);
            Debug.Log("Randomly selected destination: " + randomIndex + " (" + destinations[randomIndex].name + ")");
            return destinations[randomIndex];
        }

        Debug.LogWarning("No available destinations to teleport to!");
        return null;
    }
}
