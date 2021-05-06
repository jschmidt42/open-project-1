using UnityEngine;

public class SetSkyboxMaterial : MonoBehaviour
{
    public Material skyboxMaterial;

    private void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
    }
}
