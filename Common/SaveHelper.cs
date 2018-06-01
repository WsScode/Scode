using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;



public class SaveHelper  {

    private static string Root_Path = Application.dataPath + "/"; 

    public static bool FileIsExist(string fileName)
    {

        return File.Exists(Root_Path + fileName);
    }

    public static bool DirectoryIsExist(string dName)
    {
        return Directory.Exists(Root_Path  + dName);
    }

    /// <summary>
    /// 创建一个文件并写入内容 文件存在直接覆盖内容 添加
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="content"></param>
    public static void CreateFile(string fileName,string content)
    {
        if (FileIsExist(fileName))
        {
            File.WriteAllText(Root_Path + fileName, content);//会直接覆盖原内容(存档内容)

            //下面的方法 是不删除原文件内容 添加新内容
            //FileStream fs = new FileStream(Root_Path + fileName,FileMode.Append,FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.Write(content);
            //sw.Close();
            //fs.Close();
        }
        else
        {
            StreamWriter sw = File.CreateText(Root_Path + fileName);
            sw.Write(content);
            sw.Close();
        }
    }

    public static void CreateDirectory(string dirName)
    {
        if (Directory.Exists(Root_Path + dirName)) return;
        Directory.CreateDirectory(Root_Path + dirName);
    }

    /// <summary>
    /// 将信息保存为json数据 
    /// 文件夹默认为Resources下的Save
    /// </summary>
    /// <param name="o"></param>
    /// <param name="fileName"></param>
    /// <param name="dirName"></param>
    public static void SaveByJson(object o,string fileName,string dirName = "Resources/Save")
    {
        CreateDirectory(dirName);
        string jd = JsonMapper.ToJson(o);
        CreateFile(dirName + "/" + fileName,jd);
        //Debug.Log(jd);
    }

    /// <summary>
    /// 加载保存的Json文件
    /// </summary>
    public static JsonData LoadSaveJson(string fileName)
    {
        string s = Resources.Load<TextAsset>("Save/" +fileName).text;
        JsonData jd = JsonMapper.ToObject(s);
        return jd;
    }
	
}
