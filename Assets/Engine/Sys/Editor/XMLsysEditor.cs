using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(XMLsys))]
public class XMLsysEditor : Editor{

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		
		EditorGUILayout.Space();
		
		if (GUILayout.Button ("Read XML")){
			var xml=(XMLsys)target;
			xml.readXML();
		}
		
		EditorGUILayout.Space();
		
		if (GUILayout.Button ("Write XML")){
			var xml=(XMLsys)target;
			xml.writeXML();
		}
		
		
	}
}
