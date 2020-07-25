using UnityEngine;
using UnityEditor;

// started by googling "Editor Window" and copying over https://docs.unity3d.com/ScriptReference/EditorWindow.html
// Also, this script is in a folder called "Editor" because it should only run in the editor
public class TrackEditorWindow : EditorWindow
{
    // here is the path to the big corner piece prefab... I think this is 25x25 units?
    readonly string CORNER_PREFAB_PATH = "Assets/Karting/ModularTrackKit/Prefabs/ModularTrackCurveLarge.prefab";
    // path to straightaway track, pretty sure its 10 x 10
    readonly string STRAIGHTAWAY_PREFAB_PATH = "Assets/Karting/ModularTrackKit/Prefabs/ModularTrackStraight.prefab";

    int loopWidthTiles = 5;
    int loopLengthTiles = 4;


    // --- all of this stuff I just copied from google ---
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Track Editor Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        TrackEditorWindow window = (TrackEditorWindow)EditorWindow.GetWindow(typeof(TrackEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Build a loop", EditorStyles.boldLabel);
        loopWidthTiles = EditorGUILayout.IntField("Loop width", loopWidthTiles);
        loopLengthTiles = EditorGUILayout.IntField("Loop length", loopLengthTiles);
        if (GUILayout.Button("Build the loop"))
        {
            BuildLoop();
        }

        GUILayout.Label("Beep boop?");
        if (GUILayout.Button("Beep boop"))
        {
            Debug.Log("Beep boop!");
        }
    }

    private void BuildLoop()
    {
        // get the prefabs ready
        GameObject cornerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CORNER_PREFAB_PATH);
        GameObject straightawayPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(STRAIGHTAWAY_PREFAB_PATH);
        // make a container object to put everything in
        GameObject container = new GameObject("LoopContainer");
        
        // Every time we create an object, make it so you can undo it! otherwise its annoying
        Undo.RegisterCreatedObjectUndo(container, "Build track loop");

        // start with one corner and go around
        // keep track of which way we're pointing and where we are
        float angle = 0f;
        Vector3 position = Vector3.zero;

        // most of the code below is copied. It could be written better!

        // top side
        GameObject topRightCorner = Instantiate(cornerPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
        Undo.RegisterCreatedObjectUndo(topRightCorner, "Build track loop");
        angle -= 90f; // 90 degree turn
        position += 20f * (Vector3.forward + Vector3.left); // move the position forward. Hopefully this is the right spot
        for (int i = 0; i < loopWidthTiles; i++)
        {
            GameObject topSidePiece = Instantiate(straightawayPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
            Undo.RegisterCreatedObjectUndo(topSidePiece, "Build track loop");
            position += 10f * Vector3.left;
        }

        // left side
        GameObject topLeftCorner = Instantiate(cornerPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
        Undo.RegisterCreatedObjectUndo(topLeftCorner, "Build track loop");
        angle -= 90f;
        position += 20f * (Vector3.left + Vector3.back);
        for (int i = 0; i < loopLengthTiles; i++)
        {
            GameObject leftSidePiece = Instantiate(straightawayPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
            Undo.RegisterCreatedObjectUndo(leftSidePiece, "Build track loop");
            position += 10f * Vector3.back;
        }

        // bottom side
        GameObject bottomLeftCorner = Instantiate(cornerPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
        Undo.RegisterCreatedObjectUndo(bottomLeftCorner, "Build track loop");
        angle -= 90f;
        position += 20f * (Vector3.right + Vector3.back);
        for (int i = 0; i < loopWidthTiles; i++)
        {
            GameObject bottomSidePiece = Instantiate(straightawayPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
            Undo.RegisterCreatedObjectUndo(bottomSidePiece, "Build track loop");
            position += 10f * Vector3.right;
        }

        // right side
        GameObject bottomRightCorner = Instantiate(cornerPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
        Undo.RegisterCreatedObjectUndo(bottomRightCorner, "Build track loop");
        angle -= 90f;
        position += 20f * (Vector3.right + Vector3.forward);
        for (int i = 0; i < loopLengthTiles; i++)
        {
            GameObject rightSidePiece = Instantiate(straightawayPrefab, position, Quaternion.Euler(0, angle, 0), container.transform);
            Undo.RegisterCreatedObjectUndo(rightSidePiece, "Build track loop");
            position += 10f * Vector3.forward;
        }
    }
}