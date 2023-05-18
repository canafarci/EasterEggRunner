using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPaintingTrigger : PaintingTrigger
{
    [SerializeField] Material[] _patternMaterials;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject fx = Instantiate(_fX, other.transform.position, _fX.transform.rotation);
            Destroy(fx, 2f);

            Renderer[] renderers = other.transform.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.name == "EggPattern")
                {
                    renderer.gameObject.SetActive(true);
                    RandomSetMaterials(renderer, _patternMaterials[0]);
                }
            }
        }
    }
}
