using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Category
{
    public string categoryName; // строка с названем категории
    public string[] pathTracksQuest = new string[Constants.CATEGORIES_COUNT]; // строки с адресом треков-вопросов на локальном PC
    public string[] pathTracksAnsw = new string[Constants.CATEGORIES_COUNT]; // строки с адресом треков-ответов налокальном PC
    public int[] pointsTracks = new int[Constants.CATEGORIES_COUNT]; // целочисленные баллы за ответы

    public string[] tracksTags = new string[Constants.CATEGORIES_COUNT]; // теги ответов

    public static Category CreateCategory(string config)
    {
        
        string jsonString = File.ReadAllText(config);
        Category category = JsonUtility.FromJson<Category>(jsonString);

        return category;
    }  

}

public class RoundGrid
{
    public string[] categoryNames = new string[Constants.CATEGORIES_COUNT]; // строки с названиями категорий
    public string[,] pathTracksQuest = new string[Constants.CATEGORIES_COUNT, Constants.TRACK_COUNT]; // строки с адресом треков-вопросов на локальном PC
    public string[,] pathTracksAnsw = new string[Constants.CATEGORIES_COUNT, Constants.TRACK_COUNT]; // строки с адресом треков-ответов на локальном PC
    public int[,] pointsTracks = new int[Constants.CATEGORIES_COUNT, Constants.TRACK_COUNT]; // целочисленные баллы за ответы

    public string [,] tracksTags = new string[Constants.CATEGORIES_COUNT, Constants.TRACK_COUNT];
    public static RoundGrid CreateRoundGrid()
    {
        RoundGrid grid = new RoundGrid();
        Category [] categories = new Category[Constants.CATEGORIES_COUNT];
        string [] configs = FileHandler.GetConfigs();

        if(configs != null)
        {
            for(int i = 0; i < categories.Length; i++)
            {
                categories[i] = Category.CreateCategory(configs[PlayerPrefs.GetInt(i.ToString())]);
            }

            for (int categoryIndex = 0; categoryIndex < Constants.CATEGORIES_COUNT; categoryIndex++)
            {            
                grid.categoryNames[categoryIndex] = categories[categoryIndex].categoryName;
                for(int trackIndex = 0; trackIndex < Constants.TRACK_COUNT; trackIndex ++)
                {
                    grid.pathTracksQuest[categoryIndex, trackIndex] = categories[categoryIndex].pathTracksQuest[trackIndex]; 
                    grid.pathTracksAnsw[categoryIndex, trackIndex] = categories[categoryIndex].pathTracksAnsw[trackIndex];
                    grid.pointsTracks[categoryIndex, trackIndex] = categories[categoryIndex].pointsTracks[trackIndex];
                    grid.tracksTags[categoryIndex, trackIndex] = categories[categoryIndex].tracksTags[trackIndex];
                }  
            }
                
        }

        return grid;
    }

}

