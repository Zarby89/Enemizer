using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class RoomEdges
    {
        RoomNodes _roomNodes;
        OverworldNodes _overworldNodes;
        BossNodes _bossNodes;
        public RoomEdges(RoomNodes roomNodes, OverworldNodes overworldNodes, BossNodes bossNodes)
        {
            this._roomNodes = roomNodes;
            this._overworldNodes = overworldNodes;
            this._bossNodes = bossNodes;
            this.Edges = new List<Edge>();
            FillRoomEdges();
        }

        class RawRoomEdge
        {
            public string srcId;
            public string destId;
            public bool isTwoWay;
            public string requirements;
            public string defaultItem;
            public string alternateItem;

            public RawRoomEdge(string srcId, string destId, bool isTwoWay, string requirements, string defaultItem, string alternateItem)
            {
                this.srcId = srcId;
                this.destId = destId;
                this.isTwoWay = isTwoWay;
                this.requirements = requirements;
                this.defaultItem = defaultItem;
                this.alternateItem = alternateItem;
            }
        }

        List<RawRoomEdge> rawRoomEdges = new List<RawRoomEdge>()
        {
            { new RawRoomEdge("agahnim-agahnim", "[Agahnim Boss]", false, "L2 Sword;Bug Catching Net;Hammer", "[Agahnim Boss]", "") },
            { new RawRoomEdge("agahnim-agahnim", "{5B-pyramid}", false, "[Agahnim Boss]", "", "") },
            { new RawRoomEdge("agahnim-agahnim", "<Agahnim 1>", false, "[Agahnim Boss]", "<Agahnim 1>", "") },
            { new RawRoomEdge("agahnim-dark-bridge", "<key>", false, "", "<Aga Key>", "") },
            { new RawRoomEdge("agahnim-dark-bridge", "agahnim-pots", true, "<Aga Key>", "", "") },
            { new RawRoomEdge("agahnim-dark-maze", "agahnim-dark-bridge", true, "Lamp,<Aga Key>", "", "") },
            { new RawRoomEdge("agahnim-entrance", "agahnim-dark-maze", true, "Lamp,<Aga Key>", "", "") },
            { new RawRoomEdge("agahnim-final-bridge", "agahnim-maiden-chamber", true, "", "", "") },
            { new RawRoomEdge("agahnim-maiden-chamber", "agahnim-agahnim", true, "L1 Sword;Hammer", "", "") },
            { new RawRoomEdge("agahnim-pots", "<key>", false, "", "<Aga Key>", "") },
            { new RawRoomEdge("agahnim-pots", "agahnim-final-bridge", true, "<Aga Key>", "", "") },
            { new RawRoomEdge("cave-angry-brothers-entrance", "cave-angry-brothers-exit", true, "", "", "") },
            { new RawRoomEdge("cave-big-bomb-shop", "<Big Bomb>", false, "<Crystal 5>,<Crystal 6>", "<Big Bomb>", "") },
            { new RawRoomEdge("cave-blinds-house", "cave-blinds-basement", true, "", "", "") },
            { new RawRoomEdge("cave-bumper-top", "cave-bumper-bottom", true, "", "", "") },
            { new RawRoomEdge("cave-dm-entrance", "cave-dm-entrance-exit", false, "", "", "") },
            { new RawRoomEdge("cave-dm-exit-entrance", "cave-dm-exit", true, "", "", "") },
            { new RawRoomEdge("cave-east-dm-rocks-top", "cave-east-dm-rocks-bottom", true, "", "", "") },
            { new RawRoomEdge("cave-hookshot-entrance", "cave-hookshot-backdoor", true, "", "", "") },
            { new RawRoomEdge("cave-north-fairy-drop", "cave-north-fairy", false, "", "", "") },
            { new RawRoomEdge("cave-old-lady-left", "cave-old-lady-right", true, "", "", "") },
            { new RawRoomEdge("cave-old-man-house-entrance", "cave-old-man-house-back-exit", true, "", "", "") },
            { new RawRoomEdge("cave-spectical-rock-entrance-ledge", "cave-spectical-rock-upper-back", false, "", "", "") },
            { new RawRoomEdge("cave-spectical-rock-upper-back", "cave-spectical-rock-exit", false, "", "", "") },
            { new RawRoomEdge("cave-spectical-rock-upper-entrance", "cave-spectical-rock-exit", false, "", "", "") },
            { new RawRoomEdge("cave-spiral-cave-entrance", "cave-spiral-cave-exit", false, "", "", "") },
            { new RawRoomEdge("cave-super-bunny-entrance", "cave-super-bunny-exit", false, "", "", "") },
            { new RawRoomEdge("cave-super-bunny-exit", "cave-super-bunny-chests", true, "", "", "") },
            { new RawRoomEdge("cave-thief-hut-drop", "cave-thief-hut", false, "", "", "") },
            { new RawRoomEdge("cave-upside-down-2-chest", "cave-upside-down-shop", false, "", "", "") },
            { new RawRoomEdge("cave-upside-down-5-chest", "cave-upside-down-2-chest", true, "", "", "") },
            { new RawRoomEdge("cave-upside-down-logical-middle", "cave-upside-down-2-chest", false, "", "", "") },
            { new RawRoomEdge("cave-upside-down-logical-middle", "cave-upside-down-top", true, "", "", "") },
            { new RawRoomEdge("desert-beamos", "<key>", false, "", "<Desert Key>", "") },
            { new RawRoomEdge("desert-beamos", "desert-tile-trap", true, "<Desert Key>", "", "") },
            { new RawRoomEdge("desert-boss-entrance", "<key>", false, "", "<Desert Key>", "") },
            { new RawRoomEdge("desert-boss-entrance", "desert-beamos", true, "<Desert Key>", "", "") },
            { new RawRoomEdge("desert-east-entrance-chest", "desert-big-key", true, "", "", "") },
            { new RawRoomEdge("desert-east-entrance-hall", "desert-east-entrance", true, "", "", "") },
            { new RawRoomEdge("desert-east-entrance-hall", "desert-east-entrance-chest", true, "<Desert Key>", "", "") },
            { new RawRoomEdge("desert-lanmolas", "[Desert Boss]", false, "", "[Desert Boss]", "") },
            { new RawRoomEdge("desert-lanmolas", "{30-desert-ledge-boss-entrance}", false, "[Desert Boss]", "", "") },
            { new RawRoomEdge("desert-main-entrance", "desert-map-chest", true, "", "", "") },
            { new RawRoomEdge("desert-map-chest", "desert-big-chest", true, "", "", "") },
            { new RawRoomEdge("desert-map-chest", "desert-east-entrance-hall", true, "", "", "") },
            { new RawRoomEdge("desert-map-chest", "desert-torch-key", true, "", "", "") },
            { new RawRoomEdge("desert-map-chest", "desert-west-entrance", true, "", "", "") },
            { new RawRoomEdge("desert-tile-trap", "<key>", false, "", "<Desert Key>", "") },
            { new RawRoomEdge("desert-tile-trap", "desert-torch-puzzle", true, "<Desert Key>", "", "") },
            { new RawRoomEdge("desert-torch-puzzle", "desert-lanmolas", true, "Lamp;Fire Rod", "", "") },
            { new RawRoomEdge("eastern-armos", "[Eastern Boss]", false, "", "[Eastern Boss]", "") },
            { new RawRoomEdge("eastern-armos", "{1E-eastern-palace}", false, "[Eastern Boss]", "", "") },
            { new RawRoomEdge("eastern-big-chest-lower", "eastern-eyegore-key", true, "<Eastern Big Key>,Lamp", "", "") },
            { new RawRoomEdge("eastern-big-chest-lower", "eastern-fairy", true, "", "", "") },
            { new RawRoomEdge("eastern-big-chest-lower", "eastern-map-lower", true, "", "", "") },
            { new RawRoomEdge("eastern-big-chest-upper", "eastern-map-upper", true, "", "", "") },
            { new RawRoomEdge("eastern-big-chest-upper", "eastern-stalfos-upper", true, "", "", "") },
            { new RawRoomEdge("eastern-big-key", "eastern-stalfos-lower", false, "<Eastern Big Key>", "", "") },
            { new RawRoomEdge("eastern-cannon", "eastern-prearmos", true, "", "", "") },
            { new RawRoomEdge("eastern-dark-key", "<key>", false, "", "<Eastern Key>", "") },
            { new RawRoomEdge("eastern-dark-key", "eastern-lobby-cannon-bridge", true, "", "", "") },
            { new RawRoomEdge("eastern-entrance", "eastern-lobby-cannon", true, "", "", "") },
            { new RawRoomEdge("eastern-eyegore-key", "<key>", false, "", "<Eastern Key>", "") },
            { new RawRoomEdge("eastern-eyegore-key", "eastern-two-bubble", true, "<Eastern Key>", "", "") },
            { new RawRoomEdge("eastern-lobby-cannon", "eastern-big-chest-upper", true, "", "", "") },
            { new RawRoomEdge("eastern-lobby-cannon-bridge", "eastern-big-key", true, "", "", "") },
            { new RawRoomEdge("eastern-map-lower", "eastern-dark-key", true, "Lamp", "", "") },
            { new RawRoomEdge("eastern-prearmos", "eastern-armos", false, "Bow", "", "") },
            { new RawRoomEdge("eastern-stalfos-lower", "eastern-big-chest-lower", true, "", "", "") },
            { new RawRoomEdge("eastern-stalfos-upper", "eastern-stalfos-lower", true, "", "", "") },
            { new RawRoomEdge("eastern-two-bubble", "eastern-cannon", true, "", "", "") },
            { new RawRoomEdge("ganon-fight", "[Ganon]", false, "", "[Ganon]", "") },
            { new RawRoomEdge("ganon-fight", "triforce-room", false, "[Ganon]", "", "") },
            { new RawRoomEdge("ganon-fight", "ganon-fall", false, "", "", "") },
            { new RawRoomEdge("gt-agahnim", "[Agahnim 2 Boss]", false, "", "[Agahnim 2 Boss]", "") },
            { new RawRoomEdge("gt-agahnim", "{5B-pyramid}", false, "[Agahnim 2 Boss]", "", "") },
            { new RawRoomEdge("gt-agahnim", "<Agahnim 2>", false, "[Agahnim 2 Boss]", "<Agahnim 2>", "") },
            { new RawRoomEdge("gt-armos", "[GT Armos Boss]", false, "", "[GT Armos Boss]", "") },
            { new RawRoomEdge("gt-armos", "gt-armos-chests", true, "[GT Armos Boss]", "", "") },
            { new RawRoomEdge("gt-armos", "gt-armos-exit", false, "[GT Armos Boss]", "", "") },
            { new RawRoomEdge("gt-armos-exit", "gt-big-chest", false, "", "", "") },
            { new RawRoomEdge("gt-bomb-trap", "gt-moldorm", false, "<GT Key>", "", "") },
            { new RawRoomEdge("gt-conveyor-key", "<key>", false, "", "<GT Key>", "") },
            { new RawRoomEdge("gt-conveyor-key", "gt-right-collapse", true, "<GT Key>", "", "") },
            { new RawRoomEdge("gt-entrance", "gt-left", true, "", "", "") },
            { new RawRoomEdge("gt-entrance", "gt-mimic-puzzle", true, "Bow", "", "") },
            { new RawRoomEdge("gt-entrance", "gt-right", true, "", "", "") },
            { new RawRoomEdge("gt-falling-bridge", "gt-torch-cross", true, "", "", "") },
            { new RawRoomEdge("gt-final-gauntlet", "gt-pre-agahnim", true, "", "", "") },
            { new RawRoomEdge("gt-fire-snakes", "gt-tele-hub", false, "", "", "") },
            { new RawRoomEdge("gt-floating-platform", "gt-useless-hidden-path", false, "", "", "") },
            { new RawRoomEdge("gt-gauntlet-entrance", "gt-gauntlet-ice", true, "", "", "") },
            { new RawRoomEdge("gt-gauntlet-ice", "gt-lanmolas", false, "", "", "") },
            { new RawRoomEdge("gt-gibdo-puzzle", "gt-right-four", true, "", "", "") },
            { new RawRoomEdge("gt-hammer-time", "<key>", false, "", "<GT Key>", "") },
            { new RawRoomEdge("gt-hammer-time", "gt-hookshot", true, "Hammer", "", "") },
            { new RawRoomEdge("gt-hidden-maze", "gt-big-chest", true, "", "", "") },
            { new RawRoomEdge("gt-hidden-maze", "gt-pre-armos", true, "", "", "") },
            { new RawRoomEdge("gt-hookshot", "gt-4-chest-left", true, "Hookshot", "", "") },
            { new RawRoomEdge("gt-hookshot", "gt-star-chest", true, "<GT Key>,Hookshot", "", "") },
            { new RawRoomEdge("gt-hookshot", "gt-switch-puzzle", true, "Hookshot", "", "") },
            { new RawRoomEdge("gt-lanmolas", "[GT Lanmolas Boss]", false, "", "[GT Lanmolas Boss]", "") },
            { new RawRoomEdge("gt-lanmolas", "gt-wizzrobes", false, "[GT Lanmolas Boss]", "", "") },
            { new RawRoomEdge("gt-left", "gt-hammer-time", true, "", "", "") },
            { new RawRoomEdge("gt-mimic-puzzle", "gt-spike-gauntlet", true, "<GT Big Key>", "", "") },
            { new RawRoomEdge("gt-mini-helma", "gt-bomb-trap", true, "<GT Key>", "", "") },
            { new RawRoomEdge("gt-mini-helma", "<key>", false, "", "<GT Key>", "") },
            { new RawRoomEdge("gt-moldorm", "[GT Moldorm Boss]", false, "", "[GT Moldorm Boss]", "") },
            { new RawRoomEdge("gt-moldorm", "gt-final-gauntlet", false, "[GT Moldorm Boss]", "", "") },
            { new RawRoomEdge("gt-moldorm", "gt-moldorm-fall", true, "", "", "") },
            { new RawRoomEdge("gt-pre-agahnim", "gt-agahnim", false, "<GT Big Key>", "", "") },
            { new RawRoomEdge("gt-pre-armos", "gt-armos", false, "", "", "") },
            { new RawRoomEdge("gt-right", "gt-tile-trap", true, "Somaria", "", "") },
            { new RawRoomEdge("gt-right-collapse", "gt-floating-platform", true, "", "", "") },
            { new RawRoomEdge("gt-right-four", "gt-conveyor-key", false, "", "", "") },
            { new RawRoomEdge("gt-shooter-bridge", "gt-gauntlet-entrance", true, "", "", "") },
            { new RawRoomEdge("gt-spike-gauntlet", "gt-shooter-bridge", true, "", "", "") },
            { new RawRoomEdge("gt-spike-warp", "gt-fire-snakes", false, "", "", "") },
            { new RawRoomEdge("gt-switch-puzzle", "<key>", false, "", "<GT Key>", "") },
            { new RawRoomEdge("gt-switch-puzzle", "gt-spike-warp", true, "<GT Key>", "", "") },
            { new RawRoomEdge("gt-tele-dest", "gt-hidden-maze", false, "", "", "") },
            { new RawRoomEdge("gt-tele-hub", "gt-hidden-chests", true, "", "", "") },
            { new RawRoomEdge("gt-tele-hub", "gt-tele-dest", false, "", "", "") },
            { new RawRoomEdge("gt-tile-trap", "gt-torch-puzzle", true, "<GT Key>", "", "") },
            { new RawRoomEdge("gt-torch-cross", "gt-torch-square", true, "", "", "") },
            { new RawRoomEdge("gt-torch-puzzle", "gt-gibdo-puzzle", true, "Fire Rod", "", "") },
            { new RawRoomEdge("gt-torch-square", "gt-mini-helma", true, "", "", "") },
            { new RawRoomEdge("gt-useless-hidden-path", "gt-hidden-maze", false, "", "", "") },
            { new RawRoomEdge("gt-wizzrobes", "gt-falling-bridge", true, "", "", "") },
            { new RawRoomEdge("hera-big-chest", "hera-boss-fall", true, "", "", "") },
            { new RawRoomEdge("hera-big-key-door", "hera-big-chest", true, "", "", "") },
            { new RawRoomEdge("hera-boss-fall", "hera-fairy", true, "", "", "") },
            { new RawRoomEdge("hera-boss-fall", "hera-moldorm", true, "", "", "") },
            { new RawRoomEdge("hera-entrance", "hera-basement-free", true, "", "", "") },
            { new RawRoomEdge("hera-entrance", "hera-basement-tiles", true, "<Hera Key>", "", "") },
            { new RawRoomEdge("hera-entrance", "hera-big-key-door", true, "<Hera Big Key>", "", "") },
            { new RawRoomEdge("hera-moldorm", "[Hera Boss]", false, "", "[Hera Boss]", "") },
            { new RawRoomEdge("hera-moldorm", "{03-west-death-mountain-upper}", false, "[Hera Boss]", "", "") },
            { new RawRoomEdge("hyrule-basement-boomerang", "<key>", false, "", "<Hyrule Key>", "") },
            { new RawRoomEdge("hyrule-basement-boomerang", "hyrule-basement-stairs-to-jail", true, "<Hyrule Key>", "", "") },
            { new RawRoomEdge("hyrule-basement-chasm", "hyrule-basement-west-chasm", true, "", "", "") },
            { new RawRoomEdge("hyrule-basement-jail", "<Hyrule Big Key>", false, "", "<Hyrule Big Key>", "") },
            { new RawRoomEdge("hyrule-basement-map", "<key>", false, "", "<Hyrule Key>", "") },
            { new RawRoomEdge("hyrule-basement-map", "hyrule-basement-chasm", true, "<Hyrule Key>", "", "") },
            { new RawRoomEdge("hyrule-basement-stairs-to-jail", "hyrule-basement-jail", true, "", "", "") },
            { new RawRoomEdge("hyrule-basement-west-chasm", "hyrule-basement-boomerang", true, "", "", "") },
            { new RawRoomEdge("hyrule-east-corridor", "hyrule-hallway-to-basement", true, "", "", "") },
            { new RawRoomEdge("hyrule-east-entrance", "hyrule-east-corridor", true, "", "", "") },
            { new RawRoomEdge("hyrule-entrance", "hyrule-east-entrance", true, "", "", "") },
            { new RawRoomEdge("hyrule-entrance", "hyrule-throne-room", true, "", "", "") },
            { new RawRoomEdge("hyrule-entrance", "hyrule-west-entrance", true, "", "", "") },
            { new RawRoomEdge("hyrule-esacpe-sewer-text", "hyrule-escape-key-rat", true, "", "", "") },
            { new RawRoomEdge("hyrule-escape-behind-throne", "hyrule-escape-ropes", true, "", "", "") },
            { new RawRoomEdge("hyrule-escape-bombable", "hyrule-escape-key-rat", true, "<Hyrule Key>,Lamp", "", "") },
            { new RawRoomEdge("hyrule-escape-bombable", "hyrule-escape-switches", true, "", "", "") },
            { new RawRoomEdge("hyrule-escape-cross", "hyrule-esacpe-sewer-text", true, "<Hyrule Key>", "", "") },
            { new RawRoomEdge("hyrule-escape-key-rat", "<key>", false, "", "<Hyrule Key>", "") },
            //{ new RawRoomEdge("hyrule-escape-key-rat", "hyrule-escape-bombable", false, "<Hyrule Key>", "", "") },
            { new RawRoomEdge("hyrule-escape-ropes", "hyrule-escape-cross", true, "", "", "") },
            { new RawRoomEdge("hyrule-escape-switches", "sanctuary", false, "", "", "") },
            { new RawRoomEdge("hyrule-hallway-to-basement", "hyrule-basement-map", true, "", "", "") },
            { new RawRoomEdge("hyrule-throne-room", "hyrule-escape-behind-throne", false, "Lamp", "", "") },
            { new RawRoomEdge("hyrule-west-corridor", "hyrule-hallway-to-basement", true, "", "", "") },
            { new RawRoomEdge("hyrule-west-entrance", "hyrule-west-corridor", true, "", "", "") },
            { new RawRoomEdge("ice-big-chest", "ice-right-blocks", false, "", "", "") },
            { new RawRoomEdge("ice-big-key-chest", "ice-pengator-switch", false, "", "", "") },
            { new RawRoomEdge("ice-big-spike", "ice-iceman-chest", true, "", "", "") },
            { new RawRoomEdge("ice-big-spike", "ice-long-ice-floor", false, "", "", "") },
            { new RawRoomEdge("ice-big-spike", "ice-spike-chest", true, "<Ice Key>", "", "") },
            { new RawRoomEdge("ice-block-switch", "ice-hole-to-boss", false, "<Ice Block>;Somaria", "", "") },
            { new RawRoomEdge("ice-block-switch", "ice-switch-room", true, "<Ice Key>", "", "") },
            { new RawRoomEdge("ice-bomb-jump", "ice-penguator-row", true, "", "", "") },
            { new RawRoomEdge("ice-bridge", "ice-icefloor-pot-key", true, "", "", "") },
            { new RawRoomEdge("ice-bridge", "ice-two-bari-chest", true, "", "", "") },
            { new RawRoomEdge("ice-entrance", "ice-entrance-key", true, "Fire Rod;Bombos", "", "") },
            { new RawRoomEdge("ice-entrance-key", "<key>", false, "", "<Ice Key>", "") },
            { new RawRoomEdge("ice-entrance-key", "ice-four-way-block", true, "<Ice Key>", "", "") },
            { new RawRoomEdge("ice-four-way-block", "ice-pengator-chest", true, "", "", "") },
            { new RawRoomEdge("ice-four-way-block", "ice-pengator-switch", true, "", "", "") },
            { new RawRoomEdge("ice-four-way-block", "ice-stalfos-knight", false, "", "", "") },
            { new RawRoomEdge("ice-hole-to-boss", "ice-kholdstare", false, "Hammer", "", "") },
            { new RawRoomEdge("ice-icefloor-pot-key", "<key>", false, "", "<Ice Key>", "") },
            { new RawRoomEdge("ice-icefloor-pot-key", "ice-right-blocks", true, "", "", "") },
            { new RawRoomEdge("ice-iceman-chest", "ice-big-chest", false, "", "", "") },
            { new RawRoomEdge("ice-kholdstare", "[Ice Boss]", false, "Fire Rod;Bombos", "[Ice Boss]", "") },
            { new RawRoomEdge("ice-kholdstare", "{75-dw-lake-hylia-ice-palace}", false, "[Ice Boss]", "", "") },
            { new RawRoomEdge("ice-left-blocks", "ice-right-blocks", true, "", "", "") },
            { new RawRoomEdge("ice-long-ice-floor", "ice-left-blocks", false, "", "", "") },
            { new RawRoomEdge("ice-long-ice-floor", "ice-one-way-platform", false, "", "", "") },
            { new RawRoomEdge("ice-one-way-platform", "ice-one-way-hookshot", true, "Hookshot", "", "") },
            { new RawRoomEdge("ice-penguator-row", "ice-big-spike", true, "", "", "") },
            { new RawRoomEdge("ice-right-blocks", "ice-block-switch", false, "<Ice Big Key>,<Ice Key>", "", "") },
            { new RawRoomEdge("ice-spike-chest", "ice-one-way-hookshot", true, "", "", "") },
            { new RawRoomEdge("ice-spike-chest", "ice-two-tongue", true, "", "", "") },
            { new RawRoomEdge("ice-stalfos-knight", "<key>", false, "", "<Ice Key>", "") },
            { new RawRoomEdge("ice-stalfos-knight", "ice-bomb-jump", true, "<Ice Key>", "", "") },
            { new RawRoomEdge("ice-switch-room", "<Ice Block>", false, "", "<Ice Block>", "") },
            { new RawRoomEdge("ice-tetris", "ice-long-ice-floor", true, "<Ice Key>", "", "") },
            { new RawRoomEdge("ice-two-tongue", "<key>", false, "", "<Ice Key>", "") },
            { new RawRoomEdge("ice-two-tongue", "ice-big-key-chest", true, "", "", "") },
            { new RawRoomEdge("mire-big-door-shortcut", "mire-boss-key-door", false, "", "", "") },
            { new RawRoomEdge("mire-big-key-chest", "mire-hourglass", false, "", "", "") },
            { new RawRoomEdge("mire-block-chest", "mire-medusa-connection", false, "", "", "") },
            { new RawRoomEdge("mire-boss-key-door", "mire-bridge-lower", true, "<Mire Big Key>", "", "") },
            { new RawRoomEdge("mire-bridge-lower", "mire-cane-room", true, "", "", "") },
            { new RawRoomEdge("mire-bridge-upper", "mire-empty-connection", true, "", "", "") },
            { new RawRoomEdge("mire-cane-room", "mire-cane-room-rupees", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-cane-room", "mire-dark-switch", false, "Somaria", "", "") },
            { new RawRoomEdge("mire-corner-flames", "mire-block-chest", true, "Fire Rod,<Mire Switch>;Lamp,<Mire Switch>", "", "") },
            { new RawRoomEdge("mire-corner-flames", "mire-slug-to-upstairs", true, "", "", "") },
            { new RawRoomEdge("mire-dark-switch", "mire-cane-room", false, "", "", "") },
            { new RawRoomEdge("mire-dark-switch", "mire-final-switch", true, "", "", "") },
            { new RawRoomEdge("mire-empty-connection", "mire-spike-mesh", true, "", "", "") },
            { new RawRoomEdge("mire-entrance", "mire-wizzrobe-beamos", true, "Boots;Hookshot", "", "") },
            { new RawRoomEdge("mire-final-switch", "mire-pre-boss", true, "", "", "") },
            { new RawRoomEdge("mire-fish-lower", "mire-fish-upper", true, "<Mire Switch>", "", "") },
            { new RawRoomEdge("mire-fish-upper", "<key>", false, "", "<Mire Key>", "") },
            { new RawRoomEdge("mire-fish-upper", "<Mire Switch>", false, "", "<Mire Switch>", "") },
            { new RawRoomEdge("mire-fish-upper", "mire-bridge-upper", true, "", "", "") },
            { new RawRoomEdge("mire-hourglass", "mire-big-door-shortcut", true, "<Mire Big Key>", "", "") },
            { new RawRoomEdge("mire-hourglass", "mire-fish-lower", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-hourglass", "mire-medusa-connection", true, "", "", "") },
            { new RawRoomEdge("mire-hub", "mire-big-chest", true, "", "", "") },
            { new RawRoomEdge("mire-hub", "mire-hub-blocks", true, "<Mire Switch>", "", "") },
            { new RawRoomEdge("mire-hub", "mire-slug-room", true, "", "", "") },
            { new RawRoomEdge("mire-hub", "mire-small-chest", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-hub", "mire-switch", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-medusa-connection", "mire-hub", true, "", "", "") },
            { new RawRoomEdge("mire-pre-boss", "mire-vitreous", false, "", "", "") },
            { new RawRoomEdge("mire-slug-room", "mire-bridge-chest", true, "", "", "") },
            { new RawRoomEdge("mire-slug-room", "mire-spike-chest", true, "", "", "") },
            { new RawRoomEdge("mire-slug-to-upstairs", "mire-torch-puzzle", true, "", "", "") },
            { new RawRoomEdge("mire-spike-chest", "<key>", false, "", "<Mire Key>", "") },
            { new RawRoomEdge("mire-spike-chest", "mire-small-chest", true, "<Mire Switch>", "", "") },
            { new RawRoomEdge("mire-spike-chest", "mire-spike-mesh", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-spike-mesh", "mire-boss-key-door", true, "", "", "") },
            { new RawRoomEdge("mire-switch", "<key>", false, "", "<Mire Key>", "") },
            { new RawRoomEdge("mire-switch", "<Mire Switch>", false, "", "<Mire Switch>", "") },
            { new RawRoomEdge("mire-switch", "mire-corner-flames", true, "<Mire Key>", "", "") },
            { new RawRoomEdge("mire-torch-puzzle", "mire-big-key-chest", false, "Fire Rod;Lamp", "", "") },
            { new RawRoomEdge("mire-vitreous", "[Mire Boss]", false, "", "[Mire Boss]", "") },
            { new RawRoomEdge("mire-vitreous", "{70-mire}", false, "[Mire Boss]", "", "") },
            { new RawRoomEdge("mire-wizzrobe-beamos", "mire-hub", true, "", "", "") },
            { new RawRoomEdge("pod-basement-right", "pod-warp-room", true, "", "", "") },
            { new RawRoomEdge("pod-big-chest-path", "pod-big-chest-turtles", true, "<PoD Key>", "", "") },
            { new RawRoomEdge("pod-big-chest-path", "pod-dark-maze", true, "<PoD Key>,Lamp", "", "") },
            { new RawRoomEdge("pod-big-chest-spikes", "pod-big-hub-main", false, "", "", "") },
            { new RawRoomEdge("pod-big-chest-turtles", "pod-big-chest-spikes", true, "<PoD Key>", "", "") },
            { new RawRoomEdge("pod-big-chest-turtles", "pod-rupee-room-outer", true, "Lamp", "", "") },
            { new RawRoomEdge("pod-big-hub-main", "pod-big-chest-path", true, "<PoD Key>", "", "") },
            { new RawRoomEdge("pod-big-hub-main", "pod-hidden-switch-lower", true, "", "", "") },
            { new RawRoomEdge("pod-bombable-outer", "pod-big-hub-main", true, "", "", "") },
            { new RawRoomEdge("pod-bombable-outer", "pod-stalfos-ledge", false, "", "", "") },
            { new RawRoomEdge("pod-conveyor-hall", "pod-hidden-switch-upper", true, "", "", "") },
            { new RawRoomEdge("pod-dark-maze", "pod-big-chest-platform", true, "", "", "") },
            { new RawRoomEdge("pod-entrance", "pod-entrance-center-right", true, "", "", "") },
            { new RawRoomEdge("pod-entrance", "pod-entrance-left", true, "", "", "") },
            { new RawRoomEdge("pod-entrance-center-right", "pod-basement-right", true, "", "", "") },
            { new RawRoomEdge("pod-entrance-center-right", "pod-bombable-outer", true, "<PoD Key>", "", "") },
            { new RawRoomEdge("pod-entrance-left", "pod-basement-left", true, "", "", "") },
            { new RawRoomEdge("pod-helmasaur", "[PoD Boss]", false, "", "[PoD Boss]", "") },
            { new RawRoomEdge("pod-helmasaur", "{5E-palace-of-darkness}", false, "[PoD Boss]", "", "") },
            { new RawRoomEdge("pod-hidden-switch-lower", "pod-moving-wall-mimics", true, "Bow", "", "") },
            { new RawRoomEdge("pod-hidden-switch-upper", "pod-big-hub-small-platform", true, "", "", "") },
            { new RawRoomEdge("pod-hidden-switch-upper", "pod-hidden-switch-lower", false, "Hammer", "", "") },
            { new RawRoomEdge("pod-moving-wall-mimics", "pod-moving-wall-statue", true, "Bow", "", "") },
            { new RawRoomEdge("pod-moving-wall-statue", "pod-turtles", true, "Hammer,Lamp", "", "") },
            { new RawRoomEdge("pod-rupee-room-boss-door", "pod-helmasaur", false, "<PoD Big Key>", "", "") },
            { new RawRoomEdge("pod-stalfos-ledge", "pod-bombable-big-key", true, "<PoD Key>", "", "") },
            { new RawRoomEdge("pod-stalfos-ledge", "pod-stalfos-trap", false, "", "", "") },
            { new RawRoomEdge("pod-stalfos-trap", "pod-warp-room", false, "", "", "") },
            { new RawRoomEdge("pod-turtles", "pod-rupee-room-boss-door", true, "", "", "") },
            { new RawRoomEdge("pod-warp-room", "pod-warp-room-mimics", true, "", "", "") },
            { new RawRoomEdge("pod-warp-room-mimics", "pod-conveyor-hall", true, "Bow", "", "") },
            { new RawRoomEdge("skull-boss-entrance", "skull-torch-puzzle", true, "<Skull Key>", "", "") },
            { new RawRoomEdge("skull-boss-hole", "skull-mothula", false, "<Skull Key>", "", "") },
            { new RawRoomEdge("skull-compass-chest", "skull-gibdo-chest", true, "", "", "") },
            { new RawRoomEdge("skull-compass-chest", "skull-wallmaster-chest", false, "", "", "") },
            { new RawRoomEdge("skull-gibdo-chest", "skull-big-chest", true, "<Skull Key>", "", "") },
            { new RawRoomEdge("skull-mothula", "[Skull Boss]", false, "", "[Skull Boss]", "") },
            { new RawRoomEdge("skull-mothula", "{40-skull-woods}", false, "[Skull Boss]", "", "") },
            { new RawRoomEdge("skull-pot-key-exit", "<key>", false, "", "<Skull Key>", "") },
            { new RawRoomEdge("skull-statue-switch", "skull-pot-key-exit", true, "", "", "") },
            { new RawRoomEdge("skull-torch-puzzle", "<key>", false, "", "<Skull Key>", "") },
            { new RawRoomEdge("skull-torch-puzzle", "skull-boss-hole", true, "Fire Rod,L1 Sword", "", "") },
            { new RawRoomEdge("skull-wallmaster-chest", "skull-big-chest", true, "<Skull Key>", "", "") },
            { new RawRoomEdge("swamp-53-flooded", "swamp-push-block-lower", true, "", "", "") },
            { new RawRoomEdge("swamp-53-key-pot", "<key>", false, "", "<Swamp Key>", "") },
            { new RawRoomEdge("swamp-53-switch", "swamp-53-flooded", true, "", "", "") },
            { new RawRoomEdge("swamp-arrghus", "[Swamp Boss]", false, "Hookshot", "[Swamp Boss]", "") },
            { new RawRoomEdge("swamp-arrghus", "{7B-dw-southwest-swamp}", false, "[Swamp Boss]", "", "") },
            { new RawRoomEdge("swamp-big-chest", "<key>", false, "Hookshot", "<Swamp Key>", "") },
            { new RawRoomEdge("swamp-big-chest", "swamp-53-key-pot", true, "", "", "") },
            { new RawRoomEdge("swamp-big-chest", "swamp-53-switch", true, "<Swamp Key>", "", "") },
            { new RawRoomEdge("swamp-big-chest", "swamp-compass-chest", true, "", "", "") },
            { new RawRoomEdge("swamp-big-chest", "swamp-statue", true, "<Swamp Key>,Hookshot", "", "") },
            { new RawRoomEdge("swamp-entrance", "swamp-key-pot", true, "<Swamp Key>", "", "") },
            { new RawRoomEdge("swamp-key-pot", "<key>", false, "", "<Swamp Key>", "") },
            { new RawRoomEdge("swamp-key-pot", "swamp-map-chest", true, "", "", "") },
            { new RawRoomEdge("swamp-key-pot", "swamp-map-chest-lower", true, "<Swamp Key>", "", "") },
            { new RawRoomEdge("swamp-map-chest-flooded", "swamp-big-chest", true, "", "", "") },
            { new RawRoomEdge("swamp-map-chest-lower", "<key>", false, "", "<Swamp Key>", "") },
            { new RawRoomEdge("swamp-map-chest-lower", "swamp-map-chest-switch", true, "<Swamp Key>", "", "") },
            { new RawRoomEdge("swamp-map-chest-switch", "swamp-big-chest-bombwall", true, "", "", "") },
            { new RawRoomEdge("swamp-map-chest-switch", "swamp-map-chest-flooded", true, "", "", "") },
            { new RawRoomEdge("swamp-push-block-lower", "swamp-upstairs-fall", true, "", "", "") },
            { new RawRoomEdge("swamp-push-block-upper", "swamp-53-chest", true, "", "", "") },
            { new RawRoomEdge("swamp-push-block-upper", "swamp-push-block-lower", false, "", "", "") },
            { new RawRoomEdge("swamp-statue", "swamp-water-drain", true, "", "", "") },
            { new RawRoomEdge("swamp-swimming", "<key>", false, "", "<Swamp Key>", "") },
            { new RawRoomEdge("swamp-swimming", "swamp-arrghus", false, "<Swamp Key>", "", "") },
            { new RawRoomEdge("swamp-upstairs-fall", "swamp-push-block-upper", false, "", "", "") },
            { new RawRoomEdge("swamp-water-drain", "swamp-waterfall", true, "", "", "") },
            { new RawRoomEdge("swamp-waterfall", "swamp-swimming", true, "", "", "") },
            { new RawRoomEdge("thieves-attic-west", "thieves-attic-east", true, "", "", "") },
            { new RawRoomEdge("thieves-basement", "thieves-jail", true, "<Thieves Big Key>", "", "") },
            { new RawRoomEdge("thieves-blind", "[Thieves Boss]", false, "", "[Thieves Boss]", "") },
            { new RawRoomEdge("thieves-blind", "{58-outcast-village}", false, "[Thieves Boss]", "", "") },
            { new RawRoomEdge("thieves-conveyor-toilet", "thieves-basement", true, "", "", "") },
            { new RawRoomEdge("thieves-entrance", "thieves-northwest-entrance", true, "", "", "") },
            { new RawRoomEdge("thieves-hellway", "thieves-conveyor-toilet", true, "", "", "") },
            { new RawRoomEdge("thieves-hellway", "thieves-switch-room", true, "", "", "") },
            { new RawRoomEdge("thieves-jail", "thieves-big-chest", true, "<Thieves Key>", "", "") },
            { new RawRoomEdge("thieves-northeast-entrance", "thieves-southeast-entrance", true, "", "", "") },
            { new RawRoomEdge("thieves-northeast-entrance", "thieves-stalfos-hall", true, "", "", "") },
            { new RawRoomEdge("thieves-northwest-entrance", "thieves-northeast-entrance", true, "", "", "") },
            { new RawRoomEdge("thieves-southeast-entrance", "thieves-entrance-chest", true, "", "", "") },
            { new RawRoomEdge("thieves-stalfos-hall", "<key>", false, "", "<Thieves Key>", "") },
            { new RawRoomEdge("thieves-stalfos-hall", "thieves-zazak-room", true, "<Thieves Key>", "", "") },
            { new RawRoomEdge("thieves-stalfos-hall", "thieves-blind", false, "", "", "") },
            { new RawRoomEdge("thieves-switch-room", "<key>", false, "", "<Thieves Key>", "") },
            { new RawRoomEdge("thieves-switch-room", "thieves-attic-west", true, "<Thieves Key>", "", "") },
            { new RawRoomEdge("thieves-zazak-room", "thieves-hellway", true, "", "", "") },
            { new RawRoomEdge("turtle-antifairy-key", "<key>", false, "", "<Turtle Key>", "") },
            { new RawRoomEdge("turtle-antifairy-key", "turtle-big-key-chest-tube", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-antifairy-tube", "turtle-antifairy-key", true, "", "", "") },
            { new RawRoomEdge("turtle-back-entrance-tube", "turtle-roller-switch", true, "", "", "") },
            { new RawRoomEdge("turtle-back-exit-tube", "turtle-two-pokey", true, "", "", "") },
            { new RawRoomEdge("turtle-big-chest", "turtle-two-pokey", false, "", "", "") },
            { new RawRoomEdge("turtle-big-key-chest-tube", "turtle-useless-tubes", false, "", "", "") },
            { new RawRoomEdge("turtle-chain-chomp", "turtle-useless-tubes", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-dark-maze", "turtle-lazer-bridge", true, "Somaria,Lamp", "", "") },
            { new RawRoomEdge("turtle-entrance", "turtle-entrance-hub", true, "Somaria", "", "") },
            { new RawRoomEdge("turtle-entrance-hub", "turtle-pokey-key", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-entrance-hub", "turtle-spike-chest", true, "Somaria", "", "") },
            { new RawRoomEdge("turtle-entrance-hub", "turtle-torch-puzzle", true, "Somaria", "", "") },
            { new RawRoomEdge("turtle-lazer-bridge", "turtle-lazer-chests", true, "", "", "") },
            { new RawRoomEdge("turtle-lazer-bridge", "turtle-switch-maze", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-pokey-key", "<key>", false, "", "<Turtle Key>", "") },
            { new RawRoomEdge("turtle-pokey-key", "turtle-chain-chomp", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-pre-boss", "turtle-trinexx", false, "<Turtle Big Key>", "", "") },
            { new RawRoomEdge("turtle-roller-switch", "turtle-dark-maze", true, "<Turtle Key>", "", "") },
            { new RawRoomEdge("turtle-switch-maze", "turtle-pre-boss", true, "", "", "") },
            { new RawRoomEdge("turtle-torch-puzzle", "turtle-2-chest-roller", true, "Somaria,Fire Rod", "", "") },
            { new RawRoomEdge("turtle-trinexx", "[Turtle Boss]", false, "Fire Rod,Ice Rod", "[Turtle Boss]", "") },
            { new RawRoomEdge("turtle-trinexx", "{47-turtle-rock}", false, "[Turtle Boss]", "", "") },
            { new RawRoomEdge("turtle-two-pokey", "turtle-back-entrance-tube", true, "<Turtle Big Key>", "", "") },
            { new RawRoomEdge("turtle-two-pokey", "turtle-lazer-exit", true, "", "", "") },
            { new RawRoomEdge("turtle-useless-tubes", "turtle-antifairy-tube", true, "", "", "") },
            { new RawRoomEdge("turtle-useless-tubes", "turtle-back-exit-tube", true, "", "", "") },
        };

        public List<Edge> Edges { get; private set; }

        void FillRoomEdges()
        {
            foreach (var e in rawRoomEdges)
            {
                RoomNode src = _roomNodes.Nodes[e.srcId];
                // TODO: clean this up
                Node dest = null;
                RoomNode destTemp;
                if (!_roomNodes.Nodes.TryGetValue(e.destId, out destTemp))
                {
                    if (e.destId.StartsWith("{", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith("}", StringComparison.OrdinalIgnoreCase))
                    {
                        string d = e.destId.Split('{', '}').ToArray()[1];
                        OverworldAreaNode temp;
                        if (!_overworldNodes.Nodes.TryGetValue(d, out temp))
                        {
                            throw new Exception("Fill Room Edges - Invalid destination area");
                        }
                        dest = temp;
                    }
                    else if(e.destId == "<key>")
                    {
                        // keys
                        var dungeon = Dungeons.GetDungeonFromRoom(src.RoomId);
                        var dungeonKey = Dungeons.DungeonKeys[dungeon];
                        Item item = null;
                        if(!GameItems.Items.TryGetValue(dungeonKey, out item))
                        {
                            throw new Exception($"FillRoomEdges - Invalid dungeon item <key> {dungeonKey}");
                        }
                        dest = new ItemLocation(e.srcId + e.destId, e.srcId + e.destId, item);
                    }
                    else if (e.destId.StartsWith("<", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith(">", StringComparison.OrdinalIgnoreCase))
                    {
                        Item item = null;
                        if (!GameItems.Items.TryGetValue(e.destId, out item))
                        {
                            throw new Exception($"FillRoomEdges - Invalid special item {e.destId}");
                        }
                        dest = new ItemLocation(e.srcId + e.destId, e.srcId + e.destId, item);
                    }
                    else if(e.destId.StartsWith("[", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith("]", StringComparison.OrdinalIgnoreCase))
                    {
                        // Bosses
                        LogicalBoss temp;
                        if(!_bossNodes.Nodes.TryGetValue(e.destId, out temp))
                        {
                            throw new Exception("Fill Room Edges - Invalid boss");
                        }
                        dest = temp;
                    }
                    else
                    {
                        throw new Exception($"Fill Room Edges - Unknown destination type {e.destId}");
                    }
                }
                else
                {
                    dest = destTemp;
                }

                Edges.AddRange(Edge.MakeEdges(src, dest, e.requirements, e.isTwoWay));
            }
        }
    }
}
