using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    float rotationSpeed = 100.0f;
    float thrustForce = 3f;

    public AudioClip crashSound;
    public AudioClip shootSound;

    public GameObject bullet;

    private GameController gameController;

    void Start()
    {
        // get reference to game controller obj and script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
    }

    // called every fixed framerate frame
    void FixedUpdate()
    {
        // rotate ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        // thrust ship if necessary
        GetComponent<Rigidbody2D>()
            .AddForce(
                transform.up * thrustForce * Input.GetAxis("Vertical")
            );

        if (Input.GetMouseButtonDown(0)) // fired bullet
            ShootBullet();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // everything is asteroid, except bullet
        if (other.gameObject.tag != "Bullet")
        {
            // play crash sound
            AudioSource.PlayClipAtPoint(
                crashSound,
                Camera.main.transform.position
            );

            // ship in center
            transform.position = new Vector3(0, 0, 0);

            // remove ship velocity
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();

        }
    }

    void ShootBullet()
    {
        // spawn bullet
        Instantiate(
            bullet,
            new Vector3(
                transform.position.x,
                transform.position.y, 0
            ),
            transform.rotation
        );

        // play shoot sound
        AudioSource.PlayClipAtPoint(
            shootSound,
            Camera.main.transform.position
        );
    }
}
