using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject grapplePrefab;
    float speed = 0.25f;
    float grappleDistance = 25.0f;
    GameObject grapple;
    GameObject closest;
    Rigidbody rb;
    Transform trans;

    GameOverScreen gameOverScreen;
    PauseScreen pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;

        rb = this.GetComponent<Rigidbody>();
        trans = this.GetComponent<Transform>();

        // Finds the initial closest point
        FindClosest();

        // Sets up UI
        gameOverScreen = GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject.GetComponent<GameOverScreen>();
        pauseScreen = GameObject.Find("Canvas").transform.Find("PauseScreen").gameObject.GetComponent<PauseScreen>();
    }

    /// <summary>
    /// Finds the closest grapple point and highlights it if close enough
    /// </summary>
    void FindClosest()
    {
        GameObject point;
        Vector3 position = trans.position;
        position += 2.0f * rb.velocity.normalized;

        // For every grapple point
        foreach (Component script in (Component[]) Object.FindObjectsOfType<GrapplePointController>())
        {
            point = script.gameObject;

            // If there is no closest, set closest = current point
            if (closest == null)
            {
                closest = point;
                closest.GetComponent<GrapplePointController>().Highlight();
            }
            // If current point is closer than closest, set closest = current point
            else if (Mathf.Abs((point.GetComponent<Transform>().position - position).magnitude) < Mathf.Abs((closest.GetComponent<Transform>().position - position).magnitude))
            {
                // Dehighlight old closest
                closest.GetComponent<GrapplePointController>().DeHighlight();
                closest = point;

                if (Mathf.Abs((closest.GetComponent<Transform>().position - position).magnitude) <= grappleDistance)
                {
                    // If new closest is close enough, highlight it
                    closest.GetComponent<GrapplePointController>().Highlight();
                }
            }
        }

        // If closest is too far
        if (Mathf.Abs((closest.GetComponent<Transform>().position - position).magnitude) > grappleDistance)
        {
            // Dehighlight
            closest.GetComponent<GrapplePointController>().DeHighlight();
        }
    }

    /// <summary>
    /// Creates grapple line
    /// </summary>
    void Grapple()
    {
        if (grapple == null)
        {
            // Create new grapple
            grapple = Instantiate(grapplePrefab, Vector3.zero, Quaternion.identity);
        }

        // Modifies grapple position and scale
        grapple.GetComponent<Transform>().position = ((closest.GetComponent<Transform>().position - trans.position) / 2) + trans.position;
        Vector3 direction = (closest.GetComponent<Transform>().position - trans.position).normalized;
        grapple.GetComponent<Transform>().up = direction;
        trans.up = direction;
        grapple.GetComponent<Transform>().localScale = new Vector3(0.1f, Mathf.Abs((closest.GetComponent<Transform>().position - trans.position).magnitude) / 2, 0.1f);


        this.GetComponent<Animator>().ResetTrigger("Default");
        this.GetComponent<Animator>().ResetTrigger("Walk");
        this.GetComponent<Animator>().SetTrigger("Grapple");
    }

    // Update is called once per frame
    void Update()
    {
        // Code for locking/unlocking the Z Axis
        if (Input.GetButtonDown("Lock"))
        {
            // If Z is already locked, unlock it
            if ((rb.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            // Else lock Z
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
            }
        }

        Vector3 movement = new Vector3(0, 0, 0);

        // Horizontal movement
        if (!Input.GetButton("Jump") || Mathf.Abs((closest.GetComponent<Transform>().position - trans.position).magnitude) > grappleDistance || closest == null)
        {
            // Only update the point if the player isn't grappling, so it doesn't switch mid-grapple
            FindClosest();

            // Delete the grapple line when not grappling
            Destroy(grapple);

            // Add axes to the movement vector
            movement.x += Input.GetAxis("Horizontal");
            movement.z += Input.GetAxis("Vertical");

            this.GetComponent<Animator>().ResetTrigger("Grapple");

            // Faces the player towards where the input axes is pointing
            if (movement != Vector3.zero)
            {
                trans.forward = -movement;

                this.GetComponent<Animator>().ResetTrigger("Default");
                this.GetComponent<Animator>().SetTrigger("Walk");
            }
            else
            {
                this.GetComponent<Animator>().ResetTrigger("Walk");
                this.GetComponent<Animator>().SetTrigger("Default");
            }
        }

        // Grapple loop
        if (Input.GetButton("Jump") && Mathf.Abs((closest.GetComponent<Transform>().position - trans.position).magnitude) <= grappleDistance && closest != null)
        {
            // Disables gravity because it makes everything glitchy
            rb.useGravity = false;

            Vector3 moveVector = closest.GetComponent<Transform>().position - trans.position;

            // Moves player and updates grapple line
            if (moveVector.magnitude > 0.5f)
            {
                movement += moveVector.normalized;
                Grapple();
            }
            // Slows down and hides grapple when the player gets close to the grapple point
            else
            {
                rb.velocity = 0.75f * rb.velocity;
                Destroy(grapple);

                trans.forward = new Vector3(movement.x, 0, movement.y);
            }
        }
        // Re-enables gravity
        else if (rb.useGravity == false)
        {
            rb.useGravity = true;
            trans.forward = new Vector3(movement.x, 0, movement.y);
        }

        // Applies new velocity
        rb.velocity += speed * movement.normalized;

        // Checks for pause button
        if (Input.GetButtonDown("Pause"))
        {
            pauseScreen.Setup();
        }
    }

    void OnDestroy()
    {
        // Displays game over screen on player death
        gameOverScreen.Setup();
    }
}