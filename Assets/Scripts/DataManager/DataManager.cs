using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class DataManager
{

    const string SECURITY_KEY = "*3YuXx3/0]qKD&4";
    static string SECURE_DATA_PATH = Path.Combine(Application.persistentDataPath, "data.xml").ToString();

    static SecuredData data = LoadData();

    public static void Test()
    {
        data.money = 1000;
        data.ids.Add(10000);
        data.ids.Add(10001);
        Save();
    }

    [System.Serializable]
    public class SecuredData
    {
        public int money = 0;
        public int hightScore = 0;
        public List<int> ids = new List<int>();
    }

    static string Encrypt(string data)
    {
        data = Security.Encrypt(data, SECURITY_KEY);
        return data;
    }

    static string Decrypt(string data)
    {
        data = Security.Decrypt(data, SECURITY_KEY);
        return data;
    }


    static SecuredData LoadData()
    {
        if (System.IO.File.Exists(SECURE_DATA_PATH))
        {
            FileStream fs = new FileStream(SECURE_DATA_PATH, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            //Debug.Log(sr.ReadToEnd());
            string content = Decrypt(sr.ReadToEnd());
            Debug.Log(content);
            StringReader reader = new StringReader(content);

            XmlSerializer serializer = new XmlSerializer(typeof(SecuredData));
            SecuredData data = serializer.Deserialize(reader) as SecuredData;

            sr.Close();
            fs.Close();
            if (data == null) return new SecuredData();
            return data;
        }
        else
        {
            return new SecuredData();
        }
    }

    static void Save()
    {
        FileStream fs = new FileStream(SECURE_DATA_PATH, FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SecuredData));
        StringWriter sw = new StringWriter();
        serializer.Serialize(sw, data);
        StreamWriter writer = new StreamWriter(fs);
        string content = sw.ToString();
        content = Encrypt(content);
        writer.Write(content);
        writer.Close();
        fs.Close();
    }
}
