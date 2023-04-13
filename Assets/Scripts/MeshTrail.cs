using System.Collections;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{

    public float activeTime = 0.2f;

    public float meshRefreshRate = 0.02f;
    public float meshDestroyDelay = 0.2f;


    private bool _isTrailActive;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform spawnPos;

    public Material[] mat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isTrailActive)
        {
            _isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float time)
    {
        while (time > 0)
        {
            time -= meshRefreshRate;

            if (skinnedMeshRenderer == null)
                skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            GameObject newObject = new GameObject();

            newObject.transform.SetLocalPositionAndRotation(spawnPos.position, spawnPos.rotation);
            MeshRenderer mr = newObject.AddComponent<MeshRenderer>();
            MeshFilter mf = newObject.AddComponent<MeshFilter>();


            Mesh mesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(mesh);

            mf.mesh = mesh;
            mr.materials = mat;


            Destroy(newObject, meshDestroyDelay);


            _isTrailActive = false;
            GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(meshRefreshRate);
        }
    }
}
