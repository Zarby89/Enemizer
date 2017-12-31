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
                    if (currentPatch != null)
                    {
                        patches.Add(currentPatch);
                    }

                    // new patch
                    currentPatch = new PatchObject();
                    currentPatch.address = pd.Key;
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
    }

}
