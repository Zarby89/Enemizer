using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class ItemLocations
    {
        public ItemLocations()
        {
            Nodes = new Dictionary<string, ItemLocation>();
            FillItemNodes();
        }

        class RawItemLocation
        {
            public string LogicalId;
            public int LocationAddress;
            public string LocationName;
            public int RoomId;
            public string RoomName;
            public int ItemId;
            public string ItemName;
            public int Order;

            public RawItemLocation(string LogicalId, int LocationAddress, string LocationName, int RoomId, string RoomName, int ItemId, string ItemName, int Order)
            {
                this.LogicalId = LogicalId;
                this.LocationAddress = LocationAddress;
                this.LocationName = LocationName;
                this.RoomId = RoomId;
                this.RoomName = RoomName;
                this.ItemId = ItemId;
                this.ItemName = ItemName;
                this.Order = Order;
            }
        }

        Dictionary<string, RawItemLocation> rawItemLocations = new Dictionary<string, RawItemLocation>()
        {
            { "ow-mushroom-item", new RawItemLocation("ow-mushroom-item", 0x180013, "Mushroom", 0x8000, "Lost Woods", 0x29, "Mushroom", 0) },
            { "ow-old-man-item", new RawItemLocation("ow-old-man-item", 0x0F69FA, "Old Mountain Man", 0x8003, "West Death Mountain", 0x1A, "Mirror", 0) },
            { "ow-spectical-rock-item", new RawItemLocation("ow-spectical-rock-item", 0x180140, "Piece of Heart (Spectacle Rock)", 0x8003, "West Death Mountain", 0x17, "Piece of Heart", 0) },
            { "ow-ether-tablet-item", new RawItemLocation("ow-ether-tablet-item", 0x180016, "Ether Tablet", 0x8003, "West Death Mountain", 0x10, "Ether", 0) },
            { "ow-dm-floating-island-item", new RawItemLocation("ow-dm-floating-island-item", 0x180141, "Piece of Heart (Death Mountain - floating island)", 0x8005, "East Death Mountain", 0x17, "Piece of Heart", 0) },
            { "ow-bottle-vendor-item", new RawItemLocation("ow-bottle-vendor-item", 0x02EB18, "Bottle Vendor", 0x8018, "Kakariko Village", 0x16, "Bottle", 0) },
            { "ow-maze-race-item", new RawItemLocation("ow-maze-race-item", 0x180142, "Piece of Heart (Maze Race)", 0x8028, "Kakariko Village Maze Race", 0x17, "Piece of Heart", 0) },
            { "ow-haunted-grove-item", new RawItemLocation("ow-haunted-grove-item", 0x18014A, "Haunted Grove item", 0x802A, "Haunted Grove", 0x14, "Flute", 0) },
            { "ow-bombos-tablet-item", new RawItemLocation("ow-bombos-tablet-item", 0x180017, "Bombos Tablet", 0x8030, "Desert of Mystery", 0x0F, "Bombos", 0) },
            { "ow-desert-ledge-item", new RawItemLocation("ow-desert-ledge-item", 0x180143, "Piece of Heart (Desert - west side)", 0x8030, "Desert of Mystery", 0x17, "Piece of Heart", 0) },
            { "ow-lake-island-item", new RawItemLocation("ow-lake-island-item", 0x180144, "Piece of Heart (Lake Hylia)", 0x8035, "Lake Hylia", 0x17, "Piece of Heart", 0) },
            { "ow-purple-chest-item", new RawItemLocation("ow-purple-chest-item", 0x033D68, "Purple Chest", 0x803A, "Path Between Desert of Mystery and Great Swamp", 0x16, "Bottle", 0) },
            { "ow-dam-item", new RawItemLocation("ow-dam-item", 0x180145, "Piece of Heart (Dam)", 0x803B, "South-western Great Swamp", 0x17, "Piece of Heart", 0) },
            { "ow-bumper-cave-item", new RawItemLocation("ow-bumper-cave-item", 0x180146, "Piece of Heart (Dark World - bumper cave)", 0x804A, "Bumper Cave Entrance (DW)", 0x17, "Piece of Heart", 0) },
            { "ow-catfish-item", new RawItemLocation("ow-catfish-item", 0x0EE185, "Catfish", 0x804F, "Catfish (DW)", 0x11, "Quake", 0) },
            { "ow-pyramid-item", new RawItemLocation("ow-pyramid-item", 0x180147, "Piece of Heart (Pyramid)", 0x805B, "Pyramid (DW)", 0x17, "Piece of Heart", 0) },
            { "ow-digging-game-item", new RawItemLocation("ow-digging-game-item", 0x180148, "Piece of Heart (Digging Game)", 0x8068, "Digging Game (DW)", 0x17, "Piece of Heart", 0) },
            { "ow-flute-boy-item", new RawItemLocation("ow-flute-boy-item", 0x0330C7, "Flute Boy", 0x806A, "Haunted Grove (DW)", 0x13, "Shovel", 0) },
            { "ow-hobo-item", new RawItemLocation("ow-hobo-item", 0x033E7D, "Hobo", 0x8080, "Master Sword Glade / Under Bridge", 0x16, "Bottle", 0) },
            { "ow-pedestal-item", new RawItemLocation("ow-pedestal-item", 0x0289B0, "Pedestal", 0x8080, "Master Sword Glade / Under Bridge", 0x01, "Progressive Sword", 0) },
            { "ow-zora-king-item", new RawItemLocation("ow-zora-king-item", 0x0EE1C3, "King Zora", 0x8081, "Zora's Domain", 0x1E, "Flippers", 0) },
            { "ow-zora-ledge-item", new RawItemLocation("ow-zora-ledge-item", 0x180149, "Piece of Heart (Zora's River)", 0x8081, "Zora's Domain", 0x17, "Piece of Heart", 0) },
            { "agahnim-dark-maze-chest", new RawItemLocation("agahnim-dark-maze-chest", 0x00EAB2, "[dungeon-A1-3F] Hyrule Castle Tower - maze room", 0x00D0, "Agahnim's Tower (Dark Maze)", 0x24, "Key", 1) },
            { "agahnim-entrance-chest", new RawItemLocation("agahnim-entrance-chest", 0x00EAB5, "[dungeon-A1-2F] Hyrule Castle Tower - 2 knife guys room", 0x00E0, "Agahnim's Tower (Entrance Room)", 0x24, "Key", 1) },
            { "cave-aginah-chest", new RawItemLocation("cave-aginah-chest", 0x00E9F2, "[cave-044] Aginah's cave", 0x010A, "Aginah's Cave", 0x17, "Piece of Heart", 1) },
            { "cave-big-bomb-shop-chest", new RawItemLocation("cave-big-bomb-shop-chest", 0x00E9EF, "[cave-062] C-shaped house", 0x011C, "Bomb Shop", 0x46, "300 Rupees", 1) },
            { "cave-blinds-basement-chest1", new RawItemLocation("cave-blinds-basement-chest1", 0x00EB0F, "[cave-022-B1] Thief's hut [top chest]", 0x011D, "Blind's Basement", 0x17, "Piece of Heart", 1) },
            { "cave-blinds-basement-chest2", new RawItemLocation("cave-blinds-basement-chest2", 0x00EB12, "[cave-022-B1] Thief's hut [top left chest]", 0x011D, "Blind's Basement", 0x36, "Red Rupee", 2) },
            { "cave-blinds-basement-chest3", new RawItemLocation("cave-blinds-basement-chest3", 0x00EB15, "[cave-022-B1] Thief's hut [top right chest]", 0x011D, "Blind's Basement", 0x36, "Red Rupee", 3) },
            { "cave-blinds-basement-chest4", new RawItemLocation("cave-blinds-basement-chest4", 0x00EB18, "[cave-022-B1] Thief's hut [bottom left chest]", 0x011D, "Blind's Basement", 0x36, "Red Rupee", 4) },
            { "cave-blinds-basement-chest5", new RawItemLocation("cave-blinds-basement-chest5", 0x00EB1B, "[cave-022-B1] Thief's hut [bottom right chest]", 0x011D, "Blind's Basement", 0x36, "Red Rupee", 5) },
            { "cave-bonk-rocks-chest", new RawItemLocation("cave-bonk-rocks-chest", 0x00EB3F, "[cave-016] cave under rocks west of Santuary", 0x0124, "Unknown Cave / Bonk Cave", 0x17, "Piece of Heart", 1) },
            { "cave-checkerboard-chest", new RawItemLocation("cave-checkerboard-chest", 0x180005, "Piece of Heart (Desert - northeast corner)", 0x0126, "Checker Board Cave", 0x17, "Piece of Heart", 0) },
            { "cave-chicken-hut-chest", new RawItemLocation("cave-chicken-hut-chest", 0x00E9E9, "[cave-026] chicken house", 0x0108, "Chicken House", 0x44, "10 Arrows", 1) },
            { "cave-dam-chest", new RawItemLocation("cave-dam-chest", 0x00E98C, "[cave-047] Dam", 0x010B, "Swamp Floodway Room", 0x28, "3 Bombs", 1) },
            { "cave-dw-doorless-hut-chest", new RawItemLocation("cave-dw-doorless-hut-chest", 0x00E9EC, "[cave-063] doorless hut", 0x0106, "Chest Game / Bomb House", 0x2A, "Magical Boomerang", 1) },
            { "cave-dw-chest-game-chest", new RawItemLocation("cave-dw-chest-game-chest", 0x00EDA8, "Piece of Heart (Treasure Chest Game)", 0x0106, "Chest Game / Outcast Village Bomb House", 0x17, "Piece of Heart", 0) },
            { "cave-graveyard-mirror-cave-chest", new RawItemLocation("cave-graveyard-mirror-cave-chest", 0x180004, "Piece of Heart (Graveyard)", 0x011B, "Mirror Caves (South of Tree Boy / Above Kings Tomb)", 0x17, "Piece of Heart", 0) },
            { "cave-hammer-pegs-chest", new RawItemLocation("cave-hammer-pegs-chest", 0x180006, "Piece of Heart (Dark World blacksmith pegs)", 0x0127, "Hammer Peg Cave", 0x17, "Piece of Heart", 0) },
            { "cave-haunted-mirror-cave-chest", new RawItemLocation("cave-haunted-mirror-cave-chest", 0x180003, "Piece of Heart (south of Haunted Grove)", 0x011B, "Mirror Caves (South of Tree Boy / Above Kings Tomb)", 0x17, "Piece of Heart", 0) },
            { "cave-hookshot-entrance-chest1", new RawItemLocation("cave-hookshot-entrance-chest1", 0x00EB51, "[cave-056] Dark World Death Mountain - cave under boulder [top right chest]", 0x003C, "Cave", 0x41, "50 Rupees", 1) },
            { "cave-hookshot-entrance-chest2", new RawItemLocation("cave-hookshot-entrance-chest2", 0x00EB54, "[cave-056] Dark World Death Mountain - cave under boulder [top left chest]", 0x003C, "Cave", 0x41, "50 Rupees", 2) },
            { "cave-hookshot-entrance-chest3", new RawItemLocation("cave-hookshot-entrance-chest3", 0x00EB57, "[cave-056] Dark World Death Mountain - cave under boulder [bottom left chest]", 0x003C, "Cave", 0x41, "50 Rupees", 3) },
            { "cave-hookshot-entrance-chest4", new RawItemLocation("cave-hookshot-entrance-chest4", 0x00EB5A, "[cave-056] Dark World Death Mountain - cave under boulder [bottom right chest]", 0x003C, "Cave", 0x41, "50 Rupees", 4) },
            { "cave-hype-cave-chest", new RawItemLocation("cave-hype-cave-chest", 0x180011, "[cave-073] cave northeast of swamp palace - generous guy", 0x011E, "Hype Cave", 0x46, "300 Rupees", 0) },
            { "cave-hype-cave-chest1", new RawItemLocation("cave-hype-cave-chest1", 0x00EB1E, "[cave-073] cave northeast of swamp palace [top chest]", 0x011E, "Hype Cave", 0x36, "Red Rupee", 1) },
            { "cave-hype-cave-chest2", new RawItemLocation("cave-hype-cave-chest2", 0x00EB21, "[cave-073] cave northeast of swamp palace [top middle chest]", 0x011E, "Hype Cave", 0x36, "Red Rupee", 2) },
            { "cave-hype-cave-chest3", new RawItemLocation("cave-hype-cave-chest3", 0x00EB24, "[cave-073] cave northeast of swamp palace [bottom middle chest]", 0x011E, "Hype Cave", 0x36, "Red Rupee", 3) },
            { "cave-hype-cave-chest4", new RawItemLocation("cave-hype-cave-chest4", 0x00EB27, "[cave-073] cave northeast of swamp palace [bottom chest]", 0x011E, "Hype Cave", 0x36, "Red Rupee", 4) },
            { "cave-ice-rod-chest", new RawItemLocation("cave-ice-rod-chest", 0x00EB4E, "[cave-051] Ice Cave", 0x0120, "Ice Rod Cave", 0x08, "Ice Rod", 1) },
            { "cave-inn-top-chest", new RawItemLocation("cave-inn-top-chest", 0x00E9CE, "[cave-031] Tavern", 0x0103, "Inn / Bush House", 0x16, "Bottle", 1) },
            { "cave-kakariko-well-chest1", new RawItemLocation("cave-kakariko-well-chest1", 0x00EA8E, "[cave-021] Kakariko well [top chest]", 0x002F, "Cave (Kakariko Well HP)", 0x17, "Piece of Heart", 1) },
            { "cave-kakariko-well-chest2", new RawItemLocation("cave-kakariko-well-chest2", 0x00EA91, "[cave-021] Kakariko well [left chest row of 3]", 0x002F, "Cave (Kakariko Well HP)", 0x36, "Red Rupee", 2) },
            { "cave-kakariko-well-chest3", new RawItemLocation("cave-kakariko-well-chest3", 0x00EA94, "[cave-021] Kakariko well [center chest row of 3]", 0x002F, "Cave (Kakariko Well HP)", 0x36, "Red Rupee", 3) },
            { "cave-kakariko-well-chest4", new RawItemLocation("cave-kakariko-well-chest4", 0x00EA97, "[cave-021] Kakariko well [right chest row of 3]", 0x002F, "Cave (Kakariko Well HP)", 0x36, "Red Rupee", 4) },
            { "cave-kakariko-well-chest5", new RawItemLocation("cave-kakariko-well-chest5", 0x00EA9A, "[cave-021] Kakariko well [bottom chest]", 0x002F, "Cave (Kakariko Well HP)", 0x28, "3 Bombs", 5) },
            { "cave-kings-tomb-chest", new RawItemLocation("cave-kings-tomb-chest", 0x00E97A, "[cave-018] Graveyard - top right grave", 0x0113, "King's Tomb", 0x19, "Cape", 1) },
            { "cave-library-chest", new RawItemLocation("cave-library-chest", 0x180012, "Library", 0x0107, "Library / Bomb Farm Room", 0x1D, "Book", 0) },
            { "cave-links-house-chest", new RawItemLocation("cave-links-house-chest", 0x00E9BC, "[cave-040] Link's House", 0x0104, "Link's House", 0x12, "Lamp", 1) },
            { "cave-lumberjack-tree-chest", new RawItemLocation("cave-lumberjack-tree-chest", 0x180001, "Piece of Heart (Lumberjack Tree)", 0x00E2, "Cave (Lumberjack's Tree HP)", 0x17, "Piece of Heart", 0) },
            { "cave-magic-bat-chest", new RawItemLocation("cave-magic-bat-chest", 0x180015, "Magic Bat", 0x00E3, "Cave (1/2 Magic)", 0x4E, "Progressive Magic", 0) },
            { "cave-mimic-cave-chest", new RawItemLocation("cave-mimic-cave-chest", 0x00E9C5, "[cave-013] Mimic cave (from Turtle Rock)", 0x010C, "Mimic Cave", 0x17, "Piece of Heart", 1) },
            { "cave-mini-moldorm-chest1", new RawItemLocation("cave-mini-moldorm-chest1", 0x00EB42, "[cave-050] cave southwest of Lake Hylia [bottom left chest]", 0x0123, "Mini-Moldorm Cave", 0x28, "3 Bombs", 1) },
            { "cave-mini-moldorm-chest2", new RawItemLocation("cave-mini-moldorm-chest2", 0x00EB45, "[cave-050] cave southwest of Lake Hylia [top left chest]", 0x0123, "Mini-Moldorm Cave", 0x36, "Red Rupee", 2) },
            { "cave-mini-moldorm-chest3", new RawItemLocation("cave-mini-moldorm-chest3", 0x00EB48, "[cave-050] cave southwest of Lake Hylia [top right chest]", 0x0123, "Mini-Moldorm Cave", 0x36, "Red Rupee", 3) },
            { "cave-mini-moldorm-chest4", new RawItemLocation("cave-mini-moldorm-chest4", 0x00EB4B, "[cave-050] cave southwest of Lake Hylia [bottom right chest]", 0x0123, "Mini-Moldorm Cave", 0x44, "10 Arrows", 4) },
            { "cave-mini-moldorm-guy", new RawItemLocation("cave-mini-moldorm-guy", 0x180010, "[cave-050] cave southwest of Lake Hylia - generous guy", 0x0123, "Mini-Moldorm Cave", 0x46, "300 Rupees", 0) },
            { "cave-mire-chests-chest1", new RawItemLocation("cave-mire-chests-chest1", 0x00EA73, "[cave-071] Misery Mire west area [left chest]", 0x010D, "Cave outside Misery Mire", 0x17, "Piece of Heart", 1) },
            { "cave-mire-chests-chest2", new RawItemLocation("cave-mire-chests-chest2", 0x00EA76, "[cave-071] Misery Mire west area [right chest]", 0x010D, "Cave outside Misery Mire", 0x36, "Red Rupee", 2) },
            { "cave-pyramid-fairy-chest1", new RawItemLocation("cave-pyramid-fairy-chest1", 0x00E980, "Pyramid Fairy - Left", 0x0116, "Fat Fairy", 0x03, "Progressive Sword", 1) },
            { "cave-pyramid-fairy-chest2", new RawItemLocation("cave-pyramid-fairy-chest2", 0x00E983, "Pyramid Fairy - Right", 0x0116, "Fat Fairy", 0x58, "Silver Arrows", 2) },
            { "cave-shabadoo-house-chest1", new RawItemLocation("cave-shabadoo-house-chest1", 0x00EA82, "[cave-035] Sahasrahla's Hut [left chest]", 0x0105, "Sahasrahla's House", 0x41, "50 Rupees", 1) },
            { "cave-shabadoo-house-chest2", new RawItemLocation("cave-shabadoo-house-chest2", 0x00EA85, "[cave-035] Sahasrahla's Hut [center chest]", 0x0105, "Sahasrahla's House", 0x28, "3 Bombs", 2) },
            { "cave-shabadoo-house-chest3", new RawItemLocation("cave-shabadoo-house-chest3", 0x00EA88, "[cave-035] Sahasrahla's Hut [right chest]", 0x0105, "Sahasrahla's House", 0x41, "50 Rupees", 3) },
            { "cave-shabadoo-house-shabadoo", new RawItemLocation("cave-shabadoo-house-shabadoo", 0x02F1FC, "Sahasrahla", 0x0105, "Sahasrahla's House", 0x4B, "Boots", 0) },
            { "cave-sick-kid-chest", new RawItemLocation("cave-sick-kid-chest", 0x0339CF, "Sick Kid", 0x0102, "Sick Kid", 0x21, "Bug Catching Net", 0) },
            { "cave-smith-house-item", new RawItemLocation("cave-smith-house-item", 0x18002A, "Blacksmiths", 0x0121, "Smiths' House", 0x02, "Progressive Sword", 0) },
            { "cave-spectical-rock-upper-back-chest", new RawItemLocation("cave-spectical-rock-upper-back-chest", 0x180002, "Piece of Heart (Spectacle Rock Cave)", 0x00EA, "Cave (Inside Spectacle Rock HP)", 0x17, "Piece of Heart", 0) },
            { "cave-spike-cave-chest", new RawItemLocation("cave-spike-cave-chest", 0x00EA8B, "[cave-055] Spike cave", 0x0117, "Spike Cave", 0x18, "Byrna", 1) },
            { "cave-spiral-cave-exit-chest", new RawItemLocation("cave-spiral-cave-exit-chest", 0x00E9BF, "[cave-012-1F] Death Mountain - wall of caves - left cave", 0x00FE, "Cave", 0x41, "50 Rupees", 1) },
            { "cave-super-bunny-chests-chest1", new RawItemLocation("cave-super-bunny-chests-chest1", 0x00EA7C, "[cave-057-1F] Dark World Death Mountain - cave from top to bottom [top chest]", 0x00F8, "Cave", 0x28, "3 Bombs", 1) },
            { "cave-super-bunny-chests-chest2", new RawItemLocation("cave-super-bunny-chests-chest2", 0x00EA7F, "[cave-057-1F] Dark World Death Mountain - cave from top to bottom [bottom chest]", 0x00F8, "Cave", 0x36, "Red Rupee", 2) },
            { "cave-thief-hut-drop-chest", new RawItemLocation("cave-thief-hut-drop-chest", 0x180000, "Piece of Heart (Thieves' Forest Hideout)", 0x00E1, "Cave (Lost Woods HP)", 0x17, "Piece of Heart", 0) },
            { "cave-uncle-death-chest", new RawItemLocation("cave-uncle-death-chest", 0x00E971, "[cave-034] Hyrule Castle secret entrance", 0x0055, "Castle Secret Entrance / Uncle Death Room", 0x12, "Lamp", 1) },
            { "cave-uncle-death-uncle", new RawItemLocation("cave-uncle-death-uncle", 0x02DF45, "Uncle", 0x0055, "Castle Secret Entrance / Uncle Death Room", 0x00, "Progressive Sword", 0) },
            { "cave-upside-down-2-chest-chest1", new RawItemLocation("cave-upside-down-2-chest-chest1", 0x00EB39, "[cave-009-B1] Death Mountain - wall of caves - right cave [left chest]", 0x00FF, "Cave ", 0x28, "3 Bombs", 1) },
            { "cave-upside-down-2-chest-chest2", new RawItemLocation("cave-upside-down-2-chest-chest2", 0x00EB3C, "[cave-009-B1] Death Mountain - wall of caves - right cave [right chest]", 0x00FF, "Cave ", 0x44, "10 Arrows", 2) },
            { "cave-upside-down-5-chest-chest1", new RawItemLocation("cave-upside-down-5-chest-chest1", 0x00EB2A, "[cave-009-1F] Death Mountain - wall of caves - right cave [top left chest]", 0x00EF, "Cave (Crystal Switch / 5 Chests Room)", 0x36, "Red Rupee", 1) },
            { "cave-upside-down-5-chest-chest2", new RawItemLocation("cave-upside-down-5-chest-chest2", 0x00EB2D, "[cave-009-1F] Death Mountain - wall of caves - right cave [top left middle chest]", 0x00EF, "Cave (Crystal Switch / 5 Chests Room)", 0x36, "Red Rupee", 2) },
            { "cave-upside-down-5-chest-chest3", new RawItemLocation("cave-upside-down-5-chest-chest3", 0x00EB30, "[cave-009-1F] Death Mountain - wall of caves - right cave [top right middle chest]", 0x00EF, "Cave (Crystal Switch / 5 Chests Room)", 0x36, "Red Rupee", 3) },
            { "cave-upside-down-5-chest-chest4", new RawItemLocation("cave-upside-down-5-chest-chest4", 0x00EB33, "[cave-009-1F] Death Mountain - wall of caves - right cave [top right chest]", 0x00EF, "Cave (Crystal Switch / 5 Chests Room)", 0x36, "Red Rupee", 4) },
            { "cave-upside-down-5-chest-chest5", new RawItemLocation("cave-upside-down-5-chest-chest5", 0x00EB36, "[cave-009-1F] Death Mountain - wall of caves - right cave [bottom chest]", 0x00EF, "Cave (Crystal Switch / 5 Chests Room)", 0x36, "Red Rupee", 5) },
            { "cave-witch-shop-chest", new RawItemLocation("cave-witch-shop-chest", 0x180014, "Witch", 0x0109, "Witch Hut", 0x0D, "Powder", 0) },
            { "cave-zora-waterfall-wishing-chest1", new RawItemLocation("cave-zora-waterfall-wishing-chest1", 0x00E9B0, "Waterfall Fairy - Left", 0x0114, "Wishing Well / Cave 0x114", 0x05, "Progressive Shield", 1) },
            { "cave-zora-waterfall-wishing-chest2", new RawItemLocation("cave-zora-waterfall-wishing-chest2", 0x00E9D1, "Waterfall Fairy - Right", 0x0114, "Wishing Well / Cave 0x114", 0x2A, "Magical Boomerang", 2) },
            { "desert-big-chest-chest", new RawItemLocation("desert-big-chest-chest", 0x00E98F, "[dungeon-L2-B1] Desert Palace - big chest", 0x0073, "Desert Palace (Big Chest Room)", 0x1B, "Progressive Gloves", 1) },
            { "desert-big-key-chest", new RawItemLocation("desert-big-key-chest", 0x00E9C2, "[dungeon-L2-B1] Desert Palace - Big key room", 0x0075, "Desert Palace (Big Key Chest Room)", 0x32, "Big Key", 1) },
            { "desert-east-entrance-chest-chest", new RawItemLocation("desert-east-entrance-chest-chest", 0x00E9CB, "[dungeon-L2-B1] Desert Palace - compass room", 0x0085, "Desert Palace (East Entrance Room)", 0x8C, "<Desert Compass>", 1) },
            { "desert-boss-item", new RawItemLocation("desert-boss-item", 0x180151, "Heart Container - Lanmolas", 0x0033, "Desert Palace (Lanmolas[Boss])", 0x3E, "Heart Container", 0) },
            { "desert-boss-pendant", new RawItemLocation("desert-boss-pendant", 0x00C6FF, "Desert Palace Pendant6", 0x0033, "Desert Palace (Lanmolas[Boss])", 0x03, "<Pendant Red>", 0) },
            { "desert-map-chest-chest", new RawItemLocation("desert-map-chest-chest", 0x00E9B6, "[dungeon-L2-B1] Desert Palace - Map room", 0x0074, "Desert Palace (Map Chest Room)", 0x7C, "<Desert Map>", 1) },
            { "desert-torch-key-chest", new RawItemLocation("desert-torch-key-chest", 0x180160, "[dungeon-L2-B1] Desert Palace - Small key room", 0x0073, "Desert Palace (Big Chest Room)", 0x24, "Key", 0) },
            { "eastern-boss-item", new RawItemLocation("eastern-boss-item", 0x180150, "Heart Container - Armos Knights", 0x00C8, "Eastern Palace (Armos Knights[Boss])", 0x3E, "Heart Container", 0) },
            { "eastern-boss-pendant", new RawItemLocation("eastern-boss-pendant", 0x00C6FE, "Eastern Palace Pendant6", 0x00C8, "Eastern Palace (Armos Knights[Boss])", 0x01, "<Pendant Green>", 0) },
            { "eastern-big-chest-lower-chest", new RawItemLocation("eastern-big-chest-lower-chest", 0x00E97D, "[dungeon-L1-1F] Eastern Palace - big chest", 0x00A9, "Eastern Palace (Big Chest Room)", 0x0B, "Bow", 1) },
            { "eastern-big-key-chest", new RawItemLocation("eastern-big-key-chest", 0x00E9B9, "[dungeon-L1-1F] Eastern Palace - Big key", 0x00B8, "Eastern Palace (Big Key Room)", 0x32, "Big Key", 1) },
            { "eastern-lobby-cannon-chest", new RawItemLocation("eastern-lobby-cannon-chest", 0x00E9B3, "[dungeon-L1-1F] Eastern Palace - big ball room", 0x00B9, "Eastern Palace (Lobby Cannonballs Room)", 0x40, "100 Rupees", 1) },
            { "eastern-map-upper-chest", new RawItemLocation("eastern-map-upper-chest", 0x00E9F5, "[dungeon-L1-1F] Eastern Palace - map room", 0x00AA, "Eastern Palace (Map Chest Room)", 0x7D, "<Eastern Map>", 1) },
            { "eastern-stalfos-lower-chest", new RawItemLocation("eastern-stalfos-lower-chest", 0x00E977, "[dungeon-L1-1F] Eastern Palace - compass room", 0x00A8, "Eastern Palace (Stalfos Spawn Room)", 0x8D, "<Eastern Compass>", 1) },
            { "gt-4-chest-left-chest1", new RawItemLocation("gt-4-chest-left-chest1", 0x00EAB8, "[dungeon-A2-1F] Ganon's Tower - north of gap room [top left chest]", 0x007B, "Ganon's Tower", 0x28, "3 Bombs", 1) },
            { "gt-4-chest-left-chest2", new RawItemLocation("gt-4-chest-left-chest2", 0x00EABB, "[dungeon-A2-1F] Ganon's Tower - north of gap room [top right chest]", 0x007B, "Ganon's Tower", 0x44, "10 Arrows", 2) },
            { "gt-4-chest-left-chest3", new RawItemLocation("gt-4-chest-left-chest3", 0x00EABE, "[dungeon-A2-1F] Ganon's Tower - north of gap room [bottom left chest]", 0x007B, "Ganon's Tower", 0x36, "Red Rupee", 3) },
            { "gt-4-chest-left-chest4", new RawItemLocation("gt-4-chest-left-chest4", 0x00EAC1, "[dungeon-A2-1F] Ganon's Tower - north of gap room [bottom right chest]", 0x007B, "Ganon's Tower", 0x36, "Red Rupee", 4) },
            { "gt-armos-chests-chest1", new RawItemLocation("gt-armos-chests-chest1", 0x00EAF1, "[dungeon-A2-B1] Ganon's Tower - north of Armos room [bottom chest]", 0x001C, "Ganon's Tower (Ice Armos)", 0x32, "Big Key", 1) },
            { "gt-armos-chests-chest2", new RawItemLocation("gt-armos-chests-chest2", 0x00EAF4, "[dungeon-A2-B1] Ganon's Tower - north of Armos room [left chest]", 0x001C, "Ganon's Tower (Ice Armos)", 0x44, "10 Arrows", 2) },
            { "gt-armos-chests-chest3", new RawItemLocation("gt-armos-chests-chest3", 0x00EAF7, "[dungeon-A2-B1] Ganon's Tower - north of Armos room [right chest]", 0x001C, "Ganon's Tower (Ice Armos)", 0x28, "3 Bombs", 3) },
            { "gt-big-chest-chest", new RawItemLocation("gt-big-chest-chest", 0x00EAD6, "[dungeon-A2-1F] Ganon's Tower - big chest", 0x008C, "Ganon's Tower (East and West Downstairs / Big Chest Room)", 0x23, "Red Mail", 1) },
            { "gt-bomb-trap-chest", new RawItemLocation("gt-bomb-trap-chest", 0x00EB03, "[dungeon-A2-6F] Ganon's Tower - before Moldorm", 0x003D, "Ganon's Tower (Torch Room 2)", 0x24, "Key", 3) },
            { "gt-fire-snakes-chest", new RawItemLocation("gt-fire-snakes-chest", 0x00EAD0, "[dungeon-A2-1F] Ganon's Tower - north of teleport room", 0x007D, "Ganon's Tower (Winder / Warp Maze Room)", 0x24, "Key", 1) },
            { "gt-hidden-chests-chest1", new RawItemLocation("gt-hidden-chests-chest1", 0x00EAC4, "[dungeon-A2-1F] Ganon's Tower - west of teleport room [top left chest]", 0x007C, "Ganon's Tower (East Side Collapsing Bridge / Exploding Wall Room)", 0x44, "10 Arrows", 1) },
            { "gt-hidden-chests-chest2", new RawItemLocation("gt-hidden-chests-chest2", 0x00EAC7, "[dungeon-A2-1F] Ganon's Tower - west of teleport room [top right chest]", 0x007C, "Ganon's Tower (East Side Collapsing Bridge / Exploding Wall Room)", 0x44, "10 Arrows", 2) },
            { "gt-hidden-chests-chest3", new RawItemLocation("gt-hidden-chests-chest3", 0x00EACA, "[dungeon-A2-1F] Ganon's Tower - west of teleport room [bottom left chest]", 0x007C, "Ganon's Tower (East Side Collapsing Bridge / Exploding Wall Room)", 0x28, "3 Bombs", 3) },
            { "gt-hidden-chests-chest4", new RawItemLocation("gt-hidden-chests-chest4", 0x00EACD, "[dungeon-A2-1F] Ganon's Tower - west of teleport room [bottom right chest]", 0x007C, "Ganon's Tower (East Side Collapsing Bridge / Exploding Wall Room)", 0x28, "3 Bombs", 4) },
            { "gt-left-torch-item", new RawItemLocation("gt-left-torch-item", 0x180161, "[dungeon-A2-1F] Ganon's Tower - down left staircase from entrance", 0x008C, "Ganon's Tower (East and West Downstairs / Big Chest Room)", 0x24, "Key", 0) },
            { "gt-mini-helma-chest1", new RawItemLocation("gt-mini-helma-chest1", 0x00EAFD, "[dungeon-A2-6F] Ganon's Tower - north of falling floor four torches [top left chest]", 0x003D, "Ganon's Tower (Torch Room 2)", 0x28, "3 Bombs", 1) },
            { "gt-mini-helma-chest2", new RawItemLocation("gt-mini-helma-chest2", 0x00EB00, "[dungeon-A2-6F] Ganon's Tower - north of falling floor four torches [top right chest]", 0x003D, "Ganon's Tower (Torch Room 2)", 0x28, "3 Bombs", 2) },
            { "gt-moldorm-chest", new RawItemLocation("gt-moldorm-chest", 0x00EB06, "[dungeon-A2-6F] Ganon's Tower - Moldorm room", 0x004D, "Ganon's Tower (Moldorm Room)", 0x36, "Red Rupee", 1) },
            { "gt-pre-armos-chest", new RawItemLocation("gt-pre-armos-chest", 0x00EADF, "[dungeon-A2-1F] Ganon's Tower - above Armos", 0x008C, "Ganon's Tower (East and West Downstairs / Big Chest Room)", 0x44, "10 Arrows", 4) },
            { "gt-right-chest1", new RawItemLocation("gt-right-chest1", 0x00EAD9, "[dungeon-A2-1F] Ganon's Tower - down right staircase from entrance [left chest]", 0x008C, "Ganon's Tower (East and West Downstairs / Big Chest Room)", 0x44, "10 Arrows", 2) },
            { "gt-right-chest2", new RawItemLocation("gt-right-chest2", 0x00EADC, "[dungeon-A2-1F] Ganon's Tower - down right staircase from entrance [right chest]", 0x008C, "Ganon's Tower (East and West Downstairs / Big Chest Room)", 0x28, "3 Bombs", 3) },
            { "gt-right-four-chest1", new RawItemLocation("gt-right-four-chest1", 0x00EAE5, "[dungeon-A2-1F] Ganon's Tower - compass room [top left chest]", 0x009D, "Ganon's Tower (Compass Chest / Invisible Floor Room)", 0x25, "<GT Compass>", 1) },
            { "gt-right-four-chest2", new RawItemLocation("gt-right-four-chest2", 0x00EAE8, "[dungeon-A2-1F] Ganon's Tower - compass room [top right chest]", 0x009D, "Ganon's Tower (Compass Chest / Invisible Floor Room)", 0x34, "Green Rupee", 2) },
            { "gt-right-four-chest3", new RawItemLocation("gt-right-four-chest3", 0x00EAEB, "[dungeon-A2-1F] Ganon's Tower - compass room [bottom left chest]", 0x009D, "Ganon's Tower (Compass Chest / Invisible Floor Room)", 0x36, "Red Rupee", 3) },
            { "gt-right-four-chest4", new RawItemLocation("gt-right-four-chest4", 0x00EAEE, "[dungeon-A2-1F] Ganon's Tower - compass room [bottom right chest]", 0x009D, "Ganon's Tower (Compass Chest / Invisible Floor Room)", 0x44, "10 Arrows", 4) },
            { "gt-star-chest-chest", new RawItemLocation("gt-star-chest-chest", 0x00EAD3, "[dungeon-A2-1F] Ganon's Tower - map room", 0x008B, "Ganon's Tower (Block Puzzle / Spike Skip / Map Chest Room)", 0x72, "<GT Map>", 1) },
            { "gt-tile-trap-chest", new RawItemLocation("gt-tile-trap-chest", 0x00EAE2, "[dungeon-A2-1F] Ganon's Tower - east of down right staircase from entrance", 0x008D, "Ganon's Tower (Tile / Torch Puzzle Room)", 0x24, "Key", 1) },
            { "hera-basement-free-item", new RawItemLocation("hera-basement-free-item", 0x180162, "[dungeon-L3-1F] Tower of Hera - freestanding key", 0x0087, "Tower of Hera (Tile Room)", 0x24, "Key", 0) },
            { "hera-basement-tiles-chest", new RawItemLocation("hera-basement-tiles-chest", 0x00E9E6, "[dungeon-L3-1F] Tower of Hera - first floor", 0x0087, "Tower of Hera (Tile Room)", 0x32, "Big Key", 1) },
            { "hera-big-chest-chest1", new RawItemLocation("hera-big-chest-chest1", 0x00E9F8, "[dungeon-L3-4F] Tower of Hera - big chest", 0x0027, "Tower of Hera (Big Chest)", 0x1F, "Moon Pearl", 1) },
            { "hera-big-chest-chest2", new RawItemLocation("hera-big-chest-chest2", 0x00E9FB, "[dungeon-L3-4F] Tower of Hera - 4F [small chest]", 0x0027, "Tower of Hera (Big Chest)", 0x85, "<Hera Compass>", 2) },
            { "hera-entrance-chest", new RawItemLocation("hera-entrance-chest", 0x00E9AD, "[dungeon-L3-2F] Tower of Hera - Entrance", 0x0077, "Tower of Hera (Entrance Room)", 0x75, "<Hera Map>", 1) },
            { "hera-boss-pendant", new RawItemLocation("hera-boss-pendant", 0x00C706, "Tower of Hera Pendant6", 0x0007, "Tower of Hera (Moldorm[Boss])", 0x02, "<Pendant Blue>", 0) },
            { "hera-moldorm-chest", new RawItemLocation("hera-moldorm-chest", 0x180152, "Heart Container - Moldorm", 0x004D, "Tower of Hera (Moldorm[Boss])", 0x3E, "Heart Container", 0) },
            { "hyrule-basement-boomerang-chest", new RawItemLocation("hyrule-basement-boomerang-chest", 0x00E974, "[dungeon-C-B1] Hyrule Castle - boomerang room", 0x0071, "Hyrule Castle (Boomerang Chest Room)", 0x0C, "Boomerang", 1) },
            { "hyrule-basement-jail-chest", new RawItemLocation("hyrule-basement-jail-chest", 0x00EB09, "[dungeon-C-B3] Hyrule Castle - next to Zelda", 0x0080, "Hyrule Castle (Jail Cell Room)", 0x12, "Lamp", 1) },
            { "hyrule-basement-map-chest", new RawItemLocation("hyrule-basement-map-chest", 0x00EB0C, "[dungeon-C-B1] Hyrule Castle - map room", 0x0072, "Hyrule Castle (Map Chest Room)", 0x7F, "<Hyrule Map>", 1) },
            { "hyrule-escape-bombable-chest1", new RawItemLocation("hyrule-escape-bombable-chest1", 0x00EB5D, "[dungeon-C-B1] Escape - final basement room [left chest]", 0x0011, "Hyrule Castle (Bombable Stock Room)", 0x28, "3 Bombs", 1) },
            { "hyrule-escape-bombable-chest2", new RawItemLocation("hyrule-escape-bombable-chest2", 0x00EB60, "[dungeon-C-B1] Escape - final basement room [middle chest]", 0x0011, "Hyrule Castle (Bombable Stock Room)", 0x46, "300 Rupees", 2) },
            { "hyrule-escape-bombable-chest3", new RawItemLocation("hyrule-escape-bombable-chest3", 0x00EB63, "[dungeon-C-B1] Escape - final basement room [right chest]", 0x0011, "Hyrule Castle (Bombable Stock Room)", 0x44, "10 Arrows", 3) },
            { "hyrule-escape-cross-chest", new RawItemLocation("hyrule-escape-cross-chest", 0x00E96E, "[dungeon-C-B1] Escape - first B1 room", 0x0032, "Hyrule Castle (Sewer Key Chest Room)", 0x24, "Key", 1) },
            { "ice-big-chest-chest", new RawItemLocation("ice-big-chest-chest", 0x00E9AA, "[dungeon-D5-B5] Ice Palace - big chest", 0x009E, "Ice Palace (Big Chest Room)", 0x22, "Blue Mail", 1) },
            { "ice-big-key-chest-chest", new RawItemLocation("ice-big-key-chest-chest", 0x00E9A4, "[dungeon-D5-B1] Ice Palace - Big Key room", 0x001F, "Ice Palace (Pengator / Big Key Room)", 0x32, "Big Key", 1) },
            { "ice-iceman-chest-chest", new RawItemLocation("ice-iceman-chest-chest", 0x00E995, "[dungeon-D5-B4] Ice Palace - above Blue Mail room", 0x007E, "Ice Palace (Hidden Chest / Bombable Floor Room)", 0x28, "3 Bombs", 1) },
            { "ice-boss-chest", new RawItemLocation("ice-boss-chest", 0x180157, "Heart Container - Kholdstare", 0x00DE, "Ice Palace (Kholdstare[Boss])", 0x3E, "Heart Container", 0) },
            { "ice-boss-crystal", new RawItemLocation("ice-boss-crystal", 0x00C705, "Ice Palace Crystal6", 0x00DE, "Ice Palace (Kholdstare[Boss])", 0x06, "<Crystal 5>", 0) },
            { "ice-pengator-chest-chest", new RawItemLocation("ice-pengator-chest-chest", 0x00E9D4, "[dungeon-D5-B1] Ice Palace - compass room", 0x002E, "Ice Palace (Compass Room)", 0x86, "<Ice Compass>", 1) },
            { "ice-spike-chest-chest", new RawItemLocation("ice-spike-chest-chest", 0x00E9E0, "[dungeon-D5-B3] Ice Palace - spike room", 0x005F, "Ice Palace (Hidden Chest / Spike Floor Room)", 0x24, "Key", 1) },
            { "ice-two-bari-chest-chest", new RawItemLocation("ice-two-bari-chest-chest", 0x00E9E3, "[dungeon-D5-B5] Ice Palace - b5 up staircase", 0x00AE, "Ice Palace", 0x24, "Key", 1) },
            { "ice-two-tongue-chest", new RawItemLocation("ice-two-tongue-chest", 0x00E9DD, "[dungeon-D5-B2] Ice Palace - map room", 0x003F, "Ice Palace (Map Chest Room)", 0x76, "<Ice Map>", 1) },
            { "mire-big-chest-chest", new RawItemLocation("mire-big-chest-chest", 0x00EA67, "[dungeon-D6-B1] Misery Mire - big chest", 0x00C3, "Misery Mire (Big Chest Room)", 0x15, "Somaria", 1) },
            { "mire-big-key-chest-chest", new RawItemLocation("mire-big-key-chest-chest", 0x00EA6D, "[dungeon-D6-B1] Misery Mire - big key", 0x00D1, "Misery Mire (Conveyor Slug / Big Key Room)", 0x32, "Big Key", 1) },
            { "mire-block-chest-chest", new RawItemLocation("mire-block-chest-chest", 0x00EA64, "[dungeon-D6-B1] Misery Mire - compass", 0x00C1, "Misery Mire (Compass Chest / Tile Room)", 0x88, "<Mire Compass>", 1) },
            { "mire-bridge-chest-chest", new RawItemLocation("mire-bridge-chest-chest", 0x00EA61, "[dungeon-D6-B1] Misery Mire - end of bridge", 0x00A2, "Misery Mire (Bridge Key Chest Room)", 0x24, "Key", 1) },
            { "mire-hub-blocks-chest", new RawItemLocation("mire-hub-blocks-chest", 0x00EA5E, "[dungeon-D6-B1] Misery Mire - big hub room", 0x00C2, "Misery Mire (Big Hub Room)", 0x24, "Key", 1) },
            { "mire-small-chest-chest", new RawItemLocation("mire-small-chest-chest", 0x00EA6A, "[dungeon-D6-B1] Misery Mire - map room", 0x00C3, "Misery Mire (Big Chest Room)", 0x78, "<Mire Map>", 2) },
            { "mire-spike-chest-chest", new RawItemLocation("mire-spike-chest-chest", 0x00E9DA, "[dungeon-D6-B1] Misery Mire - spike room", 0x00B3, "Misery Mire (Spike Key Chest Room)", 0x24, "Key", 1) },
            { "mire-boss-crystal", new RawItemLocation("mire-boss-crystal", 0x00C703, "Misery Mire Crystal6", 0x0090, "Misery Mire (Vitreous[Boss])", 0x06, "<Crystal 6>", 0) },
            { "mire-boss-item", new RawItemLocation("mire-boss-item", 0x180158, "Heart Container - Vitreous", 0x0090, "Misery Mire (Vitreous[Boss])", 0x3E, "Heart Container", 0) },
            { "pod-basement-left-chest", new RawItemLocation("pod-basement-left-chest", 0x00EA5B, "[dungeon-D1-B1] Dark Palace - shooter room", 0x0009, "Palace of Darkness", 0x24, "Key", 1) },
            { "pod-big-chest-platform-chest", new RawItemLocation("pod-big-chest-platform-chest", 0x00EA40, "[dungeon-D1-1F] Dark Palace - big chest", 0x001A, "Palace of Darkness (Big Chest Room)", 0x09, "Hammer", 1) },
            { "pod-big-chest-spikes-chest", new RawItemLocation("pod-big-chest-spikes-chest", 0x00EA46, "[dungeon-D1-1F] Dark Palace - spike statue room", 0x001A, "Palace of Darkness (Big Chest Room)", 0x35, "Blue Rupee", 3) },
            { "pod-big-chest-turtles-chest", new RawItemLocation("pod-big-chest-turtles-chest", 0x00EA43, "[dungeon-D1-1F] Dark Palace - compass room", 0x001A, "Palace of Darkness (Big Chest Room)", 0x89, "<PoD Compass>", 2) },
            { "pod-big-hub-main-chest", new RawItemLocation("pod-big-hub-main-chest", 0x00EA3A, "[dungeon-D1-1F] Dark Palace - jump room [right chest]", 0x002A, "Palace of Darkness (Big Hub Room)", 0x24, "Key", 1) },
            { "pod-big-hub-small-platform-chest", new RawItemLocation("pod-big-hub-small-platform-chest", 0x00EA3D, "[dungeon-D1-1F] Dark Palace - jump room [left chest]", 0x002A, "Palace of Darkness (Big Hub Room)", 0x24, "Key", 2) },
            { "pod-bombable-big-key-chest", new RawItemLocation("pod-bombable-big-key-chest", 0x00EA37, "[dungeon-D1-1F] Dark Palace - big key room", 0x003A, "Palace of Darkness (Bombable Floor Room)", 0x32, "Big Key", 1) },
            { "pod-dark-maze-chest1", new RawItemLocation("pod-dark-maze-chest1", 0x00EA55, "[dungeon-D1-1F] Dark Palace - maze room [top chest]", 0x0019, "Palace of Darkness (Dark Maze)", 0x28, "3 Bombs", 1) },
            { "pod-dark-maze-chest2", new RawItemLocation("pod-dark-maze-chest2", 0x00EA58, "[dungeon-D1-1F] Dark Palace - maze room [bottom chest]", 0x0019, "Palace of Darkness (Dark Maze)", 0x24, "Key", 2) },
            { "pod-boss-crystal", new RawItemLocation("pod-boss-crystal", 0x00C702, "Palace of Darkness Crystal6", 0x005A, "Palace of Darkness (Helmasaur King[Boss])", 0x06, "<Crystal 1>", 0) },
            { "pod-boss-item", new RawItemLocation("pod-boss-item", 0x180153, "Heart Container - Helmasaur King", 0x005A, "Palace of Darkness (Helmasaur King[Boss])", 0x3E, "Heart Container", 0) },
            { "pod-hidden-switch-upper-chest", new RawItemLocation("pod-hidden-switch-upper-chest", 0x00EA52, "[dungeon-D1-1F] Dark Palace - statue push room", 0x002B, "Palace of Darkness (Map Chest / Fairy Room)", 0x79, "<PoD Map>", 1) },
            { "pod-rupee-room-outer-chest1", new RawItemLocation("pod-rupee-room-outer-chest1", 0x00EA4C, "[dungeon-D1-B1] Dark Palace - room leading to Helmasaur [left chest]", 0x006A, "Palace of Darkness (Rupee Room)", 0x43, "Single Arrow", 1) },
            { "pod-rupee-room-outer-chest2", new RawItemLocation("pod-rupee-room-outer-chest2", 0x00EA4F, "[dungeon-D1-B1] Dark Palace - room leading to Helmasaur [right chest]", 0x006A, "Palace of Darkness (Rupee Room)", 0x24, "Key", 2) },
            { "pod-stalfos-trap-chest", new RawItemLocation("pod-stalfos-trap-chest", 0x00EA49, "[dungeon-D1-B1] Dark Palace - turtle stalfos room", 0x000A, "Palace of Darkness (Stalfos Trap Room)", 0x24, "Key", 1) },
            { "sanctuary-chest", new RawItemLocation("sanctuary-chest", 0x00EA79, "[dungeon-C-1F] Sanctuary", 0x0012, "Sanctuary", 0x3F, "Heart Container", 1) },
            { "skull-big-chest-chest1", new RawItemLocation("skull-big-chest-chest1", 0x00E998, "[dungeon-D3-B1] Skull Woods - big chest", 0x0058, "Skull Woods (Big Chest Room)", 0x07, "Fire Rod", 1) },
            { "skull-big-chest-chest2", new RawItemLocation("skull-big-chest-chest2", 0x00E99B, "[dungeon-D3-B1] Skull Woods - east of Fire Rod room", 0x0058, "Skull Woods (Big Chest Room)", 0x77, "<Skull Map>", 2) },
            { "skull-boss-entrance-chest", new RawItemLocation("skull-boss-entrance-chest", 0x00E9FE, "[dungeon-D3-B1] Skull Woods - Entrance to part 2", 0x0059, "Skull Woods (Final Section Entrance Room)", 0x24, "Key", 1) },
            { "skull-compass-chest-chest", new RawItemLocation("skull-compass-chest-chest", 0x00E992, "[dungeon-D3-B1] Skull Woods - Compass room", 0x0067, "Skull Woods (Compass Chest Room)", 0x87, "<Skull Compass>", 1) },
            { "skull-gibdo-chest-chest", new RawItemLocation("skull-gibdo-chest-chest", 0x00E9A1, "[dungeon-D3-B1] Skull Woods - Gibdo/Stalfos room", 0x0057, "Skull Woods (Big Key Room)", 0x24, "Key", 2) },
            { "skull-boss-crystal", new RawItemLocation("skull-boss-crystal", 0x00C704, "Skull Woods Crystal6", 0x0029, "Skull Woods (Mothula[Boss])", 0x06, "<Crystal 3>", 0) },
            { "skull-boss-item", new RawItemLocation("skull-boss-item", 0x180155, "Heart Container - Mothula", 0x0029, "Skull Woods (Mothula[Boss])", 0x3E, "Heart Container", 0) },
            { "skull-statue-switch-chest", new RawItemLocation("skull-statue-switch-chest", 0x00E99E, "[dungeon-D3-B1] Skull Woods - Big Key room", 0x0057, "Skull Woods (Big Key Room)", 0x32, "Big Key", 1) },
            { "skull-wallmaster-chest-chest", new RawItemLocation("skull-wallmaster-chest-chest", 0x00E9C8, "[dungeon-D3-B1] Skull Woods - south of Fire Rod room", 0x0068, "Skull Woods (Key Chest / Trap Room)", 0x24, "Key", 1) },
            { "swamp-53-chest-chest", new RawItemLocation("swamp-53-chest-chest", 0x00EAA6, "[dungeon-D2-B1] Swamp Palace - big key room", 0x0035, "Swamp Palace (Big Key / BS Room)", 0x32, "Big Key", 1) },
            { "swamp-boss-crystal", new RawItemLocation("swamp-boss-crystal", 0x00C701, "Swamp Palace Crystal6", 0x0006, "Swamp Palace (Arrghus[Boss])", 0x06, "<Crystal 2>", 0) },
            { "swamp-boss-item", new RawItemLocation("swamp-boss-item", 0x180154, "Heart Container - Arrghus", 0x0006, "Swamp Palace (Arrghus[Boss])", 0x3E, "Heart Container", 0) },
            { "swamp-big-chest-chest", new RawItemLocation("swamp-big-chest-chest", 0x00E989, "[dungeon-D2-B1] Swamp Palace - big chest", 0x0036, "Swamp Palace (Big Chest Room)", 0x0A, "Hookshot", 1) },
            { "swamp-compass-chest-chest", new RawItemLocation("swamp-compass-chest-chest", 0x00EAA0, "[dungeon-D2-B1] Swamp Palace - south of hookshot room", 0x0046, "Swamp Palace (Compass Chest Room)", 0x8A, "<Swamp Compass>", 1) },
            { "swamp-entrance-chest", new RawItemLocation("swamp-entrance-chest", 0x00EA9D, "[dungeon-D2-1F] Swamp Palace - first room", 0x0028, "Swamp Palace (Entrance Room)", 0x24, "Key", 1) },
            { "swamp-map-chest-chest", new RawItemLocation("swamp-map-chest-chest", 0x00E986, "[dungeon-D2-B1] Swamp Palace - map room", 0x0037, "Swamp Palace (Map Chest / Water Fill Room)", 0x7A, "<Swamp Map>", 1) },
            { "swamp-push-block-upper-chest", new RawItemLocation("swamp-push-block-upper-chest", 0x00EAA3, "[dungeon-D2-B1] Swamp Palace - push 4 blocks room", 0x0034, "Swamp Palace (Push Block Puzzle / Pre-Big Key Room)", 0x36, "Red Rupee", 1) },
            { "swamp-water-drain-chest1", new RawItemLocation("swamp-water-drain-chest1", 0x00EAA9, "[dungeon-D2-B2] Swamp Palace - flooded room [left chest]", 0x0076, "Swamp Palace (Water Drain Room)", 0x36, "Red Rupee", 1) },
            { "swamp-water-drain-chest2", new RawItemLocation("swamp-water-drain-chest2", 0x00EAAC, "[dungeon-D2-B2] Swamp Palace - flooded room [right chest]", 0x0076, "Swamp Palace (Water Drain Room)", 0x36, "Red Rupee", 2) },
            { "swamp-waterfall-chest", new RawItemLocation("swamp-waterfall-chest", 0x00EAAF, "[dungeon-D2-B2] Swamp Palace - hidden waterfall door room", 0x0066, "Swamp Palace (Hidden Chest / Hidden Door Room)", 0x36, "Red Rupee", 1) },
            { "thieves-attic-east-chest", new RawItemLocation("thieves-attic-east-chest", 0x00EA0D, "[dungeon-D4-1F] Thieves' Town - Room above boss", 0x0065, "Thieves Town (East Attic Room)", 0x28, "3 Bombs", 1) },
            { "thieves-big-chest-chest", new RawItemLocation("thieves-big-chest-chest", 0x00EA10, "[dungeon-D4-B2] Thieves' Town - big chest", 0x0044, "Thieves Town (Big Chest Room)", 0x1C, "Progressive Gloves", 1) },
            { "thieves-boss-crystal", new RawItemLocation("thieves-boss-crystal", 0x00C707, "Thieves Town Crystal6", 0x00AC, "Thieves Town (Blind The Thief[Boss])", 0x06, "<Crystal 4>", 0) },
            { "thieves-boss-item", new RawItemLocation("thieves-boss-item", 0x180156, "Heart Container - Blind", 0x00AC, "Thieves Town (Blind The Thief[Boss])", 0x3E, "Heart Container", 0) },
            { "thieves-entrance-chest-chest", new RawItemLocation("thieves-entrance-chest-chest", 0x00EA01, "[dungeon-D4-B1] Thieves' Town - Bottom left of huge room [top left chest]", 0x00DB, "Thieves Town (Main (South West) Entrance Room)", 0x74, "<Thieves Map>", 1) },
            { "thieves-entrance-big-key-chest", new RawItemLocation("thieves-entrance-big-key-chest", 0x00EA04, "[dungeon-D4-B1] Thieves' Town - Bottom left of huge room [bottom right chest]", 0x00DB, "Thieves Town (Main (South West) Entrance Room)", 0x32, "Big Key", 2) },
            { "thieves-jail-chest", new RawItemLocation("thieves-jail-chest", 0x00EA13, "[dungeon-D4-B2] Thieves' Town - next to Blind", 0x0045, "Thieves Town (Jail Cells Room)", 0x24, "Key", 1) },
            { "thieves-northwest-entrance-chest", new RawItemLocation("thieves-northwest-entrance-chest", 0x00EA0A, "[dungeon-D4-B1] Thieves' Town - Top left of huge room", 0x00CB, "Thieves Town (North West Entrance Room)", 0x36, "Red Rupee", 1) },
            { "thieves-southeast-entrance-chest", new RawItemLocation("thieves-southeast-entrance-chest", 0x00EA07, "[dungeon-D4-B1] Thieves' Town - Bottom right of huge room", 0x00DC, "Thieves Town (South East Entrance Room)", 0x84, "<Thieves Compass>", 1) },
            { "turtle-2-chest-roller-chest1", new RawItemLocation("turtle-2-chest-roller-chest1", 0x00EA1C, "[dungeon-D7-1F] Turtle Rock - Map room [left chest]", 0x00B7, "Turtle Rock (Map Chest / Key Chest / Roller Room)", 0x73, "<Turtle Map>", 1) },
            { "turtle-2-chest-roller-chest2", new RawItemLocation("turtle-2-chest-roller-chest2", 0x00EA1F, "[dungeon-D7-1F] Turtle Rock - Map room [right chest]", 0x00B7, "Turtle Rock (Map Chest / Key Chest / Roller Room)", 0x24, "Key", 2) },
            { "turtle-big-chest-chest", new RawItemLocation("turtle-big-chest-chest", 0x00EA19, "[dungeon-D7-B1] Turtle Rock - big chest", 0x0024, "Turtle Rock (Double Hokku-Bokku / Big chest Room)", 0x06, "Progressive Shield", 1) },
            { "turtle-big-key-chest-tube-chest", new RawItemLocation("turtle-big-key-chest-tube-chest", 0x00EA25, "[dungeon-D7-B1] Turtle Rock - big key room", 0x0014, "Turtle Rock (Big Key Room)", 0x32, "Big Key", 1) },
            { "turtle-chain-chomp-chest", new RawItemLocation("turtle-chain-chomp-chest", 0x00EA16, "[dungeon-D7-1F] Turtle Rock - Chain chomp room", 0x00B6, "Turtle Rock (Chain Chomps Room)", 0x24, "Key", 1) },
            { "turtle-lazer-chests-chest1", new RawItemLocation("turtle-lazer-chests-chest1", 0x00EA28, "[dungeon-D7-B2] Turtle Rock - Eye bridge room [top right chest]", 0x00D5, "Turtle Rock (Laser Key Room)", 0x34, "Green Rupee", 1) },
            { "turtle-lazer-chests-chest2", new RawItemLocation("turtle-lazer-chests-chest2", 0x00EA2B, "[dungeon-D7-B2] Turtle Rock - Eye bridge room [top left chest]", 0x00D5, "Turtle Rock (Laser Key Room)", 0x35, "Blue Rupee", 2) },
            { "turtle-lazer-chests-chest3", new RawItemLocation("turtle-lazer-chests-chest3", 0x00EA2E, "[dungeon-D7-B2] Turtle Rock - Eye bridge room [bottom right chest]", 0x00D5, "Turtle Rock (Laser Key Room)", 0x36, "Red Rupee", 3) },
            { "turtle-lazer-chests-chest4", new RawItemLocation("turtle-lazer-chests-chest4", 0x00EA31, "[dungeon-D7-B2] Turtle Rock - Eye bridge room [bottom left chest]", 0x00D5, "Turtle Rock (Laser Key Room)", 0x24, "Key", 4) },
            { "turtle-roller-switch-chest", new RawItemLocation("turtle-roller-switch-chest", 0x00EA34, "[dungeon-D7-B1] Turtle Rock - Roller switch room", 0x0004, "Turtle Rock (Crysta-roller Room)", 0x24, "Key", 1) },
            { "turtle-spike-chest-chest", new RawItemLocation("turtle-spike-chest-chest", 0x00EA22, "[dungeon-D7-1F] Turtle Rock - compass room", 0x00D6, "Turtle Rock (Entrance Room)", 0x83, "<Turtle Compass>", 1) },
            { "turtle-boss-crystal", new RawItemLocation("turtle-boss-crystal", 0x00C708, "Turtle Rock Crystal6", 0x00A4, "Turtle Rock (Trinexx[Boss])", 0x06, "<Crystal 7>", 0) },
            { "turtle-boss-item", new RawItemLocation("turtle-boss-item", 0x180159, "Heart Container - Trinexx", 0x00A4, "Turtle Rock (Trinexx[Boss])", 0x3E, "Heart Container", 0) },
            { "ow-turtle-rock-medallion", new RawItemLocation("ow-turtle-rock-medallion", 0x180023, "Turtle Rock Medallion", 0x8047, "Turtle Rock", 0x02, "<Turtle Rock Token>", 0) },
            { "ow-mire-medallion", new RawItemLocation("ow-mire-medallion", 0x180022, "Misery Mire Medallion", 0x8070, "Misery Mire (DW)", 0x01, "<Misery Mire Token>", 0) },
            { "ow-frog-smith", new RawItemLocation("ow-frog-smith", 0, "Frog Smith", 0x8069, "Village of Outcasts Frog Smith (DW)", 0x8001, "<Smith>", 0) },
            { "ow-purple-chest", new RawItemLocation("ow-purple-chest", 0, "Purple Chest", 0x8062, "Smithy (DW)", 0x8002, "<Purple Chest>", 0) },
        };

        public Dictionary<string, ItemLocation> Nodes { get; }

        private void FillItemNodes()
        {
            foreach(var raw in rawItemLocations)
            {
                if(GameItems.Items.ContainsKey(raw.Value.ItemName))
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GameItems.Items[raw.Value.ItemName]));
                }
                else if(raw.Value.ItemName.ToLower() == "key")
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GetDungeonKey(raw.Value)));
                }
                else if (raw.Value.ItemName.ToLower() == "big key")
                {
                    Nodes.Add(raw.Key, new ItemLocation(raw.Value.LogicalId, raw.Value.LocationName, GetDungeonBigKey(raw.Value)));
                }
                else
                {
                    throw new Exception($"FillItemNodes - Unknown item name found '{raw.Value.ItemName}'");
                }
            }
        }

        static Item GetDungeonKey(RawItemLocation raw)
        {
            if(raw.ItemName.ToLower() != "key")
            {
                throw new Exception("GetDungeonKey - Not a key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonKeys[dungeon];
            return GameItems.Items[dungeonKey];
        }

        static Item GetDungeonBigKey(RawItemLocation raw)
        {
            if (raw.ItemName.ToLower() != "big key")
            {
                throw new Exception("GetDungeonBigKey - Not a big key!");
            }
            var dungeon = Dungeons.GetDungeonFromRoom(raw.RoomId);
            var dungeonKey = Dungeons.DungeonBigKeys[dungeon];
            return GameItems.Items[dungeonKey];
        }
    }
}
