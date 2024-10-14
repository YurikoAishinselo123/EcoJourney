using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private bool destroyObject = false;
    private GameObject targetObject; // Reference to the rubbish object to destroy
    public event System.Action<GameObject> OnDestroyed;

    private PointManager pointManager;  // Reference to PointManager
    private Vector2 direction;
    private float horizontalAxis;
    private float verticalAxis;

    void Start()
    {
        // Find and cache the PointManager
        pointManager = FindObjectOfType<PointManager>();
    }

    void Update()
    {
        Movement();
        Facing();
        Animation();
        Punch();
    }

    void Movement()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        direction = new Vector2(horizontalAxis, verticalAxis);
        transform.Translate(direction * Time.deltaTime * MovementSpeed);
    }

    void Facing()
    {
        if (horizontalAxis != 0)
        {
            bool isFacingRight = horizontalAxis > 0;
            transform.localScale = new Vector3(isFacingRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Animation()
    {
        if (horizontalAxis != 0f)
        {
            animator.SetBool("isWalk", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);
        }
    }


    void Punch()
    {
        // Always run the punch animation
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetTrigger("isPunch");

            // Only destroy if the player has triggered a rubbish object
            if (destroyObject && targetObject != null)
            {
                // Call the RubbishSpawner's RemoveObject method to destroy the target object
                RubbishSpawner rubbishSpawner = FindObjectOfType<RubbishSpawner>();
                if (rubbishSpawner != null)
                {
                    rubbishSpawner.RemoveObject(targetObject);
                    targetObject = null; // Clear the reference after destroying
                    destroyObject = false; // Reset the flag
                    pointManager.AddPoints(1);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rubbish"))
        {
            destroyObject = true;
            targetObject = collision.gameObject; // Set the target object to the triggered one
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rubbish"))
        {
            destroyObject = false;
            targetObject = null; // Clear the reference when exiting the trigger
        }
    }

    private void OnDestroy()
    {
        if (OnDestroyed != null)
        {
            OnDestroyed(gameObject);
        }
    }
}
