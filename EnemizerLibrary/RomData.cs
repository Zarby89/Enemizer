using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RomData
    {
        // 0x100 bytes to use for rom info
        public int EnemizerInfoTableBaseAddress = XkasSymbols.Instance.Symbols["enemizer_info_table"];

        public const int EnemizerInfoSeedOffset = 0x0;
        public const int EnemizerInfoSeedStringLength = 12;

        public const int EnemizerInfoVersionOffset = EnemizerInfoSeedOffset + EnemizerInfoSeedStringLength;
        public const int EnemizerInfoVersionLength = 8;

        // reserve 0x50 bytes for flags/options
        public const int EnemizerInfoFlagsOffset = EnemizerInfoVersionOffset + EnemizerInfoVersionLength;

        public const int EnemizerInfoFlagsLength = 0x50;

        // 0x20 flags total
        public int EnemizerOptionFlagsBaseAddress = XkasSymbols.Instance.Symbols["EnemizerFlags"];
        public const int RandomizeHiddenEnemiesFlag = 0x00;
        public const int CloseBlindDoorFlag = 0x01;
        public const int MoldormEyesFlag = 0x02;
        public const int RandomSpriteFlag = 0x03;
        public const int ChecksumComplimentAddress = 0x7FDC;
        public const int ChecksumAddress = 0x7FDE;

        public StringBuilder Spoiler { get; private set; } = new StringBuilder();

        internal Dictionary<int, byte> patchData = new Dictionary<int, byte>();

        public List<PatchObject> GeneratePatch()
        {
            var patches = new List<PatchObject>();

            PatchObject currentPatch = null;
            int lastAddress = 0;
            foreach(var pd in patchData.OrderBy(x => x.Key))
            {
                if(lastAddress + 1 != pd.Key)
                {
                    // add previous patch
                    //if (currentPatch != null)
                    //{
                    //    patches.Add(currentPatch);
                    //}

                    // new patch
                    currentPatch = new PatchObject();
                    currentPatch.address = pd.Key;
                    patches.Add(currentPatch);
                }
                // add the patch byte
                currentPatch.patchData.Add(pd.Value);

                // update our address tracker
                lastAddress = pd.Key;
            }

            return patches;
        }

        void SetPatchBytes(int offset, int length)
        {
            for(int i=offset; i<offset+length; i++)
            {
                patchData[i] = romData[i];
            }
        }

        public bool IsEnemizerRom
        {
            get
            {
                return romData.Length == AddressConstants.EnemizerFileLength
                    && romData[EnemizerInfoTableBaseAddress + EnemizerInfoSeedOffset] == 'E' 
                    && romData[EnemizerInfoTableBaseAddress + EnemizerInfoSeedOffset + 1] == 'N';
            }
        }

        int seed = -1;
        public int EnemizerSeed
        {
            get
            {
                if (seed < 0 && IsEnemizerRom)
                {
                    var seedBytes = new byte[EnemizerInfoSeedStringLength];
                    Array.Copy(romData, EnemizerInfoTableBaseAddress + EnemizerInfoSeedOffset, seedBytes, 0, EnemizerInfoSeedStringLength);
                    var seedString = System.Text.Encoding.ASCII.GetString(seedBytes).TrimEnd('\0').Substring(2);
                    seed = Convert.ToInt32(seedString);
                }
                return seed;
            }
            set
            {
                if(romData.Length < AddressConstants.EnemizerFileLength)
                {
                    throw new Exception("You need to expand the rom before you can use Enemizer features.");
                }

                // write to rom somewhere too
                var seedString = ASCIIEncoding.ASCII.GetBytes($"EN{value.ToString()}");
                Array.Resize(ref seedString, EnemizerInfoSeedStringLength); // make sure it's long enough
                Array.Copy(seedString, 0, romData, EnemizerInfoTableBaseAddress + EnemizerInfoSeedOffset, EnemizerInfoSeedStringLength);
                seed = value;

                SetPatchBytes(EnemizerInfoTableBaseAddress + EnemizerInfoSeedOffset, EnemizerInfoSeedStringLength);
            }
        }

        public string EnemizerVersion
        {
            get
            {
                if(!IsEnemizerRom)
                {
                    return "Not Enemizer Rom";
                }

                var versionBytes = new byte[EnemizerInfoVersionLength];
                Array.Copy(this.romData, EnemizerInfoTableBaseAddress + EnemizerInfoVersionOffset, versionBytes, 0, EnemizerInfoVersionLength);
                return System.Text.Encoding.ASCII.GetString(versionBytes).TrimEnd('\0');
            }
            set
            {
                if (romData.Length < AddressConstants.EnemizerFileLength)
                {
                    throw new Exception("You need to expand the rom before you can use Enemizer features.");
                }

                // write to rom somewhere too
                var versionString = ASCIIEncoding.ASCII.GetBytes(value);
                Array.Resize(ref versionString, EnemizerInfoVersionLength); // make sure it's long enough
                Array.Copy(versionString, 0, romData, EnemizerInfoTableBaseAddress + EnemizerInfoVersionOffset, EnemizerInfoVersionLength);

                SetPatchBytes(EnemizerInfoTableBaseAddress + EnemizerInfoVersionOffset, EnemizerInfoVersionLength);
            }
        }

        public void SetRomInfoOptionFlags(OptionFlags optionFlags)
        {
            var optionByteArray = optionFlags.ToByteArray();
            if(optionByteArray.Length > 0x100 - EnemizerInfoFlagsOffset)
            {
                throw new Exception("Option flags is too long to fit in the space allocated. Need to move data/code in asm file.");
            }
            Array.Copy(optionByteArray, 0, romData, EnemizerInfoTableBaseAddress + EnemizerInfoFlagsOffset, optionByteArray.Length);
            SetPatchBytes(EnemizerInfoTableBaseAddress + EnemizerInfoFlagsOffset, optionByteArray.Length);
        }

        public OptionFlags GetOptionFlagsFromRom()
        {
            if(!IsEnemizerRom)
            {
                return null;
            }

            byte[] optionByteArray = new byte[EnemizerInfoFlagsLength];
            Array.Copy(romData, EnemizerInfoTableBaseAddress + EnemizerInfoFlagsOffset, optionByteArray, 0, EnemizerInfoFlagsLength);
            return new OptionFlags(optionByteArray);
        }

        /// <summary>
        /// Try to avoid using this because we can't set break points to find bad writes to ROM.
        /// </summary>
        internal byte[] romData;

        public RomData(byte[] romData)
        {
            this.romData = romData;
            this.OriginalLength = romData.Length;
        }

        /// <summary>
        /// Flag to enable/disable custom enemies in grass/bushes
        /// </summary>
        public bool RandomizeHiddenEnemies
        {
            get { return GetFlag(RandomizeHiddenEnemiesFlag); }
            set
            {
                SetFlag(RandomizeHiddenEnemiesFlag, value);
                // we probably don't need this but just to be safe
                if(value == false)
                {
                    FillVanillaHiddenEnemyChancePool();
                }
            }
        }

        /// <summary>
        /// Flag to enable/disable Blind's boss fight room door auto-closing upon entering
        /// </summary>
        public bool CloseBlindDoor
        {
            get { return GetFlag(CloseBlindDoorFlag); }
            set { SetFlag(CloseBlindDoorFlag, value); }
        }

        public bool MoldormEyes
        {
            get { return GetFlag(MoldormEyesFlag); }
            set { SetFlag(MoldormEyesFlag, value); }
        }

        public bool RandomizeSprites
        {
            get { return GetFlag(RandomSpriteFlag); }
            set { SetFlag(RandomSpriteFlag, value); }
        }

        internal bool GetFlag(int offset)
        {
            return romData[EnemizerOptionFlagsBaseAddress + offset] == 0x01;
        }
        internal void SetFlag(int offset, bool val)
        {
            romData[EnemizerOptionFlagsBaseAddress + offset] = (byte)(val ? 0x01 : 0x00);

            SetPatchBytes(EnemizerOptionFlagsBaseAddress + offset, 1);
        }

        public void FillVanillaHiddenEnemyChancePool()
        {
            /*
             * 01 01 01 01 0F 01 01 12 
             * 10 01 01 01 11 01 01 03 
             */
            byte[] vanilla = { 0x01, 0x01, 0x01, 0x01, 0x0F, 0x01, 0x01, 0x12, 0x10, 0x01, 0x01, 0x01, 0x11, 0x01, 0x01, 0x03 };
            Array.Copy(vanilla, 0, romData, AddressConstants.HiddenEnemyChancePoolBaseAddress, 16);

            SetPatchBytes(AddressConstants.HiddenEnemyChancePoolBaseAddress, 16);
        }

        public void RandomizeHiddenEnemyChancePool()
        {
            // table is filled with Item Ids.
            // 0x0F is used to randomly spawn an enemy
            /*
             * 0x0D7BBB
            org $1AFBBB ;Increases chance of getting enemies under random bush
            db $01, $0F, $0F, $0F, $0F, $0F, $0F, $12 
            db $0F, $01, $0F, $0F, $11, $0F, $0F, $03
            */
            int i = AddressConstants.HiddenEnemyChancePoolBaseAddress;
            romData[i++] = 0x01;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x12;
            romData[i++] = 0x0F;
            romData[i++] = 0x01;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x11;
            romData[i++] = 0x0F;
            romData[i++] = 0x0F;
            romData[i++] = 0x03;

            SetPatchBytes(AddressConstants.HiddenEnemyChancePoolBaseAddress, 16);

        }

        public void SetCharacterSelectScreenVersion()
        {
            byte versionMajor = Version.MajorVersion;
            byte versionMinor = Version.MinorVersion;

            byte buildFirst = Version.BuildNumber / 10;
            byte buildSecond = Version.BuildNumber % 10;

            var text = new byte[]
            {
                // top
                0x63, 0x25, 0x00, 0x19,
                0x64, 0x1D, // K (top)
                0x62, 0x1D, // I (top)
                0x65, 0x1D, // L (top)
                0x65, 0x1D, // L (top)
                0x88, 0x01, // space
                0x4E, 0x15, // E (top)
                0x67, 0x15, // N (top)
                (byte)(0x40 | versionMajor), 0x15, // number (top)
                //0x88, 0x01, // space
                0x88, 0x01, // space
                (byte)(0x40 | versionMinor), 0x15, // number (top)
                0x88, 0x01, // space
                (byte)(0x40 | buildFirst), 0x15, // number (top)
                (byte)(0x40 | buildSecond), 0x15, // number (top)
                // bottom
                0x63, 0x45, 0x00, 0x19,
                0x74, 0x1D, // K (bottom)
                0x72, 0x1D, // I (bottom)
                0x75, 0x1D, // L (bottom)
                0x75, 0x1D, // L (bottom)
                0x88, 0x01, // space
                0x5E, 0x15, // E (bottom)
                0x77, 0x15, // N (bottom)
                (byte)(0x50 | versionMajor), 0x15, // number (bottom)
                0x9D, 0x15, // . (bottom)
                (byte)(0x50 | versionMinor), 0x15, // number (bottom)
                0x9D, 0x15, // . (bottom)
                (byte)(0x50 | buildFirst), 0x15, // number (top)
                (byte)(0x50 | buildSecond), 0x15, // number (top)
            };

            Array.Copy(text, 0, romData, 0x65E94, text.Length);

            SetPatchBytes(0x65E94, text.Length);
        }

        public bool IsRandomizerRom
        {
            get
            {
                // item randomizer
                if (romData[0x7FC0] == 0x56 && romData[0x7FC1] == 0x54)
                {
                    return true;
                }
                // entrance randomizer
                if (romData[0x7FC0] == 0x45 && romData[0x7FC1] == 0x52)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsItemRandomizerRom
        {
            get
            {
                // item randomizer
                if (romData[0x7FC0] == 0x56 && romData[0x7FC1] == 0x54)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsEntranceRandomizerRom
        {
            get
            {
                // entrance randomizer
                if (romData[0x7FC0] == 0x45 && romData[0x7FC1] == 0x52)
                {
                    return true;
                }

                return false;
            }
        }

        /*
    public function setTournamentType(string $setting) : self {
        switch ($setting) {
            case 'standard':
                $bytes = [0x01, 0x00];
                break;
            case 'none':
            default:
                $bytes = [0x00, 0x01];
        }

        $this->write(0x180213, pack('C*', ...$bytes));

        return $this;
    }
         */
        public bool IsRaceRom
        {
            get
            {
                if (romData[0x180213] == 0x01 && romData[0x180214] == 0x00)
                {
                    return true;
                }

                if (romData[0x7FC0] == 0x56
                    && romData[0x7FC1] == 0x54
                    && romData[0x7FC2] == 0x20
                    && romData[0x7FC3] == 0x54
                    && romData[0x7FC4] == 0x4F
                    && romData[0x7FC5] == 0x55
                    && romData[0x7FC6] == 0x52
                    && romData[0x7FC7] == 0x4E
                    && romData[0x7FC8] == 0x45
                    && romData[0x7FC9] == 0x59
                    )
                {
                    return true;
                }

                return false;
            }
        }

        public string Seed
        {
            get
            {
                /*
            public function setSeedString(string $seed) : self {
                $this->write(0x7FC0, substr($seed, 0, 21));

                return $this;
            }
                 */
                byte[] seed = new byte[21];
                Array.Copy(romData, 0x7FC0, seed, 0, 21);
                return System.Text.Encoding.ASCII.GetString(seed).TrimEnd('\0');
            }
        }

        public void ExpandRom()
        {
            Array.Resize(ref this.romData, 0x400000); // 4MB
            this.romData[0x7FD7] = 0x0C; // update header length
            SetPatchBytes(0x7FD7, 1);

            this.EnemizerVersion = EnemizerLibrary.Version.CurrentVersion;
        }

        /*
    public function writeRandomizerLogicHash(array $bytes) : self {
        $this->write(0x187F00, pack('C*', ...$bytes));

        return $this;
    }
         */


        /*
     //* set the flags byte in ROM
     //*
     //* dgGe mutT
     //* d - Nonstandard Dungeon Configuration (Not Map/Compass/BigKey/SmallKeys in same quantity as vanilla)
     //* g - Requires Minor Glitches (Fake flippers, bomb jumps, etc)
     //* G - Requires Major Glitches (OW YBA/Clips, etc)
     //* e - Requires EG
     //*
     //* m - Contains Multiples of Major Items
     //* u - Contains Unreachable Items
     //* t - Minor Trolling (Swapped around levers, etc)
     //* T - Major Trolling (Forced-guess softlocks, impossible seed, etc)
     //*
     //* @param int $flags byte of flags to set
    public function setWarningFlags(int $flags) : self {
        $this->write(0x180212, pack('C*', $flags));

        return $this;
    }
         */


        public int Length
        {
            get
            {
                return romData.Length;
            }
        }

        public int OriginalLength { get; set; }

        public byte this[int i]
        {
            get
            {
                return romData[i];
            }
            set
            {
                if(i >= 0x0 && i <= 0x0)
                {
                    Debugger.Break();
                }
                romData[i] = value;
                SetPatchBytes(i, 1);
            }
        }

        public byte[] GetDataChunk(int startingAddress, int length)
        {
            var output = new byte[length];
            Array.Copy(this.romData, startingAddress, output, 0, length);
            return output;
        }

        public void WriteDataChunk(int startingAddress, byte[] data, int length = -1)
        {
            if(length < 0)
            {
                length = data.Length;
            }
            Array.Copy(data, 0, this.romData, startingAddress, length);
            SetPatchBytes(startingAddress, length);
        }

        public void WriteRom(Stream fs)
        {
            UpdateChecksum();

            fs.Write(this.romData, 0, this.romData.Length);
        }

        public void PatchData(int address, byte[] patchData)
        {
            Array.Copy(patchData, 0, romData, address, patchData.Length);
            //SetPatchBytes(address, patchData.Length); // need to move this outside so it can be done client-side on web
        }

        public void PatchData(PatchObject patch)
        {
            var patchDataArray = patch.patchData.ToArray();
            Array.Copy(patchDataArray, 0, romData, patch.address, patchDataArray.Length);
            //SetPatchBytes(patch.address, patchDataArray.Length); // need to move this outside so it can be done client-side on web
        }

        public void UpdateChecksum()
        {
            int checksum = 0;

            // remove old checksum
            romData[ChecksumComplimentAddress] = 0xFF; // compliment
            romData[ChecksumComplimentAddress+1] = 0xFF; // compliment
            romData[ChecksumAddress] = 0x00; // checksum
            romData[ChecksumAddress+1] = 0x00; // checksum

            foreach(byte b in romData)
            {
                checksum += b;
            }

            checksum &= 0xFFFF;
            romData[ChecksumAddress] = (byte)(checksum & 0xFF);
            romData[ChecksumAddress + 1] = (byte)((checksum >> 8) & 0xFF);

            int compliment = checksum ^ 0xFFFF; // compliment
            romData[ChecksumComplimentAddress] = (byte)(compliment & 0xFF);
            romData[ChecksumComplimentAddress+1] = (byte)((compliment >> 8) & 0xFF);

            SetPatchBytes(ChecksumComplimentAddress, 4);
        }

        public HeartBeepSpeed HeartBeep
        {
            get
            {
                switch(this.romData[0x180033])
                {
                    case 0x00:
                        return HeartBeepSpeed.Off;
                    case 0x40:
                        return HeartBeepSpeed.Half;
                    case 0x80:
                        return HeartBeepSpeed.Quarter;
                    case 0x20:
                    default:
                        return HeartBeepSpeed.Default;
                }
            }
            set
            {
                byte beepSpeed = 0x20;

                switch(value)
                {
                    case HeartBeepSpeed.Off:
                        beepSpeed = 0x00;
                        break;
                    case HeartBeepSpeed.Half:
                        beepSpeed = 0x40;
                        break;
                    case HeartBeepSpeed.Quarter:
                        beepSpeed = 0x80;
                        break;
                    case HeartBeepSpeed.Default:
                    default:
                        beepSpeed = 0x20;
                        break;
                }
                romData[0x180033] = beepSpeed;
                SetPatchBytes(0x180033, 1);
            }
        }

        public void ReadFileStreamIntoRom(FileStream f, int address, int length)
        {
            f.Read(romData, address, length);
            SetPatchBytes(address, length);
        }

        public int ReadStreamIntoRom(Stream f, int address)
        {
            int b = 0;
            int pos = address;
            int length = 0;
            while (b != -1)
            {
                b = f.ReadByte();
                if (b != -1)
                {
                    romData[pos] = (byte)b;
                    length++;
                    pos++;
                }
            }
            SetPatchBytes(address, length);
            return length;
        }
        /*
public function setHeartBeepSpeed(string $setting) : self {
switch ($setting) {
   case 'off':
       $byte = 0x00;
       break;
   case 'half':
       $byte = 0x40;
       break;
   case 'quarter':
       $byte = 0x80;
       break;
   case 'normal':
   default:
       $byte = 0x20;
}

$this->write(0x180033, pack('C', $byte));

return $this;
}
*/

        /*
    public function setGoalRequiredCount(int $goal = 0) : self {
        $this->write(0x180167, pack('C', $goal));

        return $this;
    }
         */

        /*
    public function setGoalIcon(string $goal_icon = 'triforce') : self {
        switch ($goal_icon) {
            case 'triforce':
                $byte = pack('S*', 0x280E);
                break;
            case 'star':
            default:
                $byte = pack('S*', 0x280D);
                break;
        }
        $this->write(0x180165, $byte);

        return $this;
    }
         */

        /*
     //* Set Ganon to Invincible. 'dungeons' will require all dungeon bosses are dead to be able to damage Ganon.
     //*
     //* @param string $setting
     //*
     //* @return $this
    public function setGanonInvincible(string $setting = 'no') : self {
        switch ($setting) {
            case 'crystals':
                $byte = pack('C*', 0x03);
                break;
            case 'dungeons':
                $byte = pack('C*', 0x02);
                break;
            case 'yes':
                $byte = pack('C*', 0x01);
                break;
            case 'no':
            default:
                $byte = pack('C*', 0x00);
                break;
        }
        $this->write(0x18003E, $byte);

        return $this;
    }
         */

        /*
    public function setDebugMode($enable = true) : self {
        $this->write(0x65B88, pack('S*', $enable ? 0xEAEA : 0x21F0));
        $this->write(0x65B91, pack('S*', $enable ? 0xEAEA : 0x18D0));

        return $this;
    }
         */

        /*
            public function setSRAMTrace($enable = true) : self {
                $this->write(0x180030, pack('C*', $enable ? 0x01 : 0x00));

                return $this;
            }
         */

        /*
    public function setRandomizerSeedType(string $setting) : self {
        switch ($setting) {
            case 'OverworldGlitches':
                $byte = 0x02;
                break;
            case 'MajorGlitches':
                $byte = 0x01;
                break;
            case 'off':
                $byte = 0xFF;
                break;
            case 'NoMajorGlitches':
            default:
                $byte = 0x00;
        }

        $this->write(0x180210, pack('C', $byte));

        return $this;
    }
         */

        /*
    public function setGameType(string $setting) : self {
        switch ($setting) {
            case 'Plandomizer':
                $byte = 0x01;
                break;
            case 'other':
                $byte = 0xFF;
                break;
            case 'Randomizer':
            default:
                $byte = 0x00;
        }

        $this->write(0x180211, pack('C', $byte));

        return $this;
    }
         */

        /*
    public function setPlandomizerAuthor(string $name) : self {
        $this->write(0x180220, substr($name, 0, 31));

        return $this;
    }
         */

        /*
     //* Adjust some settings for hard mode
     //*
     //* @param int $level how hard to make it, higher should be harder
     //*
     //* @return $this
    public function setHardMode(int $level = 0) : self {
        $this->setBelowGanonChest(false);
        $this->setCaneOfByrnaSpikeCaveUsage();
        $this->setCapeSpikeCaveUsage();
        $this->setByrnaCaveSpikeDamage(0x08);

        switch ($level) {
            case 0:
                // Cape magic
                $this->write(0x3ADA7, pack('C*', 0x04, 0x08, 0x10));
                // Bryna magic amount used per "cycle"
                $this->write(0x45C42, pack('C*', 0x04, 0x02, 0x01));
                $this->setPowderedSpriteFairyPrize(0xE3);
                $this->setBottleFills([0xA0, 0x80]);
                $this->setShopBlueShieldCost(50);
                $this->setShopRedShieldCost(500);
                $this->setCatchableFairies(true);
                $this->setCatchableBees(true);

                $this->setRupoorValue(0);

                break;
            case 1:
                $this->write(0x3ADA7, pack('C*', 0x02, 0x02, 0x02));
                $this->write(0x45C42, pack('C*', 0x08, 0x08, 0x08));
                $this->setPowderedSpriteFairyPrize(0xD8); // 1 heart
                $this->setBottleFills([0x28, 0x40]); // 5 hearts, 1/2 magic refills
                $this->setShopBlueShieldCost(100);
                $this->setShopRedShieldCost(999);
                $this->setCatchableFairies(false);
                $this->setCatchableBees(true);

                $this->setRupoorValue(10);

                break;
            case 2:
                $this->write(0x3ADA7, pack('C*', 0x01, 0x01, 0x01));
                $this->write(0x45C42, pack('C*', 0x10, 0x10, 0x10));
                $this->setPowderedSpriteFairyPrize(0x79); // Bees
                $this->setBottleFills([0x08, 0x20]); // 1 heart, 1/4 magic refills
                $this->setShopBlueShieldCost(9990);
                $this->setShopRedShieldCost(9990);
                $this->setCatchableFairies(false);
                $this->setCatchableBees(true);

                $this->setRupoorValue(20);

                break;
        }

        return $this;
    }
         */

        /*
     //* Set Pyramid Fountain to have 2 chests
     //*
     //* @param bool $enable switch on or off
    public function setPyramidFairyChests(bool $enable = true) : self {
        $this->write(0x1FC16, $enable
            ? pack('C*', 0xB1, 0xC6, 0xF9, 0xC9, 0xC6, 0xF9)
            : pack('C*', 0xA8, 0xB8, 0x3D, 0xD0, 0xB8, 0x3D));

        return $this;
    }
         */

        /*
     //* Place 2 chests in Waterfall of Wishing Fairy.
     //*
     //* @param bool $enable switch on or off

    public function setWishingWellChests(bool $enable = false) : self {
        // set item table to proper room
        $this->write(0xE9AE, $enable ? pack('C*', 0x14, 0x01) : pack('C*', 0x05, 0x00));
        $this->write(0xE9CF, $enable ? pack('C*', 0x14, 0x01) : pack('C*', 0x3D, 0x01));

        // room 276 remodel
        $this->write(0x1F714, $enable
            ? base64_decode(
                "4QAQrA0pmgFYmA8RsWH8TYEg2gIs4WH8voFhsWJU2gL9jYNE4WL9HoMxpckxpGkxwCJNpGkxxvlJxvkQmaBcmaILmGAN6MBV6MALk" .
                "gBzmGD+aQCYo2H+a4H+q4WpyGH+roH/aQLYo2L/a4P/K4fJyGL/LoP+oQCqIWH+poH/IQLKIWL/JoO7I/rDI/q7K/rDK/q7U/rDU/" .
                "qwoD2YE8CYUsCIAGCQAGDoAGDwAGCYysDYysDYE8DYUsD8vYX9HYf/////8P+ALmEOgQ7//w==")
            : base64_decode(
                "4QAQrA0pmgFYmA8RsGH8TQEg0gL8vQUs4WH8voFhsGJU0gL9jQP9HQdE4WL9HoMxpckxpGkxwCJNpGkouD1QuD0QmaBcmaILmGAN4" .
                "cBV4cALkgBzmGD+aQCYo2H+a4H+q4WpyGH+roH/aQLYo2L/a4P/K4fJyGL/LoP+oQCqIWH+poH/IQLKIWL/JoO7I/rDI/q7K/rDK/" .
                "q7U/rDU/qwoD2YE8CYUsCIAGCQAGDoAGDwAGCYysDYysDYE8DYUsD/////8P+ALmEOgQ7//w=="));

        return $this;
    }
         */

        /*
    public function setOpenMode(bool $enable = true) : self {
        $this->write(0x180032, pack('C*', $enable ? 0x01 : 0x00));
        $this->setSewersLampCone(!$enable);
        $this->setLightWorldLampCone(false);
        $this->setDarkWorldLampCone(false);

        return $this;
    }
         */

        /*
    public function setSwordlessMode(bool $enable = false) : self {
        $this->write(0x18003F, pack('C*', $enable ? 0x01 : 0x00)); // Hammer Ganon
        $this->write(0x180040, pack('C*', $enable ? 0x01 : 0x00)); // Open Curtains
        $this->write(0x180041, pack('C*', $enable ? 0x01 : 0x00)); // Swordless Medallions
        $this->write(0x180043, pack('C*', $enable ? 0xFF : 0x00)); // set Link's starting sword 0xFF is taken sword

        $this->setHammerTablet($enable);

        return $this;
    }
         */

        /*
     //* Enable/Disable ability to Save and Quit from Boss room after item collection.
     //*
     //* @param bool $enable switch on or off
    public function setSaveAndQuitFromBossRoom(bool $enable = false) : self {
        $this->write(0x180042, pack('C*', $enable ? 0x01 : 0x00));

        return $this;
    }
         */







        /*
    public function setUncleTextString(string $string) : self {
        $offset = 0x180500;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setGanon1TextString(string $string) : self {
        $offset = 0x180600;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setGanon2TextString(string $string) : self {
        $offset = 0x180700;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setGanon1InvincibleTextString(string $string) : self {
        $offset = 0x181100;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setGanon2InvincibleTextString(string $string) : self {
        $offset = 0x181200;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setTriforceTextString(string $string) : self {
        $offset = 0x180400;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setBlindTextString(string $string) : self {
        $offset = 0x180800;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setTavernManTextString(string $string) : self {
        $offset = 0x180C00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setSahasrahla1TextString(string $string) : self {
        $offset = 0x180A00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setSahasrahla2TextString(string $string) : self {
        $offset = 0x180B00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setBombShop1TextString(string $string) : self {
        $offset = 0x180E00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setBombShop2TextString(string $string) : self {
        $offset = 0x180D00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setPyramidFairyTextString(string $string) : self {
        $offset = 0x180900;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setPedestalTextbox(string $string) : self {
        $offset = 0x180300;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setBombosTextbox(string $string) : self {
        $offset = 0x181000;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setEtherTextbox(string $string) : self {
        $offset = 0x180F00;

        $converter = new Dialog;
        foreach ($converter->convertDialog($string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setKingsReturnCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 22), 22, ' ', STR_PAD_BOTH);
        $offset = 0x76928;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setSanctuaryCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 16), 16, ' ', STR_PAD_BOTH);
        $offset = 0x76964;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setKakarikoTownCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 23), 23, ' ', STR_PAD_BOTH);
        $offset = 0x76997;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setDesertPalaceCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 24), 24, ' ', STR_PAD_BOTH);
        $offset = 0x769D4;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setMountainTowerCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 24), 24, ' ', STR_PAD_BOTH);
        $offset = 0x76A12;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setLinksHouseCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 19), 19, ' ', STR_PAD_BOTH);
        $offset = 0x76A52;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setZoraCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 20), 20, ' ', STR_PAD_BOTH);
        $offset = 0x76A85;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setMagicShopCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 23), 23, ' ', STR_PAD_BOTH);
        $offset = 0x76AC5;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setWoodsmansHutCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 16), 16, ' ', STR_PAD_BOTH);
        $offset = 0x76AFC;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setFluteBoyCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 23), 23, ' ', STR_PAD_BOTH);
        $offset = 0x76B34;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setWishingWellCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 23), 23, ' ', STR_PAD_BOTH);
        $offset = 0x76B71;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setSwordsmithsCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 23), 23, ' ', STR_PAD_BOTH);
        $offset = 0x76BAC;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setBugCatchingKidCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 20), 20, ' ', STR_PAD_BOTH);
        $offset = 0x76BDF;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setDeathMountainCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 16), 16, ' ', STR_PAD_BOTH);
        $offset = 0x76C19;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setLostWoodsCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 16), 16, ' ', STR_PAD_BOTH);
        $offset = 0x76C51;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }
    public function setAltarCredits(string $string) : self {
        $write_string = str_pad(substr($string, 0, 20), 20, ' ', STR_PAD_BOTH);
        $offset = 0x76C81;
        foreach ($this->convertCredits($write_string) as $byte) {
            $this->write($offset++, pack('C', $byte));
        }

        return $this;
    }

         */

        public void MuteMusic(bool mute = true)
        {
            var tracks_by_volume = new Dictionary<byte, int[]>()
            {
                { 0x00, new int[] { 0xD373B, 0xD375B, 0xD90F8 } },
                { 0x14, new int[] { 0xDA710, 0xDA7A4, 0xDA7BB, 0xDA7D2 } },
                { 0x3C, new int[] { 0xD5954, 0xD653B, 0xDA736, 0xDA752, 0xDA772, 0xDA792 } },
                { 0x50, new int[] { 0xD5B47, 0xD5B5E } },
                { 0x5A, new int[] { 0xD4306 } },
                { 0x64, new int[] { 0xD6878, 0xD6883, 0xD6E48, 0xD6E76, 0xD6EFB, 0xD6F2D, 0xDA211, 0xDA35B, 0xDA37B, 0xDA38E,
                    0xDA39F, 0xDA5C3, 0xDA691, 0xDA6A8, 0xDA6DF } },
                { 0x78, new int[] { 0xD2349, 0xD3F45, 0xD42EB, 0xD48B9, 0xD48FF, 0xD543F, 0xD5817, 0xD5957, 0xD5ACB, 0xD5AE8,
                    0xD5B4A, 0xDA5DE, 0xDA608, 0xDA635, 0xDA662, 0xDA71F, 0xDA7AF, 0xDA7C6, 0xDA7DD } },
                { 0x82, new int[] { 0xD2F00, 0xDA3D5 } },
                { 0xA0, new int[] { 0xD249C, 0xD24CD, 0xD2C09, 0xD2C53, 0xD2CAF, 0xD2CEB, 0xD2D91, 0xD2EE6, 0xD38ED, 0xD3C91,
                    0xD3CD3, 0xD3CE8, 0xD3F0C, 0xD3F82, 0xD405F, 0xD4139, 0xD4198, 0xD41D5, 0xD41F6, 0xD422B, 0xD4270,
                    0xD42B1, 0xD4334, 0xD4371, 0xD43A6, 0xD43DB, 0xD441E, 0xD4597, 0xD4B3C, 0xD4BAB, 0xD4C03, 0xD4C53,
                    0xD4C7F, 0xD4D9C, 0xD5424, 0xD65D2, 0xD664F, 0xD6698, 0xD66FF, 0xD6985, 0xD6C5C, 0xD6C6F, 0xD6C8E,
                    0xD6CB4, 0xD6D7D, 0xD827D, 0xD960C, 0xD9828, 0xDA233, 0xDA3A2, 0xDA49E, 0xDA72B, 0xDA745, 0xDA765,
                    0xDA785, 0xDABF6, 0xDAC0D, 0xDAEBE, 0xDAFAC } },
                { 0xAA, new int[] { 0xD9A02, 0xD9BD6 } },
                { 0xB4, new int[] { 0xD21CD, 0xD2279, 0xD2E66, 0xD2E70, 0xD2EAB, 0xD3B97, 0xD3BAC, 0xD3BE8, 0xD3C0D, 0xD3C39,
                    0xD3C68, 0xD3C9F, 0xD3CBC, 0xD401E, 0xD4290, 0xD443E, 0xD456F, 0xD47D3, 0xD4D43, 0xD4DCC, 0xD4EBA,
                    0xD4F0B, 0xD4FE5, 0xD5012, 0xD54BC, 0xD54D5, 0xD54F0, 0xD5509, 0xD57D8, 0xD59B9, 0xD5A2F, 0xD5AEB,
                    0xD5E5E, 0xD5FE9, 0xD658F, 0xD674A, 0xD6827, 0xD69D6, 0xD69F5, 0xD6A05, 0xD6AE9, 0xD6DCF, 0xD6E20,
                    0xD6ECB, 0xD71D4, 0xD71E6, 0xD7203, 0xD721E, 0xD8724, 0xD8732, 0xD9652, 0xD9698, 0xD9CBC, 0xD9DC0,
                    0xD9E49, 0xDAA68, 0xDAA77, 0xDAA88, 0xDAA99, 0xDAF04 } },
                { 0x8C, new int[] { 0xD1D28, 0xD1D41, 0xD1D5C, 0xD1D77, 0xD1EEE, 0xD311D, 0xD31D1, 0xD4148, 0xD5543, 0xD5B6F,
                    0xD65B3, 0xD6760, 0xD6B6B, 0xD6DF6, 0xD6E0D, 0xD73A1, 0xD814C, 0xD825D, 0xD82BE, 0xD8340, 0xD8394,
                    0xD842C, 0xD8796, 0xD8903, 0xD892A, 0xD91E8, 0xD922B, 0xD92E0, 0xD937E, 0xD93C1, 0xDA958, 0xDA971,
                    0xDA98C, 0xDA9A7 } },
                { 0xC8, new int[] { 0xD1D92, 0xD1DBD, 0xD1DEB, 0xD1F5D, 0xD1F9F, 0xD1FBD, 0xD1FDC, 0xD1FEA, 0xD20CA, 0xD21BB,
                    0xD22C9, 0xD2754, 0xD284C, 0xD2866, 0xD2887, 0xD28A0, 0xD28BA, 0xD28DB, 0xD28F4, 0xD293E, 0xD2BF3,
                    0xD2C1F, 0xD2C69, 0xD2CA1, 0xD2CC5, 0xD2D05, 0xD2D73, 0xD2DAF, 0xD2E3D, 0xD2F36, 0xD2F46, 0xD2F6F,
                    0xD2FCF, 0xD2FDF, 0xD302B, 0xD3086, 0xD3099, 0xD30A5, 0xD30CD, 0xD30F6, 0xD3154, 0xD3184, 0xD333A,
                    0xD33D9, 0xD349F, 0xD354A, 0xD35E5, 0xD3624, 0xD363C, 0xD3672, 0xD3691, 0xD36B4, 0xD36C6, 0xD3724,
                    0xD3767, 0xD38CB, 0xD3B1D, 0xD3B2F, 0xD3B55, 0xD3B70, 0xD3B81, 0xD3BBF, 0xD3D34, 0xD3D55, 0xD3D6E,
                    0xD3DC6, 0xD3E04, 0xD3E38, 0xD3F65, 0xD3FA6, 0xD404F, 0xD4087, 0xD417A, 0xD41A0, 0xD425C, 0xD4319,
                    0xD433C, 0xD43EF, 0xD440C, 0xD4452, 0xD4494, 0xD44B5, 0xD4512, 0xD45D1, 0xD45EF, 0xD4682, 0xD46C3,
                    0xD483C, 0xD4848, 0xD4855, 0xD4862, 0xD486F, 0xD487C, 0xD4A1C, 0xD4A3B, 0xD4A60, 0xD4B27, 0xD4C7A,
                    0xD4D12, 0xD4D81, 0xD4E90, 0xD4ED6, 0xD4EE2, 0xD5005, 0xD502E, 0xD503C, 0xD5081, 0xD51B1, 0xD51C7,
                    0xD51CF, 0xD51EF, 0xD520C, 0xD5214, 0xD5231, 0xD5257, 0xD526D, 0xD5275, 0xD52AF, 0xD52BD, 0xD52CD,
                    0xD52DB, 0xD549C, 0xD5801, 0xD58A4, 0xD5A68, 0xD5A7F, 0xD5C12, 0xD5D71, 0xD5E10, 0xD5E9A, 0xD5F8B,
                    0xD5FA4, 0xD651A, 0xD6542, 0xD65ED, 0xD661D, 0xD66D7, 0xD6776, 0xD68BD, 0xD68E5, 0xD6956, 0xD6973,
                    0xD69A8, 0xD6A51, 0xD6A86, 0xD6B96, 0xD6C3E, 0xD6D4A, 0xD6E9C, 0xD6F80, 0xD717E, 0xD7190, 0xD71B9,
                    0xD811D, 0xD8139, 0xD816B, 0xD818A, 0xD819E, 0xD81BE, 0xD829C, 0xD82E1, 0xD8306, 0xD830E, 0xD835E,
                    0xD83AB, 0xD83CA, 0xD83F0, 0xD83F8, 0xD844B, 0xD8479, 0xD849E, 0xD84CB, 0xD84EB, 0xD84F3, 0xD854A,
                    0xD8573, 0xD859D, 0xD85B4, 0xD85CE, 0xD862A, 0xD8681, 0xD87E3, 0xD87FF, 0xD887B, 0xD88C6, 0xD88E3,
                    0xD8944, 0xD897B, 0xD8C97, 0xD8CA4, 0xD8CB3, 0xD8CC2, 0xD8CD1, 0xD8D01, 0xD917B, 0xD918C, 0xD919A,
                    0xD91B5, 0xD91D0, 0xD91DD, 0xD9220, 0xD9273, 0xD9284, 0xD9292, 0xD92AD, 0xD92C8, 0xD92D5, 0xD9311,
                    0xD9322, 0xD9330, 0xD934B, 0xD9366, 0xD9373, 0xD93B6, 0xD97A6, 0xD97C2, 0xD97DC, 0xD97FB, 0xD9811,
                    0xD98FF, 0xD996F, 0xD99A8, 0xD99D5, 0xD9A30, 0xD9A4E, 0xD9A6B, 0xD9A88, 0xD9AF7, 0xD9B1D, 0xD9B43,
                    0xD9B7C, 0xD9BA9, 0xD9C84, 0xD9C8D, 0xD9CAC, 0xD9CE8, 0xD9CF3, 0xD9CFD, 0xD9D46, 0xDA35E, 0xDA37E,
                    0xDA391, 0xDA478, 0xDA4C3, 0xDA4D7, 0xDA4F6, 0xDA515, 0xDA6E2, 0xDA9C2, 0xDA9ED, 0xDAA1B, 0xDAA57,
                    0xDABAF, 0xDABC9, 0xDABE2, 0xDAC28, 0xDAC46, 0xDAC63, 0xDACB8, 0xDACEC, 0xDAD08, 0xDAD25, 0xDAD42,
                    0xDAD5F, 0xDAE17, 0xDAE34, 0xDAE51, 0xDAF2E, 0xDAF55, 0xDAF6B, 0xDAF81, 0xDB14F, 0xDB16B, 0xDB180,
                    0xDB195, 0xDB1AA } },
                { 0xD2, new int[] { 0xD2B88, 0xD364A, 0xD369F, 0xD3747 } },
                { 0xDC, new int[] { 0xD213F, 0xD2174, 0xD229E, 0xD2426, 0xD4731, 0xD4753, 0xD4774, 0xD4795, 0xD47B6, 0xD4AA5,
                    0xD4AE4, 0xD4B96, 0xD4CA5, 0xD5477, 0xD5A3D, 0xD6566, 0xD672C, 0xD67C0, 0xD69B8, 0xD6AB1, 0xD6C05,
                    0xD6DB3, 0xD71AB, 0xD8E2D, 0xD8F0D, 0xD94E0, 0xD9544, 0xD95A8, 0xD9982, 0xD9B56, 0xDA694, 0xDA6AB,
                    0xDAE88, 0xDAEC8, 0xDAEE6, 0xDB1BF } },
                { 0xE6, new int[] { 0xD210A, 0xD22DC, 0xD2447, 0xD5A4D, 0xD5DDC, 0xDA251, 0xDA26C } },
                { 0xF0, new int[] { 0xD945E, 0xD967D, 0xD96C2, 0xD9C95, 0xD9EE6, 0xDA5C6 } },
                { 0xFA, new int[] { 0xD2047, 0xD24C2, 0xD24EC, 0xD25A4, 0xD3DAA, 0xD51A8, 0xD51E6, 0xD524E, 0xD529E, 0xD6045,
                    0xD81DE, 0xD821E, 0xD94AA, 0xD9A9E, 0xD9AE4, 0xDA289 } },
                { 0xFF, new int[] { 0xD2085, 0xD21C5, 0xD5F28 } },
            };

            foreach(var vol in tracks_by_volume)
            {
                foreach(var addr in vol.Value)
                {
                    this[addr] = (byte)(mute ? 0x00 : vol.Key);
                }
            }
            //foreach ($tracks_by_volume as $original_volume => $tracks) {
            //	$byte = pack('C*', $enable ? 0x00 : $original_volume);
            //	foreach ($tracks as $address) {
            //		$this->write($address, $byte);
            //	}
            //}
        }
    }

}
