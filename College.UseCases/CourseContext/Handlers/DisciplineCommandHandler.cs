﻿using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Repositories;
using College.Entities.CourseContext.Entities;

namespace College.UseCases.CourseContext.Handlers
{
    public class DisciplineCommandHandler : ICommandHandler<DisciplineInputRegister>
    {
        private readonly IDisciplineRepository _DREP;

        public DisciplineCommandHandler(IDisciplineRepository DREP)
        {
            _DREP = DREP;
        }

        public ICommandResult Handle(DisciplineInputRegister command)
        {
            // TO DO: Cryptography
            var discipline = new Discipline(command.Name, command.CourseId, command.ProfessorId, command.WeeklyWorkload, command.Period);
            var result = new StandardResult();
            if (discipline.Notifications.Count == 0)
            {
                _DREP.Create(discipline);
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            }
            else
            {
                foreach (var notification in discipline.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(DisciplineInputUpdate command)
        {
            var discipline = new Discipline(command.Name, command.CourseId, command.ProfessorId, command.WeeklyWorkload, command.Period);
            var result = new StandardResult();
            if (discipline.Notifications.Count == 0)
            {
                _DREP.Update(discipline);
                result.Notifications.Add("Success", "O Acadêmico foi Editado");
            }
            else
            {
                foreach (var notification in discipline.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(DisciplineInputDelete command)
        {
            _DREP.Delete(command.DisciplineId);
            var result = new StandardResult();
            if (_DREP.Get(command.DisciplineId) != null)
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }
    }
}
