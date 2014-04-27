using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;

public class GenerateExtentions : EditorWindow {

	const string FILE_MONOBEHAVIOUREX = "Assets/Scripts/Generated/MonoBehaviourEx.cs";

	[MenuItem("Prancake/Generate Extentions")]
	public static void Generate() {
		//string codeBase = File.ReadAllText("Assets/Scripts/Generated/Templates/Extensions.txt");
		//"$REPLACE_EXTENSIONS"

		List<Type> componentTypes = new List<Type>();

		Assembly assembly_csharp = Assembly.GetAssembly(typeof(MonoBehaviourEx));
		Type[] allTypes = assembly_csharp.GetTypes();
		foreach (Type T in allTypes) {
			if (typeof(Component).IsAssignableFrom(T) && T.IsVisible)
				componentTypes.Add(T);
		}

		Assembly assembly_unityEngine = Assembly.GetAssembly(typeof(MonoBehaviour));
		allTypes = assembly_unityEngine.GetTypes();
		foreach (Type T in allTypes) {
			if (typeof(Component).IsAssignableFrom(T) && T.IsVisible && !T.Name.Equals("RaycastCollider") && !T.Name.Equals("NetworkView"))
				componentTypes.Add(T);
		}

		string extentionsCode = File.ReadAllText(FILE_MONOBEHAVIOUREX);

		int idx0 = extentionsCode.IndexOf("//COMPONENTS START") + 18;
		int idx1 = extentionsCode.IndexOf("//COMPONENTS END");
		int idx2 = extentionsCode.IndexOf("//ASSIGNS START") + 15;
		int idx3 = extentionsCode.IndexOf("//ASSIGNS END");

		StringBuilder codeBase = new StringBuilder();
		StringBuilder declarations = new StringBuilder();

		codeBase.Append(extentionsCode.Substring(0, idx0) + "\n");

		StringBuilder assigns = new StringBuilder();

		//string assignTemplate = "\t\t\t\tcase \"{0}\":\n\t\t\t\t\tComponents.{0} = GetComponent<{0}>();\n\t\t\t\t\tbreak;\n";
		//string assignTemplate = "\t\t\t\tcase typeof({0}):\n\t\t\t\t\tComponents.{0} = c as {0};\n\t\t\t\t\tbreak;\n";
		string assignTemplate = "\t\t\t\tcase \"{0}\":\n\t\t\t\t\tComponents.{0} = c as {0};\n\t\t\t\t\tbreak;\n";
		foreach (Type T in componentTypes) {
			if (!T.Name.Equals("RaycastCollider")) {
				declarations.AppendFormat("\t\tpublic static {0} {0};\n", T.Name);
				assigns.AppendFormat(assignTemplate, T.Name);
			}
		}

		codeBase.Append(declarations);

		codeBase.Append("\t\t" + extentionsCode.Substring(idx1, idx2 - idx1));

		codeBase.Append("\n" + assigns);

		codeBase.Append(extentionsCode.Substring(idx3));

		File.WriteAllText(FILE_MONOBEHAVIOUREX, codeBase.ToString());

		AssetDatabase.ImportAsset(FILE_MONOBEHAVIOUREX, ImportAssetOptions.ForceUpdate);
	}
}
