namespace DragDrop
{
    public class Card
    {
        public Image CardPic;
        public int Width;
        public int Height;
        public Point position=new Point();
        public bool Active = false;
        public Rectangle rect;
        public Card(string imageLocation)
        {
            CardPic = Image.FromFile(imageLocation);
            Width = 65;
            Height = 105;
            rect=new Rectangle(position.X,position.Y,Width,Height);
        }
    }
}
