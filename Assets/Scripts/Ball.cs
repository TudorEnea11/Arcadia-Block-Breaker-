
using UnityEngine;

public class Ball : MonoBehaviour {

    // Configuration Parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 3f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float ballSpeed = 6f;
    [SerializeField] float accelerator = 0.05f;

    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Use this for initialization
    void Start ()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
    {
        if (!hasStarted)
        {
            LockBalltoPaddle();
            LaunchOnMouseClick();
        }
    }

    private void speedUpBall()
    {
        myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * ballSpeed;
        ballSpeed += accelerator;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBalltoPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted) 
        {
            Vector2 velocityTweak = 
                new Vector2(Random.Range(0f, randomFactor),
                Random.Range(0f, randomFactor)); 

            AudioClip Clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(Clip);
            myRigidBody2D.velocity += velocityTweak;

            speedUpBall();
        }
    }
       
}
