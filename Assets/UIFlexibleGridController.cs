using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))] 
public class UIFlexibleGridController : MonoBehaviour {


     private RectTransform rt;
     private GridLayoutGroup glg;
     private float lastWidth;
     private float lastHeight;
     private float cellAspectRatio;
     public bool scaleOnlyOnStartup = true;
     public bool keepAspectRatio = false;
     public bool keepSpacingZero = true;

     void Start () {
         rt = GetComponent<RectTransform>();
         glg = GetComponent<GridLayoutGroup>();
         if (rt == null || glg == null) {
             Debug.LogError("UIFlexibleGridController couldn't find a RectTransform or a GridLayoutGroup");
             return;
         }
         lastWidth = rt.rect.width;
         lastHeight = rt.rect.height;
         cellAspectRatio = rt.rect.width / rt.rect.height;
         if (glg.constraint == GridLayoutGroup.Constraint.FixedColumnCount || glg.constraint == GridLayoutGroup.Constraint.FixedRowCount) {
             UpdateCellSizes();
         } else {
             Debug.LogWarning("GridLayoutGroup contraints do not make this UIFlexibleGridController necessary. Consider removing it.");
             return;
         }
     }

     void Update () {
         if (scaleOnlyOnStartup) return;
         if (!HasSizedChanged()) return;
         lastWidth = rt.rect.width;
         lastHeight = rt.rect.height;
         UpdateCellSizes();
     }

     private bool HasSizedChanged() {
         return lastHeight != rt.rect.height || lastWidth != rt.rect.width;
     }

     private string GameObjectPathName(Transform t) {
         if (t.parent == null) {
             return t.name;
         } else {
             return GameObjectPathName(t.parent) + "/" + t.name;
         }
     }

     private void UpdateCellSizes() {
         float w = 0f;
         float h = 0f;
         float sx = 0f;
         float sy = 0f;
         w = (rt.rect.width / (float)glg.constraintCount);
         h = (rt.rect.height / (float)glg.constraintCount);
         if (w == 0 || h == 0) {
             Debug.LogError(string.Format("Invalid width ({0}) or height ({1}) at {2}", w, h, GameObjectPathName(this.transform)));
             return;
         }
         if (!keepSpacingZero) {
             w = (int) w;
             h = (int) h;
         }
         if (keepAspectRatio) {
             h = w * cellAspectRatio;
         }
         Vector2 newSize = glg.cellSize;
         newSize.x = w;
         newSize.y = h;
         glg.cellSize = newSize;
         if (!keepSpacingZero && glg.constraintCount != 1) {
             sx = (rt.rect.width - (w * glg.constraintCount)) / (float)(glg.constraintCount-1);
             sy = (rt.rect.height - (w * glg.constraintCount)) / (float)(glg.constraintCount-1);
         } 
         Vector2 newSpacing = glg.spacing;
         newSpacing.x = sx;
         newSpacing.y = sy;
         glg.spacing = newSpacing;
     }
} 