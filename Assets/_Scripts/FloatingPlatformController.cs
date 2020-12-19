using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public bool isActive;
    public float platformTimer; 
    public float threshold;
    public PlayerBehaviour player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

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
            _Shrink();
        }
    }

    private void _Shrink()
    {
        if (transform.localScale.magnitude >= 0.01f)
        {
            var currentScale = transform.localScale;
            transform.localScale = currentScale * 0.999f;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void Reset()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        platformTimer = 0;
    }
}
