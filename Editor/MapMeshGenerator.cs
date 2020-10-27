using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEditor;

public class MapMeshGenerator : ExtendedEditorWindow {
    [SerializeField] private Transform[] obstacles;

    [SerializeField] private Transform upBound;
    [SerializeField] private Transform downBound;

    [SerializeField] private GameObject pointObj;
    private static MapMeshData mapMeshData;

    private Vector2[] boundaryPoints = new Vector2[4];
    public static void Open(MapMeshData dataObject) {
        mapMeshData = dataObject;
        MapMeshGenerator window = GetWindow<MapMeshGenerator>("Map Mesh Generator");
        window.serializedObject = new SerializedObject(dataObject);
    }
    private void OnGUI() {

        //EditorGUILayout.PropertyField(upBoundProperty);
        if (serializedObject == null) return;
        currentProperty = serializedObject.FindProperty("mapMeshData");
        DrawProperties(currentProperty,true);

        Apply();

        if (GUILayout.Button("Generate")) {
            Generate();
        }
        
    }

    private class Segment {
        public Vector2 A;
        public Vector2 B;

        public Segment(Vector2 a, Vector2 b) {
            A = a;
            B = b;
        }
    }
    private class MeshRay {
        public Direction4 direction;
        public Vector2 point;

        public MeshRay(Vector2 point, Direction4 direction) {
            this.direction = direction;
            this.point = point;
        }
    }

    public void Generate() {
        var container = mapMeshData.mapMeshData;
        obstacles = container.obstacles;
        upBound = container.upBound;
        downBound = container.downBound;
        pointObj = container.pointObj;
        //Check if MapBaker object already exists
        if (FindObjectOfType<MapBaker>() != null) {
            Debug.LogError("MapBaker already exists in the scene!");
            return;
        }

        //Create MapBaker Object
        MapBaker mb = new GameObject().AddComponent<MapBaker>();

        //Calculate Bounds points:
        #region CALCULATE BOUNDS POITNS
        #region UPPER LEFT CORNER
        float x2 =upBound.localScale.x / 2f;
        float y2 = upBound.localScale.y / 2f;
        Vector2 point = upBound.position - new Vector3(x2, y2, 0f);
        point += new Vector2(upBound.localScale.y, -upBound.localScale.y);
        boundaryPoints[0]=point;
        #endregion
        #region UPPER RIGHT CORNER
        point = upBound.position + new Vector3(x2, y2, 0f);
        point += new Vector2(-upBound.localScale.y, -upBound.localScale.y);
        boundaryPoints[1] = point;
        #endregion
        #region LOWER LEFT CORNER
        x2 = downBound.localScale.x / 2f;
        y2 = downBound.localScale.y / 2f;
        point = upBound.position - new Vector3(x2, y2, 0f);
        point += new Vector2(upBound.localScale.y, upBound.localScale.y);
        boundaryPoints[2] = point;
        #endregion
        #region LOWER RIGHT CORNER
        x2 = downBound.localScale.x / 2f;
        y2 = downBound.localScale.y / 2f;
        point = upBound.position + new Vector3(x2, y2, 0f);
        point += new Vector2(-upBound.localScale.y, upBound.localScale.y);
        boundaryPoints[3] = point;
        #endregion
        #endregion
        List<Vector2> leftUpperPoints = new List<Vector2>();
        List<Vector2> rightUpperPoints = new List<Vector2>();
        List<Vector2> leftLowerPoints = new List<Vector2>();
        List<Vector2> rightLowerPoints = new List<Vector2>();
        foreach (var box in obstacles) {
            Vector3 halfScale = box.localScale / 2f;
            Vector2 leftUpperCorner = box.position + new Vector3(-halfScale.x,halfScale.y);
            Vector2 rightUpperCorner = box.position + halfScale;
            Vector2 leftLowerCorner = box.position - halfScale;
            Vector2 rightLowerCorner = box.position + new Vector3(halfScale.x, -halfScale.y);
            leftLowerPoints.Add(leftLowerCorner);
            leftUpperPoints.Add(leftUpperCorner);
            rightUpperPoints.Add(rightUpperCorner);
            rightLowerPoints.Add(rightLowerCorner);
        }
        List<MeshRay> rays = new List<MeshRay>();
        foreach (Vector2 P in leftLowerPoints) {
            rays.Add(new MeshRay(P,Direction4.LEFT));
            rays.Add(new MeshRay(P,Direction4.DOWN));
        }
        foreach (Vector2 P in leftUpperPoints) {
            rays.Add(new MeshRay(P, Direction4.LEFT));
            rays.Add(new MeshRay(P, Direction4.UP));
        }
        foreach (Vector2 P in rightUpperPoints) {
            rays.Add(new MeshRay(P, Direction4.RIGHT));
            rays.Add(new MeshRay(P, Direction4.UP));
        }
        foreach (Vector2 P in rightLowerPoints) {
            rays.Add(new MeshRay(P, Direction4.RIGHT));
            rays.Add(new MeshRay(P, Direction4.DOWN));
        }

        List<Vector2> intersections = new List<Vector2>();
        foreach(var ray in rays) {
            Debug.DrawRay(ray.point,UtilFunc.Direction4ToVector2(ray.direction),Color.green,30f);
        }
        foreach (var ray in rays) {
            var hit = Physics2D.Raycast(ray.point, UtilFunc.Direction4ToVector2(ray.direction));
            var hits = Physics2D.RaycastAll(ray.point, UtilFunc.Direction4ToVector2(ray.direction));
            if(hits.Length >= 2) {
                intersections.Add(hits[1].point);
                intersections.Add(hits[0].point);
            }
            else if (hits.Length > 0){
                intersections.Add(hits[0].point);
            }

        }
        GameObject parent = new GameObject();
        foreach(var intersection in intersections) {

            GameObject g = Instantiate(pointObj, parent.transform);
            g.transform.position = intersection;
        }

        //Rays create a grid
        //Sort Rays y x,
        
    }


}
