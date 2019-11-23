using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreeDeathObserver
{
    private static List<SlimeGenerator> generators = new List<SlimeGenerator>();

    public static void Subscribe(SlimeGenerator generator)
    {
        generators.Add(generator);
    }

    public static void Unsubscribe(SlimeGenerator generator)
    {
        generators.Remove(generator);
        if (generators.Count == 0)
        {
            Notify();
        }
    }

    private static void Notify()
    {
        GameManager.OnAllTreesDead();
    }
}
