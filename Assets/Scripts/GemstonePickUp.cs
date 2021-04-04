using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemstonePickUp : MonoBehaviour
{
    public AudioClip pickupClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player") 
            // checks the name of the layer
            // if the collided gameobject's layer is named as plater, get its value
        {
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
            Destroy(this.gameObject);
        }
    }
}
