﻿namespace Model.Domain.Validator
{
    public class ParticipantValidator : IValidator<Participant>
    {
        public void Validate(Participant entity)
        {
            string errors = "";
            if (entity.FirstName == "")
                errors += "First name cannot be empty\n";
            if (entity.LastName == "")
                errors += "Last name cannot be empty\n";
            if (entity.Age < 0)
                errors += "Age cannot be negative\n";
            if (errors.Length > 0)
                throw new ValidatorException(errors);
        }
    }
}
