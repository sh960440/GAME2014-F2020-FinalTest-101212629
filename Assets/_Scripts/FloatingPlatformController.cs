/****************************************************************************************************
    Source File Name: FloatingPlatformController.cs
    Student Name: Shun min Hsieh
    StudentID: 101212629
    Date Last Modified: 2020/12/19
    Program Description: A class contols the behavior of a floating platform. It includes shrinking, expanding and floating.
    Revision History: 
        - 2020/12/19
            - Added Start function
            - Added Update function
            - Added Shrink function
            - Added Expand function
            - Added Float function
            - Added ResetFromZero function
    Class: 
        - FloatingPlatformController
    Methods: 
        - Start()
        - Update()
        - Shrink()
        - Expand()
        - Float()
        - ResetFromZero()
****************************************************************************************************/
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
        startYPosition = transform.position.y; // Store initial Y position
        isActive = false;
    }

    void Update()
    {
        if (isActive) // If the player steps on this platform
        {
            Shrink();

            if (transform.localScale.x < minimumSize) // If the scale is set to 0
            {
                StartCoroutine("ResetFromZero");
            }
        }
        else // If the player is not on this platform
        {
            Float();
            if (transform.localScale.x <= 1.0f) // If it's still smaller than the original size
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
            transform.localScale = Vector3.zero; // If it's already too small, directly set it to 0 instead of shrinking
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
        // Periodically move up and down between two Y positions
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
