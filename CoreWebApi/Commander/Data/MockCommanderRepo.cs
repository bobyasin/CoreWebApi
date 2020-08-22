using System.Collections.Generic;
using Commander.Models;
namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>()
            {
               new Command()
            {
                Id = 0,
                HowTo="Boil an egg 1",
                Line="Boil Water 1",
                Platform="kattle & Pan 1"
            },new Command()
            {
                Id = 1,
                HowTo="Boil an egg 2",
                Line="Boil Water 2",
                Platform="kattle & Pan 2"
            },
            new Command()
            {
                Id = 2,
                HowTo="Boil an egg 3",
                Line="Boil Water 3" ,
                Platform="kattle & Pan 3"
            }
           };
            return commands;
        }
        public Command GetCommandById(int id)
        {
            return new Command()
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil Water",
                Platform = "kattle & Pan"
            };
        }
        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}