using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Unity.Mathematics;
namespace Utilities {
    public enum Direction4 {UP,DOWN,LEFT,RIGHT }
    public static class UtilFunc {
        /// <summary>
        /// Rotates Transform to mouse in 2D space
        /// </summary>
        /// <param name="objToRotate"></param>
        public static void RotateToMouse2D(Transform objToRotate) {
            LookAt2D(objToRotate, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //Vector3 objectPositon = Camera.main.WorldToScreenPoint(objToRotate.position);
            //float angle = Mathf.Atan2(Input.mousePosition.y - objectPositon.y,
            //                            Input.mousePosition.x - objectPositon.x) * Mathf.Rad2Deg;
            //objToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        public static Vector3 GetMouseWorldPosition() {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        public static void LookAt2D(Transform objToRotate, Vector2 target) {
            float angle = Mathf.Atan2(target.y - objToRotate.position.y,
                                        target.x - objToRotate.position.x) * Mathf.Rad2Deg;
            objToRotate.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        public static Quaternion GetQuaternionFromLookAt2D(Transform objRotationData, Vector2 target) {
            float angle = Mathf.Atan2(target.y - objRotationData.position.y,
                                        target.x - objRotationData.position.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, angle);
        }

        public static float GetAngle(Vector2 V) {
            return Mathf.Atan(V.y/V.x);
        }
        public static Vector2 GenerateDirectionFromAngle(float angle) {
            return new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
        }

        public static float3 Getfloat3fromVector2(Vector2 v) {
            return new float3 { x = v.x, y = v.y };
        }
        private static GameObject _prefab;
        private static SpriteRenderer _spriteRendererPrefab;
        public static SpriteRenderer SpriteRendererPrefab {
            get {
                if (_spriteRendererPrefab == null) _spriteRendererPrefab = Prefab.GetComponent<SpriteRenderer>();
                return _spriteRendererPrefab;
            }
        }
        public static GameObject Prefab {
            get {
                if (_prefab == null) _prefab = Resources.Load<GameObject>("prefab");
                return _prefab;
            }
        }
        public static Vector2 Direction4ToVector2(Direction4 dir) {
            switch (dir) {
                case Direction4.UP:
                    return Vector2.up;
                case Direction4.DOWN:
                    return Vector2.down;
                case Direction4.LEFT:
                    return Vector2.left;
                case Direction4.RIGHT:
                    return Vector2.right;
                default:
                    return Vector2.zero;

            }
        }
        public static Direction4 Vector2ToDirection4(Vector2 dir) {
            if (dir.y > 0f) return Direction4.UP;
            if (dir.y < 0f) return Direction4.DOWN;
            if (dir.x < 0f) return Direction4.LEFT;
            if (dir.x > 0f) return Direction4.RIGHT;
            return Direction4.UP;
            
        }
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 20,
                                               Color color = default(Color), TextAnchor textAnchor = TextAnchor.MiddleCenter, 
                                               TextAlignment textAligment =TextAlignment.Center, int sortingOrder=0) {
            if (color == null) color = Color.white;
            GameObject gameObj = new GameObject(text, typeof(TextMesh));            
            gameObj.transform.SetParent(parent);
            gameObj.transform.localPosition = localPosition;
            TextMesh textMesh = gameObj.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAligment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;

        }
        public static void CallFunction(MonoBehaviour obj,float delay, Action action) {          
            obj.StartCoroutine(CallFunctionB(delay, action));            
        }
        public static void CallFunction(MonoBehaviour obj, float delay, Action<object[]> action, object[] args) {
            obj.StartCoroutine(CallFunctionB(delay, action,args));
        }
        public static void SetVectorX(ref Vector3 vector,float xValue) {
            vector = new Vector3(xValue, vector.y,vector.z);
        }
        public static void SetVectorY(ref Vector3 vector, float yValue) {
            vector = new Vector3(vector.x, yValue, vector.z);
        }
        public static void SetVectorZ(ref Vector3 vector, float zValue) {
            vector = new Vector3(vector.x, vector.y, zValue);
        }
        public static void SetScaleX(Transform transform,float xValue) {
            transform.localScale = new Vector3(xValue, transform.localScale.y, transform.localScale.z);
        }
        public static void SetScaleY(Transform transform, float yValue) {
            transform.localScale = new Vector3(transform.localScale.x, yValue, transform.localScale.z);
        }
        public static void SetScaleZ(Transform transform, float zValue) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, zValue);
        }
        public static void SetPositionX(Transform transform, float xValue) {
            transform.position = new Vector3(xValue, transform.position.y,transform.position.z);
        }
        public static void SetPositionY(Transform transform, float yValue) {
            transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
        }
        public static void SetPositionZ(Transform transform, float zValue) {
            transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
        }
        public static void CallOnFixedUpdate(IOnFixedUpdate obj, Action action) {
            obj.AddFixedUpdateListener(action);
        }
        private static IEnumerator CallFunctionB(float delay,Action action) {
            yield return new WaitForSeconds(delay); 
            action();
        }
        private static IEnumerator CallFunctionB(float delay, Action<object[]> action, object[] args) {
            yield return new WaitForSeconds(delay);
            action(args);
        }
        public static T GetItem<T>(T[] tab, int index) {
            if (index < tab.Length)
                return tab[index];
            else
                return tab[tab.Length - 1];
        }
        public static T GetItemLoop<T>(T[] tab, int index) {
            if (index < 0) return GetItemLoop(tab, tab.Length + index);
            return tab[index % tab.Length];
        }
        public static int GetItemLoop(Array array, int index) {
            if (index < 0) return GetItemLoop(array, array.Length + index);
            return index % array.Length;
        }
        public static int GetEnumItemLoop(Type @enum,int index) {
            return GetItemLoop(Enum.GetValues(@enum), index);
        }
        private static AudioSource _tmpSource;
        public static AudioSource TmpSource {
            get {
                if (_tmpSource == null) _tmpSource = new GameObject().AddComponent<AudioSource>();
                return _tmpSource;
            }
        }
        private static MonoBehaviour _tmpMonoBehaviour;
        public static MonoBehaviour TmpMonoBehaviour {
            get {
                if (_tmpMonoBehaviour == null) _tmpMonoBehaviour = new GameObject().AddComponent<EmptyBehaviour>();
                return _tmpMonoBehaviour;
            }
        }
        public static void PlayOneShot(AudioClip clip) {
            TmpSource.PlayOneShot(clip);
        }
        public static void Initialiaze2DCollection(int xMax, int yMax, Action<int, int> action) {
            for (int x = 0; x < xMax; x++) {
                for (int y = 0; y < yMax; y++) {
                    action(x,y);
                }
            }
        }
        public static void For(int iMax, Action<int> action) {
            for (int i = 0; i < iMax; i++) {
                action(i);
            }
        }
        public static void SetVector3ArrayVector2Array(Vector2[] data, Vector3[] destination) {
            for (int i =0;i<destination.Length; i++) {

            }
        }
        public static Vector3[] GetVector3Array(Vector2[] array) {
            Vector3[] result = new Vector3[array.Length];
            For(array.Length,(int i)=> {
                result[i] = array[i];
            });
            return result;
        }

