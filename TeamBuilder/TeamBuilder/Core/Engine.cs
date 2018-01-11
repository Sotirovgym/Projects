namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    Console.Write("Enter command: ");
                    var input = Console.ReadLine();

                    var commandTokens = input.Split(' ');

                    var commandName = commandTokens[0];

                    var commandArgs = commandTokens.Skip(1).ToArray();

                    var command = CommandParser.ParseCommand(serviceProvider, commandName);

                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }
    }
}
