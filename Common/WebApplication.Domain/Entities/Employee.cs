﻿using WebApplication.Domain.Entities.Base;

namespace WebApplication.Domain.Entities;

/// <summary>Сотрудник</summary>
public class Employee : Entity
{
    /// <summary>Фамилия</summary>
    public string FirstName { get; set; }
    /// <summary>Имя</summary>
    public string LastName { get; set; }
    /// <summary>Отчество</summary>
    public string Patronymic { get; set; }
    /// <summary>Возраст</summary>
    public int Age { get; set; }
}

