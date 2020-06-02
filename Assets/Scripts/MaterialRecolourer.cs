using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adjusts the color properties of a material via changes to the material's internal MaterialPropertyBlock.
/// This allows changing material properties without creating a new instance of that material.
/// </summary>

[Serializable]
public class MaterialRecolourer
{
    // Unity doesn't allow initialization of a MaterialPropertyBlock in a constructor, hence this redundant-appearing property
    private MaterialPropertyBlock propertyBlock;
    private MaterialPropertyBlock PropertyBlock
    {
        get
        {
            if(propertyBlock == null)
            {
                propertyBlock = new MaterialPropertyBlock();
            }
            return propertyBlock;
        }
        set
        {
            propertyBlock = value;
        }
    }

    /// <summary>
    /// Apply a MaterialPropertyBlock to add emission to all passed MeshRenderers.
    /// This allows the properties of the material to be changed on this Thing only WITHOUT creating an instance.
    /// </summary>
    public void Recolour(MeshRenderer meshRenderer, Color color)
    {
        meshRenderer.GetPropertyBlock(PropertyBlock);
        PropertyBlock.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(PropertyBlock);
    }

    /// <summary>
    /// Apply a MaterialPropertyBlock to add emission to all passed MeshRenderers.
    /// This allows the properties of the material to be changed on this Thing only WITHOUT creating an instance.
    /// </summary>
    public void Emission(MeshRenderer meshRenderer, Color emissionColor, float emissionLevel)
    {
        Color finalColor = emissionColor * Mathf.LinearToGammaSpace(emissionLevel);
        meshRenderer.GetPropertyBlock(PropertyBlock);
        PropertyBlock.SetColor("_EmissionColor", finalColor);
        meshRenderer.SetPropertyBlock(PropertyBlock);
    }

    /// <summary>
    /// Reset ALL the MaterialPropertyBlock settings on the renderer, causing all materials to revert to their defaults.
    /// </summary>
    public void Reset(MeshRenderer meshRenderer)
    {
        // NOTE that this will clear any starting colors/emissions
        PropertyBlock.Clear();
        meshRenderer.SetPropertyBlock(PropertyBlock);
    }

}
