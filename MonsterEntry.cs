using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RogueHunters
{
    public enum ThreatLevel 
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Extreme = 4
    }

    public class MonsterEntry 
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("weaknesses")]
        public List<string> Weaknesses { get; set; } = new();

        [JsonPropertyName("lore")]
        public string Lore { get; set; } = string.Empty;

        [JsonPropertyName("threat_level")]
        public ThreatLevel ThreatLvl { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; } = string.Empty;

        public MonsterEntry(int id, string name, string description, List<string> weaknesses, string lore, ThreatLevel threatLvl, string origin)
        {
            Id = id;
            Name = name;
            Description = description;
            Weaknesses = weaknesses;
            Lore = lore;
            ThreatLvl = threatLvl;
            Origin = origin;
        }
    }
}
