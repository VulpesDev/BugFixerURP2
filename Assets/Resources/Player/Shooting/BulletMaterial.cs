using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMaterial : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color emission;
    Renderer _renderer;
    MaterialPropertyBlock _propBlock;
    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", emission);
        _renderer.SetPropertyBlock(_propBlock);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
