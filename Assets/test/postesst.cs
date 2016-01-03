using UnityEngine;

public class postesst : MonoBehaviour
{
    public GameObject child;
    public Vector3 add;
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
