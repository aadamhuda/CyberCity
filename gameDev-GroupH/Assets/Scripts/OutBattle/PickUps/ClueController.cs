using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueController : MonoBehaviour
{
    public float rotationSpeed;
    public float bobSpeed;
    public float ceiling;
    public float floor;
    bool moveUp;


    // Update is called once per frame
    void Update()
    {
        Rotate();
        Bob();

    }

    // Clue rotation
    void Rotate()
    {
        
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
    }

    // Clue bobbing up and down
    void Bob()
    {
        if (moveUp)
        {
            transform.Translate(new Vector3(0, 0.1f, 0) * bobSpeed * Time.deltaTime);
            if (transform.position.y > ceiling)
            {
                moveUp = false;
            }

        }
        else
        {
            transform.Translate(new Vector3(0, -0.1f, 0) * bobSpeed * Time.deltaTime);
            if (transform.position.y < floor)
            {
                moveUp = true;
            }
        }
    }
}
