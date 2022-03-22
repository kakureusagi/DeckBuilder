using UniRx;

namespace App
{
	public class Enemy
	{
		public int Id { get; }

		public IReadOnlyReactiveProperty<int> MaxHp => maxHp;
		readonly ReactiveProperty<int> maxHp;
		
		public IReadOnlyReactiveProperty<int> Hp => hp;
		readonly ReactiveProperty<int> hp;

		public Enemy(int id, int hp)
		{
			Id = id;
			this.hp = new(hp);
			this.maxHp = new(hp);
		}

		public void AddHp(int hp)
		{
			this.hp.Value += hp;
		}
	}
}