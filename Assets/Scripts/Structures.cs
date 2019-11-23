using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

class Structure
{
    public float originAngle;
    public (int, int)[] points;
    public List<int> availablePoints;


    public bool IsStructFull()
    {
        return availablePoints.Count < 1;
    }
    public (int, int) GetRandomAvailablePoint()
    {
        if (IsStructFull())
            throw new Exception("No available point found !");
        int pointIndex = Random.Range(0, availablePoints.Count);
        availablePoints.Remove(pointIndex);
        return points[pointIndex];
    }

    public static Structure GetDolmen()
    {
        Structure dolmen = new Structure();
        dolmen.points = new[] {(4,0), (6,0), (3,1), (4,1), (5,1), (6,1), (7,1)};
        dolmen.availablePoints = new List<int>();
        for (int i = 0; i < 7; i++)
            dolmen.availablePoints.Add(i);

        return dolmen;
    }
}
