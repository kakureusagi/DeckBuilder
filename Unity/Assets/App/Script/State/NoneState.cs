namespace App.State
{
	public class NoneState : IState
	{
		public void OnEnter()
		{
		}

		public StateType Update()
		{
			return StateType.None;
		}

		public void OnLeave()
		{
		}
	}
}