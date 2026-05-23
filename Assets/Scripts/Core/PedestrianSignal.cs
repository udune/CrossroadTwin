using UnityEngine;

public class PedestrianSignal : MonoBehaviour
{
    [SerializeField] private SignalLight lightRed;
    [SerializeField] private SignalLight lightGreen;

    public void SetRed()
    {
        if (lightRed != null) lightRed.SetOn(true);
        if (lightGreen != null) lightGreen.SetOn(false);
    }

    public void SetGreen()
    {
        if (lightRed != null) lightRed.SetOn(false);
        if (lightGreen != null) lightGreen.SetOn(true);
    }

    public void SetBlink(bool isOn)
    {
        if (lightGreen != null) lightGreen.SetOn(isOn);
    }
}
