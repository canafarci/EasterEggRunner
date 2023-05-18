using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePaintingTrigger : MonoBehaviour
{
    [SerializeField] Color _baseColor;
    [SerializeField] GameObject _fX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject fx = Instantiate(_fX, other.transform.position, _fX.transform.rotation);
            Destroy(fx, 2f);

            Renderer[] renderers = other.transform.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.name == "EggWhole")
                    renderer.material.DOColor(_baseColor, 0.2f);
            }
        }
    }
}
