using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EarthAvatar : MonoBehaviour
    {
        private float _radius;

        private void Start()
        {
            // we suppose the scale to be the same in X and Y
            _radius = GetComponent<CircleCollider2D>().radius;
            Debug.Log(GetUnityCoords(0));
            Debug.Log(GetAngle(new Vector3(0, _radius)));
            Debug.Log("Angle 10: " + GetUnityCoords(10f));
            Debug.Log("(1.9, -3): " + GetAngle(new Vector3(1.9f, -3f)));
            Debug.Log("Angle 10 to angle: " + GetAngle(GetUnityCoords(10f)));
            Debug.Log("Angle 180 to angle: " + GetAngle(GetUnityCoords(180f)));
            Debug.Log("Angle 90 to angle: " + GetAngle(GetUnityCoords(90f)));
        }

        // the angle 0 is at the top
        public Vector3 GetUnityCoords(float angle)
        {
            return _radius * new Vector3(Mathf.Cos((90f-angle)*2*Mathf.PI/360f), Mathf.Sin((90f-angle)*2*Mathf.PI/360f)) + transform.position;
        }

        public float GetAngle(Vector3 unityCoords)
        {
            return Vector3.SignedAngle(Vector3.up, (unityCoords - transform.position).normalized,
                Vector3.back);
        }
    }
}
