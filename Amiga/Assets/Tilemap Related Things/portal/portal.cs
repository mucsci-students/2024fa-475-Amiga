using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private List<Transform> destinations;  // List of possible destinations

    [SerializeField] private AudioMixer mixer; // link to the bgm music mixer
    
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

            SwitchSongs ();

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
            //Debug.Log("Randomly selected destination: " + randomIndex + " (" + destinations[randomIndex].name + ")");
            return destinations[randomIndex];
        }

        Debug.LogWarning("No available destinations to teleport to!");
        return null;
    }

    private void SwitchSongs ()
    {
        float baseMusic1Vol;
        mixer.GetFloat ("Base Music 1 Volume", out baseMusic1Vol);
        if (baseMusic1Vol == 0f)
        {
            mixer.SetFloat ("Base Music 1 Volume", -80f);
            mixer.SetFloat ("Base Music 2 Volume", 0f);
            print ("1");
        }
        else
        {
            mixer.SetFloat ("Base Music 1 Volume", 0f);
            mixer.SetFloat ("Base Music 2 Volume", -80f);
            print ("2");
        }
    }
}
