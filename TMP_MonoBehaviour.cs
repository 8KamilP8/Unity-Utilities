using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class TMP_MonoBehaviour : MonoBehaviour {

    public Action<object[]> UpdateAction;
    public Action<object[]> DestroyAction;
    private object[] updateArgs;
    private object[] destroyArgs;
    public void SetUp(object[] updateArgs,object[] destroyArgs) {
        this.updateArgs = updateArgs;
        this.destroyArgs = destroyArgs;
    }
    private void Update() {
        UpdateAction?.Invoke(updateArgs);
    }
    private void OnDestroy() {
        DestroyAction?.Invoke(destroyArgs);
    }
}
