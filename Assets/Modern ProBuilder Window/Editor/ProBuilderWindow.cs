using UnityEditor;
using UnityEngine;

using UnityEngine.ProBuilder;
using UnityEditor.ProBuilder;
using UnityEngine.ProBuilder.Shapes;

namespace ShirdhankarTechnologies.ModernProBuilder
{
    public class ProBuilderWindow : EditorWindow
    {
        bool showShapes = true;
        bool showSelection = true;
        bool showGeometryTools = true;
        bool showObjectActions = true;
        bool showGridTools = true;

        Vector2 scrollPos;

        Texture2D companyLogo;

        GUIStyle headerStyle;
        GUIStyle subHeaderStyle;

        GUIStyle sectionStyle;
        GUIStyle modernButtonStyle;
        GUIStyle foldoutStyle;

        float buttonWidth;
        float twoButtonWidth;

        [MenuItem("Tools/Modern ProBuilder Window")]
        public static void ShowWindow()
        {
            ProBuilderWindow window = GetWindow<ProBuilderWindow>("ProBuilder");

            window.minSize = new Vector2(420, 700);

            window.companyLogo = AssetDatabase.LoadAssetAtPath<Texture2D>(
                "Assets/PBWindow/Icons/CompanyLogo.png");
        }

        void InitStyles()
        {
            // =====================================================
            // HEADER
            // =====================================================

            if (headerStyle == null)
            {
                headerStyle = new GUIStyle(EditorStyles.boldLabel);

                headerStyle.fontSize = 20;
                headerStyle.alignment = TextAnchor.MiddleCenter;

                headerStyle.normal.textColor = Color.white;
            }

            if (subHeaderStyle == null)
            {
                subHeaderStyle = new GUIStyle(EditorStyles.label);

                subHeaderStyle.fontSize = 11;
                subHeaderStyle.alignment = TextAnchor.MiddleCenter;

                subHeaderStyle.normal.textColor =
                    new Color(0.8f, 0.8f, 0.8f);
            }

            // =====================================================
            // SECTION STYLE
            // =====================================================

            if (sectionStyle == null)
            {
                sectionStyle = new GUIStyle("box");

                sectionStyle.padding = new RectOffset(12, 12, 10, 10);

                sectionStyle.margin = new RectOffset(8, 8, 6, 6);
            }

            // =====================================================
            // BUTTON STYLE
            // =====================================================

            if (modernButtonStyle == null)
            {
                modernButtonStyle = new GUIStyle(GUI.skin.button);

                modernButtonStyle.fixedHeight = 34;

                modernButtonStyle.fontSize = 11;

                modernButtonStyle.alignment = TextAnchor.MiddleCenter;

                modernButtonStyle.margin = new RectOffset(4, 4, 4, 4);

                modernButtonStyle.normal.textColor = Color.white;
            }

            // =====================================================
            // FOLDOUT STYLE
            // =====================================================

            if (foldoutStyle == null)
            {
                foldoutStyle = new GUIStyle(EditorStyles.foldout);

                foldoutStyle.fontStyle = FontStyle.Bold;

                foldoutStyle.fontSize = 12;

                foldoutStyle.normal.textColor = Color.white;

                foldoutStyle.onNormal.textColor = Color.white;
            }
        }

