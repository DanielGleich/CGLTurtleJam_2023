using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggPhysics : MonoBehaviour
{
    private Rigidbody2D eggRb;
    [SerializeField] float collisionThreshhold = 0;
    [SerializeField] float bounceDampThreshhold = 0;
    public Vector2 recentCheckpoint = new Vector2(0,0);
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] AudioClip CRACKED;

    // Start is called before the first frame update
    void Start()
    {
        eggRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void BackToCheckpoint() {
        transform.position = new Vector3(recentCheckpoint.x, recentCheckpoint.y, 0);
        transform.rotation = new Quaternion();
        eggRb.angularVelocity = 0;
        eggRb.velocity = Vector2.zero;
    }

    public void SetCheckpoint(Vector2 pos) {
        recentCheckpoint = pos;
    }

    void BreakEgg()
    {
        soundPlayer.PlayOneShot(CRACKED);
        BackToCheckpoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float materialDamper = 0;
        if (collision.gameObject.CompareTag("Bounce")) {
            materialDamper = bounceDampThreshhold;
        }

        if (collision.relativeVelocity.x >= collisionThreshhold + materialDamper || collision.relativeVelocity.y >= collisionThreshhold + materialDamper  || collision.relativeVelocity.x <= -collisionThreshhold - materialDamper || collision.relativeVelocity.y <= -collisionThreshhold - materialDamper ) {
            BreakEgg();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint")) {
            LevelManager.Instance.CheckPointReached(collision.gameObject.GetComponent<CheckPoint>().GetCheckPointNumber());
        }
    }
}
