using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;

public class Level_Generation_Utility : EditorWindow {

    //level settings
    bool editing_level_settings;
    float level_scroll_speed = 1.0f;
    float level_time = 0.0f;
    float last_level_time;
    float time_offset;

    //enemy generation settings
    bool generating_enemy;
    string enemy_name;
    Sprite enemy_sprite;
    AnimatorController animation_controller;
    
    float fire_speed, enemy_radius, move_speed;

    //wave generation settings
    bool generating_wave;
    string wave_name;
    int num_wave_objects;
    GameObject[] wave_object_prefabs;

    //placement settings
    bool placing_objects;
    GameObject wave_to_place;
    GameObject enemy_to_place;

    //display settings
    bool draw_helper_info;
    string time_display;
    float time_line_size = 5f;
    float border_line_size = 5f;
    float border_line_distance;
    Color time_line_color = Color.red;
    Color border_line_color = Color.red;
    bool draw_time, draw_borders;
    bool show_draw_helper_info, show_draw_time, show_draw_borders;

    //misc
    Vector2 scroll_position; //for scrolling view

    [MenuItem("Tools/Level Generation Utility")]
    public static void Show_Window()
    {
        EditorWindow.GetWindow(typeof(Level_Generation_Utility));
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scroll_position = EditorGUILayout.BeginScrollView(scroll_position, GUILayout.Width(0), GUILayout.Height(0));
        Options_Section();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        Level_Section();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        Generation_Section();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        Placement_Settings();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    void Options_Section()
    {
        
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            draw_helper_info = EditorGUILayout.Toggle("Draw Helper Info", draw_helper_info);//should helper info be drawn
            if (draw_helper_info)
            {
                show_draw_helper_info = EditorGUILayout.Foldout(show_draw_helper_info, "Show helper info");
                if (show_draw_helper_info)
                {
                    if (draw_helper_info == true)
                    {
                        EditorGUI.indentLevel++;
                        draw_time = EditorGUILayout.Toggle("Draw Timeline", draw_time); //should timeline be drawn
                        if (draw_time == true)
                        {
                            EditorGUI.indentLevel++;
                            time_line_size = EditorGUILayout.FloatField("Timeline Size", time_line_size);
                            time_line_color = EditorGUILayout.ColorField("Timeline Color", time_line_color);
                            EditorGUI.indentLevel--;
                        }

                        draw_borders = EditorGUILayout.Toggle("Draw Borders", draw_borders); //should borders be drawn
                        if (draw_borders == true)
                        {
                            EditorGUI.indentLevel++;
                            border_line_size = EditorGUILayout.FloatField("Border Line Size", border_line_size);
                            border_line_color = EditorGUILayout.ColorField("Border Line Color", border_line_color);
                            EditorGUI.indentLevel--;
                        }
                        EditorGUI.indentLevel--;
                    }
                }
            
        }
       
    }

    void Level_Section()
    {
        editing_level_settings = EditorGUILayout.Foldout(editing_level_settings, "Level Settings");
        if (editing_level_settings == true)
        {
            EditorGUILayout.LabelField("Level Settings", EditorStyles.boldLabel);
            Level_Speed();
            Time_Scrubber();
            if(GUILayout.Button("Set Camera To Time"))
            {
                Vector3 scene_view_pivot = SceneView.lastActiveSceneView.pivot;
                scene_view_pivot.y = Get_Scroll_Position();
                SceneView.lastActiveSceneView.pivot = scene_view_pivot;
                SceneView.lastActiveSceneView.Repaint();
            }
            if (GUILayout.Button("Zero Camera View"))
            {
                SceneView.lastActiveSceneView.pivot = Vector3.zero;
                SceneView.lastActiveSceneView.Repaint();
            }
        }
        
    }

    void Level_Speed()
    {
        level_scroll_speed = EditorGUILayout.FloatField("Level Scroll Speed", level_scroll_speed);
        time_offset = EditorGUILayout.FloatField("Time Offset", time_offset);
    }

    void Time_Scrubber()
    {
        level_time = EditorGUILayout.Slider("Time: ", level_time, 0f, 300f);
        time_display = "Time: " + (level_time / 60).ToString()[0] + ":" + (level_time % 60).ToString();
        GUILayout.Label(time_display, EditorStyles.centeredGreyMiniLabel);
        if (level_time != last_level_time)
        {
            Vector3 position = SceneView.lastActiveSceneView.pivot;
            position.y = Get_Scroll_Position();
            SceneView.lastActiveSceneView.pivot = position;
            SceneView.lastActiveSceneView.Repaint();
            last_level_time = level_time;
        }
    }

    void Generation_Section()
    {
        EditorGUILayout.LabelField("Generation Settings", EditorStyles.boldLabel);
        Enemy_Section();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        Wave_Section();
    }

    void Enemy_Section()
    {
        if (GUILayout.Button("Generate Enemy"))
        {
            EditorWindow.GetWindow(typeof(Enemy_Generation_Utility));
            //Enemy_Generation.Generate_Enemy(enemy_name, enemy_sprite, animation_controller, fire_pattern_type, fire_speed, enemy_radius, move_speed);
        }
        
    }

    float Get_Scroll_Position()
    {
        return (level_time + time_offset) * level_scroll_speed;
    }

    //gets all textures in set folder, not currently in use
    /*void Get_Enemy_Sprites()
    {
        List<Texture2D> sprites = new List<Texture2D>();
        string[] png = System.IO.Directory.GetFiles("Assets/Art/Enemies/", "*.png", SearchOption.AllDirectories);
        string[] jpg = System.IO.Directory.GetFiles("Assets/Art/Enemies/", "*.jpg", SearchOption.AllDirectories);
        for (int i = 0; i < png.Length; i++)
        {
            Object png_i = AssetDatabase.LoadMainAssetAtPath(png[i]);
            if (png_i.GetType() == typeof(Texture2D))
            {
                sprites.Add((Texture2D)png_i);
            }
        }
        for (int i = 0; i < jpg.Length; i++)
        {
            Object jpg_i = AssetDatabase.LoadMainAssetAtPath(jpg[i]);
            if (jpg_i.GetType() == typeof(Texture2D))
            {
                sprites.Add((Texture2D)jpg_i);
            }
        }
        enemy_textures = sprites.ToArray();
    }*/

    void Wave_Section()
    {
        generating_wave = EditorGUILayout.Foldout(generating_wave, "Wave Generation");
        if (generating_wave)
        {
            wave_name = EditorGUILayout.TextField("Wave Name", wave_name);

            num_wave_objects = EditorGUILayout.IntField("Wave Objects", num_wave_objects);
            if (wave_object_prefabs.Length != num_wave_objects && num_wave_objects != 0)
            {
                GameObject[] temp_holder = new GameObject[wave_object_prefabs.Length];
                for (int i = 0; i < wave_object_prefabs.Length; i++)
                {
                    temp_holder[i] = wave_object_prefabs[i];
                }
                wave_object_prefabs = new GameObject[num_wave_objects];
                for (int i = 0; i < wave_object_prefabs.Length; i++)
                {
                    if (i < temp_holder.Length)
                    {
                        wave_object_prefabs[i] = temp_holder[i];
                    }
                }
            }
            for (int i = 0; i < wave_object_prefabs.Length; i++)
            {
                wave_object_prefabs[i] = (GameObject)EditorGUILayout.ObjectField("", wave_object_prefabs[i], typeof(GameObject), allowSceneObjects: false);
            }

            if(GUILayout.Button("Clear Settings"))
            {
                wave_name = "";
                num_wave_objects = 0;
                for(int i = 0; i < wave_object_prefabs.Length; i++)
                {
                    wave_object_prefabs[i] = null;
                }
                wave_object_prefabs = new GameObject[0];
            }

            if (GUILayout.Button("Generate Wave"))
            {
                Wave_Generation.Generate_Wave(wave_name, wave_object_prefabs);
            }
        }
    }

    void Placement_Settings()
    {
        EditorGUILayout.LabelField("Placement Section", EditorStyles.boldLabel);
        wave_to_place = (GameObject)EditorGUILayout.ObjectField("Wave ", wave_to_place, typeof(GameObject), allowSceneObjects: false);
        enemy_to_place = (GameObject)EditorGUILayout.ObjectField("Enemy", enemy_to_place, typeof(GameObject), allowSceneObjects: false);
        if(GUILayout.Button("Place Wave"))
        {
            Vector3 position = Vector3.zero;
            position.y = Get_Scroll_Position();
            GameObject inst_wave_object = Instantiate(wave_to_place, position, Quaternion.identity) as GameObject;
            Undo.RegisterCreatedObjectUndo(inst_wave_object, inst_wave_object.name);
        }
        if(GUILayout.Button("Place Enemy"))
        {
            Vector3 position = Vector3.zero;
            position.y = Get_Scroll_Position();
            GameObject inst_enemy_object = Instantiate(enemy_to_place, position, Quaternion.identity) as GameObject;
            Undo.RegisterCreatedObjectUndo(inst_enemy_object, inst_enemy_object.name);
            
        }
    }



    //draw helpers on screen
    void OnFocus()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (draw_helper_info == true)
        {
            Draw_Helpers();
        }
    }

