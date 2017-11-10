using NUnit.Framework.Constraints;

namespace TodoApp.Contracts.Base.EqualityComparer
{
    public static class TodosEqualityComparerExtensions
    {
        public static EqualConstraint UsingTodosEqualityComparer(this EqualConstraint equalConstraint) => 
            equalConstraint.Using(TodosEqualityComparer.Instance);
    }
}