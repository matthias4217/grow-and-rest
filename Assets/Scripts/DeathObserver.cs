using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathObserver
{
    private static List<SlimeGenerator> generators = new List<SlimeGenerator>();
    private static List<SlimeAvatar> avatars = new List<SlimeAvatar>();

    public static void Subscribe(SlimeGenerator generator)
    {
        generators.Add(generator);
    }

    public static void Unsubscribe(SlimeGenerator generator)
    {
        generators.Remove(generator);
        if (generators.Count == 0 && avatars.Count == 0)
        {
            Notify();
        }
    }

    public static void Subscribe(SlimeAvatar avatar)
    {
        avatars.Add(avatar);
    }

    public static void Unsubscribe(SlimeAvatar avatar)
    {
        avatars.Remove(avatar);
        if (generators.Count == 0 && avatars.Count == 0)
        {
            Notify();
        }
    }

    private static void Notify()
    {
        GameManager.OnAllTreesDead();
    }
}
