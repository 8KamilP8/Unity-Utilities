using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldScreenTextManager : MonoBehaviour {
    [SerializeField] private TextMeshPro textPrefab;
    private string _text = "";
    private Vector3? _position = null;
    public void SpawnText(string text) {
        _text = text;
    }
    public void SpawnText(Transform transformPosition) {
        _position = transformPosition.position;
        
    }
    public void SpawnText(string text,Vector2 position) {
        TextMeshPro textMesh = Instantiate(textPrefab, position,Quaternion.identity);
        textMesh.text = text;
    }
    private void Update() {
        if(_text != "" && _position != null) {
            Debug.Log("Text Spawned");
            SpawnText(_text, (Vector2)_position);
            _text = "";
            _position = null;
        }
    }
}
