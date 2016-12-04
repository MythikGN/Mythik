using System;
using Server;
using Server.Mobiles;
using Server.Factions;
using Server.Items;
using Scripts.Mythik;

namespace Server.Misc
{
	public class SkillCheck
	{
        //Used to calculate the base repetition quantity.
        public const int TotalRepetitionParts = 78;


        private static readonly bool AntiMacroCode = false;// !Core.ML;		//Change this to false to disable anti-macro code

		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes( 5.0 ); //How long do we remember targets/locations?
		public const int Allowance = 3;	//How many times may we use the same location/target for gain
		private const int LocationSize = 5; //The size of eeach location, make this smaller so players dont have to move as far
		private static bool[] UseAntiMacro = new bool[]
		{
			// true if this skill uses the anti-macro code, false if it does not
			false,// Alchemy = 0,
			true,// Anatomy = 1,
			true,// AnimalLore = 2,
			true,// ItemID = 3,
			true,// ArmsLore = 4,
			false,// Parry = 5,
			true,// Begging = 6,
			false,// Blacksmith = 7,
			false,// Fletching = 8,
			true,// Peacemaking = 9,
			true,// Camping = 10,
			false,// Carpentry = 11,
			false,// Cartography = 12,
			false,// Cooking = 13,
			true,// DetectHidden = 14,
			true,// Discordance = 15,
			true,// EvalInt = 16,
			true,// Healing = 17,
			true,// Fishing = 18,
			true,// Forensics = 19,
			true,// Herding = 20,
			true,// Hiding = 21,
			true,// Provocation = 22,
			false,// Inscribe = 23,
			true,// Lockpicking = 24,
			true,// Magery = 25,
			true,// MagicResist = 26,
			false,// Tactics = 27,
			true,// Snooping = 28,
			true,// Musicianship = 29,
			true,// Poisoning = 30,
			false,// Archery = 31,
			true,// SpiritSpeak = 32,
			true,// Stealing = 33,
			false,// Tailoring = 34,
			true,// AnimalTaming = 35,
			true,// TasteID = 36,
			false,// Tinkering = 37,
			true,// Tracking = 38,
			true,// Veterinary = 39,
			false,// Swords = 40,
			false,// Macing = 41,
			false,// Fencing = 42,
			false,// Wrestling = 43,
			true,// Lumberjacking = 44,
			true,// Mining = 45,
			true,// Meditation = 46,
			true,// Stealth = 47,
			true,// RemoveTrap = 48,
			true,// Necromancy = 49,
			false,// Focus = 50,
			true,// Chivalry = 51
			true,// Bushido = 52
			true,//Ninjitsu = 53
			true // Spellweaving
		};

