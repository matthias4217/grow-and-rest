using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFactory : MonoBehaviour
{
    private static SlimeFactory instance;

    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private int slimesToPreinstantiate;

    private static Queue<SlimeAvatar> availableSlimes = new Queue<SlimeAvatar>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void Start()
    {
        PreinstantiateSlimes(slimesToPreinstantiate);
    }

    public static SlimeAvatar GetSlime(Vector2 SpawnPosition)
    {
        SlimeAvatar slime = null;
        if (availableSlimes.Count > 0)
        {
            slime = availableSlimes.Dequeue();
        }
        if (slime == null)
        {
            slime = CreateSlime();
        }

        slime.gameObject.SetActive(true);
        slime.transform.position = SpawnPosition;
        return slime;
    }

    public static SlimeAvatar CreateSlime()
    {
        GameObject slimeInstance = Instantiate(instance.slimePrefab);
        slimeInstance.transform.parent = instance.transform;
        slimeInstance.SetActive(false);
        SlimeAvatar slime = slimeInstance.GetComponent<SlimeAvatar>();
        return slime;
    }

    private void PreinstantiateSlimes(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SlimeAvatar slime = CreateSlime();
            availableSlimes.Enqueue(slime);
        }
    }

    public static void Release(SlimeAvatar slime)
    {
        slime.gameObject.SetActive(false);
        availableSlimes.Enqueue(slime);
    }
}
