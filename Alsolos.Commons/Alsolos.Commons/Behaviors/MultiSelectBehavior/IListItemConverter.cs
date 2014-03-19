namespace Alsolos.Commons.Behaviors.MultiselectBehavior {
    public interface IListItemConverter {
        object Convert(object masterListItem);
        object ConvertBack(object targetListItem);
    }
}