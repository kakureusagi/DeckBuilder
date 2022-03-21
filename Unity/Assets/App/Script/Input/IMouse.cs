using UnityEngine;

namespace App.Input
{
	
	public interface IMouse
	{
		Vector3 Position { get; }
		Vector3 ScreenPosition { get; }
		MouseButton Button { get; }
	}
}