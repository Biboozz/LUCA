/*
* Copyright (c) Mad Pixel Machine
* http://www.madpixelmachine.com/
*/

using UnityEngine;

namespace EnergyBarToolkit {

[ExecuteInEditMode]
public class FollowTargetUGUI : MonoBehaviour {

    #region Public Fields

    public Transform target;

    public Camera renderCamera;

    #endregion

    #region Private Fields and Properties

    private RectTransform rectTransform;

    #endregion

    #region Public Methods
    #endregion

    #region Unity Methods

    void OnEnable() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update() {
        if (target == null) {
            return;
        }

        // ReSharper disable once ConvertConditionalTernaryToNullCoalescing
        var cam = renderCamera != null ? renderCamera : Camera.main;
        var screenPoint = cam.WorldToScreenPoint(target.transform.position);

        rectTransform.localPosition = new Vector3(screenPoint.x - Screen.width / 2f, screenPoint.y - Screen.height / 2f);
    }

    #endregion

    #region Private Methods
    #endregion

    #region Inner and Anonymous Classes
    #endregion
}

} // namespace