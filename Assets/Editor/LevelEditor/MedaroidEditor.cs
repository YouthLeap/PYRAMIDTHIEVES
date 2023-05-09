using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
public class MedaroidEditor : Editor
{
    //public static string dataFilePath = Path.Combine(Path.Combine(Path.Combine(Application.dataPath, "Resources"), "Data"), "Medaroid.xml");

    //[MenuItem("Tools/Medaroid Editor/Open Editor Window %#E")]
    //static void ShowEditorWindow()
    //{
    //    MedaroidEditorWindow.OpenWindow();
    //}

    //[MenuItem("Tools/Medaroid Editor/Save")]
    //public static void Save()
    //{
    //    XmlSerializer xml = new XmlSerializer(typeof(List<Medaroid>));
    //    FileStream fs = new FileStream(dataFilePath, FileMode.Create);
    //    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
    //    xml.Serialize(sw, MedaroidEditorWindow.mData);
    //    fs.Close();
    //}

    //[MenuItem("Tools/Medaroid Editor/Clear PlayerPrefs")]
    //public static void ClearPlayerPrefs()
    //{
    //    PlayerPrefs.DeleteAll();
    //}


    //public static List<Medaroid> GetData()
    //{
    //    List<Medaroid> data = new List<Medaroid>();
    //    TextAsset file = Resources.Load("Data/Medaroid") as TextAsset;
    //    XmlSerializer xml = new XmlSerializer(typeof(List<Medaroid>));
    //    StringReader textData = new StringReader(file.text);
    //    data = xml.Deserialize(textData) as List<Medaroid>;
    //    textData.Close();
    //    return data;
    //}
}
