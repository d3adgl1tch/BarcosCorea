using System.Collections;
using System.Linq;
using UnityEngine;

public class DockScript : MonoBehaviour
{
    [SerializeField]private GameObject arrow;
    ParticleSystem arrivedParticles;
    AudioSource arrivedSound;

    void Start()
    {
        arrivedParticles = GetComponentInChildren<ParticleSystem>();
        arrivedSound = GetComponent<AudioSource>();
        
    }
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         PlayerMovement playerRef = other.GetComponent<PlayerMovement>();
            if (playerRef != null)
            {
                Arrived(playerRef);
                GetComponent<BoxCollider>().enabled = false;
            }
      }
   }
    void Arrived(PlayerMovement player)
    {
            player.BeginStop(this.transform);
            arrivedParticles.Play();
            arrivedSound.Play();
            arrow.SetActive(false);
        
    }

    
}
