using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp1
{
    public abstract class Criterion
    {
        protected Criterion(string title)
        {
            Title = title;
        }

        public string Title { get; }

        public ValueType ValueType { get; protected set; }
        public abstract bool Matches(object dataContext);
    }

    public abstract class ReferenceCriterion : Criterion
    {
        protected ReferenceCriterion(string title, string propertyName, object reference) : base(title)
        {
            PropertyName = propertyName;
            Reference = reference;
        }

        public object Reference { get; }
        public string PropertyName { get; }

        protected object GetValue(object reference)
        {
            var propertyInfo = reference.GetType().GetProperties()
                                    .First(p => p.Name == PropertyName);

            return propertyInfo.GetValue(reference);
        }
    }

    public class OrCriterion : Criterion
    {
        public IEnumerable<Criterion> Items { get; }
        public OrCriterion(string title, IEnumerable<Criterion> items) : base(title)
        {
            Items = items;
        }

        public override bool Matches(object dataContext)
        {
            if (Items.Any(i => i.Matches(dataContext)))
                return true;

            return false;
        }
    }

    public class AndCriterion : Criterion
    {
        public IEnumerable<Criterion> Items { get; }
        public AndCriterion(string title, IEnumerable<Criterion> items) : base(title)
        {
            Items = items;
        }

        public override bool Matches(object dataContext)
        {
            if (Items.Any(i => i.Matches(dataContext) == false))
                return false;

            return true;
        }
    }

    public class BooleanCriterion : ReferenceCriterion
    {
        public BooleanCriterion(string title, string propertyName, bool reference)
            : base(title, propertyName, reference)
        {
        }

        public override bool Matches(object dataContext)
        {
            var actualValue = (bool)GetValue(dataContext);
            var reference = (bool)Reference;

            return reference == actualValue;
        }
    }

    public enum Comparison
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }

    public class NumberCriterion : ReferenceCriterion
    {
        public NumberCriterion(string title, string propertyName, double reference, Comparison comparison)
            : base(title, propertyName, reference)
        {
            Comparison = comparison;
        }

        public Comparison Comparison { get; }

        public override bool Matches(object dataContext)
        {
            var value = Convert.ToDouble(GetValue(dataContext));
            var reference = Convert.ToDouble(Reference);
            return SatisfiesComparison(value, reference, Comparison);
        }

        private static bool SatisfiesComparison(double actual, double reference, Comparison comparison)
        {
            const double Epsilon = 0.0001;
            switch (comparison)
            {
                case Comparison.Equal:
                    return Math.Abs(actual - reference) <= Epsilon;
                case Comparison.NotEqual:
                    return Math.Abs(actual - reference) > Epsilon;
                case Comparison.GreaterThan:
                    return actual > reference;
                case Comparison.GreaterThanOrEqual:
                    return actual >= reference;
                case Comparison.LessThan:
                    return actual < reference;
                case Comparison.LessThanOrEqual:
                    return actual <= reference;
            }

            throw new ArgumentOutOfRangeException();
        }
    }

    public class DateCriterion : ReferenceCriterion
    {
        public DateCriterion(string title, string propertyName, DateTime reference, Comparison comparison)
            : base(title, propertyName, reference)
        {
            Comparison = comparison;
        }

        public Comparison Comparison { get; }

        public override bool Matches(object dataContext)
        {
            var value = (DateTime)GetValue(dataContext);
            var reference = (DateTime)Reference;
            return SatisfiesComparison(value, reference, Comparison);
        }

        private static bool SatisfiesComparison(DateTime actual, DateTime reference, Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    return reference.Date == actual.Date;
                case Comparison.NotEqual:
                    return reference.Date != actual.Date;
                case Comparison.GreaterThan:
                    return actual.Date > reference.Date;
                case Comparison.GreaterThanOrEqual:
                    return actual.Date >= reference.Date;
                case Comparison.LessThan:
                    return actual.Date < reference.Date;
                case Comparison.LessThanOrEqual:
                    return actual.Date <= reference.Date;
            }

            throw new ArgumentOutOfRangeException();
        }
    }

    public enum TextComparison
    {
        Equal,
        NotEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    public class TextCriterion : ReferenceCriterion
    {
        public TextCriterion(string title, string propertyName, string reference, TextComparison comparison)
            : base(title, propertyName, reference)
        {
            Comparison = comparison;
        }

        public TextComparison Comparison { get; }

        public override bool Matches(object dataContext)
        {
            var value = (string)GetValue(dataContext);
            var reference = (string)Reference;
            return SatisfiesComparison(value, reference, Comparison);
        }

        private static bool SatisfiesComparison(string actual, string reference, TextComparison comparison)
        {
            switch (comparison)
            {
                case TextComparison.Equal:
                    return actual == reference;
                case TextComparison.NotEqual:
                    return actual != reference;
                case TextComparison.Contains:
                    return actual.Contains(reference);
                case TextComparison.StartsWith:
                    return actual.StartsWith(reference);
                case TextComparison.EndsWith:
                    return actual.EndsWith(reference);
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
