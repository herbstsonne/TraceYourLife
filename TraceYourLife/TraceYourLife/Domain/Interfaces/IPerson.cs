﻿using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Interfaces
{
    public interface IPerson
    {
        int Id { get; }
        string Name { get; set; }
        int Age { get; set; }
        int Height { get; set; }
        Gender Gender { get; set; }
        decimal StartWeight { get; set; }
        string Password { get; set; }

        IPerson LoadFirstPerson();
        bool SavePerson();
        IPerson GetPerson(string name);
    }
}
