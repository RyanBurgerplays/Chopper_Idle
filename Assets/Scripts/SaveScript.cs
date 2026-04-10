using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class SaveScript : MonoBehaviour
{
    private SaveManager savething;

    private void Start()
    {
        savething = new SaveManager();
        StartCoroutine(SaveTime());
        savething.Load();
    }

    IEnumerator SaveTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            savething.Save();
        }
    }

    public class GameData
    {
        public List<ShopData> ShopUpgrades = new List<ShopData>();
    }

    [Serializable]
    public class ShopData
    {
        public string name;
        public int amount;
    }

    public class SaveManager
    {

        public string fileName = "save.xml";

        public void Save()
        {
            ShopUpgrade[] objs = GameObject.FindObjectsByType<ShopUpgrade>(FindObjectsSortMode.None);

            GameData data = new GameData();

            foreach (var obj in objs)
            {
                ShopData od = new ShopData();
                od.name = obj.gameObject.name;
                od.amount = obj.amount;

                data.ShopUpgrades.Add(od);
            }
            UpgradeScript[] objs2 = GameObject.FindObjectsByType<UpgradeScript>(FindObjectsSortMode.None);

            GameData data2 = new GameData();

            foreach (var obj2 in objs2)
            {
                ShopData od2 = new ShopData();
                od2.name = obj2.gameObject.name;
                od2.amount = obj2.amount;

                data.ShopUpgrades.Add(od2);
            }

            string path = Path.Combine(Application.persistentDataPath, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream stream = new FileStream(path, FileMode.Create);

            serializer.Serialize(stream, data);
            stream.Close();

            Debug.Log("Saved to: " + path);
        }
        public void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (!File.Exists(path))
            {
                Debug.Log("No save file found.");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = (GameData)serializer.Deserialize(stream);
            stream.Close();

            ShopUpgrade[] objs = GameObject.FindObjectsByType<ShopUpgrade>(FindObjectsSortMode.None);

            foreach (var obj in objs)
            {
                foreach (var saved in data.ShopUpgrades)
                {
                    if (obj.gameObject.name == saved.name)
                    {
                        Debug.Log($"{saved.name}");
                        obj.amount = saved.amount;
                        obj.UpdateUI();
                    }
                }
            }
            UpgradeScript[] objs2 = GameObject.FindObjectsByType<UpgradeScript>(FindObjectsSortMode.None);

            foreach (var obj2 in objs2)
            {
                foreach (var saved in data.ShopUpgrades)
                {
                    if (obj2.gameObject.name == saved.name)
                    {
                        Debug.Log($"{saved.name}");
                        obj2.amount = saved.amount;
                        obj2.UpdateUI();
                    }
                }
            }

        }
    }
}