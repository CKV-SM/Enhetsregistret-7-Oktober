using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Enhetsregistret.Models;

public class Student
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
