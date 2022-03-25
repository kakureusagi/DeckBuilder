using UniRx;

namespace App
{
	public class Player
	{
		public IReadOnlyReactiveProperty<int> Hp => hp;
		readonly ReactiveProperty<int> hp;

		public IReadOnlyReactiveProperty<int> MaxHp => maxHp;
		readonly ReactiveProperty<int> maxHp;
		
		public Player(int hp)
		{
			this.hp = new(hp);
			this.maxHp = new(hp);
		}

		public void AddHp(int hp)
		{
			this.hp.Value += hp;
		}
	}
}