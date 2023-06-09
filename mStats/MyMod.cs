using HarmonyLib;
using ModLoader;
using ModLoader.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace mStats
{
    /**
     * You only need to implement the Mod class once in your mod. The Mod class is how 
     * you tell the mod loader what it needs to load and execute.
     */
    public class MyMod : Mod
    {
        public static MyMod Main;

        // this ModNameID can be whatever you want
        public override string ModNameID => "mStats";

        public override string DisplayName => "mStats";

        public override string Author => "0xnim";

        public override string MinimumGameVersionNecessary => "1.5.10";

        // I recommend use MAJOR.MINOR.PATCH Semantic versioning. 
        // Reference link: https://semver.org/ 
        public override string ModVersion => "0.1.0";

        public override string Description => "This mod collects info about what mods are popular.";

        // With this variable you can define if your mods needs the others mods to work
        public override Dictionary<string, string> Dependencies
        {
            get
            {
                return this._dependencies;
            }
        }

        // Here you can specify which mods and version you need
        private Dictionary<string, string> _dependencies = new Dictionary<string, string>() {};

        
        public override void Early_Load()
        {
            // This is for a singleton pattern. you can see more about singleton here https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
            Main = this;

            // Make use to use Debug.log from UnityEngine
            Debug.Log("Running Early load code");

            // Use early load to use Harmony and patch function
            Harmony harmony = new Harmony(ModNameID);
            // I use ModNameID in Harmony, because you need to pass string ID to create an instance of Harmony.

            // This function finds all the patches you have created and runs them
            harmony.PatchAll();

        }

        public override void Load()
        {
            Debug.Log("Running Load code");

            // init your keybindings
            Stats.Setup();

        }
    }
}
