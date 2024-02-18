using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Rotates the collectible
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the player touches the xp pickup it increments the player's xp and then destroys itself
        if (other.gameObject.CompareTag("Player"))
        {
            PrototypeGame.instance.IncrementEXP();
            Destroy(this.gameObject);
        }
    }
}
