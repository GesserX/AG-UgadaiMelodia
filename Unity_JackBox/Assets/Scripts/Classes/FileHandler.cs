using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;

public class FileHandler
{
    public static string[] GetConfigs()
    {
        string [] fileNames = null;
        List<string> tempList = new List<string>(); //так как конфигов можеть быть много - создаем временный List
        
        // Ищем все конфиги с расширением .json в папке StreamingAssets и добавляем во временный List
        foreach (string file in Directory.GetFiles(Application.streamingAssetsPath + "/db/"))
        {
            if(Path.GetExtension(file) == ".json")
            {
                //TODO : Проверка, что все пути к трекам в конфиге валидны (песни существуют и к ним есть доступ)
                //если конфиг валиден:
                //Добавляем конфиг в массив
                tempList.Add(file);
            }
        }

        if(tempList.Count > 0) // если хоть один конфиг был добавлен - конвертируем List в массив
        fileNames = tempList.ToArray();
        //TODO: если конфигов не было добавлено - вывод предупреждения, и отмена запуска игры.

        return fileNames; 
    } 

    public static void SaveConfig(Category category)
    {
        string cat = JsonUtility.ToJson(category);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/db/" + category.categoryName + ".json", cat);
    }
     
}
