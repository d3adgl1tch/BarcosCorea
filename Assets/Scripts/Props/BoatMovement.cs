using DG.Tweening;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    private Transform startPos;
    private BoxCollider boxCollider;
    private ParticleSystem particles;
    private AudioSource audiocrash;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;


    [SerializeField] private float verticalVel = 0.5f;
    [SerializeField] private float horizontalVel = 0.5f;

    Vector3 direction;
    private void FixedUpdate()
    {
        direction = new Vector3(horizontalVel, 0, verticalVel).normalized;
        rb.linearVelocity = direction * speed;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        image = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        audiocrash = GetComponentInChildren<AudioSource>();
        Animation();
    }

    private void Animation()
    {
        DOTween.Kill(image);

        image.transform
            .DOBlendableRotateBy(new Vector3(10f, 3.0f, 0), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particles.Play();
            audiocrash.Play();
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.Crash();
            }
            PlayerMovementVersus playerVersus = collision.gameObject.GetComponentInChildren<PlayerMovementVersus>();
            if(playerVersus != null)
            {
                playerVersus.Crash();
            }
        }
        if (collision.gameObject.CompareTag("Boat"))
        {
            horizontalVel *= -1;
            verticalVel *= -1;
            direction.y = 0;
            direction.Normalize();
            Debug.Log("choco con bote");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoatWall")){

            BoatWall wall = other.GetComponent<BoatWall>();
            if (wall == null) return;

            switch (wall.type)
            {
                case WallType.RIGHT:
                case WallType.LEFT:
                    horizontalVel *= -1;
                    break;
                case WallType.UP:
                case WallType.DOWN:
                    verticalVel *= -1;
                    break;
            }
            direction.y = 0;
            direction.Normalize();
        }
    }
   
}
