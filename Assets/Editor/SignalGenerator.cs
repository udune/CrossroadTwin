using UnityEngine;
using UnityEditor;

public class SignalGenerator : Editor
{
    private const string MATERIALS_PATH = "Assets/Materials/";

    [MenuItem("Tools/CrossroadTwin/Generate Signals")]
    public static void GenerateSignals()
    {
        // Signals 부모 오브젝트 찾기 or 생성
        GameObject signalsParent = GameObject.Find("Signals");
        if (signalsParent != null)
        {
            DestroyImmediate(signalsParent);
        }
        signalsParent = new GameObject("Signals");

        // 재질 폴더 생성
        if (!AssetDatabase.IsValidFolder(MATERIALS_PATH))
        {
            AssetDatabase.CreateFolder("Assets", "Materials");
        }

        // 6개 신호등 생성
        CreatePedestrianSignal_10Clock(signalsParent.transform);
        CreateVehicleSignal_10Clock(signalsParent.transform);
        CreatePedestrianSignal_12Clock(signalsParent.transform);
        CreateVehicleSignal_12Clock(signalsParent.transform);
        CreatePedestrianSignal_5Clock(signalsParent.transform);
        CreateVehicleSignal_5Clock(signalsParent.transform);

        Debug.Log("Signals generated successfully.");
    }

