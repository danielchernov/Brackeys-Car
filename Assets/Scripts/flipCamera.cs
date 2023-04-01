using UnityEngine;

public class flipCamera : MonoBehaviour
{
    Camera flipinCamera;
    public bool flipHorizontal;

    void Awake()
    {
        flipinCamera = GetComponent<Camera>();
    }

    void Start()
    {
        flipinCamera.ResetWorldToCameraMatrix();
        flipinCamera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        flipinCamera.projectionMatrix = flipinCamera.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreCull()
    {
        flipinCamera.ResetWorldToCameraMatrix();
        flipinCamera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        flipinCamera.projectionMatrix = flipinCamera.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreRender()
    {
        GL.invertCulling = flipHorizontal;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
