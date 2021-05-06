using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.BoatSelector;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class Boat
    {
        public string name { get; set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }

        public void Attack(int damage)
        {
            currentHealth -= damage;
        }

        public bool IsDeath()
        {
            return currentHealth <= 0;
        }
    }
}
