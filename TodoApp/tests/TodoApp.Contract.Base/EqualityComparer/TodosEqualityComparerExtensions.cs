using NUnit.Framework.Constraints;

namespace TodoApp.Contract.Base.EqualityComparer
{
    public static class TodosEqualityComparerExtensions
    {
        public static EqualConstraint UsingTodosEqualityComparer(this EqualConstraint equalConstraint)
        {
            return equalConstraint.Using(TodosEqualityComparer.Instance);
        }
    }
}