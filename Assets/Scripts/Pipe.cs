using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    
    private float leftEdge;
    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
