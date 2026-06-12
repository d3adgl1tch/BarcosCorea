using UnityEngine;

public class FishRecolected : MonoBehaviour
{
    [Header("Particlee")]
    [SerializeField] private ParticleSystem confettiFX;
    [SerializeField] private AudioClip collectSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerMovement playerRef = other.GetComponent<PlayerMovement>();
            if (playerRef != null)
            {
                playerRef.RecolectFish();
                Recolected();
                Debug.Log("Choco con pex");
            }
            PlayerMovementVersus playerVersus = other.GetComponent<PlayerMovementVersus>();
            if (playerRef != null)
            {
                playerRef.RecolectFish();
                Recolected();
                Debug.Log("Choco con pex");
            }
               

        }
    }
    private void Recolected()
    {
        if(confettiFX != null)
        {
            ParticleSystem fx = Instantiate(confettiFX,transform.position,Quaternion.identity);
            Destroy(fx.gameObject, fx.main.duration);
        }
        if (collectSound != null)
        {
            GameObject audioGO = new GameObject("FishCollectAudio");
            AudioSource audio = audioGO.AddComponent<AudioSource>();
            audio.clip = collectSound;
            audio.Play();

            Destroy(audioGO, collectSound.length);
        }
        Destroy(gameObject);
    }

}
