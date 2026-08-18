using System;
using System.Collections.Generic;
using System.Text;

namespace RogueHunters
{
    public class Hunter
    {
        public string Name;
        public string FamilyName;

        /// <summary>
        /// All four skills have effects and rolls both in Investigation phase and in
        /// Hunting phase. 
        /// 
        /// Observations:
        ///     Investigation: Spotting hidden environmental details
        ///     Combat: Determines if you notice an ambush before combat starts, granting
        ///     a Free Strike.
        /// 
        /// Lore/Erudition:
        ///     Investigation: Accurately identify what monster you are tracking based on
        ///     Rumors, or clues. Higher Lore reveals more explicit weaknesses ("This is a
        ///      Strigoi, Vulnerable to Garlic and Iron")
        ///      Combat: Unlocks bonus damage or critical multipliers when exploiting correct
        ///      weaknesses. Using the wrong weapon, this stat won't save you.
        /// 
        /// Grit/Nerves:
        ///     Investigation: Resisting panic or madness when coming face-to-face with horrifying
        ///     scenes.
        ///     Combat: Paranormal entities project fear, illusions or psychic dread. Low Nerves
        ///     causes your attacks to miss, your hand to shake while loading or your character to
        ///     freeze if not outright fly in a fit of sheer panic.
        ///     
        ///     
        /// Instinct/Reflexes:
        ///     Investigation: Tailing Moving targets without beign spotted, quick maneuvers,
        ///     escaping collapsing investigation sites.
        ///     Combat: Dictates Initiative (who attacks first) and your ability to Dodge or 
        ///     Parry a lethat Strike. In brutal combat, hitting first with a prepared weakness
        ///     ends the fight immediately.
        /// </summary>
        //Skills
        int Observation;

        int Lore;

        int Nerves;

        int Instinct;

        //Stats
        Limb[] Limbs;

        //Statistics
        public int TotalHunts;
        public int SuccessfulHunts;
        public int FailedHunts;


        public Hunter(string hunterName, string hunterFamilyName, int hunterObservation, 
            int hunterLore, int hunterNerves, int hunterInstinct, Limb[] hunterLimbs) 
        {
            this.Name = hunterName;
            this.FamilyName = hunterFamilyName;
            this.Observation = hunterObservation;
            this.Lore = hunterLore;
            this.Nerves = hunterNerves;
            this.Instinct = hunterInstinct;
            this.Limbs = hunterLimbs;
        }
    }
}
