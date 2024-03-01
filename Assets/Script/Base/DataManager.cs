using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Framework
{
    public class GameDataManager
    {
        public static void Save<T>(T data, string filePath)
        {
            var path = Path.Combine(Application.persistentDataPath, filePath);
            var jsonData = JsonConvert.SerializeObject(data);
            var bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                Debug.LogError($"(Error catched!) {e}");
            }
        }

        public static T Load<T>(string filePath) where T : class
        {
            var path = Path.Combine(Application.persistentDataPath, filePath);
            if (!File.Exists(path))
            {
                Debug.Log($"file not found! {path}");
                return null;
            }
            try
            {
                var bytes = File.ReadAllBytes(path);
                var jsonData = System.Text.Encoding.UTF8.GetString(bytes);
                var data = JsonConvert.DeserializeObject<T>(jsonData);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"(Error catched!) {e}");
                return null;
            }
        }

        public static void Delete(string filePath)
        {
            string path = Path.Combine(Application.persistentDataPath, filePath);
            if (!File.Exists(path))
                return;
            File.Delete(path);
        }

    }
}
