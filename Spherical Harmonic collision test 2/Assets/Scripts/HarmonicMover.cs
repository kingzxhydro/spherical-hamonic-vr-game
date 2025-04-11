using UnityEngine;

public class HarmonicMover : MonoBehaviour
{
    public float moveSpeed = 2f;  // Speed of movement

    void Update()
    {
        // Move left when Left Arrow Key is pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}
