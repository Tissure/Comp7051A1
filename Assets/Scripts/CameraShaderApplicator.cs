using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraShaderApplicator : MonoBehaviour
{

    private Material _Material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Check for when no material is provided. If none, apply the pre-existing source.
        if (_Material == null)
        {
            Graphics.Blit(source, destination);
            return;
        } 

        // Material is Provided, modify the incoming Source.
        Graphics.Blit(source, destination, _Material);
    }

    public bool setImageEffect(Material inputMaterial)
    {
        if (inputMaterial != null) 
        { 
            _Material = inputMaterial;
            return true;
        }

        // Something went wrong, check valid material.
        return false;
    }

    public bool wipeImageEffect()
    {
        _Material = null;
        return true;
    }

}
