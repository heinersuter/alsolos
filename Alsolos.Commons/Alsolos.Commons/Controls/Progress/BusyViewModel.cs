namespace Alsolos.Commons.Controls.Progress
{
    using Alsolos.Commons.Mvvm;

    public abstract class BusyViewModel : ViewModel
    {
        protected BusyViewModel()
        {
            BusyHelper = new BusyHelper();
        }

        public BusyHelper BusyHelper { get; private set; }
    }
}
