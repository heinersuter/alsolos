namespace Alsolos.Commons.UnitTest.Controls.FindInSelector {
    public class MyItem {
        public string Name { get; set; }
        public string Details { get; set; }
        public int Number { get; set; }

        public override string ToString() {
            return Name + " (" + Details + ") [" + Number + "]";
        }
    }
}
