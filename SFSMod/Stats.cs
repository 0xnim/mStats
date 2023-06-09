﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using ModLoader;
using Newtonsoft.Json;
using UnityEngine;

namespace SFSMod
{
    public class ModList
    {
        [JsonProperty("mods")]
        public List<string> Mods { get; set; }
    }

    public class Stats
    {
        public static Stats Main;

        public static async void Setup()
        {
            var modList = new ModList
            {
                Mods = Loader.main.GetAllMods().Select(mod => mod.ModNameID).ToList()
            };

            var json = JsonConvert.SerializeObject(modList);

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://mstats.astromods.xyz/track-mods", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Debug.Log(responseString);
            }
        }
    }
}