namespace HiddenLove.DataAccess.Entities
{
    /// <summary>
    /// Identifie une entité provenant de la base de données
    /// </summary>
    /// <typeparam name="TKey">
    /// Type de la clef primaire. Pour les clefs composites, favorisez l'utilisation d'un <see cref="System.Tuple"/>
    /// </typeparam>
    public interface IEntity<TKey> { }
}