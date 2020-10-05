using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float angleX = 90;
    public GameObject toFollow;
    public GameObject reverseCamera;
    public HashSet<GameObject> TrackedObjects = new HashSet<GameObject>();
    public bool ignoreFirstFrame = false;

    void Update()
    {
        if (!ignoreFirstFrame)
        {
            ignoreFirstFrame = true;
            return;
        }

        float mouseAccel = 1000 * Screen.height / 1080;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            mouseAccel /= 100;
        }

        angleX = Mathf.Clamp(angleX - mouseAccel * Time.deltaTime * Input.GetAxis("Mouse Y"), 20, 170);

        Vector3 offset = Quaternion.AngleAxis(angleX, Vector3.right) * Vector3.down * 7.2f;
        Vector3 newPosition = toFollow.transform.position +
                              toFollow.transform.TransformDirection(offset);

        Vector3 delta = newPosition - toFollow.transform.position;

        var newObjects = getObjectsBetweenCameraAndPlayer();
        foreach (var collider in newObjects)
        {
            TrackedObjects.Remove(collider.gameObject);

            var meshRenderer = collider.gameObject.GetComponent<MeshRenderer>();
            ToFadeMode(meshRenderer.material);
            var materialColor = meshRenderer.material.color;
            materialColor.a = Mathf.Lerp(0, 0.5f,
                (newPosition - collider.transform.position).magnitude / delta.magnitude);
            meshRenderer.material.color = materialColor;
        }

        foreach (var trackedObject in TrackedObjects)
        {
            var meshRenderer = trackedObject.GetComponent<MeshRenderer>();
            ToOpaqueMode(meshRenderer.material);
            var materialColor = meshRenderer.material.color;
            materialColor.a = 1f;
            meshRenderer.material.color = materialColor;
        }

        TrackedObjects = newObjects;

        transform.position = newPosition;
        transform.LookAt(toFollow.transform);
    }

    public static void ToOpaqueMode(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    public static void ToFadeMode(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
    }

    private HashSet<GameObject> getObjectsBetweenCameraAndPlayer()
    {
        var newObjects = new HashSet<GameObject>();

        var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(reverseCamera.GetComponent<Camera>());
        // DestroyImmediate(virtualCamera);

        Vector3 delta = toFollow.transform.position - transform.position;
        var colliders = Physics.OverlapSphere(transform.position, delta.magnitude, 1 << 8);
        foreach (var collider in colliders)
        {
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, collider.bounds))
            {
                if (collider.bounds.size.magnitude < 3)
                {
                    newObjects.Add(collider.gameObject);
                }
            }
        }

        return newObjects;
    }
}