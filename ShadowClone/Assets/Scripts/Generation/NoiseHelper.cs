using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseHelper
{

    // NoiseHelper generates various noise values, including Perlin


    #region PERLIN


    // Gets 2D Perlin noise value
    // NOTE: PERLIN NOISE CAN BE STRETCHED AND CLAMPED
    //     (values almost always fell within .25 to .75 range, stretched to 0 to 1 range)
    //     (Note this may mean Perlin operations might not behave as expected when dealing with random numbers)
    //--------------------------------------//
    public static float Perlin2D(Vector2 position, float offset, float scale, bool stretchValues, bool clampValues)
    //--------------------------------------//
    {
        // Get noise
        float noise = Mathf.PerlinNoise((position.x + .1f) / scale + offset, (position.y + .1f) / scale + offset);

        // Stretch noise (values rarely fall outside of the .25 to .75 range, stretch to 0 to 1 range)
        if (stretchValues)
            noise = (noise * 2f) - .5f;

        // Clamp noise if called for
        if (clampValues)
        {
            if (noise < 0f)
            {
                noise = 0f;
            }
            else if (noise > 1f)
            {
                noise = 1f;
            }
        }

        return noise;

    } // END Perlin2D


    // 2D Perlin override, defaultly does not stretch or clamp the values
    //--------------------------------------//
    public static float Perlin2D(Vector2 position, float offset, float scale)
    //--------------------------------------//
    {
        return Perlin2D(position, offset, scale, false, false);

    } // END Perlin2D Override


    #endregion


} // END NoiseHelper.cs