        private void OnGUI()
        {
            InitStyles();

            // =====================================================
            // DARK BACKGROUND
            // =====================================================

            EditorGUI.DrawRect(
                new Rect(0, 0, position.width, position.height),
                new Color(0.13f, 0.13f, 0.13f)
            );

            // =====================================================
            // BUTTON WIDTHS
            // =====================================================

            buttonWidth = (position.width - 60) / 3f;

            twoButtonWidth = (position.width - 50) / 2f;

            GUILayout.Space(10);

            // =====================================================
            // HEADER
            // =====================================================

            GUILayout.BeginVertical("box");

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (companyLogo != null)
            {
                GUILayout.Label(companyLogo,
                    GUILayout.Width(90),
                    GUILayout.Height(90));
            }

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.Label("Shirdhankar Technologies", headerStyle);

            GUILayout.Space(2);

            GUILayout.Label("Modern ProBuilder Window", subHeaderStyle);

            GUILayout.Space(10);

            GUILayout.EndVertical();

            GUILayout.Space(10);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            // =====================================================
            // CREATE SHAPES
            // =====================================================

            GUILayout.BeginVertical(sectionStyle);

            showShapes = EditorGUILayout.Foldout(
                showShapes,
                "Create Shapes",
                true,
                foldoutStyle
            );

            if (showShapes)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Create Cube", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreateCube();

                if (GUILayout.Button("Create Plane", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreatePlane();

                if (GUILayout.Button("Create Cylinder", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreateCylinder();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Create Sphere", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreateSphere();

                if (GUILayout.Button("Create Stairs", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreateStairs();

                if (GUILayout.Button("Create Cone", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CreateCone();

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            // =====================================================
            // SELECTION MODE
            // =====================================================

            GUILayout.BeginVertical(sectionStyle);

            showSelection = EditorGUILayout.Foldout(
                showSelection,
                "Selection Mode",
                true,
                foldoutStyle
            );

            if (showSelection)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Vertex Mode", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    SetVertexMode();

                if (GUILayout.Button("Edge Mode", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    SetEdgeMode();

                if (GUILayout.Button("Face Mode", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    SetFaceMode();

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            // =====================================================
            // GEOMETRY OPERATIONS
            // =====================================================

            GUILayout.BeginVertical(sectionStyle);

            showGeometryTools = EditorGUILayout.Foldout(
                showGeometryTools,
                "Geometry Operations",
                true,
                foldoutStyle
            );

            if (showGeometryTools)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Insert Edge Loop", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    InsertEdgeLoop();

                if (GUILayout.Button("Extrude Faces", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    ExtrudeFaces();

                if (GUILayout.Button("Bridge Edges", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    BridgeEdges();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Bevel Edges", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    BevelEdges();

                if (GUILayout.Button("Fill Hole", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    FillHole();

                if (GUILayout.Button("Flip Normals", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    FlipNormals();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Detach Faces", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    DetachFaces();

                if (GUILayout.Button("Merge Objects", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    MergeObjects();

                if (GUILayout.Button("Collapse Vertices", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    CollapseVertices();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(
                    "Weld Vertices",
                    modernButtonStyle,
                    GUILayout.Width(buttonWidth * 2)))
                {
                    WeldVertices();
                }

                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            // =====================================================
            // OBJECT ACTIONS
            // =====================================================

            GUILayout.BeginVertical(sectionStyle);

            showObjectActions = EditorGUILayout.Foldout(
                showObjectActions,
                "Object Actions",
                true,
                foldoutStyle
            );

            if (showObjectActions)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Duplicate Object", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    DuplicateObject();

                if (GUILayout.Button("Delete Object", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    DeleteObject();

                if (GUILayout.Button("Move To Origin", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    MoveToOrigin();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Reset Rotation", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    ResetRotation();

                if (GUILayout.Button("Freeze Transform", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    FreezeTransform();

                if (GUILayout.Button("Random Y Rotation", modernButtonStyle,
                    GUILayout.Width(buttonWidth)))
                    RandomYRotation();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Duplicate Along X", modernButtonStyle,
                    GUILayout.Width(twoButtonWidth)))
                    DuplicateAlongX();

                if (GUILayout.Button("Duplicate Along Z", modernButtonStyle,
                    GUILayout.Width(twoButtonWidth)))
                    DuplicateAlongZ();

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            // =====================================================
            // GRID & SNAPPING
            // =====================================================

            GUILayout.BeginVertical(sectionStyle);

            showGridTools = EditorGUILayout.Foldout(
                showGridTools,
                "Grid & Snapping",
                true,
                foldoutStyle
            );

            if (showGridTools)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Snap Selected To Grid",
                    modernButtonStyle,
                    GUILayout.Width(buttonWidth * 2)))
                    SnapToGrid();

                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            GUILayout.Space(20);

            EditorGUILayout.EndScrollView();
        }

        // =====================================================
        // CREATE SHAPES
        // =====================================================

        void CreateCube()
        {
            ProBuilderMesh cube = ShapeFactory.Instantiate<Cube>();

            cube.transform.position = Vector3.zero;

            Selection.activeGameObject = cube.gameObject;
        }

        void CreatePlane()
        {
            ProBuilderMesh plane =
                ShapeFactory.Instantiate<UnityEngine.ProBuilder.Shapes.Plane>();

            plane.transform.position = Vector3.zero;

            Selection.activeGameObject = plane.gameObject;
        }

        void CreateCylinder()
        {
            ProBuilderMesh cylinder = ShapeFactory.Instantiate<Cylinder>();

            cylinder.transform.position = Vector3.zero;

            Selection.activeGameObject = cylinder.gameObject;
        }

        void CreateSphere()
        {
            ProBuilderMesh sphere = ShapeFactory.Instantiate<Sphere>();

            sphere.transform.position = Vector3.zero;

            Selection.activeGameObject = sphere.gameObject;
        }

        void CreateStairs()
        {
            ProBuilderMesh stairs = ShapeFactory.Instantiate<Stairs>();

            stairs.transform.position = Vector3.zero;

            Selection.activeGameObject = stairs.gameObject;
        }

        void CreateCone()
        {
            ProBuilderMesh cone = ShapeFactory.Instantiate<Cone>();

            cone.transform.position = Vector3.zero;

            Selection.activeGameObject = cone.gameObject;
        }

        // =====================================================
        // SELECTION MODES
        // =====================================================

        void SetVertexMode()
        {
            ProBuilderEditor.selectMode = SelectMode.Vertex;

            SceneView.RepaintAll();
        }

        void SetEdgeMode()
        {
            ProBuilderEditor.selectMode = SelectMode.Edge;

            SceneView.RepaintAll();
        }

        void SetFaceMode()
        {
            ProBuilderEditor.selectMode = SelectMode.Face;

            SceneView.RepaintAll();
        }

        // =====================================================
        // OBJECT ACTIONS
        // =====================================================

        void DuplicateObject()
        {
            if (Selection.activeGameObject == null)
                return;

            GameObject clone = Instantiate(Selection.activeGameObject);

            clone.name = Selection.activeGameObject.name;

            Undo.RegisterCreatedObjectUndo(clone, "Duplicate Object");

            Selection.activeGameObject = clone;
        }

        void DeleteObject()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.DestroyObjectImmediate(Selection.activeGameObject);
        }

        void MoveToOrigin()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.RecordObject(
                Selection.activeGameObject.transform,
                "Move To Origin"
            );

            Selection.activeGameObject.transform.position = Vector3.zero;
        }

        void ResetRotation()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.RecordObject(
                Selection.activeGameObject.transform,
                "Reset Rotation"
            );

            Selection.activeGameObject.transform.rotation =
                Quaternion.identity;
        }

        void FreezeTransform()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.RecordObject(
                Selection.activeGameObject.transform,
                "Freeze Transform"
            );

            Selection.activeGameObject.transform.position = Vector3.zero;

            Selection.activeGameObject.transform.rotation =
                Quaternion.identity;

            Selection.activeGameObject.transform.localScale =
                Vector3.one;
        }

        void RandomYRotation()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.RecordObject(
                Selection.activeGameObject.transform,
                "Random Rotation"
            );

            Selection.activeGameObject.transform.rotation =
                Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        void DuplicateAlongX()
        {
            if (Selection.activeGameObject == null)
                return;

            GameObject clone = Instantiate(Selection.activeGameObject);

            clone.transform.position += Vector3.right * 2f;

            Undo.RegisterCreatedObjectUndo(clone, "Duplicate Along X");

            Selection.activeGameObject = clone;
        }

        void DuplicateAlongZ()
        {
            if (Selection.activeGameObject == null)
                return;

            GameObject clone = Instantiate(Selection.activeGameObject);

            clone.transform.position += Vector3.forward * 2f;

            Undo.RegisterCreatedObjectUndo(clone, "Duplicate Along Z");

            Selection.activeGameObject = clone;
        }

        // =====================================================
        // GEOMETRY OPERATIONS
        // =====================================================

        void InsertEdgeLoop()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Insert Edge Loop"
            );
        }

        void ExtrudeFaces()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Extrude"
            );
        }

        void BridgeEdges()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Bridge Edges"
            );
        }

        void BevelEdges()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Bevel Edges"
            );
        }

        void FillHole()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Fill Hole"
            );
        }

        void FlipNormals()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Flip Face Normals"
            );
        }

        void DetachFaces()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Detach Faces"
            );
        }

        void MergeObjects()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Object/Merge Objects"
            );
        }

        void CollapseVertices()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Collapse Vertices"
            );
        }

        void WeldVertices()
        {
            EditorApplication.ExecuteMenuItem(
                "Tools/ProBuilder/Geometry/Weld Vertices"
            );
        }

        // =====================================================
        // GRID
        // =====================================================

        void SnapToGrid()
        {
            if (Selection.activeGameObject == null)
                return;

            Undo.RecordObject(
                Selection.activeGameObject.transform,
                "Snap To Grid"
            );

            Vector3 pos =
                Selection.activeGameObject.transform.position;

            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);

            Selection.activeGameObject.transform.position = pos;
        }
    }
}