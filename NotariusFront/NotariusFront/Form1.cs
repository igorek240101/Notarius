namespace NotariusFront
{
    public partial class Form1 : Form
    {
        internal static string Token { get; set; } 
        internal static Form1 That { get; set; }
        public Form1()
        {
            InitializeComponent();
            That = this;
        }
    }
}