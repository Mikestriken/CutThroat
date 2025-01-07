using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FPS_Limiter : MonoBehaviour
{
    private void Start() {
        Application.targetFrameRate = (int)math.round((float)Screen.currentResolution.refreshRateRatio.value);
    }
}
