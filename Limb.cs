using System;
using System.Collections.Generic;
using System.Text;

namespace RogueHunters
{

    public enum LimbState 
    {
        Healthy,
        Bleeding,
        Burned,
        Broken,
        Shattered,
        Severed
    }

    public class Limb
    {
        public string Name;
        public LimbState LimbState;

        public Limb(string name, LimbState limbState) 
        {
            this.Name = name;
            this.LimbState = limbState;
        }

        public void SetLimbState(LimbState limbState) 
        {
            this.LimbState = limbState;
        }

        public LimbState GetLimbState() 
        {
            return this.LimbState;
        }
        
    }
}