        public static T Instantiate<T>(SpawnInfo<T> spawnInfo) where T : Component {
            return GameObject.Instantiate(spawnInfo.prefab, spawnInfo.position, spawnInfo.rotation);
        }
        public static T SpawnTmpObject<T>(MonoBehaviour onObject,SpawnInfo<T> spawnInfo, float lifeTime, Action<object[]> OnUpdateAction, Action<object[]> OnDestroyAction,object[] updateArgs,object[] destroyArgs) where T : Component {
            T @object = Instantiate(spawnInfo);
            TMP_MonoBehaviour monoBehaviour;
            if (@object.TryGetComponent(out monoBehaviour)) {
                monoBehaviour.DestroyAction = OnDestroyAction;
                monoBehaviour.UpdateAction = OnUpdateAction;
                monoBehaviour.SetUp(updateArgs,destroyArgs);
            }
            
            //monoBehaviour.SetUp(updateArgs, destroyArgs);
            GameObject.Destroy(@object, lifeTime); 
            return @object;
        }
        public static T[] AddToArray<T>(T[] array, T item) {
            T[] copy = new T[array.Length];
            array.CopyTo(copy, 0);
            array = new T[copy.Length + 1];
            copy.CopyTo(array, 0);
            array[copy.Length] = item;
            return array;
        }
        public static T[] RemoveFromArray<T>(T[] array, int index) {
            T[] copy = new T[array.Length];
            array.CopyTo(copy, 0);
            for(int i = index; i < copy.Length - 1; i++) {
                copy[i] = copy[i + 1];
            }
            array = new T[copy.Length -1];
            for(int i =0; i < array.Length; i++) {
                array[i] = copy[i];
            }
            return array;
        }
        public static GraphNode<Rectangle> GetGraphNodeFromPosition(Graph<Rectangle> graph, Vector3 position) {
            foreach (var node in graph.Nodes) {
                if (IsOverlapping(ref node.nodeData, position)) return node;
            }
            return graph.Nodes[0];
        }
        public static int GetGraphNodeIndexFromPosition(Graph<Rectangle> graph, Vector3 position) {
            foreach(var node in graph.Nodes) {
                if (IsOverlapping(ref node.nodeData, position)) return node.index;

            }
            return 0;
        }
        public static bool IsBetweenValues(float x,float lv, float rv) {
            return x >= lv && x <= rv;
        }
        public static bool IsOverlapping(ref Rectangle rectangle, Vector2 point) {
            bool x = IsBetweenValues(point.x,rectangle.position.x-rectangle.width/2f,rectangle.position.x+rectangle.width/2f);
            bool y = IsBetweenValues(point.y, rectangle.position.y - rectangle.height / 2f, rectangle.position.y + rectangle.height / 2f);
            return x && y;
        }
       
