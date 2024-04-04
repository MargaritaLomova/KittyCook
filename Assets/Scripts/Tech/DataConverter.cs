using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.IO.Pipes;

namespace KittyCook.Helpers
{
    public class DataConverter : MonoBehaviour
    {
        [SerializeField]
        protected string fileName;

        private string filesCommonPath = "Data";

        protected virtual void Awake() { }

        public void AddDataLine(JObject dataObject)
        {
            var existedData = GetAllData();

            existedData.Add(dataObject);

            var fileStream = new StreamWriter(Path.Combine(filesCommonPath, $"{fileName}.json"));

            fileStream.Write(existedData.ToString());
            fileStream.Close();

            Debug.Log($"Successfully added {dataObject.Value<string>("ENName")}");
        }

        public void RemoveDataLine(string objectName)
        {
            var existedData = GetAllData();

            JToken dataForRemove = null;

            foreach (var data in existedData)
            {
                if (data.Value<string>("ENName").ToLower() == objectName.ToLower())
                    dataForRemove = data;
            }

            if (dataForRemove == null)
            {
                Debug.LogError($"Cannot find object with name {objectName}!");
                return;
            }

            existedData.Remove(dataForRemove);

            var fileStream = new StreamWriter(Path.Combine(filesCommonPath, $"{fileName}.json"));

            fileStream.Write(existedData.ToString());
            fileStream.Close();

            Debug.Log($"Successfully removed {objectName}");
        }

        public JArray GetAllData()
        {
            if (!Directory.Exists(filesCommonPath))
            {
                Debug.LogError($"Directory {filesCommonPath} is not exist!");
                return null;
            }

            StreamReader reader = new StreamReader(Path.Combine(filesCommonPath, $"{fileName}.json"));
            var simpleText = reader.ReadToEnd();
            JArray jsonData = new JArray();
            if (simpleText != string.Empty)
                jsonData = JArray.Parse(simpleText);

            reader.Close();

            return jsonData;
        }
    }
}