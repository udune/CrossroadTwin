using UnityEngine;

[CreateAssetMenu(fileName = "SignalConfig", menuName = "CrossroadTwin/SignalConfig")]
public class SignalConfig : ScriptableObject
{
    [Header("SLMP Communication")]
    public string plcIpAddress = "192.168.3.39";
    public int plcPort = 5007;
    public float pollingInterval = 0.1f; // 100ms

    [Header("Phase A Timing (1~21sec)")]
    public float phaseA_GreenTime = 18f;
    public float phaseA_YellowTime = 3f;
    public float phaseA_5ClockCrosswalk_SolidTime = 9f;
    public float phaseA_5ClockCrosswalk_BlinkTime = 9f;

    [Header("Phase B Timing (22~48sec)")]
    public float phaseB_GreenTime = 24f;
    public float phaseB_YellowTime = 3f;
    public float phaseB_10ClockCrosswalk_SolidTime = 15f;
    public float phaseB_10ClockCrosswalk_BlinkTime = 9f;

    [Header("Phase C Timing (49~90sec)")]
    public float phaseC_StraightTime = 39f;
    public float phaseC_YellowTime = 3f;
    public float phaseC_12ClockCrosswalk_SolidTime = 9f;
    public float phaseC_12ClockCrosswalk_BlinkTime = 9f;

    [Header("Special Mode Timers")]
    public float elderlyExtensionTime = 10f;
    public float emergencyYellowTime = 5f;
    public float emergencyRedBlinkTime = 10f;
    public float noVehicleDetectionTime = 10f;

    [Header("Blink Interval")]
    public float blinkInterval = 0.5f;
}
