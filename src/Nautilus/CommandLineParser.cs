using System.Collections.Generic;
using System.Linq;

namespace Nautilus
{
    public class CommandLineParser
    {
        public CommandLineParser(params string[] args)
        {
            var queue = new Queue<string>(args);

            Options = new Dictionary<string, string>();
            var errors = new List<string>();

            while (queue.Any())
            {
                var item = queue.Dequeue();

                if (!queue.Any() || IsKey(queue.Peek()))
                {
                    errors.Add(string.Format("Option {0} is missing its required value.", item));
                    break;
                }

                var key = KeyName(item);
                var value = queue.Dequeue();

                Options.Add(key, value);
            }

            Errors = errors.ToArray();
        }

        public Dictionary<string, string> Options { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public bool HasErrors
        {
            get { return Errors.Any(); }
        }

        static bool IsKey(string item)
        {
            return item.StartsWith("--");
        }

        static string KeyName(string item)
        {
            return item.Substring("--".Length);
        }
    }

    public class CommandLineOption
    {
        public const string PathToBuildInstructions = "instructions";
        public const string MethodToInvoke = "call";
    }
}