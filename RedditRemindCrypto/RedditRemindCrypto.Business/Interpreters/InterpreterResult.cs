using System;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class InterpreterResult
    {
        public bool Result { get; private set; }
        public bool? IsAlwaysFalse { get; private set; }

        public InterpreterResult(bool result, bool? isAlwaysFalse)
        {
            if (result && isAlwaysFalse.HasValue && isAlwaysFalse.Value)
                throw new InvalidOperationException("Result can not be always false when value is true");

            this.IsAlwaysFalse = isAlwaysFalse;
            this.Result = result;
        }

        public static InterpreterResult And(InterpreterResult left, InterpreterResult right)
        {
            var result = left.Result && right.Result;
            var isAlwaysFalse = (bool?)null;
            if (left.IsAlwaysFalse.HasValue && right.IsAlwaysFalse.HasValue)
                isAlwaysFalse = left.IsAlwaysFalse.Value || right.IsAlwaysFalse.Value;

            if (left.IsAlwaysFalse.HasValue && !right.IsAlwaysFalse.HasValue)
                isAlwaysFalse = left.IsAlwaysFalse.Value;

            if (!left.IsAlwaysFalse.HasValue && right.IsAlwaysFalse.HasValue)
                isAlwaysFalse = right.IsAlwaysFalse.Value;

            return new InterpreterResult(result, isAlwaysFalse);
        }

        public static InterpreterResult Or(InterpreterResult left, InterpreterResult right)
        {
            var result = left.Result || right.Result;
            var isAlwaysFalse = (bool?)null;
            if (left.IsAlwaysFalse.HasValue && right.IsAlwaysFalse.HasValue)
                isAlwaysFalse = left.IsAlwaysFalse.Value && right.IsAlwaysFalse.Value;

            return new InterpreterResult(result, isAlwaysFalse);
        }
    }
}
