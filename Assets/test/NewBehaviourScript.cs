using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [ContextMenu("mesh")]
    public void testmeshexp()
    {
        Meshes.Export(GetComponent<MeshFilter>().sharedMesh, GetComponent<MeshRenderer>().sharedMaterials);
    }

    [ContextMenu("mat")]
    public void testmatexp()
    {
        Materials.Export(GetComponent<MeshRenderer>().sharedMaterials);
    }

    [ContextMenu("test")]
    public void test()
    {
        var p = AssetDatabase.GetAssetPath(GetComponent<MeshFilter>().sharedMesh);

        Debug.Log(Path.GetFileName(p));
    }


    private GameObject _lastclose;

    private NavMeshAgent _agent;

    private List<GameObject> _destoy = new List<GameObject>();
    public float Health = 100f;
    private float _rev = 1f;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        var test = GameObject.FindGameObjectsWithTag("blue");
        var closeest = GameObject.FindGameObjectWithTag("blue");
        var lastdis = Mathf.Infinity;

        for (var i = 0; i < test.Length; i++)
        {
            var dis = Vector3.Distance(transform.position, test[i].transform.position);

            if (dis < lastdis)
            {
                closeest = test[i];
                lastdis = dis;
            }
            if (dis < 1)
            {
                var health = test[i].GetComponent<NewBehaviourScript>();
                if (health != null)
                {
                    health.Health -= Random.Range(Time.deltaTime/2, Time.deltaTime*2);
                }

            }

        }
        _rev -= Time.deltaTime;

        if (_lastclose != closeest || _rev < 0)
        {
            _rev = Random.Range(0.7f, 1.2f);
            _lastclose = closeest;
            if (_lastclose != null)
                _agent.SetDestination(_lastclose.transform.position);
        }

        if (Health < 0)
        {
            Destroy(gameObject);
        }
    }













}