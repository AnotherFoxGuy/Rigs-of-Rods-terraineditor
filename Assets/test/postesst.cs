using UnityEngine;

public class postesst : MonoBehaviour
{
    public Vector3 add;
    public GameObject child;
    public bool useEuler;

    public void OnDrawGizmosSelected()
    {
        if (useEuler)
        {
            var r = transform.eulerAngles;
            child.transform.eulerAngles = r + add;
        }
        else
        {
            var r = transform.rotation;
            child.transform.rotation = r*Quaternion.Euler(add);
        }
    }
}