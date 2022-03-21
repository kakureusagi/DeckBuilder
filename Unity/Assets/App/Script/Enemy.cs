using UniRx;

namespace App
{
	public class Enemy
	{
		public IReadOnlyReactiveProperty<int> MaxHp => maxHp;
		readonly ReactiveProperty<int> maxHp;
		
		public IReadOnlyReactiveProperty<int> Hp => hp;
		readonly ReactiveProperty<int> hp;

		public Enemy(int hp)
		{
			this.hp = new(hp);
			this.maxHp = new(hp);
		}
	}
}