    // 1. Signal_Pedestrian_10Clock
    private static void CreatePedestrianSignal_10Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Pedestrian_10Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(-9f, 0f, 3.5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.25f, 0.5f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.4f, 0.08f), "#FF2222", "#330000");
        GameObject lightGreen = CreateLight(head.transform, "Light_Green", new Vector3(0f, 2.15f, 0.08f), "#00CC44", "#003300");

        PedestrianSignal pedSignal = signal.AddComponent<PedestrianSignal>();
        pedSignal.lightRed = lightRed.GetComponent<SignalLight>();
        pedSignal.lightGreen = lightGreen.GetComponent<SignalLight>();

        pedSignal.SetRed();
    }

    // 2. Signal_Vehicle_10Clock
    private static void CreateVehicleSignal_10Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Vehicle_10Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(-9f, 0f, 1.5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.3f, 0.9f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.55f, 0.08f), "#FF2222", "#330000");
        GameObject lightYellow = CreateLight(head.transform, "Light_Yellow", new Vector3(0f, 2.3f, 0.08f), "#FFBB00", "#332200");
        GameObject lightGreen = CreateLight(head.transform, "Light_Green", new Vector3(0f, 2.05f, 0.08f), "#00CC44", "#003300");
        GameObject lightLeftArrow = CreateLight(head.transform, "Light_LeftArrow", new Vector3(-0.16f, 2.3f, 0.08f), "#44FF88", "#003300");

        VehicleSignal vehSignal = signal.AddComponent<VehicleSignal>();
        vehSignal.lightRed = lightRed.GetComponent<SignalLight>();
        vehSignal.lightYellow = lightYellow.GetComponent<SignalLight>();
        vehSignal.lightGreen = lightGreen.GetComponent<SignalLight>();
        vehSignal.lightLeftArrow = lightLeftArrow.GetComponent<SignalLight>();

        vehSignal.SetRed();
    }

    // 3. Signal_Pedestrian_12Clock
    private static void CreatePedestrianSignal_12Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Pedestrian_12Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(-1f, 0f, 5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.25f, 0.5f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.4f, 0.08f), "#FF2222", "#330000");
        GameObject lightGreen = CreateLight(head.transform, "Light_Green", new Vector3(0f, 2.15f, 0.08f), "#00CC44", "#003300");

        PedestrianSignal pedSignal = signal.AddComponent<PedestrianSignal>();
        pedSignal.lightRed = lightRed.GetComponent<SignalLight>();
        pedSignal.lightGreen = lightGreen.GetComponent<SignalLight>();

        pedSignal.SetRed();
    }

    // 4. Signal_Vehicle_12Clock (좌회전 전용)
    private static void CreateVehicleSignal_12Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Vehicle_12Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(1f, 0f, 5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.3f, 0.9f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.55f, 0.08f), "#FF2222", "#330000");
        GameObject lightYellow = CreateLight(head.transform, "Light_Yellow", new Vector3(0f, 2.3f, 0.08f), "#FFBB00", "#332200");
        GameObject lightLeftArrow = CreateLight(head.transform, "Light_LeftArrow", new Vector3(-0.16f, 2.3f, 0.08f), "#44FF88", "#003300");

        VehicleSignal vehSignal = signal.AddComponent<VehicleSignal>();
        vehSignal.lightRed = lightRed.GetComponent<SignalLight>();
        vehSignal.lightYellow = lightYellow.GetComponent<SignalLight>();
        vehSignal.lightGreen = null;
        vehSignal.lightLeftArrow = lightLeftArrow.GetComponent<SignalLight>();

        vehSignal.SetRed();
    }

    // 5. Signal_Pedestrian_5Clock
    private static void CreatePedestrianSignal_5Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Pedestrian_5Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(8f, 0f, -3.5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 270f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.25f, 0.5f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.4f, 0.08f), "#FF2222", "#330000");
        GameObject lightGreen = CreateLight(head.transform, "Light_Green", new Vector3(0f, 2.15f, 0.08f), "#00CC44", "#003300");

        PedestrianSignal pedSignal = signal.AddComponent<PedestrianSignal>();
        pedSignal.lightRed = lightRed.GetComponent<SignalLight>();
        pedSignal.lightGreen = lightGreen.GetComponent<SignalLight>();

        pedSignal.SetRed();
    }

    // 6. Signal_Vehicle_5Clock (직진 전용)
    private static void CreateVehicleSignal_5Clock(Transform parent)
    {
        GameObject signal = new GameObject("Signal_Vehicle_5Clock");
        signal.transform.SetParent(parent);
        signal.transform.localPosition = new Vector3(8f, 0f, -1.5f);
        signal.transform.localRotation = Quaternion.Euler(0f, 270f, 0f);

        GameObject pole = CreatePole(signal.transform);
        GameObject head = CreateHead(signal.transform, new Vector3(0.3f, 0.9f, 0.15f));

        GameObject lightRed = CreateLight(head.transform, "Light_Red", new Vector3(0f, 2.55f, 0.08f), "#FF2222", "#330000");
        GameObject lightYellow = CreateLight(head.transform, "Light_Yellow", new Vector3(0f, 2.3f, 0.08f), "#FFBB00", "#332200");
        GameObject lightGreen = CreateLight(head.transform, "Light_Green", new Vector3(0f, 2.05f, 0.08f), "#00CC44", "#003300");

        VehicleSignal vehSignal = signal.AddComponent<VehicleSignal>();
        vehSignal.lightRed = lightRed.GetComponent<SignalLight>();
        vehSignal.lightYellow = lightYellow.GetComponent<SignalLight>();
        vehSignal.lightGreen = lightGreen.GetComponent<SignalLight>();
        vehSignal.lightLeftArrow = null;

        vehSignal.SetRed();
    }

    // 기둥 생성
    private static GameObject CreatePole(Transform parent)
    {
        GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pole.name = "Pole";
        pole.transform.SetParent(parent);
        pole.transform.localPosition = new Vector3(0f, 1f, 0f);
        pole.transform.localScale = new Vector3(0.08f, 2f, 0.08f);

        Material poleMat = GetOrCreateMaterial("SignalPole", "#1A1A1A");
        pole.GetComponent<Renderer>().sharedMaterial = poleMat;

        return pole;
    }

    // 헤드 생성
    private static GameObject CreateHead(Transform parent, Vector3 scale)
    {
        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Cube);
        head.name = "Head";
        head.transform.SetParent(parent);
        head.transform.localPosition = new Vector3(0f, 2.3f, 0f);
        head.transform.localScale = scale;

        Material headMat = GetOrCreateMaterial("SignalHead", "#111111");
        head.GetComponent<Renderer>().sharedMaterial = headMat;

        return head;
    }

    // 불빛 생성
    private static GameObject CreateLight(Transform parent, string name, Vector3 localPos, string colorOn, string colorOff)
    {
        GameObject light = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        light.name = name;
        light.transform.SetParent(parent);
        light.transform.localPosition = localPos;
        light.transform.localScale = new Vector3(0.1f, 0.1f, 0.05f);

        SignalLight signalLight = light.AddComponent<SignalLight>();
        signalLight.lightRenderer = light.GetComponent<Renderer>();
        signalLight.colorOn = ParseColor(colorOn);
        signalLight.colorOff = ParseColor(colorOff);

        Material lightMat = GetOrCreateMaterial(name + "_Mat", colorOff);
        light.GetComponent<Renderer>().sharedMaterial = lightMat;

        return light;
    }

    // 재질 가져오기 or 생성
    private static Material GetOrCreateMaterial(string matName, string hexColor)
    {
        string matPath = MATERIALS_PATH + matName + ".mat";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);

        if (mat == null)
        {
            mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = ParseColor(hexColor);
            AssetDatabase.CreateAsset(mat, matPath);
            AssetDatabase.SaveAssets();
        }

        return mat;
    }

    // Hex 색상 파싱
    private static Color ParseColor(string hex)
    {
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        if (hex.Length == 6)
        {
            byte r = System.Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = System.Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = System.Convert.ToByte(hex.Substring(4, 2), 16);
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        return Color.white;
    }
}
