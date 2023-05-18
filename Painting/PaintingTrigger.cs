using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingTrigger : MonoBehaviour
{
    [SerializeField] protected GameObject _fX;
    protected void RandomSetMaterials(Renderer renderer, Material material, bool isBrush = false)
    {
        string baseMaterial = GameManager.Instance.References.GameConfig.BaseMaterial.name + " (Instance)";
        Material[] mats = new Material[3];

        mats = renderer.materials;
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            if (i == 1)
                continue;

            print(renderer.sharedMaterials[i]);
            if (renderer.sharedMaterials[i].name == baseMaterial)
            {
                
                mats[i] = material;
                print("called");
                break;
            }


            
        }

        renderer.materials = mats;
    }

    protected Material GetRandomMaterial(Material[] materialArray, Material mtl1, Material mtl2)
    {
        int index = Random.Range(0, materialArray.Length);
        Material randomMaterial = materialArray[index];

        if (randomMaterial != mtl1 && randomMaterial != mtl2)
            return randomMaterial;
        else
            return GetRandomMaterial(materialArray, mtl1, mtl2);
    }
}