    void Draw_Helpers()
    {
        if (draw_time == true)
        {
            Draw_Time();
        }
        if (draw_borders == true)
        {
            Draw_Borders();
        }
    }

    void Draw_Time()
    {
        Handles.color = time_line_color;
        Vector3 position_one, position_two;
        position_one = position_two = new Vector3(0, Get_Scroll_Position(), 0);
        position_one.x = -1000;
        position_two.x = 1000;
        Vector3[] points = new Vector3[2] { position_one, position_two };
        Handles.DrawAAPolyLine(time_line_size, points);
    }

    void Draw_Borders()
    {
        Handles.color = border_line_color;
        Vector3 position_one, position_two, position_three, position_four;
        position_one = position_two = position_three = position_four = new Vector3(0, 0, 0);
        position_one.x = position_two.x = -4.2f;
        position_three.x = position_four.x = 4.2f;
        position_one.y = position_three.y = Get_Scroll_Position() - 1000;
        position_two.y = position_four.y = Get_Scroll_Position() + 1000;
        Vector3[] points_left = new Vector3[2] { position_one, position_two };
        Vector3[] points_right = new Vector3[2] { position_three, position_four };
        Handles.DrawAAPolyLine(border_line_size, points_left);
        Handles.DrawAAPolyLine(border_line_size, points_right);
        Handles.Label(new Vector3(0, Get_Scroll_Position(),  0), time_display);
    }


}
