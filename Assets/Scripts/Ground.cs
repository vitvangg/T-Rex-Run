using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    
    private MeshRenderer meshRender;
    private void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        meshRender.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
