namespace CodeCabana.Core.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(PredicateType predicate, string[] parameters);
    }
}
