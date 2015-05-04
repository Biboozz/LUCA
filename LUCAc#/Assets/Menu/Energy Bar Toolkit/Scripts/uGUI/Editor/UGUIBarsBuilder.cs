/*
* Copyright (c) Mad Pixel Machine
* http://www.madpixelmachine.com/
*/

using EnergyBarToolkit;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EnergyBarToolkit {

public class UGUIBarsBuilder {

    private const string FilledBar = "c64125d34f21e8343b920f3e9331f8a8";
    private const string FilledFg = "0508dd4e3733e2847bc8d31ac5041009";
    private static readonly Color FilledColor = new Color(88/255f, 167/255f, 199/255f);

    private const string RepeatedSlot = "ce4d97c03a644fb419b04f2fbabdcf37";
    private const string RepeatedIcon = "2fe3848be60b3da48b228f43b00ab971";

    private const string SequenceGrid = "a84437ff025eff848a43559624c44ff7";

    private const string TransformBg = "bfdb5578d45f2504eb9bed9a9c5a5f6c";
    private const string TransformObject = "942197ba5e7797f47b8ccdc19d7900af";

    public static void CreateFilled() {
        var canvas = FindCanvas();
        if (canvas == null) {
            return;
        }

        var bar = CreateUnder<FilledRendererUGUI>(canvas.transform, "Filled Bar");
        bar.spritesForeground.Add(new EnergyBarUGUIBase.SpriteTex(LoadSprite(FilledFg)));
        bar.spriteBar = LoadSprite(FilledBar);
        bar.spriteBarColor = FilledColor;
        bar.SetNativeSize();

        Selection.activeGameObject = bar.gameObject;
    }

    private static Sprite LoadSprite(string guid) {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var sprite = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
        return sprite as Sprite;
    }

    public static void CreateRepeated() {
        var canvas = FindCanvas();
        if (canvas == null) {
            return;
        }

        var bar = CreateUnder<RepeatedRendererUGUI>(canvas.transform, "Repeated Bar");
        bar.spriteSlot = new EnergyBarUGUIBase.SpriteTex(LoadSprite(RepeatedSlot));
        bar.spriteIcon = new EnergyBarUGUIBase.SpriteTex(LoadSprite(RepeatedIcon));
        bar.SetNativeSize();

        Selection.activeGameObject = bar.gameObject;
    }

    public static void CreateSequence() {
        var canvas = FindCanvas();
        if (canvas == null) {
            return;
        }

        var bar = CreateUnder<SequenceRendererUGUI>(canvas.transform, "Sequence Bar");
        bar.gridSprite = new EnergyBarUGUIBase.SpriteTex(LoadSprite(SequenceGrid));
        bar.gridWidth = 7;
        bar.gridHeight = 9;
        bar.frameCountAuto = false;
        bar.frameCount = 59;
        bar.SetNativeSize();

        Selection.activeGameObject = bar.gameObject;
    }

    public static void CreateTransform() {
        var canvas = FindCanvas();
        if (canvas == null) {
            return;
        }

        var bar = CreateUnder<TransformRendererUGUI>(canvas.transform, "Transform Bar");
        bar.spritesBackground.Add(new EnergyBarUGUIBase.SpriteTex(LoadSprite(TransformBg)));
        bar.spriteObject = new EnergyBarUGUIBase.SpriteTex(LoadSprite(TransformObject));

        bar.transformRotate = true;
        bar.rotateFunction.endAngle = 360;
        bar.SetNativeSize();

        Selection.activeGameObject = bar.gameObject;
    }

    private static Canvas FindCanvas() {
        Canvas canvas;
        if (Selection.activeObject is GameObject) {
            canvas = ((GameObject) Selection.activeObject).GetComponent<Canvas>();
            if (canvas != null) {
                return canvas;
            }
        }

        canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null) {
            EditorUtility.DisplayDialog("Canvas not found!",
                "There's no Canvas on your current scene. Please create one and try again.\n\n" +
                "You can create canvas using the menu item: GameObject -> UI -> Canvas", "OK");
        }

        return canvas;
    }

    private static T CreateUnder<T>(Transform transform, string name) where T : Component {
        var go = new GameObject(name);
        Undo.RegisterCreatedObjectUndo(go, "Created Bar");
        go.transform.parent = transform;

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        go.AddComponent<RectTransform>();
        return go.AddComponent<T>();
    }

    
}

} // namespace