using DG.Tweening;
using Tween;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomButtonByComposition))]
public class HelperTest : Editor
{
    private CustomButtonByComposition Button;
    private string _typeOfEase;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Button = (CustomButtonByComposition)target;
        _typeOfEase = nameof(Button._curveEase);

        switch (Button.AnimationButtonType)
        {
            case AnimationButtonType.None:
                break;
            case AnimationButtonType.ChangePosition:
                Button.Duration = EditorGUILayout.FloatField(nameof(Button.Duration), Button.Duration);
                Button.Strength = EditorGUILayout.FloatField(nameof(Button.Strength), Button.Strength);
                break;
            case AnimationButtonType.ChangeRotation:
                Button.Duration = EditorGUILayout.FloatField(nameof(Button.Duration), Button.Duration);
                Button.Strength = EditorGUILayout.FloatField(nameof(Button.Strength), Button.Strength);
                break;
            case AnimationButtonType.ChangeSize:
                EditorGUILayout.LabelField("Curve ease");
                DrawEase();
                Button.Duration = EditorGUILayout.FloatField(nameof(Button.Duration), Button.Duration);
                Button.Strength = EditorGUILayout.FloatField(nameof(Button.Strength), Button.Strength);
                Button.CountOfLoop = EditorGUILayout.IntField(nameof(Button.CountOfLoop), Button.CountOfLoop);
                break;
        }
    }
    private void DrawEase()
    {
        if (EditorGUILayout.DropdownButton(new GUIContent(_typeOfEase), FocusType.Passive))
        {
            GenericMenu menu = new GenericMenu();
            AddMenuItemForColor(menu, nameof(Ease.Flash), Ease.Flash);
            AddMenuItemForColor(menu, nameof(Ease.Linear), Ease.Linear);
            menu.ShowAsContext();
        }
    }
    void AddMenuItemForColor(GenericMenu menu, string menuPath, Ease ease)
    {
        menu.AddItem(new GUIContent(menuPath), Button._curveEase.Equals(ease), OnTypeSelected, ease);
    }
    void OnTypeSelected(object ease)
    {
        _typeOfEase = ease.ToString();
        Button._curveEase = (Ease)ease;
    }
}