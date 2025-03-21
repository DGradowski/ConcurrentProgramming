namespace Model
{
	public class Ball
	{
		private int posX;
		private int posY;

		public Ball(int posX, int posY)
		{
			this.posX = posX;
			this.posY = posY;
		}

		public int PosX { get => posX; }
		public int PosY { get => posY; }

		public void Move(int x, int y)
		{
			this.posX += x;
			this.posY += y;
		}
	}
}