		public static void Initialize()
		{
			Mobile.SkillCheckLocationHandler = new SkillCheckLocationHandler( Mobile_SkillCheckLocation );
			Mobile.SkillCheckDirectLocationHandler = new SkillCheckDirectLocationHandler( Mobile_SkillCheckDirectLocation );

			Mobile.SkillCheckTargetHandler = new SkillCheckTargetHandler( Mobile_SkillCheckTarget );
			Mobile.SkillCheckDirectTargetHandler = new SkillCheckDirectTargetHandler( Mobile_SkillCheckDirectTarget );
            SkillInfo.Table[0].GainFactor = 127000; // Alchemy = 0, 
                                                    //Macro type: People will create nightsights, delay 8 seconds.
                                                    //Time estimate: 12 days 24/7.

            SkillInfo.Table[1].GainFactor = 39200; // Anatomy = 1, 
                                                   //Macro type: Target yourself, delay 1 second
                                                   //Time estimate: 11 hours.

            SkillInfo.Table[2].GainFactor = 39200; // AnimalLore = 2, 
                                                   //Macro type: Target animal, delay 1 second
                                                   //Time estimate: 11 hours.

            SkillInfo.Table[3].GainFactor = 39200; // ItemID = 3, 
                                                   //Macro type: Target weapon/armor, delay 1 second
                                                   //Time estimate: 11 hours.

            SkillInfo.Table[4].GainFactor = 39200; // ArmsLore = 4,
                                                   //Macro type: Target weapon/armor, delay 1 second
                                                   //Time estimate: 11 hours.

            SkillInfo.Table[5].GainFactor = 56800; // Parry = 5, 
                                                   //Macro type: Sparring with someone that wrestles you, delay 2 seconds
                                                   //Time estimate: 1½ days 24/7.

            SkillInfo.Table[6].GainFactor = 39200; // Begging = 6, 
                                                   //Macro type: On mobs, delay 3 seconds
                                                   //Time estimate: 1½ days 24/7

            SkillInfo.Table[7].GainFactor = 223428; // Blacksmith = 7, 
                                                    //Macro type: Make daggers, delay about 3.5 seconds in average
                                                    //Time estimate: 9-10 days 24/7

            SkillInfo.Table[8].GainFactor = 144040; // Fletching = 8, 
                                                    //Macro type: Make kindlings, delay about 3.5 seconds in average
                                                    //Time estimate: 6 days 24/7

            SkillInfo.Table[9].GainFactor = 34500; // Peacemaking = 9, 
                                                   //Macro type: Target self, delay 11 seconds when failing, 5½ seconds when success. Estimate 50/50
                                                   //Time estimate: 3-4 days 24/7

            SkillInfo.Table[10].GainFactor = 56400; // Camping = 10,
                                                    //Macro type: Light kindlings, delay 1 second
                                                    //Time estimate: 16 hours

            SkillInfo.Table[11].GainFactor = 248114; // Carpentry = 11,
                                                     //Macro type: Clubs or anything that requires 3 logs up to 70, then blank scrolls that requires 1 log, delay about 3 seconds average
                                                     //Time estimate: 9 days 24/7          

            SkillInfo.Table[12].GainFactor = 152000; // Cartography = 12, 
                                                     //Macro type: Create local map, average delay of about 3 seconds
                                                     //Time estimate: 6 days 24/7

            SkillInfo.Table[13].GainFactor = 191840; // Cooking = 13,
                                                     //Macro type: Use raw ribs and cook them,  delay 2.5 seconds
                                                     //Time estimate: 6 days 24/7

            SkillInfo.Table[14].GainFactor = 76400; // DetectHidden = 14, 
                                                    //Macro type: Just press skill, delay 1 second
                                                    //Time estimate: 1 day 24/7

            SkillInfo.Table[15].GainFactor = 60600; // Discordance = 15, 
                                                    //Macro type: Target creature, delay 1 second
                                                    //Time estimate: 17 hours

            SkillInfo.Table[16].GainFactor = 39200; // EvalInt = 16,
                                                    //Macro type: Target yourself, delay 1 second
                                                    //Time estimate: 11 hours

            SkillInfo.Table[17].GainFactor = 40200; // Healing = 17, 
                                                    //Macro type: Hurt yourself somehow, delay 3 seconds
                                                    //Time estimate: 1½ days 24/7

            SkillInfo.Table[18].GainFactor = 107600; // Fishing = 18, 
                                                     //Macro type: Just fish in water, delay 3 seconds
                                                     //Time estimate: 4 days 24/7

            SkillInfo.Table[19].GainFactor = 49200; // Forensics = 19, 
                                                    //Macro type: Target corpse, delay 1 second
                                                    //Time estimate: 15 hours

            SkillInfo.Table[20].GainFactor = 149600; // Herding = 20,
                                                     //Macro type: Herd any tamable animal, delay 1 second
                                                     //Time estimate: 2 days 24/7

            SkillInfo.Table[21].GainFactor = 48640; // Hiding = 21,
                                                    //Macro type: Just repeat, delay 2.5 seconds
                                                    //Time estimate: 1½ days 24/7

            SkillInfo.Table[22].GainFactor = 28000; // Provocation = 22,
                                                    //Macro type: Target two creatures, delay 11 seconds when succeding, 5½ seconds when fail. Estimate 50/50
                                                    //Time estimate: 2 days 24/7

            SkillInfo.Table[23].GainFactor = 164400; // Inscribe = 23,
                                                     //Macro type: People will do just make the easiest scroll, and with meditation delay is calculated to about 7 seconds
                                                     //Time estimate: 14 days 24/7.

            SkillInfo.Table[24].GainFactor = 49900; // Lockpicking = 24, 
                                                    //Macro type: Repeat lockpicking, if needed relock the chest, delay 3.5 seconds, estimate 4
                                                    //Time estimate: 2½ days 24/7

            SkillInfo.Table[25].GainFactor = 29250; // Magery = 25, 
                                                    //Macro type: Repeat casting easy spell such as nightsight, delay with meditation calculated to about 7 seconds
                                                    //Time estimate: 2½ days 24/7

            SkillInfo.Table[26].GainFactor = 95800; // MagicResist = 26, 
                                                    //Macro type: Run in firefield healing yourself, train magery etc, estimated delay ½-1 second
                                                    //Time estimate: 1 day 24/7

            SkillInfo.Table[27].GainFactor = 55800; // Tactics = 27, 
                                                    //Macro type: Wrestle someone, delay 2 seconds
                                                    //Time estimate: 1½ days 24/7.

            SkillInfo.Table[28].GainFactor = 50840; // Snooping = 28, 
                                                    //Macro type: Open another mob/players backpack, delay 2.5 seconds
                                                    //Time estimate: 1½ days 24/7.

            SkillInfo.Table[29].GainFactor = 15525; // Musicianship = 29, 
                                                    //Macro type: Play instrument, repeat. Delay 7 seconds
                                                    //Time estimate: 1½ days 24/7.

            SkillInfo.Table[30].GainFactor = 16280; // Poisoning = 30 
                                                    //Macro type: Poison weapons with poison bottles. Delay 10 seconds
                                                    //Time estimate: 2 days 24/7

            SkillInfo.Table[31].GainFactor = 36560; // Archery = 31
                                                    //Macro type: Shoot with bow on monsters/friends. Delay 2.5 seconds
                                                    //Time estimate: 1 day 24/7

            SkillInfo.Table[32].GainFactor = 30560; // SpiritSpeak = 32 
                                                    //Macro type: Just repeat, delay 2.5 seconds
                                                    //Time estimate: 1 day 24/7

            SkillInfo.Table[33].GainFactor = 116400; // Stealing = 33
            //Macro type: Just repeat, delay 4 seconds
            //Time estimate: 5-6 days 24/7

            SkillInfo.Table[34].GainFactor = 178000; // Tailoring = 34 
                                                     //Macro type: Create bandanas, repeat. Delay about 4 seconds in average
                                                     //Time estimate: 8-9 days 24/7

            SkillInfo.Table[35].GainFactor = 49800; // AnimalTaming = 35 
                                                    //Macro type: Tame any animal, release, repeat. Delay about 16 seconds in average
                                                    //Time estimate: 9-10 days 24/7

            SkillInfo.Table[36].GainFactor = 40200; // TasteID = 36 
                                                    //Macro type: Target food, delay 1 second
                                                    //Time estimate: 11 hours

            SkillInfo.Table[37].GainFactor = 111200; // Tinkering = 37
                                                     //Macro type: Make clockparts, delay about 4 seconds in average
                                                     //Time estimate: 5-6 days 24/7

            SkillInfo.Table[38].GainFactor = 93600; // Tracking = 38 
                                                    //Macro type: Keep tracking, gain all the time. Delay 1 second
                                                    //Time estimate: 1 day 24/7

            SkillInfo.Table[39].GainFactor = 59200; // Veterinary = 39 
                                                    //Macro type: Hurt a mob, then heal it, delay 5 seconds
                                                    //Time estimate: 3-4 days 24/7

            SkillInfo.Table[40].GainFactor = 28400; // Swords = 40 
                                                    //Macro type: Hit something with a butcher's knife, delay 2 seconds
                                                    //Time estimate: 16 hours

            SkillInfo.Table[41].GainFactor = 28600; // Macing = 41 
                                                    //Macro type: Hit something with a club, delay 3 seconds
                                                    //Time estimate: 16 hours

            SkillInfo.Table[42].GainFactor = 28400; // Fencing = 42 
                                                    //Macro type: Hit something with a dagger, delay 2 seconds
                                                    //Time estimate: 16 hours

            SkillInfo.Table[43].GainFactor = 26400; // Wrestling = 43
                                                    //Macro type: Wrestle someone, delay 2 seconds
                                                    //Time estimate: 15 hours

            SkillInfo.Table[44].GainFactor = 70200; // Lumberjacking = 44
                                                    //Macro type: Chop chop wood in the foooorest, delay average about 4 seconds
                                                    //Time estimate: 3-4 days 24/7

            SkillInfo.Table[45].GainFactor = 52500; // Mining = 45
                                                    //Macro type: Mine all day long, delay average about 5 seconds
                                                    //Time estimate: 3 days 24/7

            SkillInfo.Table[46].GainFactor = 8575; // Meditation = 46
                                                   //Macro type: Meditation while macroing magery, delay 5 seconds (although longer since you have to use a spell as well)
                                                   //Time estimate: ½-1 day 24/7

            SkillInfo.Table[47].GainFactor = 72020; // Stealth = 47 
                                                    //Macro type: Use stealth to hide, delay 2.5 seconds
                                                    //Time estimate: 2 days 24/7

            SkillInfo.Table[48].GainFactor = 39280; // RemoveTrap = 48 
                                                    //Macro type: Use skill on a high level trapped chest, delay 5 seconds
                                                    //Time estimate: 2 days 24/7

            SkillInfo.Table[49].GainFactor = 55555; // Necromancy = 49
                                                    //Not used

            SkillInfo.Table[50].GainFactor = 55555; // Focus = 50
                                                    //Not used

            SkillInfo.Table[51].GainFactor = 55555; // Chivalry = 51
                                                    //Not used

            //Compute this now so that we do not have to do it on each skill check
            for (int i = 0; i < SkillInfo.Table.Length; i++)
                SkillInfo.Table[i].GainFactor /= TotalRepetitionParts;



        }

