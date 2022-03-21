using UnityEngine;

namespace App.Input
{
	public class Mouse : IMouse
	{
		readonly Camera camera;
		public Vector3 ScreenPosition { get; private set; }
		public Vector3 Position { get; private set; }
		public MouseButton Button { get; private set; }

		public Mouse(Camera camera)
		{
			this.camera = camera;
		}

		public void Update()
		{
			Position = UnityEngine.Input.mousePosition;
			ScreenPosition = camera.ScreenToWorldPoint(Position);
			
			if (UnityEngine.Input.GetMouseButtonDown(0))
			{
				Button = MouseButton.Down;
			}
			else if (UnityEngine.Input.GetMouseButtonUp(0))
			{
				Button = MouseButton.Up;
			}
			else
			{
				Button = MouseButton.None;
			}
		}
	}
}