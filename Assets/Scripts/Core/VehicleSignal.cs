using UnityEngine;

public class VehicleSignal : MonoBehaviour
{
    [SerializeField] private SignalLight lightRed;
    [SerializeField] private SignalLight lightYellow;
    [SerializeField] private SignalLight lightGreen;
    [SerializeField] private SignalLight lightLeftArrow;

    public void SetRed()
    {
        if (lightRed != null) lightRed.SetOn(true);
        if (lightYellow != null) lightYellow.SetOn(false);
        if (lightGreen != null) lightGreen.SetOn(false);
        if (lightLeftArrow != null) lightLeftArrow.SetOn(false);
    }

    public void SetYellow()
    {
        if (lightRed != null) lightRed.SetOn(false);
        if (lightYellow != null) lightYellow.SetOn(true);
        if (lightGreen != null) lightGreen.SetOn(false);
        if (lightLeftArrow != null) lightLeftArrow.SetOn(false);
    }

    public void SetGreen()
    {
        if (lightRed != null) lightRed.SetOn(false);
        if (lightYellow != null) lightYellow.SetOn(false);
        if (lightGreen != null) lightGreen.SetOn(true);
        if (lightLeftArrow != null) lightLeftArrow.SetOn(false);
    }

    public void SetLeftTurn()
    {
        if (lightRed != null) lightRed.SetOn(true);
        if (lightYellow != null) lightYellow.SetOn(false);
        if (lightGreen != null) lightGreen.SetOn(false);

        if (lightLeftArrow != null)
        {
            lightLeftArrow.SetOn(true);
        }
    }
}
