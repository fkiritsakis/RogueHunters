using System;
using System.Collections.Generic;
using System.Text;

namespace RogueHunters
{
    class MonsterEntry
    {
        public string Name; //Monster Name
        public string Description; //Visual Description of the Monster
        public string Lore; // Lore from onlnine sources etc to make it seem more real
        public string[] Weaknesses; // Weaknesses to silver, holy water, blessed weapons, iron
        public string Mythology; //Mythology of Origin
        public enum ThreatLevel; // High, Mid, Low


        public MonsterEntry(string name, string description) 
        {
            name = this.Name;
            description = this.Description;
        }
    }


}
