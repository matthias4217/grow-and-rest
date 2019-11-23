using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFactory : MonoBehaviour
{
    private static SlimeFactory _instance;

    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private int slimesToPreinstantiate;

    private static Queue<SlimeAvatar> _availableSlimes = new Queue<SlimeAvatar>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }

        _instance = this;
    }

    private void Start()
    {
        PreinstantiateSlimes(/*slimesToPreinstantiate*/);
    }

    public static SlimeAvatar GetSlime(Vector2 spawnPosition)
    {
        SlimeAvatar slime = null;
        if (_availableSlimes.Count > 0)
        {
            slime = _availableSlimes.Dequeue();
        }
        if (slime == null)
        {
            slime = CreateSlime();
        }

        slime.gameObject.SetActive(true);
        slime.transform.position = spawnPosition;
        return slime;
    }

    public static SlimeAvatar CreateSlime()
    {
        GameObject slimeInstance = Instantiate(_instance.slimePrefab);
        slimeInstance.transform.parent = _instance.transform;
        slimeInstance.SetActive(false);
        SlimeAvatar slime = slimeInstance.GetComponent<SlimeAvatar>();
        return slime;
    }

    private void PreinstantiateSlimes(/*int quantity*/)
    {
        SlimeGenerator[] slimeGenerators = FindObjectsOfType<SlimeGenerator>();
        slimesToPreinstantiate = 0;

        for (int j = 0; j < slimeGenerators.Length; j++)
        {
            slimesToPreinstantiate += slimeGenerators[j].ToGenerate;
        }


        for (int i = 0; i < slimesToPreinstantiate; i++)
        {
            SlimeAvatar slime = CreateSlime();
            _availableSlimes.Enqueue(slime);
        }
    }

    public static void Release(SlimeAvatar slime)
    {
        slime.ResetState();
        slime.gameObject.SetActive(false);
        _availableSlimes.Enqueue(slime);
    }
}
