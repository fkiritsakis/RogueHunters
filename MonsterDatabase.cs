using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RogueHunters
{
    public class MonsterDatabase 
    {
        private MonsterEntry[] _monsters = Array.Empty<MonsterEntry>();

        public MonsterEntry[] Monsters => _monsters;
        public MonsterDatabase() { }


        /// <summary>
        /// Reads monster data from a json file and populates array
        /// </summary>
        /// <param name="filePath">Path to Json File</param>
        public void LoadMonsters(string filePath) 
        {
            if (!File.Exists(filePath)) 
            {
                throw new FileNotFoundException("File not Found");
            }

            string jsonContent = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            { 
                Converters = {new JsonStringEnumConverter()}
            };

            _monsters = JsonSerializer.Deserialize<MonsterEntry[]>(jsonContent, options)
                ?? Array.Empty<MonsterEntry>();

        }
    }
}
