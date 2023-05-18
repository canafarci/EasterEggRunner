using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampPaintingTrigger : PaintingTrigger
{
    [SerializeField] Material[] _stampMaterials;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer[] renderers = other.transform.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.name == "EggPattern_brush")
                {
                    renderer.gameObject.SetActive(true);
                    RandomSetMaterials(renderer, _stampMaterials[0], true);
                }
            }
        }
    }
}
