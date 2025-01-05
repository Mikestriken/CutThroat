using Unity.VisualScripting;
using UnityEngine;

public class FPS_Limiter : MonoBehaviour
{
    private void Start() {
        Application.targetFrameRate = Screen.currentResolution.refreshRateRatio.ConvertTo<int>();
    }
}
