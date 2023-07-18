using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColourSettings settings;

    public ColorGenerator(ColourSettings _settings)
    {
        this.settings = _settings;
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }
}
