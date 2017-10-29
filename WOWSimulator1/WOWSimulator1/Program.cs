using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOWSimulator1
{
	class Program
	{
		static void Main(string[] args)
		{
			WoWSampleSimulation();
		}

		private static void WoWSampleSimulation()
		{
			BadBoss badBoy = new BadBoss() { AttackSpeed = 0.6F, Energy = 250, MaxEnergy = 2500, Name = "Big Boss" };
			badBoy.Attack = 40;

			IEnumerable<WoWCreature> players = new List<WoWCreature>()
			{
				new Warrior() {Attack=20, Energy=100, MaxEnergy=100, Name="Player1" },
				new Warrior() {Attack=21, Energy=98, MaxEnergy=100, Name="Player2" },
				new Shaman() {Attack=10, Energy=60, MaxEnergy=80, HealingValue=11, Name="Shaman" }
			};

			int playerCount = 3;
			Random rnd = new Random();
			int deadPlayers = 0;
			while (deadPlayers < playerCount && badBoy.Energy > 0)
			{
				foreach (WoWCreature player in players)
				{
					if (player.Energy <= 0)
						continue;
					player.DoAttack(badBoy);
					if (badBoy.Energy <= 0)
						break;
					if (player is Shaman)
						((Shaman)player).Heal(player);
					if (rnd.Next(0, 10) / 10.0F <= badBoy.AttackSpeed)
						badBoy.DoAttack(player);
					if (player.Energy <= 0)
						deadPlayers++;
				}
			}

			if (badBoy.Energy <= 0)
				Console.WriteLine("Victory! the boss is dead!");
			else
				Console.WriteLine("you lost!");

			Console.ReadKey();
		}
	}

	public abstract class WoWCreature
	{
		public string Name { get; set; }
		public int Attack { get; set; }
		public int Energy { get; set; }
		public int MaxEnergy { get; set; }
		public void DoAttack(WoWCreature creature)
		{
			creature.Energy -= this.Attack;
		}
	}

	public class Warrior : WoWCreature { }

	public class BadBoss : WoWCreature
	{
		public float AttackSpeed { get; set; }
	}

	public interface IHealingCreature
	{
		int HealingValue { get; set; }
		void Heal(WoWCreature creature);
	}

	public class Shaman : WoWCreature, IHealingCreature
	{
		public int HealingValue { get; set; }
		public void Heal(WoWCreature creature)
		{
			creature.Energy += HealingValue;
			if (creature.Energy >= creature.MaxEnergy)
				creature.Energy = creature.MaxEnergy;
		}
	}
}
