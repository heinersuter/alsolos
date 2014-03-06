namespace Alsolos.Commons.Behaviors.MultiSelectBehavior {
    public interface IListItemConverter {
        object Convert(object masterListItem);
        object ConvertBack(object targetListItem);
    }
}