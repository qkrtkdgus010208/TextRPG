using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Data
{
    internal class GameSaveData
    {
        public CharacterData CharacterData { get; set; } = new CharacterData();

        public InventoryData InventoryData { get; set; } = new InventoryData();
    }
}
