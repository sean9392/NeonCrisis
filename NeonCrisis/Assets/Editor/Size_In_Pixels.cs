using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Size_In_Pixels : MonoBehaviour {

    [MenuItem("Tools/Size In Pixels")]
	public static void Get_Size_In_Pixels()
    {
        GameObject g_object = Selection.activeGameObject;
        SpriteRenderer renderer = g_object.GetComponent<SpriteRenderer>();
        Sprite sprite = renderer.sprite;
        Vector2 sprite_size = sprite.rect.size;
        Vector2 local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        Vector3 world_size = local_sprite_size;
        world_size.x *= g_object.transform.lossyScale.x;
        world_size.y *= g_object.transform.lossyScale.y;

        Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
        screen_size.y *= Camera.main.aspect;

        Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;
        Debug.Log(sprite.rect);
    }
}