        public static bool Mobile_SkillCheckLocation( Mobile from, SkillName skillName, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

            // always a chance to gain.
            if(value >= maxSkill)
                maxSkill = value + 0.1;


			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = (value - minSkill) / (maxSkill - minSkill);


			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		public static bool Mobile_SkillCheckDirectLocation( Mobile from, SkillName skillName, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

            // always a chance to gain.
            if (chance >= 1.0)
                chance = 0.99;

            if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		private static bool CheckSkill( Mobile from, Skill skill, object amObj, double chance )
		{
            if (from.Skills.Cap == 0)
                return false;

            bool success = (chance >= Utility.RandomDouble());

            //Repetition multiplier.
            //Calculate how many 5-percent steps the player is over 40%.
            //EG: 50%-54.99% would be 2 steps over 40%, 45%-49.99% would be 1.
            double multiplier = ((int)skill.Base / 5) - 9;

            if (multiplier >= 18)			//At 125% or higer. Multiplier never gets bigger than 18.
                multiplier = 18;
            else if (multiplier <= -5)		//At 0%-20%, 5% of the amount required at 40 and up.
                multiplier = 0.05;
            else if (multiplier <= -4)		//At 25%-30%, 10% of the amount required at 40 and up.
                multiplier = 0.1;
            else if (multiplier <= -3)		//At 30%-35%, 25% of the amount required at 40 and up.
                multiplier = 0.25;
            else if (multiplier <= -2)		//At 35%-40%. 50% of the amount required at 40 and up.
                multiplier = 0.5;
            else if (multiplier <= -1)		//At 40%-45%. 70% of the amount required at 40 and up.
                multiplier = 0.7;
            else if (multiplier <= 0)		//At 45%-50%. 90% of the amount required at 40 and up.
                multiplier = 0.9;

            //The chance to gain at your current skill level
            //Divides 50 tenths (5.0 skill%) with the repetitions required to reach next level (not from your current skill, but from the start of your level).
            double gc = 50 / (skill.Info.GainFactor * multiplier);

            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                gc *= 2;

           // if (SkillBoost.Running) //Taran: Skillgain boost has been enabled
           //     gc *= Values.SkillBoostValues[skill.Info.SkillID];

            if (from.Alive && ((gc >= Utility.RandomDouble() && AllowGain(from, skill, amObj)) || skill.Base < 10.0))
                Gain(from, skill);

            return success;
        }

		public static bool Mobile_SkillCheckTarget( Mobile from, SkillName skillName, object target, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

            // always a chance to gain.
            if (value >= maxSkill)
                maxSkill = value + 0.1;

            if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = (value - minSkill) / (maxSkill - minSkill);

			return CheckSkill( from, skill, target, chance );
		}

		public static bool Mobile_SkillCheckDirectTarget( Mobile from, SkillName skillName, object target, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

            // always a chance to gain.
            if (chance >= 1.0)
                chance = 0.99;

            if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			return CheckSkill( from, skill, target, chance );
		}

		private static bool AllowGain( Mobile from, Skill skill, object obj )
		{
			if ( Core.AOS && Faction.InSkillLoss( from ) )	//Changed some time between the introduction of AoS and SE.
				return false;

			if ( AntiMacroCode && from is PlayerMobile && UseAntiMacro[skill.Info.SkillID] )
				return ((PlayerMobile)from).AntiMacroCheck( skill, obj );
			else
				return true;
		}

		public enum Stat { Str, Dex, Int }

		public static void Gain( Mobile from, Skill skill )
		{
            //Old skill val
            double skillBase = skill.Base;

            if ( from.Region.IsPartOf( typeof( Regions.Jail ) ) )
				return;

			if ( from is BaseCreature && ((BaseCreature)from).IsDeadPet )
				return;

            //Disable Focus gains
			if ( skill.SkillName == SkillName.Focus && from is PlayerMobile )
				return;

			if ( skill.Base < skill.Cap && skill.Lock == SkillLock.Up )
			{
				int toGain = 1;

				if ( skill.Base <= 10.0 )
					toGain = Utility.Random( 4 ) + 1;

				Skills skills = from.Skills;

				if ( from.Player && ( skills.Total / skills.Cap ) >= Utility.RandomDouble() )//( skills.Total >= skills.Cap )
				{
					for ( int i = 0; i < skills.Length; ++i )
					{
						Skill toLower = skills[i];

						if ( toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain )
						{
							toLower.BaseFixedPoint -= toGain;
							break;
						}
					}
				}

				#region Scroll of Alacrity
				PlayerMobile pm = from as PlayerMobile;

				if ( pm != null && skill.SkillName == pm.AcceleratedSkill && pm.AcceleratedStart > DateTime.UtcNow )
					toGain *= Utility.RandomMinMax(2, 5);
				#endregion

				if ( !from.Player || (skills.Total + toGain) <= skills.Cap )
				{
                    EventSink.InvokeSkillGain(new SkillGainEventArgs(from, skill, toGain));
                    skill.BaseFixedPoint += toGain;

                }
            }

			if ( skill.Lock == SkillLock.Up )
			{
				SkillInfo info = skill.Info;

				if ( from.StrLock == StatLockType.Up && (info.StrGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Str );
				else if ( from.DexLock == StatLockType.Up && (info.DexGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Dex );
				else if ( from.IntLock == StatLockType.Up && (info.IntGain / 33.3) > Utility.RandomDouble() )
					GainStat( from, Stat.Int );
			}
            if (skill.Base == 100.0 && skillBase < 100.0)
                InvokeReward(from, skill.Info);
        }

        private static void InvokeReward(Mobile from, SkillInfo skillInfo)
        {
            //Gump is problematic, would need to track if they have recieved their 
            //reward for each skill, and recheck each login incase they closed the gump without accepting etc
            //Just give them a reward directly for now.
            Item rewardItem = null;
            switch (skillInfo.SkillID)
            {
                case 0: // alchemy
                    rewardItem = new MortarPestle();
                    break;
                case 7: // Blacksmithy
                    rewardItem = new SmithHammer();
                    break;
                case 8: // Bowcraft/fletching
                    rewardItem = new Bow();
                    break;
                case 11: // Carpentry
                    rewardItem = new SmoothingPlane();
                    break;
                case 23: // Inscription
                    rewardItem = new ScribesPen();
                    break;
                case 25: // Magery
                    rewardItem = new Spellbook();
                    (rewardItem as Spellbook).Content = ulong.MaxValue;
                    break;
                case 34: // Tailoring
                    rewardItem = new SewingKit();
                    break;
                case 35: // Animal Taming
                    rewardItem = new ShepherdsCrook();
                    break;
               /* case 44: //Lumberjacking
                    rewardItem = new Hatchet();
                    break;
                case 45: // Mining
                    rewardItem = new Pickaxe();
                    break;*/

                //Bardic skills
                case 9:
                case 15:
                case 22:
                case 29:
                    rewardItem = new Drums();
                    break;
                //"Thieving" skills
                case 21:
                case 28:
                case 33:
                case 47:
                    rewardItem = new Cloak();
                    break;
            }
            if(rewardItem != null)
            {
                rewardItem.Hue = MythikStaticValues.GetGMRewardHue();
                rewardItem.LootType = LootType.Blessed;
                if (rewardItem.DefaultName != null)
                    rewardItem.Name = from.RawName + "'s " + rewardItem.DefaultName;
                from.PlaceInBackpack(rewardItem);
                from.SendAsciiMessage("You have recieved an item for becoming a Grand Master.");
            }
            else
            {
                from.SendAsciiMessage("You have recieved nothing but a sense of self worth for your efforts becoming a Grand Master.");

            }


        }


        public static bool CanLower( Mobile from, Stat stat )
		{
            //Disable stat loss for players.
            if (from is PlayerMobile)
                return false;
            switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Down && from.RawStr > 10 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Down && from.RawDex > 10 );
				case Stat.Int: return ( from.IntLock == StatLockType.Down && from.RawInt > 10 );
			}

			return false;
		}

		public static bool CanRaise( Mobile from, Stat stat )
		{
            //Disable stat gains for players.
            if (from is PlayerMobile)
                return false;
            if ( !(from is BaseCreature && ((BaseCreature)from).Controlled) )
			{
				if ( from.RawStatTotal >= from.StatCap )
					return false;
			}

			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < 100 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < 100);
				case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < 100);
			}

