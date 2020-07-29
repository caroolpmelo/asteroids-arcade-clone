using System.Collections;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    // public AudioClip destroySound;
    public GameObject smallAsteroid;

    private GameController gameController;

    void Start()
    {
        // get reference to game controller obj and script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();

        // push asteroid in direction it's facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));

        // random angular velocity/rotation
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject); // destroy bullet

            // large asteroid spawn new ones
            if (tag.Equals("Large Asteroid"))
            {
                // spawn small asteroids
                Instantiate(
                    smallAsteroid,
                    new Vector3(
                        transform.position.x - .5f,
                        transform.position.y - .5f, 0
                    ),
                    Quaternion.Euler(0, 0, 90)
                );
                Instantiate(
                    smallAsteroid,
                    new Vector3(
                        transform.position.x + .5f,
                        transform.position.y + .0f, 0
                    ),
                    Quaternion.Euler(0, 0, 0)
                );
                Instantiate(
                    smallAsteroid,
                    new Vector3(
                        transform.position.x + .5f,
                        transform.position.y - .5f, 0
                    ),
                    Quaternion.Euler(0, 0, 270)
                );

                gameController.SplitAsteroid(); // + 2
            }
            else
            {
                // destroy small asteroid
                gameController.DecrementAsteroids();
            }

            // play destroy sound
            // AudioSource.PlayClipAtPoint(
            //     destroySound,
            //     Camera.main.transform.position
            // );

            gameController.IncrementScore(); // add to score

            Destroy(gameObject); // destroy current asteroid
        }
    }
}
