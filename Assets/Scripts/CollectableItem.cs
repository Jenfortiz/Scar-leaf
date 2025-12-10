using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")) 
        {
            PlayerHealth healthScript = other.GetComponent<PlayerHealth>();

            if (healthScript != null)
            {

                healthScript.Heal(10); 
            }

            Destroy(gameObject); 
        }
    }
}