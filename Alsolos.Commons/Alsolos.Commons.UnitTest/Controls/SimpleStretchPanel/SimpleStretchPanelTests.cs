using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.SimpleStretchPanel {
    [TestClass]
    public class SimpleStretchPanelTests {
        [TestMethod]
        public void OpenWindowTest() {
            new MainWindow().ShowDialog();
        }
    }
}