			return false;
		}

		public static void IncreaseStat( Mobile from, Stat stat, bool atrophy )
		{
			atrophy = atrophy || (from.RawStatTotal >= from.StatCap);

			switch ( stat )
			{
				case Stat.Str:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Dex ) && (from.RawDex < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawDex;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Str ) )
						++from.RawStr;

					break;
				}
				case Stat.Dex:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Dex ) )
						++from.RawDex;

					break;
				}
				case Stat.Int:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawDex || !CanLower( from, Stat.Dex )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Dex ) )
							--from.RawDex;
					}

					if ( CanRaise( from, Stat.Int ) )
						++from.RawInt;

					break;
				}
			}
		}

		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes( 15.0 );
		private static TimeSpan m_PetStatGainDelay = TimeSpan.FromMinutes( 5.0 );

		public static void GainStat( Mobile from, Stat stat )
		{
			switch( stat )
			{
				case Stat.Str:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastStrGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastStrGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastStrGain = DateTime.UtcNow;
					break;
				}
				case Stat.Dex:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastDexGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}
					else if( (from.LastDexGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastDexGain = DateTime.UtcNow;
					break;
				}
				case Stat.Int:
				{
					if ( from is BaseCreature && ((BaseCreature)from).Controlled ) {
						if ( (from.LastIntGain + m_PetStatGainDelay) >= DateTime.UtcNow )
							return;
					}

					else if( (from.LastIntGain + m_StatGainDelay) >= DateTime.UtcNow )
						return;

					from.LastIntGain = DateTime.UtcNow;
					break;
				}
			}

			bool atrophy = ( (from.RawStatTotal / (double)from.StatCap) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}