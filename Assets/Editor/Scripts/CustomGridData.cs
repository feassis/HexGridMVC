using UnityEngine;
using UnityEditor;
using MVC.Model.Grid;


//proprerty drawer based on https://www.youtube.com/watch?v=mxqD1B2e4ME
[CustomPropertyDrawer(typeof(GridData))]
public class CustomGridData : PropertyDrawer
{
    private const int numberOfCollums = 20;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);

        Rect newPosition = position;
        newPosition.y += 30f;
        SerializedProperty rows = property.FindPropertyRelative("GridConfig");

        for (int i = 0; i < numberOfCollums; i++)
        {
            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("Row");

            newPosition.height = 20;

            if(row.arraySize != numberOfCollums)
            {
                row.arraySize = numberOfCollums;
            }

            newPosition.width = 30;

            for (int j = 0; j < numberOfCollums; j++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width + 10;
            }

            newPosition.x = position.x;
            newPosition.y += 25;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 12;
    }
}
