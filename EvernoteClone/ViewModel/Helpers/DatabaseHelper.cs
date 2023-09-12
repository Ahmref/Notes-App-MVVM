using EvernoteClone.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        private static string dbPath = "https://notes-app-wpf-457ab-default-rtdb.europe-west1.firebasedatabase.app/";

        public static async Task<bool> Insert<T>(T item)
        {
            //bool result = false;

            //using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    conn.CreateTable<T>();
            //    int rows = conn.Insert(item);
            //    if (rows > 0)
            //        result = true;
            //}

            //return result;
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var Client = new HttpClient())
            {
                var result = await Client.PostAsync($"{dbPath}{item.GetType().Name.ToLower()}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
        }

        public async static Task<bool> Update<T>(T item) where T : HasId
        {
            //bool result = false;

            //using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    conn.CreateTable<T>();
            //    int rows = conn.Update(item);
            //    if (rows > 0)
            //        result = true;
            //}

            //return result;
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var Client = new HttpClient())
            {
                var result = await Client.PatchAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
        }

        public async static Task<bool> Delete<T>(T item) where T : HasId
        {
            //bool result = false;

            //using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    conn.CreateTable<T>();
            //    int rows = conn.Delete(item);
            //    if (rows > 0)
            //        result = true;
            //}

            //return result;

            using (var Client = new HttpClient())
            {
                var result = await Client.DeleteAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.Id}.json");
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
        }

        public static async Task<List<T>> Read<T>() where T : HasId
        {
            //List<T> items;

            //using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    conn.CreateTable<T>();
            //    items = conn.Table<T>().ToList();
            //}

            //return items;
            using (var Client = new HttpClient())
            {
                var result = await Client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");
                var jsonResult = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);
                    List<T> list = new List<T>();
                    if (objects != null)
                    {
                        foreach (var o in objects)
                        {
                            o.Value.Id = o.Key;
                            list.Add(o.Value);
                        }

                    }
                    return list;
                }

                else
                { return null; }

            }
        }
    }
}

