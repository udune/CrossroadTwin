using UnityEngine;
using UnityEditor;
using System.IO;

public class IntersectionGenerator : Editor
{
    [MenuItem("Tools/CrossroadTwin/Generate Intersection")]
    public static void GenerateIntersection()
    {
        // Environment 오브젝트 삭제 후 재생성
        GameObject existingEnv = GameObject.Find("Environment");
        if (existingEnv != null)
        {
            DestroyImmediate(existingEnv);
        }

        GameObject environment = new GameObject("Environment");
        environment.transform.position = Vector3.zero;

        // Materials 폴더 확인 및 생성
        if (!Directory.Exists("Assets/Materials"))
        {
            AssetDatabase.CreateFolder("Assets", "Materials");
        }

        // 도로 생성
        CreateObject("Road_Main", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0, 0), new Vector3(30, 1, 10), "#2C2C2C");

        CreateObject("Road_Bogwang", PrimitiveType.Plane, environment.transform,
            new Vector3(-4, 0, 10), new Vector3(6, 1, 14), "#2C2C2C");

        // 중앙 삼각 섬
        CreateObject("Island_Center", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, 2), new Vector3(4, 1, 4), "#8A8A8A");

        // 인도
        CreateObject("Sidewalk_South", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, -6.5f), new Vector3(30, 1, 3), "#8A8A8A");

        CreateObject("Sidewalk_North", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, 6.5f), new Vector3(30, 1, 3), "#8A8A8A");

        CreateObject("Sidewalk_Bogwang_West", PrimitiveType.Plane, environment.transform,
            new Vector3(-8, 0.02f, 10), new Vector3(2, 1, 14), "#8A8A8A");

        CreateObject("Sidewalk_Bogwang_East", PrimitiveType.Plane, environment.transform,
            new Vector3(-1, 0.02f, 10), new Vector3(2, 1, 14), "#8A8A8A");

        // 횡단보도
        CreateObject("Crosswalk_10Clock", PrimitiveType.Plane, environment.transform,
            new Vector3(-9, 0.03f, 0), new Vector3(1.5f, 1, 10), "#EEEEEE");

        CreateObject("Crosswalk_12Clock", PrimitiveType.Plane, environment.transform,
            new Vector3(-4, 0.03f, 4), new Vector3(6, 1, 1.5f), "#EEEEEE");

        CreateObject("Crosswalk_5Clock", PrimitiveType.Plane, environment.transform,
            new Vector3(8, 0.03f, 0), new Vector3(1.5f, 1, 10), "#EEEEEE");

        // 차선
        CreateObject("Line_Center", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, 0), new Vector3(30, 1, 0.1f), "#FFDD00");

        CreateObject("Line_Lane_North", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, 2.5f), new Vector3(30, 1, 0.08f), "#FFFFFF");

        CreateObject("Line_Lane_South", PrimitiveType.Plane, environment.transform,
            new Vector3(0, 0.02f, -2.5f), new Vector3(30, 1, 0.08f), "#FFFFFF");

        // 경계석
        CreateObject("Curb_South", PrimitiveType.Cube, environment.transform,
            new Vector3(0, 0.05f, -5), new Vector3(30, 0.1f, 0.2f), "#555555");

        CreateObject("Curb_North", PrimitiveType.Cube, environment.transform,
            new Vector3(0, 0.05f, 5), new Vector3(30, 0.1f, 0.2f), "#555555");

        Debug.Log("Intersection generated successfully.");
    }

    private static void CreateObject(string name, PrimitiveType type, Transform parent,
        Vector3 position, Vector3 scale, string colorHex)
    {
        GameObject obj = GameObject.CreatePrimitive(type);
        obj.name = name;
        obj.transform.parent = parent;
        obj.transform.position = position;
        obj.transform.localScale = scale;

        Material mat = GetOrCreateMaterial(name + "_Mat", colorHex);
        obj.GetComponent<Renderer>().material = mat;
    }

    private static Material GetOrCreateMaterial(string materialName, string colorHex)
    {
        string materialPath = "Assets/Materials/" + materialName + ".mat";

        // 이미 존재하는 재질 재사용
        Material existingMat = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
        if (existingMat != null)
        {
            return existingMat;
        }

        // 새 재질 생성
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        Color color;
        if (ColorUtility.TryParseHtmlString(colorHex, out color))
        {
            mat.color = color;
        }

        AssetDatabase.CreateAsset(mat, materialPath);
        AssetDatabase.SaveAssets();

        return mat;
    }
}
