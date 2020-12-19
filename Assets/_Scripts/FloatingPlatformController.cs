using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public bool isActive;
    public float platformTimer; 
    public float floatingDistance;
    public float floatSpeed = 2.0f;
    //public float threshold;
    //public PlayerBehaviour player;
    private float startYPosition;
    private int movingDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerBehaviour>();

        startYPosition = transform.position.y;
        platformTimer = 0.1f;
        platformTimer = 0;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            platformTimer += Time.deltaTime;
            Shrink();
        }
        else
        {
            Float();
        }
    }

    private void Shrink()
    {
        if (transform.localScale.magnitude >= 0.1f)
        {
            var currentScale = transform.localScale;
            transform.localScale = currentScale * 0.995f;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }

    private void Float()
    {
        if (transform.position.y >= startYPosition + floatingDistance)
        {
            movingDirection = -1;
        }
        else if (transform.position.y <= startYPosition - floatingDistance)
        {
            movingDirection = 1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * movingDirection * floatSpeed, transform.position.z);
    }

    public void Reset()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        platformTimer = 0;
    }
}