        public static AudioSource CreateAudioSource(Vector3 position) {
            AudioSource source = (AudioSource)GameObject.Instantiate(TmpMonoBehaviour.gameObject, position,Quaternion.identity).AddComponent(typeof(AudioSource));

            return source;
        }
    }
    [Serializable]
    public struct ColorToPrefab {
        public Color color;
        public GameObject gameOject;
    }

    public class SpawnInfo<T> where T: Component {
        public T prefab;
        public Quaternion rotation;
        public Vector3 position;

        public SpawnInfo(T prefab, Vector3 position, Quaternion rotation) {
            this.prefab = prefab;
            this.rotation = rotation;
            this.position = position;
        }
    }
    public class EmptyBehaviour : MonoBehaviour { }
    public class Timer {
        float endTime;
        float currentTime;
        public Timer(float endTime,float startTime=0f) {
            this.endTime = endTime;
            currentTime = startTime;
        }
        void UpdateTime() {
            currentTime += Time.deltaTime;
        }
        public bool CheckTime() {
            if (currentTime >= endTime) {
                currentTime = 0f;
                return true;
            }
            return false;
        }
        public void OnUpdate(object sender, EventArgs args) {
            UpdateTime();
        }
    }
    public interface IOnFixedUpdate {
        void AddFixedUpdateListener(Action action);
        void RemoveAllFixedUpdateListener();
    }
    public class Grid<T> {
        public bool debuging = true;
        private int width;
        private int height;
        private float cellSize;
        private T[,] gridArray;
        private TextMesh[,] debugText;
        private Vector2 originPosition;
        public event EventHandler<OnGridValueChangedArgs> OnGridValueChanged;
        public class OnGridValueChangedArgs : EventArgs {
            public int x;
            public int y;
        }
        public Grid(int width, int height, float cellSize, Vector2 originPosition,Func<Grid<T>,int,int,T> createGridObject,bool debuging=true) {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.debuging = debuging;

            debugText = new TextMesh[width, height];
            gridArray = new T[width, height];
            ForEach((x,y) => {
                gridArray[x, y] = createGridObject(this, x, y);
            });
            if (debuging) {
                DebugGrid();
            }
        }
        public void ForEach(Action action) {
            for(int x = 0; x < width; x++) {
                for(int y = 0; y < height; y++) {
                    action();
                }
            }
        }
        public void ForEach(Action<int,int> action) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    action(x,y);
                }
            }
        }
        public void ForEach(Action<T> action) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    action(GetGridObject(x, y));
                }
            }
        }
        private bool IsValidXY(int x, int y) {
            return x >= 0 && y >= 0 && x < width && y < height;
        }
        public void SetGridObject(int x, int y, T v) {
            if (IsValidXY(x, y)) {
                gridArray[x, y] = v;
                debugText[x, y].text = v.ToString();
            }
            OnGridValueChanged?.Invoke(this, new OnGridValueChangedArgs { x = x, y = y });
        }
        public T GetGridObject(int x, int y) {
            if (IsValidXY(x, y))
                return gridArray[x, y];
            else
                return default(T);
        }
        public Vector2Int GetXY(Vector2 position) {
            return new Vector2Int(Mathf.FloorToInt((position.x - originPosition.x) / cellSize), Mathf.FloorToInt((position.y - originPosition.y) / cellSize));
        }
        public Vector2 GetWorldPosition(int x, int y) {
            return new Vector2(x, y) * cellSize + originPosition;
        }
        public Vector2 GetCenterWorldPosition(int x, int y) {
            return new Vector2(x, y) * cellSize + originPosition + new Vector2(cellSize, cellSize) * 0.5f;
        }
        public void SetGridObject(Vector2 position, T v) {
            Vector2Int xy = GetXY(position);
            SetGridObject(xy.x, xy.y, v);
        }
        public T GetGridObject(Vector2 position) {
            Vector2Int xy = GetXY(position);
            return GetGridObject(xy.x, xy.y);
        }
        public List<T> GetNeihgbours(int x, int y) {
            List<T> neighbours = new List<T>(8);
            int xC;
            int yC;
            //Debug.Log("GetNeihgbours(" + x + "," + y+")");
            for(int i = 0; i < 9; i++) {                
                xC = (x + 1) - (i % 3);
                yC = (y + 1) - (i / 3);
                if (xC == x && yC == y) {
                    continue;
                }
                //Debug.Log(xC + "," + yC);

                if (IsValidXY(xC, yC)) {
                    neighbours.Add(GetGridObject(xC, yC));
                }
            }
            //Debug.Log("END GetNeihgbours(" + x + "," + y + ")");
            return neighbours;

        }
        public void DebugGrid() {
            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugText[x, y] = UtilFunc.CreateWorldText(gridArray[x, y]?.ToString(), null, GetCenterWorldPosition(x, y), 10, Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }
    public static class PauseManager {
        private static bool _pause = false;
        public static bool Pause {
            get { return _pause; }
            set { _pause = value; OnPauseChange?.Invoke(null, _pause); }
        }
        public static EventHandler<bool> OnPauseChange;
    }
    public class PauseMonoBehaviour : MonoBehaviour {
        protected virtual void OnUpdate() {

        }
        protected virtual void OnLateUpdate() {

        }
        protected virtual void OnFixedUpdate() {

        }
        private void Update() {
            if (!PauseManager.Pause) {
                OnUpdate();
            }
        }
        private void FixedUpdate() {
            if (!PauseManager.Pause) {
                OnFixedUpdate();
            }
        }
        private void LateUpdate() {
            if (!PauseManager.Pause) {
                OnLateUpdate();
            }
        }
    }
    public class Coroutine {
        private MonoBehaviour _monoBehaviour;
        public Action PreperationAction;
        public Action<float,float> OnCycle;
        public Action OnCleanAction;
        private float _time;
        private bool tmp = false;
        private bool cleaned = false;
        public void StartCoroutine() {
            _monoBehaviour.StartCoroutine(CoroutineFunction(_time));
            

        }
        public void StopCoroutine() {
            
            if (!cleaned) {
                _monoBehaviour.StopAllCoroutines();
                cleaned = true;
                OnCleanAction?.Invoke();
                if (tmp)
                    GameObject.Destroy(_monoBehaviour);
            }
            
        }
        public Coroutine(Action preperationAction, Action<float, float> onCycle, Action onCleanAction, float time, bool autoStart) {
            _monoBehaviour = new GameObject().AddComponent<EmptyBehaviour>();
            PreperationAction = preperationAction;
            OnCycle = onCycle;
            OnCleanAction = onCleanAction;
            _time = time;
            cleaned = false;
            tmp = true;
            if (autoStart) StartCoroutine();
        }
        public Coroutine(MonoBehaviour monoBehaviour, Action preperationAction, Action<float, float> onCycle, Action onCleanAction, float time,bool autoStart) {
            _monoBehaviour = monoBehaviour;
            PreperationAction = preperationAction;
            OnCycle = onCycle;
            OnCleanAction = onCleanAction;
            _time = time;
            cleaned = false;
            if (autoStart) StartCoroutine();
        }

        private IEnumerator CoroutineFunction(float time) {
            float timer = 0f;
            PreperationAction?.Invoke();
            while (timer <= time) {
                OnCycle(timer,time);
                timer += Time.deltaTime;
                yield return null;
            }
            if (!cleaned) {
                _monoBehaviour.StopAllCoroutines();
                cleaned = true;
                OnCleanAction?.Invoke();
                if (tmp)
                    GameObject.Destroy(_monoBehaviour);
            }
        }
    }
    [Serializable]
    public class Graph<T> {        
        public GraphNode<T>[] Nodes;
        public GraphNode<T> this[int i] {
            get { return Nodes[i]; }
            set { Nodes[i] = value; }
        }
        /// <summary>
        /// Creates Graph instance with one node element (index = 0, neighbours = int[0])
        /// </summary>
        public Graph() {
            Nodes = new GraphNode<T>[1];
            Nodes[0] = new GraphNode<T>(0, new int[0]);
        }
        /// <summary>
        /// Creates Graph instance with size number node element (index = i, neighbours = int[0])
        /// </summary>
        public Graph(int size) {
            Nodes = new GraphNode<T>[size];
            for(int i = 0; i< size; i++) {
                Nodes[i] = new GraphNode<T>(i, new int[0]);
            }
            
        }

        public GraphNode<T>[] GetNeighbours(int j) {
            int[] indexes = Nodes[j].Neighbours;
            GraphNode<T>[] tab = new GraphNode<T>[indexes.Length];
            for(int i =0; i < tab.Length; i++) {
                tab[i] = Nodes[indexes[i]];
            }
            return tab;
        }
    }
    [Serializable]
    public class GraphNode<T> {
        public T nodeData;
        public int index;
        public int[] Neighbours;

        public GraphNode(int index, int[] neighbours) {
            this.index = index;
            Neighbours = neighbours;
        }
    }
    [Serializable]
    public struct Rectangle {
        public Vector3 position;
        public float width;
        public float height;

        public Rectangle(Vector3 position, float width, float height) {
            this.position = position;
            this.width = width;
            this.height = height;
        }
        public Rectangle AddPadding(float x, float y) {
            return new Rectangle(position, width-x,height-y);
        }
    }
    public interface IntParser {
        int ParseToInt();
    }
    public interface IMovement {
        void Move(Vector3 target);
    }
    public class MonoBehaviour<T>  : MonoBehaviour where T : class {
        protected T component {
            get { if (_component == null) _component = GetComponent<T>();
                return _component;
            }
            set { _component = value; }
        }
        private T _component;
    }
    public interface IInitializable {
        void Init();
    }
    public interface IEffect {
        void TriggerEffect();
    }
    public interface IVolumeHolder {
        void SetVolume(float volume);
        float GetVolume();
    }
    public interface IAnimation {
        void TriggerAnimation();
        void OnAnimationEnd();
        float GetAnimationLength();
    }
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShaderAnimation : MonoBehaviour, IAnimation {

        protected Material material;
        private bool animationOn = false;
        protected Action OnAnimationEndAction;
        private void Start() {
            material = GetComponent<SpriteRenderer>().material;
        }

        public void TriggerAnimation() {
            animationOn = true;
        }

        private void Update() {
            if (animationOn) Animation();
        }
        protected virtual void Animation() {
            Debug.Log(name + " is playing empty shader animation");
        }

        public void OnAnimationEnd() {
            OnAnimationEndAction?.Invoke();
        }

        public virtual float GetAnimationLength() {
            throw new NotImplementedException();
        }
    }
    public class MonoBehaviourMultiScene : MonoBehaviour {

        private WaitForScene waitForScene;

        public virtual void OnMultiSceneStart() {
            Debug.Log("Empty OnMultiSceneStart");
        }
        


    }
    public class MonoBehaviourWrapper<T> : MonoBehaviour {
        public T item;
    }
}

