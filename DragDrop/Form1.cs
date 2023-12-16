namespace DragDrop
{
    public partial class Form1 : Form
    {

        List<Card> cards = new List<Card>();
        Card SelectedCard;
        int indexValue;
        int xPos = 5;
        List<string> imageLocation = new List<string>();
        int cardNumber = -1;
        int totalCards = 0;
        int lineAnimation = 0;
        public Form1()
        {
            InitializeComponent();
            SetUpApp();
        }
        private void SetUpApp()
        {
            imageLocation = Directory.GetFiles("cards", "*.png").ToList();
            totalCards = imageLocation.Count;
            for (int i = 0; i < totalCards; i++)
            {
                MakeCards();
            }
            label1.Text = "Card " + (cardNumber + 1) + " " + "of" + " " + totalCards;
        }
        private void MakeCards()
        {
            cardNumber++;
            xPos += 55;
            Card newCard = new Card(imageLocation[cardNumber]);
            newCard.position.X = xPos;
            newCard.position.Y = 300;
            newCard.rect.X = newCard.position.X;
            newCard.rect.Y = newCard.position.Y;
            cards.Add(newCard);
        }
        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = new Point(e.X, e.Y);
            foreach (Card newCard in cards)
            {
                if (SelectedCard == null)
                {
                    if (newCard.rect.Contains(mousePosition))
                    {
                        SelectedCard = newCard;
                        newCard.Active = true;
                        indexValue = cards.IndexOf(newCard);
                        label1.Text = "Card " + (indexValue + 1) + " " + "of" + " " + totalCards;
                    }
                }
            }
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedCard != null)
            {
                SelectedCard.position.X = e.X - (SelectedCard.Width / 2);
                SelectedCard.position.Y = e.Y - (SelectedCard.Height / 2);
            }
        }

        private void FormMouseUp(object sender, MouseEventArgs e)
        {
            foreach (Card tempCard in cards)
            {
                tempCard.Active = false;
            }
            SelectedCard = null;
            lineAnimation = 0;
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            foreach (Card card in cards)
            {
                e.Graphics.DrawImage(card.CardPic, card.position.X, card.position.Y, card.Width,
                    card.Height);
                Pen outline;
                if (card.Active)
                {
                    outline = new Pen(Color.Red, lineAnimation);
                }
                else
                {
                    outline = new Pen(Color.Transparent, 1);
                }
                e.Graphics.DrawRectangle(outline, card.rect);
            }
            if (SelectedCard!=null)
            {
                e.Graphics.DrawImage(SelectedCard.CardPic, SelectedCard.position.X,
                    SelectedCard.position.Y, SelectedCard.Width, SelectedCard.Height);
            }
        }

        private void FormTimerEvent(object sender, EventArgs e)
        {
            foreach (Card card in cards)
            {
                card.rect.X = card.position.X;
                card.rect.Y = card.position.Y;
            }
            if (SelectedCard != null)
            {
                if (lineAnimation < 10)
                {
                    lineAnimation++;
                }
            }
            this.Invalidate();
        }
    }
}