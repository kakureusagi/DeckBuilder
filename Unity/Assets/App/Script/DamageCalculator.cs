namespace App
{

	public class AttackPrediction
	{
		public int Damage { get; set; }
	}
	
	public class DamageCalculator
	{

		public AttackPrediction Attack(int enemyId)
		{
			return new AttackPrediction
			{
				Damage = 5,
			};
		}
	}
}