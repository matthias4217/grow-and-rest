using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreeGenObserver
{
    /*private static List<SlimeGenerator> generators = new List<SlimeGenerator>();

    public static void Subscribe(SlimeGenerator generator)
    {
        generators.Add(generator);
    }

    private static void Unsubscribe(SlimeGenerator generator)
    {
        generators.Remove(generator);
    }

    public static bool Notify()
    {
        return (generators.Count > 0);
    }

    public static void UnsubscribeAll()
    {
        foreach (SlimeGenerator generator in generators)
        {
            Unsubscribe(generator);
        }
    }*/
    private static bool generatorClicked;

    public static void Notify()
    {
        Debug.Log("Notify received");
        generatorClicked = true;
    }

    public static bool GeneratorClicked()
    {
        Debug.Log("GeneratorClicked requested");
        return generatorClicked;
    }

    public static void EndEvent()
    {
        generatorClicked = false;
    }
}
