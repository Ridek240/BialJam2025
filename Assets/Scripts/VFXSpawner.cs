using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class VFXSpawner : MonoBehaviour
{

    public VisualEffectAsset vfxGraph;

    public static VFXSpawner instance;


    private void Awake()
    {
        instance = this;
    }
    public void SpawnVFX(Vector3 position)
    {
        // Tworzenie pustego obiektu
        GameObject vfxObject = new GameObject("VFX_" + vfxGraph.name);
        vfxObject.transform.position = position;

        // Dodanie komponentu VisualEffect
        VisualEffect vfx = vfxObject.AddComponent<VisualEffect>();
        vfx.visualEffectAsset = vfxGraph;

        // Uruchomienie coroutine
        StartCoroutine(WaitAndDestroy(vfxObject, vfx));
    }

    private IEnumerator WaitAndDestroy(GameObject obj, VisualEffect vfx)
    {
        yield return new WaitForSeconds(2f);

        Destroy(obj);
    }
}
