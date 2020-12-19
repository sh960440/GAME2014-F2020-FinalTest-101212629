using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public bool isActive;
    [SerializeField] 
    float floatingDistance;
    [SerializeField]
    float floatSpeed = 2.0f;
    [SerializeField]
    float minimumSize = 0.05f;
    public AudioSource audioSource;
    public AudioClip[] sounds;
    private float shrinkingFactor = 0.996f;
    private float expandingFactor = 1.006f;
    private float startYPosition;
    private int movingDirection = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startYPosition = transform.position.y;
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {
            Shrink();

            if (transform.localScale.x < minimumSize)
            {
                StartCoroutine("ResetFromZero");
            }
        }
        else
        {
            Float();
            if (transform.localScale.x <= 1.0f)
            {
                Expand();
            }
        }
    }

    private void Shrink()
    {
        if (transform.localScale.x >= minimumSize)
        {
            var currentScale = transform.localScale;
            transform.localScale = currentScale * shrinkingFactor;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }

    private void Expand()
    {
        if (transform.localScale.x <= 1.0f)
        {
            var currentScale = transform.localScale;
            transform.localScale = currentScale * expandingFactor;
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

    IEnumerator ResetFromZero()
    {
        yield return new WaitForSeconds(1.0f);
        transform.localScale = new Vector3(minimumSize, minimumSize, minimumSize);
        isActive = false;
    }
}
