using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsolos.Commons.UnitTest.Controls.NoAutoSize {
    [TestClass]
    public class NoAutoSizeDecoratorTests {
        [TestMethod]
        public void OpenWindowTest() {
            new NoAutoSizeDecoratorWindow().ShowDialog();
        }
    }
}
