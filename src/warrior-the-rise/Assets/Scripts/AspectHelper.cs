using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class AspectHelper
{
    public const int X_ASPECT = 9;
    public const int Y_ASPECT = 16;
    public const float ASPECT_RATIO = (float)X_ASPECT / Y_ASPECT;
    public const float CAMERA_SIZE = 10;


    public static float GetScaleFactor(float currentAspect)
    {
        return currentAspect / ASPECT_RATIO;
    }

    public static float GetWidthInUnits()
    {
        return CAMERA_SIZE * ASPECT_RATIO;
    }

    public static float GetHeightInUnits()
    {
        return CAMERA_SIZE;
    }
}

