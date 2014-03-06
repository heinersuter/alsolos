using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.ProportionalColumnsPanel {
    [TestClass]
    public class ProportionalColumnsPanelTests {
        [TestMethod]
        public void OpenWindowTest() {
            new SimpleStretchPanel.MainWindow().ShowDialog();
        }
    }
}
