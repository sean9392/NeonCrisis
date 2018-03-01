using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public struct Enemy_Information_Section
{   
    public float move_speed, fire_rate, fire_speed, start_time;
   
    public GameObject movement_curve;
    public string fire_pattern_type;
    public int choice_index;
    public int curve_picker_ids;
}

public class Enemy_Generation_Utility : EditorWindow {

    Enemy_Information_Section[] enemy_sections;
    string[] fire_types = new[] { "straight", "circle", "loop", "homing" };
    public string enemy_name;
    public int health;
    public Sprite enemy_sprite;
    float collider_size;
    int choice_index = 0;
    int amount_of_sections;
    Vector2 scroll_position;

    //object picker
    string enemy_search_string = "_eshp";
    string enemy_curve_string = "_Crv";
    int sprite_picker_id = 1;


    private void OnGUI()
    {
        GUILayout.BeginVertical();
        scroll_position = GUILayout.BeginScrollView(scroll_position);
        Display_Options();
        if(GUILayout.Button("Generate Enemy"))
        {
            Enemy_Generation.Generate_Enemy(enemy_name, enemy_sprite, collider_size, health, enemy_sections);
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    void Display_Options()
    {
        amount_of_sections = EditorGUILayout.IntField("Amount Of Sections", amount_of_sections);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        enemy_name = EditorGUILayout.TextField("Enemy Name", enemy_name);

        //enemy sprite selection start
        if(enemy_sprite != null)
        {
            enemy_sprite = (Sprite)EditorGUILayout.ObjectField("Enemy Sprite", enemy_sprite, typeof(Sprite), allowSceneObjects: false);
        }
        if(GUILayout.Button("Select Enemy Sprite"))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>((Sprite)enemy_sprite, false, enemy_search_string, sprite_picker_id);
        }

        if(Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == sprite_picker_id)
        {
            enemy_sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();
        }

        health = EditorGUILayout.IntField("Health", health);

        //enemy sprite selection end
        collider_size = EditorGUILayout.FloatField("Collider Size", collider_size);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (enemy_sections == null)
        {
            if(amount_of_sections != 0)
            {
                enemy_sections = new Enemy_Information_Section[amount_of_sections];
            }
        }

        if (enemy_sections != null)
        {
            for (int i = 0; i < enemy_sections.Length; i++)
            {
                enemy_sections[i].curve_picker_ids = i + 4;
                enemy_sections[i].start_time = EditorGUILayout.FloatField("Start Time", enemy_sections[i].start_time);
                enemy_sections[i].move_speed = EditorGUILayout.FloatField("Move Speed", enemy_sections[i].move_speed);
                enemy_sections[i].fire_rate = EditorGUILayout.FloatField("Fire Rate", enemy_sections[i].fire_rate);
                enemy_sections[i].fire_speed = EditorGUILayout.FloatField("Fire Speed", enemy_sections[i].fire_speed);

                if(GUILayout.Button("Select Enemy Movement Curve"))
                {
                    EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, enemy_curve_string, enemy_sections[i].curve_picker_ids);
                }
                if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == enemy_sections[i].curve_picker_ids)
                {
                    enemy_sections[i].movement_curve = (GameObject)EditorGUIUtility.GetObjectPickerObject() as GameObject;
                }
                if (enemy_sections[i].movement_curve != null)
                {
                    enemy_sections[i].movement_curve = (GameObject)EditorGUILayout.ObjectField("Enemy Movement Curve", enemy_sections[i].movement_curve, typeof(GameObject), allowSceneObjects: false);
                }

                
                enemy_sections[i].choice_index = EditorGUILayout.Popup(enemy_sections[i].choice_index, fire_types);
                enemy_sections[i].fire_pattern_type = fire_types[choice_index];
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

    }
}
