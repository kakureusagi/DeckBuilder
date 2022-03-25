namespace App.State
{
	public interface IState
	{
		void OnEnter();
		StateType Update();
		void OnLeave();
	}
}